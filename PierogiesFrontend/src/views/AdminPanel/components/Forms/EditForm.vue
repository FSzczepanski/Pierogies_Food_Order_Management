<template>
  <position-modal @ok="positionCreated" ref="PositionModalRef" />
  <create-available-date-modal
    @ok="availableDateCreated"
    ref="AvailableDateModalRef"
  />
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="createPath" />
    <div class="mt-5 tableShape colorFourth">
      <h4 class="mt-4">Edycja formularza</h4>
      <div class="p-4 row">
        <div class="tableShape bg-light col-lg-7 m-3 ms-5">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="form-label required"> Nazwa formularza </label>
              <el-input v-model="editModel.name" disabled />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="editModel.description"
                :rows="3"
                type="textarea"
                placeholder="Wprowadź dodatkowy opis"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Koszt dostawy </label>
              <el-input v-model="editModel.deliveryPrice" disabled />
            </div>
            <div class="mt-4 mb-5">
              <label class="form-label required"> Formularz aktywny </label>
              <el-date-picker
                class="w-100"
                v-model="formActive"
                type="daterange"
                range-separator="To"
                start-placeholder="Start date"
                end-placeholder="End date"
              >
              </el-date-picker>
            </div>
          </div>
        </div>

        <div class="tableShape bg-light col-lg-4 m-3">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="mt-4 form-label required"> Typ formularza </label>
              <el-input
                class="col-lg-8"
                v-model="FormTypeEnumTranslation[editModel.formType-1]"
                disabled
              />
            </div>
            <label class="mt-4 form-label required">
              Dostępne metody płatności
            </label>
            <div>
              <el-select v-model="editModel.paymentMethods" multiple disabled>
                <el-option
                  v-for="(item, index) in editModel.paymentMethods"
                  :value="item"
                  :key="index"
                >
                  {{ PaymentMethodEnumTranslation[item] }}
                </el-option>
              </el-select>
            </div>
            <div v-if="editModel.formType === 1" class="mt-4">
              <label class="form-label required">
                Dostępne lokalizacje odbioru
              </label>
              <div>
                <el-select v-model="editModel.availableLocations" multiple disabled>
                  <el-option
                    v-for="(item, index) in editModel.availableLocations"
                    :value="index"
                    :label="item.name"
                    :key="index"
                  >
                    {{ item.name }}
                  </el-option>
                </el-select>
              </div>
            </div>
            <div v-if="editModel.formType === 1" class="mt-5">
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
                      v-for="(item, index) in editModel.availableDates"
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
        <button class="btn btn-primary" @click="updateForm">Zapisz</button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import {defineComponent, onMounted, reactive, ref, watch} from "vue";
import PanelPath from "@/components/PanelPath.vue";
import {
  AvailableDate,
  FormsClient,
  FormTypeEnum,
  IPositionAm,
  IUpdateFormCommand,
  Location,
  PositionCategoryEnum,
  PositionsClient,
  UpdateFormCommand,
} from "@/core/api/pierogiesApi";
import {useRoute, useRouter} from "vue-router";
import {FormTypeEnumTranslation, PaymentMethodEnumTranslation, PositionCategoryEnumTranslation} from "@/helpers/enums";
import PositionModal from "@/views/AdminPanel/components/Positions/PositionModal.vue";
import CreateAvailableDateModal from "@/views/AdminPanel/components/Forms/CreateAvailableDateModal.vue";
import moment from "moment/moment";
import {showToast} from "@/helpers/confirmationsAdapter";

export default defineComponent({
  name: "UpdateForm",
  components: {
    CreateAvailableDateModal,
    PositionModal,
    PanelPath,
  },
  props: {},
  setup: function (props, { emit }) {
    const router = useRouter();
    const route = useRoute();
    const formClient = new FormsClient(process.env.VUE_APP_API_BASE_PATH);

    const createPath = ref<Array<any>>([
      { label: "Formularze", path: "/board/forms" },
      { label: "Edycja", path: "/board/updateForm" },
    ]);

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

    

    //temp values to createModel
    const formActive = ref<Array<Date>>(Array.of(new Date(), new Date()));
    const selectedPositions = reactive({ items: [] as Array<IPositionAm> });

    const editModel = ref<IUpdateFormCommand>({
      id: "",
      name: "",
      description: "",
      deliveryPrice: 0,
      formActive: {} as AvailableDate,
      formType: FormTypeEnum.ForHere,
      availableDates: [] as Array<AvailableDate>,
      paymentMethods: [] as Array<number>,
      availableLocations: [] as Array<Location>,
      positions: [] as Array<string>,
      isActive: true,
      placeOnList: 0,
    });

    const getForm = () => {
      if (route.params.formId != "")
        formClient.get(route.params.formId as string).then((response) => {
          editModel.value.id = response.id;
          editModel.value.name = response.name;
          editModel.value.description = response.description;
          editModel.value.deliveryPrice = response.deliveryPrice;
          editModel.value.formActive = response.formActive;
          editModel.value.formType = response.formType;
          editModel.value.availableDates = response.availableDates;
          editModel.value.paymentMethods = response.paymentMethods;
          editModel.value.availableLocations = response.availableLocations;

          response.positions?.forEach((value) => {
            selectedPositions.items.push({
              id: value.positionId,
              identityNumber: 0,
              amount: value.amount,
              description: value.description,
              vat: value.vat,
              name: value.name,
              positionCategory: value.positionCategory as PositionCategoryEnum,
              portionSize: value.portionSize,
              price: value.price,
              hasPhoto: value.hasPhoto,
              photo: value.photo,
            });
            
            formActive.value[0] = editModel.value.formActive?.from as Date;
            formActive.value[1] = editModel.value.formActive?.to as Date;
          });
          editModel.value.isActive = response.isActive;
          editModel.value.placeOnList = response.placeOnList;
        });
    };
    

    const formTypeEnumValues = Object.keys(FormTypeEnum).filter((item) => {
      return isNaN(Number(item));
    });
    const paymentMethodsPl = ref(["Na miejscu", "Przelewy24"]);

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
    
    const AvailableDateModalRef = ref<typeof CreateAvailableDateModal>();
    const openCreateAvailableDateModal = () => {
      AvailableDateModalRef.value?.openCreate();
    };
    const availableDateCreated = (from: Date, to: Date) => {
      if (from && to) {
        editModel.value.availableDates?.push({from: from, to: to} as AvailableDate);
      }
    };

    const deleteAvailableDate = (index: number) => {
      editModel.value.availableDates?.splice(index, 1);
    };
    

    const updateForm = () => {
      selectedPositions.items.forEach((pos) => {
        editModel.value.positions?.push(pos.id);
      });

      editModel.value.formActive = {
        from: formActive.value[0],
        to: formActive.value[1],
      } as AvailableDate;

      formClient
        .update(editModel.value as UpdateFormCommand, editModel.value.id)
        .then((response) => {
          router.push({
            name: "Forms",
          });
          console.log("sukces, " + response);
        })
        .catch((err) => {
          showToast("Wypełnij wszystkie pola");
          console.log(err);
        });
    };

    onMounted(() => {
      getPositions();
      getForm();
    });

    return {
      createPath,
      editModel,
      formActive,
      updateForm,
      positionsList,
      formTypeEnumValues,
      paymentMethodsPl,
      selectedPositions,
      positionToAdd,
      PositionCategoryEnumTranslation,
      deletePosition,
      PositionModalRef,
      openCreatePositionModal,
      positionCreated,
      deleteAvailableDate,
      availableDateCreated,
      openCreateAvailableDateModal,
      AvailableDateModalRef,
      moment,
      FormTypeEnumTranslation,
      PaymentMethodEnumTranslation,
    };
  },
});
</script>
