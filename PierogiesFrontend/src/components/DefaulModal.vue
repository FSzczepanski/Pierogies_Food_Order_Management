<template>
  <div class="modal fade" tabindex="-1" role="dialog" >
    <div class="modal-dialog modal-sm" role="document" style="width:80%;">
      <div class="modal-content">
        <div class="modal-header">
          <div class="modal-title h3">
            Dane pozycji
          </div>
        </div>
        <div class="modal-body">
          <form @submit="onSubmit">
            asd
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" @click="saveChanges">Save changes</button>
          <button type="button" class="btn btn-secondary" @click="closeModal">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">


import {defineComponent, ref} from "vue";

export default defineComponent({
  name: "DefaultModal",
  props: {
    show: {type: Boolean, default: false},
  },
  components: {},
  emits: ["ok", "close-modal-event"],

  setup: function (props, {emit}) {
    const modalVisible = ref(false);


    const onSubmit = (values: string) => {
      console.log(values)
      modalVisible.value = false;
      emit("ok", "ok")
    };


    const closeModal = () => {
      emit('close-modal-event');
    };

    const saveChanges = () => {
      closeModal();
    };

    return {
      onSubmit,
      closeModal,
      saveChanges
    };
  }
});
</script>

<style scoped>

.modal {
  display: block;
}
</style>