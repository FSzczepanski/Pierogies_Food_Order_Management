<template>
  <position-details-modal
    :show="positionModalVisible"
    :name="selectedPosition.name"
    :portion-size="selectedPosition.portionSize"
    :price="selectedPosition.price"
    :photo-url="selectedPositionPhotoUrl"
    @closeModal="hidePositionModal"
    @ok="addPositionToOrder"
  />
  <div style="min-height: 900px" class="disable-select">
    <div class="d-flex justify-content-center list-group-horizontal">
      <div
        v-for="(item, index) in formsList"
        @click="loadFormData(item.id)"
        :key="index"
        class="mb-5 mt-5"
      >
        <div class="homeFormItem m-4 list-group-item enablePointer">
          <div>
            <img class="homeFormImage" src="icon.png" alt="zdjecie" />
          </div>
          <div style="font-size: 24px">
            {{ item.name }}
          </div>
          <div class="col-lg-10 m-auto" style="font-size: 15px">
            {{ item.description }}
          </div>
        </div>
      </div>
    </div>
    <div class="m-1">
      <div class="mt-5 mb-5">
        {{ selectedForm.description }}
      </div>
      <div
        class="col-lg-10 m-auto"
        v-for="(positions, groupIndex) in positionsList.items"
        :key="groupIndex"
      >
        <div class="h1 m-5 d-flex justify-content-between">
          {{ PositionCategoryEnumTranslation[positions[0].positionCategory] }}
        </div>
        <div v-for="(item, index) in positions" :key="index">
          <div class="row positionItem" @click="showPositionModal(item)">
            <div class="col text-start ms-5 mt-3">
              <div class="textThird fs-5 fw-bold">
                {{ item.name }}
              </div>
              <div class="mt-2" style="font-size: 13px">
                {{ item.description }}
              </div>
              <div class="fs-4 text-info mt-3">{{ item.price }} zł</div>
            </div>
            <div class="col">
              <img
                v-if="item.hasPhoto"
                class="positionImage m-3"
                :src="loadPositionPhoto(item.positionId)"
                alt="Zdjecie"
              />
            </div>
          </div>
          <hr class="bg-light mt-3" />
        </div>
      </div>
    </div>
  </div>
  <div class="fixed-bottom disable-select">
    <transition name="fade">
      <div v-if="price > 0 && !orderVisible" class="row colorThird cart">
        <div
          class="row text-center justify-content-center mt-2 enablePointer"
          @click="orderVisible = true"
        >
          <div class="h3 col-lg-6 row">
            <div class="col">
              <div
                class="rounded-circle text-info bg-light p-1 m-auto"
                style="height: 5vh; width: 5vh"
              >
                {{ orderedPositions.items.length }}
              </div>
            </div>
            <div class="p-1 col">Zamówienie</div>
            <div class="p-1 col fs-4">{{ price }} zł</div>
          </div>
        </div>
      </div>
    </transition>
    <transition name="fade">
      <div v-if="orderVisible">
        <div
          class="row colorThird cart enablePointer"
          @click="orderVisible = false"
        >
          <div class="justify-content-end d-flex text-white">
            <i class="bi h1 bi-chevron-down me-5" />
          </div>
        </div>
        <div class="cartPositions">
          <div class="textSecond row orderRow m-auto fw-bold">
            <div class="col">Nazwa</div>
            <div class="col">porcja</div>
            <div class="col">ilość</div>
            <div class="col">usuń</div>
          </div>
          <div
            v-for="(item, index) in orderedPositions.items"
            :key="index"
            class="textSecond fs-2 mt-3"
          >
            <div class="row orderRow m-auto">
              <div class="col">
                {{ item.name }}
              </div>
              <div class="col">
                {{ item.portionSize }}
              </div>
              <div class="col">
                <el-input
                  min="1"
                  max="100"
                  type="number"
                  class="col-lg-3"
                  v-model="item.amount"
                  step="1"
                />
              </div>
              <div class="col">
                <p
                  class="btn btn-danger rounded-circle bg-white"
                  @click="deletePosition(item.positionId)"
                >
                  <i class="bi bi-trash text-danger"></i>
                </p>
              </div>
            </div>
          </div>
        </div>
        <div class="bg-light pt-3">
          <p
            class="m-auto mb-3 btn btn-primary col-lg-8"
            v-if="orderVisible"
            @click="confirmOrder"
          >
            Podsumowanie
            <i class="bi bi-cart-check h4"></i>
          </p>
        </div>
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from "vue";
import {
  FormsClient,
  FormTypeEnum,
  IFormAm,
  IFormDetailListAm,
  IFormPosition,
  IOrderPosition,
} from "@/core/api/pierogiesApi";
import PositionDetailsModal from "@/views/Home/PositionDetailsModal.vue";
import { useRouter } from "vue-router";
import { PositionPhoto } from "@/helpers/inferfaces";
import { PositionCategoryEnumTranslation } from "@/helpers/enums";
import { showToast } from "@/helpers/confirmationsAdapter";

export default defineComponent({
  name: "HomeForms",
  components: {
    PositionDetailsModal,
  },
  setup: () => {
    const router = useRouter();
    const positionModalVisible = ref(false);
    const orderVisible = ref(false);
    const client = new FormsClient(process.env.VUE_APP_API_BASE_PATH);

    const formsList = ref(Array<any>([]));
    const selectedForm = ref<IFormAm>({
      name: "",
      formActive: undefined,
      formType: FormTypeEnum.ForHere,
      isActive: true,
      id: "",
      minimumTotalPrice: 0,
      deliveryPrice: 0,
      description: "",
      placeOnList: 0,
      availableDates: undefined,
      availableLocations: undefined,
      paymentMethods: undefined,
      positions: undefined,
    });

    const positionsList = reactive({
      items: [] as Array<Array<IFormPosition>>,
    });
    const positionPhotos = ref<Array<PositionPhoto>>([]);
    const orderedPositions = reactive({ items: [] as Array<IOrderPosition> });
    const selectedPosition = ref<IOrderPosition>({
      name: "",
      vat: 0,
      portionSize: "0",
      price: 0,
      amount: 0,
    });

    const price = ref<number>(0);

    client.getForms(true).then((response) => {
      formsList.value = response.items as Array<IFormDetailListAm>;
    });

    const loadFormData = (id: string) => {
      client.getForClient(id).then((response) => {
        positionsList.items = response.positionsGrouped as Array<
          Array<IFormPosition>
        >;
        selectedForm.value = response as IFormAm;
        orderedPositions.items.splice(0, orderedPositions.items.length);
        positionsList.items.forEach((pGroup) => {
          pGroup.forEach((p) => {
            if (p.hasPhoto) {
              const url = process.env.VUE_APP_API_BASE_PATH;
              positionPhotos.value.push({
                positionId: p.positionId,
                photoUrl: url + "/api/v1/core/positions/photo/" + p.positionId,
              });
            }
          });
        });
      });
    };

    const selectedPositionPhotoUrl = ref<string>("");
    const showPositionModal = (position: IFormPosition) => {
      selectedPosition.value = position;
      const photoUrl = positionPhotos.value.find(
        (photo) => photo.positionId == position.positionId
      )?.photoUrl;
      selectedPositionPhotoUrl.value = photoUrl ?? "";
      positionModalVisible.value = true;
    };

    const addPositionToOrder = (amountVal: number) => {
      const positionAdded = orderedPositions.items.find(
        (item) => item.positionId === selectedPosition.value.positionId
      );

      positionAdded != undefined
        ? (positionAdded.amount = positionAdded.amount - 0 + (amountVal - 0))
        : orderedPositions.items.push({
            positionId: selectedPosition.value.positionId,
            name: selectedPosition.value.name,
            amount: amountVal,
            vat: selectedPosition.value.vat,
            portionSize: selectedPosition.value.portionSize,
            price: selectedPosition.value.price,
          });
    };

    const confirmOrder = () => {
      const minPrice = selectedForm.value.minimumTotalPrice!;
      price.value < minPrice
        ? showToast("Minimalna wartość zamówienia to " + minPrice + " zł")
        : router.push({
            name: "ConfirmOrder",
            params: {
              orderPositions: JSON.stringify(orderedPositions.items),
              form: JSON.stringify(selectedForm.value),
            },
          });
    };

    const hidePositionModal = () => {
      positionModalVisible.value = false;
    };

    const loadPositionPhoto = (itemId: string) => {
      return positionPhotos.value.find((photo) => photo.positionId == itemId)
        ?.photoUrl;
    };

    watch(orderedPositions.items, (o) => {
      let newPrice = 0;
      o.forEach((pos) => (newPrice += pos.price * pos.amount));
      price.value = newPrice;
    });

    const deletePosition = (id: string) => {
      const removeIndex = orderedPositions.items.findIndex(
        (item) => item.positionId === id
      );
      orderedPositions.items.splice(removeIndex, 1);
    };

    return {
      formsList,
      positionsList,
      loadFormData,
      showPositionModal,
      hidePositionModal,
      positionModalVisible,
      selectedPosition,
      addPositionToOrder,
      price,
      orderVisible,
      orderedPositions,
      confirmOrder,
      selectedForm,
      deletePosition,
      loadPositionPhoto,
      PositionCategoryEnumTranslation,
      selectedPositionPhotoUrl,
    };
  },
});
</script>

<style scoped>
.fade-enter-active {
  transition: all 0.5s ease;
}
.fade-enter-from {
  opacity: 1;
  transform: translateY(170px);
}
</style>
