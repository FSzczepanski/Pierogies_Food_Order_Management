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
                v-model="locationModel.name"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="locationModel.description"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Kod pocztowy </label>
              <el-input
                v-model="locationModel.zipCode"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Ulica i numer </label>
              <el-input
                v-model="locationModel.street"
                placeholder="Wprowadź nazwę pozycji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Miasto </label>
              <el-input
                v-model="locationModel.cityName"
                placeholder="Wprowadź nazwę pozycji"
              />
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
import { ILocation } from "@/core/api/pierogiesApi";

export default defineComponent({
  name: "CreateLocationModal",
  components: {},
  emits: ["ok"],
  setup: function (props, { emit }) {
    const modalName = ref("Dodaj nową lokalizacje");

    const locationModel = ref<ILocation>({
      name: "",
      description: "",
      street: "",
      zipCode: "",
      cityName: "",
      countryName: "Polska",
      isDefault: true,
    });

    const showModal = ref(false);
    const closeModal = () => {
      showModal.value = false;
      locationModel.value = {
        name: "",
        description: "",
        street: "",
        zipCode: "",
        cityName: "",
        countryName: "Polska",
        isDefault: true,
      };
    };

    const openCreate = () => {
      showModal.value = true;
    };

    const saveForm = () => {
      const location = locationModel.value as ILocation;
      emit("ok", location);
      closeModal();
    };

    return {
      closeModal,
      saveForm,
      locationModel,
      showModal,
      openCreate,
      modalName,
    };
  },
});
</script>
