<template>
  <teleport to="body">
    <div v-if="show" class="modal d-block" tabindex="-1" role="dialog">
      <div
        class="modal-dialog-centered modal-lg m-2 m-auto"
        style="max-width: 80vh"
      >
        <div class="modal-content">
          <div class="modal-header">
            <div class="modal-title h4">
              {{ name }}
            </div>
            <div class="h2 enablePointer" @click="closeModal">
              <i class="justify-content-end d-flex bi bi-x"></i>
            </div>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="mb-5 col">
                <div>
                  <label class="form-label"> Ilość porcji </label>
                  <el-input
                      min="1"
                      max="100"
                      type="number"
                      v-model="amountValue"
                      step="1"
                  />
                </div>
                <div>
                  <label class="form-label fw-bold mt-3"> Cena </label>
                  <div>
                    {{ price }}
                  </div>
                </div>
                <div>
                  <label class="form-label fw-bold mt-3"> Porcja </label>
                  <div>
                    {{ portionSize }}
                  </div>
                </div>
              </div>
              <div class="col" v-if="photoUrl">
                <img class="positionModalImage m-3" :src="photoUrl" alt="Zdjecie" />
              </div>
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
    portionSize: { type: String, default: "" },
    price: { type: Number, default: undefined },
    photoUrl: { type: String, default: undefined },
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
