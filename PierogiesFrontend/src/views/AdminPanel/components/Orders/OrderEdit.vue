<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="createPath" />
    <div class="mt-5 tableShape colorFourth">
      <h4 class="mt-4">Edycja zamówienia</h4>
      <div class="p-4 row">
        <div class="tableShape bg-light col-lg-7 m-3 ms-5">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="form-label required"> Imię i nazwisko </label>
              <el-input v-model="editModel.item.purchaserName" disabled />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Email </label>
              <el-input v-model="editModel.item.email" />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Numer telefonu </label>
              <el-input v-model="editModel.item.phone" />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="editModel.item.locationDescription"
                :rows="3"
                type="textarea"
                placeholder="Dodatkowy opis"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Koszt dostawy </label>
              <el-input v-model="editModel.item.deliveryPrice" />
            </div>
          </div>
        </div>

        <div class="tableShape bg-light col-lg-4 m-3">
          <div class="col-lg-8 m-auto">
            <div class="mt-4 mb-5">
              <label class="form-label required"> Data odbioru/dostawy </label>
              <el-date-picker
                  class="w-100"
                  type="datetime"
                  v-model="editModel.item.date"
              >
              </el-date-picker>
            </div>
            <div class="mt-3">
              <label class="form-label required"> Ulica i numer </label>
              <el-input
                v-model="editModel.item.street"
                placeholder="Wprowadź ulicę i numer"
              />
            </div>
            <div>
              <label class="form-label required"> Kod pocztowy </label>
              <el-input
                v-model="editModel.item.zipCode"
                placeholder="Wprowadź kod pocztowy"
              />
            </div>
            <div>
              <label class="form-label required"> Miejscowość </label>
              <el-input
                v-model="editModel.item.cityName"
                placeholder="Wprowadź nazwe miejscowości"
              />
            </div>
            <div class="mt-3">
              <label class="form-label required">
                Dodatkowe informacje o odbiorze
              </label>
              <el-input
                v-model="editModel.item.description"
                placeholder="Wprowadź dodatkowe informacje od klienta"
              />
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
                  <th>Cena</th>
                  <th>Liczba porcji</th>
                  <th>Porcja</th>
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
                    {{ item.price }} zł
                  </td>
                  <td>
                    {{ item.amount }}
                  </td>
                  <td>
                    {{ item.portionSize }}
                  </td>
                  <td>
                    <button
                      class="btn btn-danger rounded-circle ms-2 bi bi-trash"
                      @click="deletePosition(item.positionId)"
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
            </div>
          </div>
        </div>
        <button class="btn btn-primary" @click="updateOrder">Zapisz</button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, onMounted, reactive, ref, watch } from "vue";
import PanelPath from "@/components/PanelPath.vue";
import {
  IOrderPosition,
  IPositionAm,
  IUpdateOrderCommand,
  OrderClient,
  OrderPosition,
  PositionsClient,
  UpdateOrderCommand,
} from "@/core/api/pierogiesApi";
import { useRoute, useRouter } from "vue-router";
import moment from "moment/moment";
import { showToast } from "@/helpers/confirmationsAdapter";
import apiService from "@/core/api/ApiService";
import {PositionCategoryEnumTranslation} from "@/helpers/enums";

export default defineComponent({
  name: "UpdateForm",
  components: {
    PanelPath,
  },
  setup() {
    const router = useRouter();
    const route = useRoute();
    const orderId = route.params.orderId;

    const createPath = ref<Array<any>>([
      { label: "Zamowienia", path: "/board/orders" },
      { label: "Edycja", path: "" },
    ]);

    const ordersClient = new OrderClient(
      process.env.VUE_APP_API_BASE_PATH,
      apiService.instance
    );

    const positionsClient = new PositionsClient(
      process.env.VUE_APP_API_BASE_PATH,
      apiService.instance
    );

    const positionsList = reactive({ items: [] as Array<IPositionAm> });
    const selectedPositions = reactive({ items: [] as Array<IOrderPosition> });

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

    const positionToAdd = ref<IPositionAm>();
    //Watch position to add
    watch(
      () => positionToAdd.value,
      () => {
        if (positionToAdd.value?.name)
          selectedPositions.items.push({
            positionId: positionToAdd.value?.id,
            name: positionToAdd.value?.name,
            price: positionToAdd.value?.price,
            vat: positionToAdd.value?.vat,
            amount: positionToAdd.value?.amount,
            portionSize: positionToAdd.value?.portionSize,
          });
      }
    );

    const deletePosition = (id: string) => {
      selectedPositions.items = selectedPositions.items.filter(
        (x) => x.positionId !== id
      );
    };

    const editModel = reactive({ item: {} as IUpdateOrderCommand });

    const getOrder = () => {
      if (orderId)
        ordersClient.getForEdit(orderId as string).then((response) => {
          editModel.item = {
            id: response.id,
            purchaserName: response.name,
            email: response.email,
            phone: response.phone,
            positions: response.positions,
            date: response.date,
            locationName: response.locationName,
            locationDescription: response.locationDescription,
            street: response.street,
            zipCode: response.zipCode,
            cityName: response.cityName,
            countryName: response.countryName,
            isDefault: response.isDefault,
            formId: response.formId,
            deliveryPrice: response.deliveryPrice,
            description: response.description,
          };
          selectedPositions.items = response.positions as Array<IOrderPosition>;
        });
    };

    const updateOrder = () => {
      editModel.item.positions =
        selectedPositions.items as Array<OrderPosition>;

      ordersClient
        .update(editModel.item as UpdateOrderCommand, editModel.item.id)
        .then((response) => {
          router.push({
            name: "Orders",
          });
          console.log("sukces, " + response);
        })
        .catch((err) => {
          showToast("Poprawnie wypełnij wszystkie pola");
          console.log(err);
        });
    };

    onMounted(() => {
      getPositions();
      getOrder();
    });

    return {
      createPath,
      editModel,
      updateOrder,
      positionsList,
      selectedPositions,
      positionToAdd,
      deletePosition,
      moment,
      PositionCategoryEnumTranslation,
    };
  },
});
</script>
