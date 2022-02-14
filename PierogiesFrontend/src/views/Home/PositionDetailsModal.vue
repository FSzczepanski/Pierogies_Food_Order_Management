<template>
  <teleport to="body">
    <div v-if="show" class="modal d-block" tabindex="-1" role="dialog">
      <div
        class="modal-dialog-centered modal-lg m-2 m-auto"
        style="max-width: 80vh"
      >
        <div class="modal-content">
          <div class="modal-header">
            <div class="modal-title h3">
              {{ name }}
            </div>
          </div>
          <div class="modal-body">
            <div class="mb-5">
              <label class="form-label"> Ilość porcji </label>
              <el-input
                min="0"
                max="100"
                type="number"
                v-model="amountValue"
                step="1"
              />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" @click="saveForm">
              Dodaj do zamówienia
            </button>
            <button type="button" class="btn btn-info" @click="closeModal">
              Zamknij
            </button>
          </div>
        </div>
      </div>
    </div>
  </teleport>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "PositionDefaultModal",
  props: {
    show: { type: Boolean, default: false },
    name: { type: String, default: "Nazwa" },
    amount: { type: Number, default: 1 },
  },
  components: {},
  emits: ["ok", "closeModal"],

  setup: function (props, { emit }) {
    const amountValue = ref(props.amount);

    const closeModal = () => {
      amountValue.value = 1;
      emit("closeModal");
    };

    const saveForm = () => {
      emit("ok", amountValue.value);
      closeModal();
    };

    return {
      closeModal,
      saveForm,
      amountValue,
    };
  },
});
</script>

