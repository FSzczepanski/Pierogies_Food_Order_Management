<template>
  <div class="imga text-center background-image-order">
    <form @submit="onSubmit">
      <div
        v-if="shownCard === 0"
        class="orderForm mt-5 bg-light textSecond h-auto"
      >
        <div class="h4 pt-4">Wprowadź dane do zamówienia</div>
        <div class="row m-4">
          <div class="col">
            <button class="order-tab-item bg-info"></button>
          </div>
          <div class="col"><button class="order-tab-item"></button></div>
          <div class="col"><button class="order-tab-item"></button></div>
        </div>
        <div class="m-4 mt-5">
          <div>
            <label class="form-label required mt-3"> Imię i nazwisko </label>
            <el-input
              v-model="purchaserName"
              placeholder="Wprowadź imię i nazwisko"
            />
            <span class="text-danger fs-6">{{ errors["purchaserName"] }}</span>
          </div>
          <div>
            <label class="form-label required mt-3"> E-mail </label>
            <el-input v-model="email" placeholder="Wprowadź email" />
            <span class="text-danger fs-6">{{ errors["email"] }}</span>
          </div>
          <div>
            <label class="form-label required mt-3"> Telefon </label>
            <el-input v-model="phone" placeholder="Wprowadź numer telefonu" />
            <span class="text-danger fs-6">{{ errors["phone"] }}</span>
          </div>
          <label class="form-label required mt-3"> Metoda płatności </label>
          <div class="mb-4">
            <el-select v-model="paymentMethod" class="col-lg-12">
              <el-option
                v-for="(item, index) in paymentMethodEnumValues"
                :value="PaymentMethodEnum[item]"
                :label="PaymentMethodEnumTranslation[index]"
                :key="index"
              >
                {{ PaymentMethodEnumTranslation[index] }}
              </el-option>
            </el-select>
            <span class="text-danger fs-6">{{ errors["paymentMethod"] }}</span>
          </div>
          <button class="btn-primary m-4" @click="shownCard = 1">
            Przejdź dalej
          </button>
        </div>
      </div>
      <div
        v-if="shownCard === 1"
        class="orderForm mt-5 bg-light textSecond mb-5"
      >
        <div class="h4 pt-4">Lokalizacja i data odbioru</div>
        <div class="row m-4">
          <div class="col">
            <button
              class="order-tab-item bg-info"
              @click="shownCard = 0"
            ></button>
          </div>
          <div class="col">
            <button class="order-tab-item bg-info"></button>
          </div>
          <div class="col"><button class="order-tab-item"></button></div>
        </div>

        <div class="m-4">
          <label class="form-label required mt-4"> Lokalizacja </label>
          <div>
            <el-select
              v-model="selectedLocation"
              value-key="name"
              class="col-lg-12"
            >
              <el-option
                v-for="(item, index) in availableLocations"
                :key="index"
                :value="item"
                :label="item.name"
              >
                {{ item.name }}
              </el-option>
            </el-select>
            <span class="text-danger fs-6" v-if="errors['street']">
              Pole wymagane
            </span>
          </div>
          <div v-if="form.formType !== 2">
            <div v-if="selectedLocation.name === 'Dostawa do domu'">
              <div class="mt-3">
                <label class="form-label required"> Ulica i numer </label>
                <el-input
                  v-model="street"
                  placeholder="Wprowadź ulicę i numer"
                />
                <span class="text-danger fs-6">{{ errors["street"] }}</span>
              </div>
              <div>
                <label class="form-label required"> Kod pocztowy </label>
                <el-input
                  v-model="zipCode"
                  placeholder="Wprowadź kod pocztowy"
                />
                <span class="text-danger fs-6">{{ errors["zipCode"] }}</span>
              </div>
              <div>
                <label class="form-label required"> Miejscowość </label>
                <el-input
                  v-model="cityName"
                  placeholder="Wprowadź nazwe miejscowości"
                />
                <span class="text-danger fs-6">{{ errors["cityName"] }}</span>
              </div>
            </div>
          </div>
          <div v-if="form.formType === 2">
            <label class="form-label required col-lg-12 mt-4">
              Data i godzina
            </label>
            <div>
              <el-date-picker
                v-model="date"
                type="datetime"
                placeholder="Wybierdz date i godzine"
                class="w-100"
              >
              </el-date-picker>
              <span class="text-danger fs-6">{{ errors["date"] }}</span>
            </div>
          </div>
          <div v-else>
            <div>
              <label class="form-label required col-lg-12 mt-4">
                Wybierz dzień dostawy / odbioru
              </label>
              <el-select
                v-model="selectedDateRange"
                value-key="from"
                class="col-lg-12"
              >
                <el-option
                  v-for="(item, index) in form.availableDates"
                  :value="item"
                  :label="
                    moment(item.from).format('DD.MM.yyyy HH:mm') +
                    ' do ' +
                    moment(item.to).format('DD.MM.yyyy HH:mm')
                  "
                  :key="index"
                >
                  {{
                    moment(item.from).format("DD.MM.yyyy HH:mm") +
                    " do " +
                    moment(item.to).format("DD.MM.yyyy HH:mm")
                  }}
                </el-option>
              </el-select>
            </div>
            <div v-if="enableDatePicker">
              <label class="form-label required col-lg-12 mt-4">
                Wybierz date i godzine
              </label>
              <div>
                <el-date-picker
                  class="w-100 mb-3"
                  v-model="date"
                  :disabled-date="datesOutOfSelectedRange"
                  type="datetime"
                  placeholder="Wybierz date i godzine z możliwego zakresu podświetlonych dni"
                />
              </div>
            </div>
            <span class="text-danger fs-6">{{ errors["date"] }}</span>
          </div>
          <div>
            <label class="form-label required mt-3">
              Dodatkowe informacje
            </label>
            <el-input
              v-model="description"
              placeholder="Co jeszcze chciałabyś/chciałbyś nam przekazać?"
            />
            <span class="text-danger fs-6">{{ errors["description"] }}</span>
          </div>
          <!--          <div>
            Faktura
            <el-checkbox v-model="needInvoice" />
          </div>-->
        </div>

        <button
          class="btn-primary m-4 justify-content-end"
          @click="goToSummary"
        >
          Przejdź dalej
        </button>
      </div>

      <!--  Podsumowanie zamówienia  -->
      <div
        v-if="shownCard === 2"
        class="orderForm mt-5 bg-light textSecond mb-5"
      >
        <div class="mt-3">
          <div class="h4 pt-4">Podsumowanie zamówienia</div>
          <div class="row m-4">
            <div class="col">
              <button
                class="order-tab-item bg-info"
                @click="shownCard = 0"
              ></button>
            </div>
            <div class="col">
              <button
                class="order-tab-item bg-info"
                @click="shownCard = 1"
              ></button>
            </div>
            <div class="col">
              <button class="order-tab-item bg-info"></button>
            </div>
          </div>
          <div class="m-5">
            <div class="cartPositions bg-light tableShape">
              <div class="textSecond row orderRow m-auto fw-bold">
                <div class="col">Nazwa</div>
                <div class="col">porcja</div>
                <div class="col">Cena</div>
                <div class="col">ilość</div>
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
                  <div class="col">{{ item.price }} zł</div>
                  <div class="col">
                    {{ item.amount }}
                  </div>
                </div>
              </div>
              <div class="mt-5 h5 textThird fw-bold text-decoration-underline">
                Razem: {{ fullPrice }} zł
              </div>
            </div>

            <div class="mt-5">
              <div class="tableShape">
                <h4>Dane zamówienia</h4>
                <div class="clearfix">
                  <div>
                    <label class="form-label mt-3 fw-bold">
                      Imię i nazwisko:
                    </label>
                    {{ purchaserName }}
                  </div>
                  <div>
                    <label class="form-label mt-3 fw-bold"> Email: </label>
                    {{ email }}
                  </div>
                  <div>
                    <label class="form-label mt-3 fw-bold"> Telefon: </label>
                    {{ phone }}
                  </div>
                  <div>
                    <label class="form-label mt-3 fw-bold">
                      Dodatkowe informacje:
                    </label>
                    {{ description }}
                  </div>
                  <div>
                    <label class="form-label mt-3 fw-bold">
                      Data i godzina zamówienia:
                    </label>
                    {{ new Date(date).toLocaleString() }}
                  </div>
                  <div>
                    <label class="form-label mt-3 fw-bold">
                      Metoda płatności:
                    </label>
                    {{ paymentMethod === 1 ? "Na miejscu" : "Płatność online" }}
                  </div>
                  <div>
                    <label class="form-label mt-4 fw-bold">
                      Lokalizacja:
                    </label>
                    {{ street + " " + zipCode + " " + cityName }}
                  </div>
                </div>
              </div>
            </div>

            <button class="mt-4 btn btn-primary w-50 mb-3" @click="onSubmit">
              <span @click="shownCard = 0">Złóż zamówienie</span>
            </button>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  CreateOrderCommand,
  FormTypeEnum,
  IAvailableDate,
  IFormAm,
  ILocation,
  IOrderPosition,
  OrderClient,
  OrderPosition,
  PaymentMethodEnum,
} from "@/core/api/pierogiesApi";
import { useField, useForm } from "vee-validate";
import confirmOrderValidationSchema from "@/views/Home/helpers/confirmOrderValidationSchema";
import moment from "moment";
import { PaymentMethodEnumTranslation } from "@/helpers/enums";
import ApiService from "@/core/api/ApiService";

export default defineComponent({
  name: "ConfirmOrder",
  setup: () => {
    const route = useRoute();
    const router = useRouter();
    const { errors, handleSubmit } = useForm({
      validationSchema: confirmOrderValidationSchema(),
    });

    const shownCard = ref(0);

    if (route.params.form == undefined) {
      router.push({ name: "Home" });
    }
    const form = ref<IFormAm>(
      JSON.parse(route.params.form as string) as IFormAm
    );

    //Location fields
    const availableLocations = form.value.availableLocations as ILocation[];

    const orderedPositions = reactive({
      items: JSON.parse(
        route.params.orderPositions as string
      ) as Array<IOrderPosition>,
    });

    const fullPrice = ref<number>(0);

    const selectedLocation = ref<ILocation>({
      name: "",
      zipCode: "",
      street: "",
      countryName: "",
      cityName: "",
      isDefault: false,
      description: "",
    });

    //Date fields
    const selectedDateRange = ref<IAvailableDate>({
      from: new Date(),
      to: new Date(),
    });
    const enableDatePicker = ref(false);

    const datesOutOfSelectedRange = (time: Date) => {
      if (selectedDateRange.value.from && selectedDateRange.value.to)
        return !moment(time.getTime()).isBetween(
          moment(selectedDateRange.value.from as Date).add(-1, "days"),
          selectedDateRange.value.to as Date
        );
      else return time.getTime() < Date.now();
    };

    watch(
      () => selectedDateRange.value,
      () => {
        enableDatePicker.value = true;
      }
    );

    const paymentMethodEnumValues = Object.keys(PaymentMethodEnum).filter(
      (item) => {
        return isNaN(Number(item));
      }
    );

    const { value: purchaserName } = useField<string>("purchaserName");
    const { value: email } = useField<string>("email");
    const { value: phone } = useField<string>("phone");
    const { value: date } = useField<Date>("date");
    const { value: deliveryPrice } = useField<number>("deliveryPrice");
    const { value: formId } = useField<string>("formId");
    const { value: description } = useField<string>("description");
    const { value: locationDescription } = useField<string | undefined>(
      "locationDescription"
    );
    const { value: locationName } = useField<string | undefined>(
      "locationName"
    );
    const { value: street } = useField<string | undefined>("street");
    const { value: zipCode } = useField<string | undefined>("zipCode");
    const { value: cityName } = useField<string | undefined>("cityName");
    const { value: countryName } = useField<string | undefined>("countryName");
    const { value: isDefault } = useField<boolean>("isDefault");
    const { value: needInvoice } = useField<boolean>("needInvoice");
    const { value: paymentMethod } = useField<number>("paymentMethod");
    const { value: positions } = useField<Array<OrderPosition>>("positions");

    const goToSummary = () => {
      shownCard.value = 2;
      orderedPositions.items.forEach(
        (p) => (fullPrice.value += p.price * p.amount)
      );
      if (selectedLocation.value.name == "Dostawa do domu") {
        isDefault.value = false;
      } else {
        isDefault.value = true;
        locationName.value = selectedLocation.value.name;
        locationDescription.value = selectedLocation.value.description;
        cityName.value = selectedLocation.value.cityName;
        street.value = selectedLocation.value.street;
        zipCode.value = selectedLocation.value.zipCode;
      }
    };

    const onSubmit = handleSubmit((values: any) => {
      const client = new OrderClient(process.env.VUE_APP_API_BASE_PATH, ApiService.instance);
      client
        .create(values as CreateOrderCommand)
        .then(() => {
          router.push({ name: "OrderConfirmed" });
        })
        .catch((err) => {
          console.log(err);
        });
    });

    const loadData = () => {
      if (form.value.formType != FormTypeEnum.ForHere) {
        availableLocations?.unshift({
          name: "Dostawa do domu",
          zipCode: "",
          street: "",
          cityName: "",
          countryName: "",
          isDefault: false,
          description: "",
        });
      }
      deliveryPrice.value = form.value.deliveryPrice;
      formId.value = form.value.id;
      needInvoice.value = false;
      countryName.value = "Polska";
      positions.value = orderedPositions.items as Array<OrderPosition>;
    };

    onMounted(() => {
      loadData();
    });

    return {
      //fields
      purchaserName,
      email,
      phone,
      date,
      description,
      cityName,
      zipCode,
      street,
      locationName,
      locationDescription,
      needInvoice,
      paymentMethod,

      orderedPositions,
      onSubmit,
      fullPrice,
      form,
      selectedLocation,
      goToSummary,
      shownCard,
      availableLocations,
      moment,
      datesOutOfSelectedRange,
      selectedDateRange,
      enableDatePicker,
      errors,
      paymentMethodEnumValues,
      PaymentMethodEnumTranslation,
      PaymentMethodEnum,
    };
  },
});
</script>
