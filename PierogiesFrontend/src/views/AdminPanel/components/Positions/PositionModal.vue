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
              <label class="form-label required"> Stawka vat % </label>
              <el-input
                type="number"
                step="1"
                min="1"
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
            <div class="mt-4">
              <label class="form-label required"> Zdjęcie </label>
              <div>
                <input
                  class="form-control"
                  type="file"
                  @change="photoChanged"
                />
              </div>
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
import { defineComponent, reactive, ref } from "vue";
import {
  CreatePositionCommand,
  ICreatePositionCommand,
  PositionCategoryEnum,
  PositionsClient,
  UpdatePositionCommand,
} from "@/core/api/pierogiesApi";
import { PositionCategoryEnumTranslation } from "@/helpers/enums";
import { toArray } from "@/helpers/enums";
import axios from "axios";
import { showToast } from "@/helpers/confirmationsAdapter";
import ApiService from "@/core/api/ApiService";

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
      vat: 8,
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
        vat: 8,
        portionSize: "",
        price: 0,
      };
    };

    const positionsClient = new PositionsClient(
      process.env.VUE_APP_API_BASE_PATH, ApiService.instance
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
          positionModel.value.vat = response.vat * 100;
          positionModel.value.portionSize = response.portionSize as string;
          positionModel.value.price = response.price;
        })
        .catch((err) => {
          console.log(err);
        });

      modalName.value = "Edytuj pozycję";

      showModal.value = true;
    };

    const photoFile = reactive({ photo: new FormData() });

    const photoChanged = (event: any) => {
      var photo = event.target.files[0];
      photoFile.photo.append("file", photo);
    };

    const sendPhoto = (positionId: string) => {
      const url = process.env.VUE_APP_API_BASE_PATH;
      axios
        .put(
          url + "/api/v1/core/positions/photo/" + positionId,
          photoFile.photo,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        )
        .then(() => {
          console.log("success");
        })
        .catch((err) => {
          console.log(err);
        });
    };

    const saveForm = () => {
      positionModel.value.vat = positionModel.value.vat / 100;

      if (positionModel.value.id != "") {
        positionsClient
          .update(
            positionModel.value as UpdatePositionCommand,
            positionModel.value.id
          )
          .then(() => {
            sendPhoto(positionModel.value.id);
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
          .then((id) => {
            sendPhoto(id);
            emit("ok");
            closeModal();
          })
          .catch((err) => {
            showToast(
              "Wystąpił błąd podczas wysyłania, wypełnij wszystkie pola!"
            );
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
      photoFile,
      photoChanged,
    };
  },
});
</script>
