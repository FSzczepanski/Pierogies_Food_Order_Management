<template>
  <div
    class="imga text-center"
    :style="{
      'background-image': `url(${require('/public/background.png')})`,
      width: '300vh',
      height: '100vh',
      overflowX: 'auto',
    }"
  >
    <div v-if="!showSummary" class="myForm mt-5 bg-light textSecond mb-5">
      <h2 class="mt-5">{{ form.name }}</h2>
      <div class="myInput mt-4">
        <div>
          <label class="form-label required"> Imię i nazwisko </label>
          <el-input
            v-model="orderModel.purchaserName"
            placeholder="Wprowadź imię i nazwisko"
          />
        </div>
        <div>
          <label class="form-label required"> Email </label>
          <el-input v-model="orderModel.email" placeholder="Wprowadź email" />
        </div>
        <div>
          <label class="form-label required"> Telefon </label>
          <el-input
            v-model="orderModel.phone"
            placeholder="Wprowadź numer telefonu"
          />
        </div>
        <div>
          <label class="form-label required"> Dodatkowe informacje </label>
          <el-input
            v-model="orderModel.description"
            placeholder="Co jeszcze chciałabyś/chciałbyś nam przekazać?"
            class="mb-5"
          />
        </div>
        <div>
          <label class="form-label required"> Data i godzina zamówienia </label>
          <el-date-picker
            v-model="orderModel.date"
            type="datetime"
            placeholder="Wybierdz date i godzine"
          >
          </el-date-picker>
        </div>
        <label class="form-label required mt-2"> Metoda płatności </label>
        <div>
          <el-select v-model="selectedPaymentMethodPl">
            <el-option v-for="item in paymentMethodsPl" :key="item">
              {{ item }}
            </el-option>
          </el-select>
        </div>
      </div>
      <div class="myInput mt-5">
        <label class="form-label required"> Lokalizacja </label>
        <div>
          <el-select v-model="selectedLocation">
            <el-option
              v-for="item in form.availableLocations"
              :key="item"
              :label="item.name"
            >
              {{ item.name }}
            </el-option>
          </el-select>
        </div>
        <div v-if="form.formType !== 'ForHere'">
          <div>
            Dostawa do domu
            <el-checkbox v-model="isDeliveryNeeded" />
          </div>
          <div v-if="isDeliveryNeeded">
            <div class="mt-3">
              <label class="form-label required"> Ulica i numer </label>
              <el-input
                v-model="orderModel.street"
                placeholder="Wprowadź ulicę i numer"
              />
            </div>
            <div>
              <label class="form-label required"> Kod pocztowy </label>
              <el-input
                v-model="orderModel.zipCode"
                placeholder="Wprowadź kod pocztowy"
              />
            </div>
            <div>
              <label class="form-label required"> Miejscowość </label>
              <el-input
                v-model="orderModel.cityName"
                placeholder="Wprowadź nazwe miejscowości"
              />
            </div>
          </div>
        </div>
        <div>
          Faktura
          <el-checkbox v-model="orderModel.needInvoice" />
        </div>
      </div>
      <div>
        <button
          class="mt-4 btn btn-primary w-50 mb-3"
          @click="goToSummary"
        >
          Podsumowanie
        </button>
      </div>
    </div>
    <div v-if="showSummary" class="myForm mt-5 bg-light textSecond mb-5">
      <OrderSummary :order="orderModel"></OrderSummary>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  ICreateOrderCommand,
  IFormAm,
  ILocation,
  IOrderPosition,
  OrderPosition,
  PaymentMethodEnum,
} from "@/core/api/pierogiesApi";
import OrderSummary from "@/views/Home/OrderSummary.vue";

export default defineComponent({
  name: "ConfirmOrder",
  components: { OrderSummary },
  setup: () => {
    const route = useRoute();
    const router = useRouter();

    if (route.params.form == undefined) {
      router.push({ name: "Home" });
    }
    const form = ref<IFormAm>(
      JSON.parse(route.params.form as string) as IFormAm
    );
    const orderedPositions = reactive({
      items: JSON.parse(
        route.params.orderPositions as string
      ) as Array<IOrderPosition>,
    });

    const paymentMethodsPl = ref(["Na miejscu", "Przelewy24"]);
    const selectedPaymentMethodPl = ref("");
    const isDeliveryNeeded = ref(false);

    const showSummary = ref(false);
    const selectedLocation = ref<ILocation>({
      name: "",
      zipCode: "",
      street: "",
      countryName: "",
      cityName: "",
      isDefault: false,
      description: "",
    });

    const orderModel = ref<ICreateOrderCommand>({
      purchaserName: "",
      email: "",
      phone: "",
      date: new Date(),
      deliveryPrice: form.value.deliveryPrice,
      formId: form.value.id,
      description: "",
      locationDescription: "",
      locationName: "",
      cityName: "",
      countryName: "Polska",
      isDefault: false,
      needInvoice: false,
      paymentMethod: 1,
      positions: orderedPositions.items as Array<OrderPosition>,
    });

    const goToSummary = () => {
      orderModel.value.locationName = selectedLocation.value.name;
      orderModel.value.locationDescription = selectedLocation.value.description;
      orderModel.value.cityName = selectedLocation.value.cityName;
      orderModel.value.street = selectedLocation.value.street;
      orderModel.value.zipCode = selectedLocation.value.zipCode;
      orderModel.value.isDefault = !isDeliveryNeeded.value;
      orderModel.value.paymentMethod =
        selectedPaymentMethodPl.value == "Na miejscu"
          ? PaymentMethodEnum.OnPlace
          : PaymentMethodEnum.Przelewy24;

      showSummary.value = true;
    };

    return {
      orderedPositions,
      form,
      orderModel,
      selectedLocation,
      paymentMethodsPl,
      selectedPaymentMethodPl,
      isDeliveryNeeded,
      goToSummary,
      showSummary,
    };
  },
});
</script>
