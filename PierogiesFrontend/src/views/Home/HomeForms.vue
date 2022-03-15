<template>
  <position-details-modal
    :show="positionModalVisible"
    :name="selectedPosition.name"
    @closeModal="hidePositionModal"
    @ok="addPositionToOrder"
  />
  <div style="min-height: 900px" class="disable-select">
    <div class="d-flex justify-content-center list-group list-group-horizontal">
      <div
        v-for="(item, index) in formsList"
        @click="loadFormData(item.id)"
        :key="index"
        class="mb-5 mt-5"
      >
        <div class="homeFormItem m-4 list-group-item">
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
    <div class="m-5">
      <div class="mt-5 mb-5">
        {{ selectedForm.description }}
      </div>
      <div
        class="col-lg-10 m-auto"
        v-for="(item, index) in positionsList.items"
        :key="index"
      >
        <div class="row positionItem" @click="showPositionModal(item)">
          <hr class="bg-light" />
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
      </div>
    </div>
  </div>
  <div class="fixed-bottom">
    <transition name="fade">
      <div v-if="price > 0" class="row colorThird cart justify-content-center">
        <div v-if="orderVisible" class="row justify-content-center mt-2">
          <div class="col-lg-2">
            <p class="btn btn-primary" @click="orderVisible = false">Powrót</p>
          </div>
          <div class="col-lg-2">
            <p
              class="btn btn-primary"
              v-if="orderVisible"
              @click="confirmOrder"
            >
              Zamawiam
            </p>
          </div>
        </div>
        <div v-else class="row justify-content-center mt-2 ms-5">
          <div class="col-lg-2 ms-5">
            <p class="btn btn-primary" @click="orderVisible = true">
              Zobacz zamówienie
            </p>
          </div>
          <div class="col-lg-2 mt-2 h4">{{ price }} zł</div>
        </div>
      </div>
    </transition>
    <transition name="fade">
      <div v-if="orderVisible" class="cartPositions">
        <div class="textSecond row orderRow m-auto fw-bold">
          <div class="col">Nazwa</div>
          <div class="col">porcja</div>
          <div class="col">ilość</div>
          <div class="col">usuń</div>
        </div>
        <div
          v-for="(item, index) in orderedPositions.items"
          :key="index"
          class="textSecond"
        >
          <hr />
          <div class="row orderRow m-auto">
            <div class="col">
              {{ item.name }}
            </div>
            <div class="col">
              {{ item.portionSize }}
            </div>
            <div class="col">
              <el-input
                min="0"
                max="100"
                type="number"
                v-model="item.amount"
                step="1"
              />
            </div>
            <div class="col">
              <p class="btn text-danger h3 btn-danger">
                <i class="bi bi-x text-white" @click="deletePosition(item)"></i>
              </p>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
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
      deliveryPrice: 0,
      description: "",
      placeOnList: 0,
      availableDates: undefined,
      availableLocations: undefined,
      paymentMethods: undefined,
      positions: undefined,
    });

    const positionsList = reactive({ items: [] as Array<IFormPosition> });
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
      client.get(id).then((response) => {
        positionsList.items = response.positions as Array<IFormPosition>;
        selectedForm.value = response as IFormAm;

        positionsList.items.forEach((p) => {
          if (p.hasPhoto) {
            const url = process.env.VUE_APP_API_BASE_PATH;
            positionPhotos.value.push({
              positionId: p.positionId,
              photoUrl: url + "/api/v1/core/positions/photo/" + p.positionId,
            });
          }
        });
      });
    };

    const showPositionModal = (position: IFormPosition) => {
      selectedPosition.value = position;
      positionModalVisible.value = true;
    };

    const addPositionToOrder = (amountVal: number) => {
      orderedPositions.items.push({
        name: selectedPosition.value.name,
        amount: amountVal,
        vat: selectedPosition.value.vat,
        portionSize: selectedPosition.value.portionSize,
        price: selectedPosition.value.price,
      });

      price.value += selectedPosition.value.price * amountVal;
    };

    const confirmOrder = () => {
      router.push({
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

    const deletePosition = (item: any) => {
      console.log(item);
      orderedPositions.items.filter((p) => p.price == item.price);
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
    };
  },
});
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: all 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
  transform: translateX(40px);
}

.cart-enter-active,
.cart-leave-active {
  transition: all 0.5s ease;
}

.cart-enter-from,
.cart-leave-to {
  opacity: 0;
  transform: translateY(100vh);
}
</style>
