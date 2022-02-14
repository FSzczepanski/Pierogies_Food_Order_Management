<template>
  <teleport to="body">
    <div v-if="showModal" class="modal d-block" tabindex="-1" role="dialog">
      <div
        class="modal-dialog-centered modal-lg m-2 m-auto"
        style="max-width: 80vh"
      >
        <div class="modal-content">
          <div class="modal-header">
            <div class="modal-title h3">
              {{ modalName }}
            </div>
          </div>
          <div class="modal-body">
            <div class="mt-4">
              <label class="form-label required"> Nazwa </label>
              <el-input
                v-model="positionModel.name"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="positionModel.description"
                :rows="2"
                type="textarea"
                placeholder="Wprowadź opis"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Cena </label>
              <el-input
                type="number"
                min="0"
                v-model="positionModel.price"
                placeholder="Wprowadź cenę"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Stawka vat </label>
              <el-input
                type="number"
                step="0.01"
                min="0.01"
                v-model="positionModel.vat"
                placeholder="Wprowadź stawke vat"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Wielkość porcji </label>
              <el-input
                v-model="positionModel.portionSize"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <label class="mt-4 form-label required"> Kategoria </label>
            <div>
              <el-select
                class="col-lg-12"
                v-model="positionModel.positionCategory"
              >
                <el-option
                  v-for="(item, index) in toArray(PositionCategoryEnum)"
                  :value="index"
                  :label="PositionCategoryEnumTranslation[index]"
                  :key="index"
                >
                  {{ PositionCategoryEnumTranslation[index] }}
                </el-option>
              </el-select>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" @click="saveForm">
              Dodaj
            </button>
            <button type="button" class="btn btn-info" @click="closeModal">
              Anuluj
            </button>
          </div>
        </div>
      </div>
    </div>
  </teleport>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";
import {
  CreatePositionCommand,
  ICreatePositionCommand,
  PositionCategoryEnum,
  PositionsClient,
  UpdatePositionCommand,
} from "@/core/api/pierogiesApi";
import { PositionCategoryEnumTranslation } from "@/helpers/enums";
import { toArray } from "@/helpers/enums";

export default defineComponent({
  name: "PositionModal",
  components: {},
  emits: ["ok"],

  setup: function (props, { emit }) {
    const modalName = ref("Dodaj nową pozycje");

    const positionModel = ref({
      id: "",
      name: "",
      amount: 0,
      positionCategory: 0,
      description: "",
      vat: 0,
      portionSize: "",
      price: 0,
    });

    const showModal = ref(false);
    const closeModal = () => {
      showModal.value = false;
      positionModel.value = {
        id: "",
        name: "",
        amount: 0,
        positionCategory: 0,
        description: "",
        vat: 0,
        portionSize: "",
        price: 0,
      };
    };

    const positionsClient = new PositionsClient(
      process.env.VUE_APP_API_BASE_PATH
    );

    const openCreate = () => {
      showModal.value = true;
    };

    const openUpdate = (id: string) => {
      positionsClient
        .get(id)
        .then((response) => {
          positionModel.value.id = response.id;
          positionModel.value.name = response.name as string;
          positionModel.value.description = response.description as string;
          positionModel.value.positionCategory = response.positionCategory;
          positionModel.value.vat = response.vat;
          positionModel.value.portionSize = response.portionSize as string;
          positionModel.value.price = response.price;
        })
        .catch((err) => {
          console.log(err);
        });

      modalName.value = "Edytuj pozycję";

      showModal.value = true;
    };

    const saveForm = () => {
      if (positionModel.value.id != "") {
        positionsClient
          .update(
            positionModel.value as UpdatePositionCommand,
            positionModel.value.id
          )
          .then(() => {
            emit("ok");
            closeModal();
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        const model = positionModel.value as ICreatePositionCommand;

        positionsClient
          .create(model as CreatePositionCommand)
          .then(() => {
            emit("ok");
            closeModal();
          })
          .catch((err) => {
            console.log(err);
          });
      }
    };

    return {
      closeModal,
      saveForm,
      positionModel,
      PositionCategoryEnumTranslation,
      PositionCategoryEnum,
      toArray,
      showModal,
      openCreate,
      openUpdate,
      modalName,
    };
  },
});
</script>
