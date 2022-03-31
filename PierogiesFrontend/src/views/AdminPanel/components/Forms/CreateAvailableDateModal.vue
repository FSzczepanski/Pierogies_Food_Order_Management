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
          <div class="modal-body m-auto mt-4">
            <div>
              <label> Od </label>
            </div>
            <div>
              <el-date-picker
                class="m-3"
                v-model="from"
                type="datetime"
                placeholder="Wybierz date i godzine"
              />
            </div>
            <div>
              <label> Do </label>
            </div>
            <div>
              <el-date-picker
                class="m-3"
                v-model="to"
                type="datetime"
                placeholder="Wybierz date i godzine"
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
import { showToast } from "@/helpers/confirmationsAdapter";

export default defineComponent({
  name: "CreateAvailableDateModal",
  components: {},
  emits: ["ok"],
  setup: function (props, { emit }) {
    const modalName = ref("Dodaj nowy zakres dat");
    const from = ref();
    const to = ref();

    const showModal = ref(false);
    const closeModal = () => {
      showModal.value = false;
      from.value = undefined;
      to.value = undefined;
    };

    const openCreate = () => {
      showModal.value = true;
    };

    const saveForm = () => {
      const fromDate = from.value as Date;
      const toDate = to.value as Date;

      if (fromDate.getTime() >= toDate.getTime()) {
        showToast("Data początkowa musi być mniejsza niż końcowa");
      } else {
        emit("ok", fromDate, toDate);
        closeModal();
      }
    };

    return {
      closeModal,
      saveForm,
      from,
      to,
      showModal,
      openCreate,
      modalName,
    };
  },
});
</script>
