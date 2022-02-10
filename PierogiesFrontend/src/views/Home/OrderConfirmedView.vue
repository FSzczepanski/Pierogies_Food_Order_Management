<template>
  <div>
    <h2>Zamówienie zostało złożone!</h2>
    <div>Aby zobaczyć potwierdzenie złożenia zamówienia sprawdź pocztę</div>
    <div>
      <router-link to="/">
        <div class="mt-4 btn btn-primary w-50 mb-3">
          Przejdź do strony głównej
        </div>
      </router-link>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "OrderConfirmedVue",
  props: {},
  setup: function (props, { emit }) {


    return;
  },
});
</script>
