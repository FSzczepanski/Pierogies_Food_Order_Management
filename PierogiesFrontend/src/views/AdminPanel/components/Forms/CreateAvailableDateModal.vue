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
            <el-date-picker
              v-model="formActive"
              value-format="yyyy-MM-dd hh-mm"
              type="datetimerange"
              range-separator="do"
              start-placeholder="Początek"
              end-placeholder="Koniec"
            >
            </el-date-picker>
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
import { IAvailableDate } from "@/core/api/pierogiesApi";

export default defineComponent({
  name: "CreateAvailableDateModal",
  components: {},
  emits: ["ok"],
  setup: function (props, { emit }) {
    const modalName = ref("Dodaj nowy zakres dat");
    const formActive = ref();
    const availableDateModel = ref<IAvailableDate>({
      from: new Date(),
      to: new Date()
    });

    const showModal = ref(false);
    const closeModal = () => {
      showModal.value = false;
      availableDateModel.value = {
        from: new Date(),
        to: new Date()
      };
      formActive.value = undefined;
    };

    const openCreate = () => {
      showModal.value = true;
    };

    const saveForm = () => {
      availableDateModel.value = {
        from: formActive.value[0],
        to: formActive.value[1],
      };
      
      emit("ok", availableDateModel as IAvailableDate);
      closeModal();
    };

    return {
      closeModal,
      saveForm,
      formActive,
      showModal,
      openCreate,
      modalName,
    };
  },
});
</script>
