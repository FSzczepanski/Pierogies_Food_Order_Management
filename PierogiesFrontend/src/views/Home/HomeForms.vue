<template>
  <position-details-modal
    :show="positionModalVisible"
    :name="selectedPosition.name"
    @closeModal="hidePositionModal"
    @ok="addPositionToOrder"
  />
  <div style="min-height: 900px">
    <h1 class="mt-5">Wybierz menu</h1>

    <div class="d-flex justify-content-center">
      <div
        v-for="(item, index) in formsList"
        @click="loadFormData(item.id)"
        :key="index"
        class="btn btn-dark m-5"
        style="font-size: 30px"
      >
        {{ item.name }}
      </div>
    </div>
    <div>
      <div class="m-5 mx-auto w-50">
        {{ selectedForm.description }}
      </div>
      <div
        v-for="(item, index) in positionsList.items"
        :key="index"
        class="mb-5"
      >
        <div class="m-2 mt-5 row mx-auto border-white positionItem">
          <div class="col-md-5">
            <div class="textThird mt-4 fs-3 fw-bold">
              {{ item.name }}
            </div>
            <div class="mt-2" style="font-size: 16px">
              {{ item.description }}
            </div>
            <div>
              <button
                class="mt-4 btn btn-primary w-50"
                @click="showPositionModal(item)"
              >
                Dodaj
              </button>
            </div>
          </div>
          <div class="col-md-3 mt-5">
            <div class="colorThird fs-4 w-50 h-25 mt-3 rounded-circle">
              {{ item.price }} zł
            </div>
            <div class="mt-2" style="font-size: 16px">
              porcja: {{ item.portionSize }}
            </div>
          </div>
          <div class="col-md-4">
            <div
              class="imga text-center mt-1"
              :style="{
                'background-image': `url(${require('/public/kluska.jpg')})`,
                width: '40vh',
                height: '30vh',
              }"
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
                <i class="bi bi-x text-white"></i>
              </p>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive } from "vue";
import {
  FormsClient, IFormAm,
  IFormDetailListAm,
  IFormPosition,
  IOrderPosition,
} from "@/core/api/pierogiesApi";
import PositionDetailsModal from "@/views/Home/PositionDetailsModal.vue";
import {  useRouter } from "vue-router";

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
      formType: undefined,
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
    const orderedPositions = reactive({ items: [] as Array<IOrderPosition> });
    const selectedPosition = ref<IOrderPosition>({
      name: "",
      vat: 0,
      portionSize: "0",
      price: 0,
      amount: 0,
    });

    const price = ref<number>(0);

    client.getForms().then((response) => {
      formsList.value = response.items as Array<IFormDetailListAm>;
    });

    const loadFormData = (id: string) => {
      client.get(id).then((response) => {
        positionsList.items = response.positions as Array<IFormPosition>;
        selectedForm.value = response as IFormAm;
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
          form: JSON.stringify(selectedForm.value)
        },
      });
    };

    const hidePositionModal = () => {
      positionModalVisible.value = false;
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
