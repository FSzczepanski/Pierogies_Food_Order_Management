<template>
  <position-modal @ok="positionCreated" ref="PositionModalRef" />
  <create-location-modal @ok="locationCreated" ref="LocationModalRef" />
  <create-available-date-modal
    @ok="availableDateCreated"
    ref="AvailableDateModalRef"
  />
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="createPath" />
    <div class="mt-5 tableShape colorFourth">
      <h4 class="mt-4">Tworzenie nowego formularza</h4>
      <div class="p-4 row">
        <div class="tableShape bg-light col-lg-7 m-3 ms-5">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="form-label required"> Nazwa formularza </label>
              <el-input
                v-model="createModel.name"
                max="40"
                placeholder="Wprowadź nazwę formularza"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="createModel.description"
                :rows="3"
                max="300"
                type="textarea"
                placeholder="Wprowadź dodatkowy opis"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Koszt dostawy </label>
              <el-input
                v-model="createModel.deliveryPrice"
                type="number"
                min="0"
                max="1000"
                placeholder="Wprowadź cenę dostawy"
              />
            </div>
            <div class="mt-4 mb-5">
              <label class="form-label required"> Formularz aktywny </label>
              <el-date-picker
                class="w-100"
                v-model="formActive"
                type="daterange"
                range-separator="do"
                start-placeholder="Początek"
                end-placeholder="Koniec"
              >
              </el-date-picker>
            </div>
          </div>
        </div>

        <div class="tableShape bg-light col-lg-4 m-3">
          <div class="col-lg-8 m-auto">
            <label class="mt-4 form-label required"> Typ formularza </label>
            <div>
              <el-select v-model="selectedFormType">
                <el-option
                  v-for="(item, index) in formTypeEnumValues"
                  :value="item"
                  :label="FormTypeEnumTranslation[index]"
                  :key="index"
                >
                  {{ FormTypeEnumTranslation[index] }}
                </el-option>
              </el-select>
            </div>
            <label class="mt-4 form-label required">
              Dostępne metody płatności
            </label>
            <div>
              <el-select v-model="selectedPaymentMethodsPl" multiple>
                <el-option
                  v-for="(item, index) in paymentMethodsPl"
                  :value="item"
                  :key="index"
                >
                  {{ item }}
                </el-option>
              </el-select>
            </div>
            <div v-if="selectedFormType == 'Event'" class="mt-4">
              <label class="form-label required">
                Dostępne lokalizacje odbioru
              </label>
              <div>
                <el-select v-model="availableLocationIndexes.items" multiple>
                  <el-option
                    v-for="(item, index) in locations.items"
                    :value="index"
                    :label="item.name"
                    :key="index"
                  >
                    {{ item.name }}
                  </el-option>
                </el-select>
                <button
                  class="btn-sm btn-secondary"
                  @click="openCreateLocationModal"
                >
                  <i class="bi bi-plus"></i>
                </button>
              </div>
            </div>
            <div v-if="selectedFormType === 'Event'" class="mt-5">
              <label class="form-label required">
                Dostępne możliwe daty odbioru
              </label>
              <div>
                <table class="table table-hover">
                  <thead>
                    <tr>
                      <th>Od</th>
                      <th>Do</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="(item, index) in createModel.availableDates"
                      :key="index"
                    >
                      <td>
                        <span>{{
                          moment(item.from).format("MM/DD/YYYY HH:mm")
                        }}</span>
                      </td>
                      <td>
                        <span>{{
                          moment(item.to).format("MM/DD/YYYY HH:mm")
                        }}</span>
                      </td>
                      <td>
                        <button
                          class="btn btn-secondary rounded-circle ms-2"
                          @click="deleteAvailableDate(index)"
                        >
                          X
                        </button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <button
                  class="btn-sm btn-info"
                  @click="openCreateAvailableDateModal"
              >
                Dodaj
              </button>
            </div>
            
          </div>
        </div>
        <div class="tableShape bg-light col-lg-11 mb-5 ms-5">
          <h3 class="mt-3">Pozycje</h3>
          <div class="col-lg-9 m-auto mt-4">
            <table class="table table-rounded table-hover border gy-2 gs-2">
              <thead>
                <tr>
                  <th>#</th>
                  <th>Pozycja</th>
                  <th>kategoria</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(item, index) in selectedPositions.items"
                  :key="index"
                >
                  <td>
                    {{ index + 1 }}
                  </td>
                  <td>
                    {{ item.name }}
                  </td>
                  <td>
                    {{ PositionCategoryEnumTranslation[item.positionCategory] }}
                  </td>
                  <td>
                    <button
                      class="btn btn-danger rounded-circle ms-2 bi bi-trash"
                      @click="deletePosition(item.id)"
                    ></button>
                  </td>
                </tr>
              </tbody>
            </table>

            <div class="row">
              <div class="col">
                <label class="mt-4 form-label required">Wybierz </label>
                <div>
                  <el-select class="col-lg-11" v-model="positionToAdd">
                    <el-option
                      v-for="(item, index) in positionsList.items"
                      :value="item"
                      :key="index"
                    >
                      {{ index + 1 }} | {{ item.name }} |
                      {{
                        PositionCategoryEnumTranslation[item.positionCategory]
                      }}
                      <hr />
                    </el-option>
                  </el-select>
                </div>
              </div>
              <div class="col-lg-4 mb-5 me-5 mt-5">
                <button
                  class="btn btn-info col-lg-8"
                  @click="openCreatePositionModal"
                >
                  Dodaj nową
                </button>
              </div>
            </div>
          </div>
        </div>
        <button class="btn btn-primary" @click="createForm">
          Zapisz formularz
        </button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, reactive, ref, watch } from "vue";
import PanelPath from "@/components/PanelPath.vue";
import {
  AvailableDate,
  CreateFormCommand,
  FormsClient,
  FormTypeEnum,
  IAvailableDate,
  ICreateFormCommand,
  ILocation,
  IPositionAm,
  Location,
  PositionsClient,
  SystemSettingsClient,
} from "@/core/api/pierogiesApi";
import { useRouter } from "vue-router";
import {
  PositionCategoryEnumTranslation,
  FormTypeEnumTranslation,
} from "@/helpers/enums";
import PositionModal from "@/views/AdminPanel/components/Positions/PositionModal.vue";
import CreateLocationModal from "@/views/AdminPanel/components/Forms/CreateLocationModal.vue";
import CreateAvailableDateModal from "@/views/AdminPanel/components/Forms/CreateAvailableDateModal.vue";
import moment from "moment/moment";
import {showToast} from "@/helpers/confirmationsAdapter";
export default defineComponent({
  name: "CreateForm",
  components: {
    CreateAvailableDateModal,
    CreateLocationModal,
    PositionModal,
    PanelPath,
  },
  props: {},
  setup: function (props, { emit }) {
    const router = useRouter();

    const createPath = ref<Array<any>>([
      { label: "Formularze", path: "/board/forms" },
      { label: "Nowy", path: "/board/createForm" },
    ]);
    const formTypeEnumValues = Object.keys(FormTypeEnum).filter((item) => {
      return isNaN(Number(item));
    });
    const paymentMethodsPl = ref(["Na miejscu", "Przelewy24"]);

    const positionsList = reactive({ items: [] as Array<IPositionAm> });

    const positionsClient = new PositionsClient(
      process.env.VUE_APP_API_BASE_PATH
    );

    const getPositions = () => {
      positionsClient
        .getPositions()
        .then((response) => {
          positionsList.items = response.items as Array<IPositionAm>;
        })
        .catch((err) => {
          console.log(err);
        });
    };

    getPositions();

    //temp values to createModel
    const formActive = ref<any>('');
    const selectedPaymentMethodsPl = ref<Array<string>>([]);
    const selectedFormType = ref();
    const selectedPositions = reactive({ items: [] as Array<IPositionAm> });

    const createModel = ref<ICreateFormCommand>({
      name: "",
      description: "",
      deliveryPrice: 0,
      formActive: {} as AvailableDate,

      formType: FormTypeEnum.Event,
      availableDates: [] as Array<AvailableDate>,
      paymentMethods: [] as Array<number>,
      availableLocations: [] as Array<Location>,
      positions: [] as Array<string>,
      isActive: true,
      placeOnList: 0,
    });

    const positionToAdd = ref<IPositionAm>();
    //Watch position to add
    watch(
      () => positionToAdd.value,
      () => {
        if (positionToAdd.value?.name)
          selectedPositions.items.push(positionToAdd.value as IPositionAm);
      }
    );

    const deletePosition = (id: string) => {
      selectedPositions.items = selectedPositions.items.filter(
        (x) => x.id !== id
      );
    };

    const PositionModalRef = ref<typeof PositionModal>();
    const openCreatePositionModal = () => {
      PositionModalRef.value?.openCreate();
    };

    const positionCreated = () => {
      getPositions();
    };

    const availableLocationIndexes = reactive({ items: [] as Array<number> });
    const locations = reactive({ items: [] as Array<ILocation> });
    const LocationModalRef = ref<typeof CreateLocationModal>();
    const openCreateLocationModal = () => {
      LocationModalRef.value?.openCreate();
    };

    const locationCreated = (location: ILocation) => {
      if (location) {
        locations.items.push(location);
      }
    };

    const AvailableDateModalRef = ref<typeof CreateAvailableDateModal>();
    const openCreateAvailableDateModal = () => {
      AvailableDateModalRef.value?.openCreate();
    };
    const availableDateCreated = (from: Date, to: Date) => {
      if (from && to) {
        createModel.value.availableDates?.push({from: from, to: to} as AvailableDate);
      }
    };

    const deleteAvailableDate = (index: number) => {
      createModel.value.availableDates?.splice(index, 1);
    };

    const settingClient = new SystemSettingsClient(
      process.env.VUE_APP_API_BASE_PATH
    );
    settingClient.get().then((response) => {
      if (response.location) {
        locations.items.push(response.location as ILocation);
      }
    });

    const formClient = new FormsClient(process.env.VUE_APP_API_BASE_PATH);

    const createForm = () => {
      createModel.value.formActive = {
        from: formActive.value[0],
        to: formActive.value[1],
      } as AvailableDate;

      createModel.value.formType =
        selectedFormType.value == "Event"
          ? FormTypeEnum.Event
          : selectedFormType.value == "ForHere"
          ? FormTypeEnum.ForHere
          : FormTypeEnum.Delivery;
      selectedPaymentMethodsPl.value.forEach((pm) => {
        var paymentMethodInt = pm == "Na miejscu" ? 1 : 2;

        if (paymentMethodInt)
          createModel.value.paymentMethods?.push(paymentMethodInt);
      });

      selectedPositions.items.forEach((pos) => {
        createModel.value.positions?.push(pos.id);
      });

      availableLocationIndexes.items.forEach((i) => {
        const location = locations.items[i];
        if (location)
          createModel.value.availableLocations?.push(location as Location);
      });

      formClient
        .create(createModel.value as CreateFormCommand)
        .then((response) => {
          router.push({
            name: "Forms",
          });
          console.log("sukces, " + response);
        })
        .catch((err) => {
          showToast("Wystąpił błąd podczas wysyłania, wypełnij wszystkie pola!");
          console.log(err);
        });
    };

    return {
      createPath,
      createModel,
      formTypeEnumValues,
      paymentMethodsPl,
      selectedPaymentMethodsPl,
      selectedFormType,
      formActive,
      createForm,
      positionsList,
      selectedPositions,
      positionToAdd,
      locations,
      PositionCategoryEnumTranslation,
      FormTypeEnumTranslation,
      deletePosition,
      PositionModalRef,
      LocationModalRef,
      openCreatePositionModal,
      positionCreated,
      locationCreated,
      openCreateLocationModal,
      availableLocationIndexes,
      availableDateCreated,
      AvailableDateModalRef,
      openCreateAvailableDateModal,
      moment,
      deleteAvailableDate,
    };
  },
});
</script>
