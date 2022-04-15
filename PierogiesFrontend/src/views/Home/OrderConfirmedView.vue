<template>
  <div style="min-height: 95vh">
    <img class="w-25 mt-3" src="icon.png" alt="zdjecie" />
    <div class="h1 mt-4">Dziękujemy!</div>
    <div class="h3 m-4">Twoje zamówienie zostało złożone</div>
    <div>
      Aby zobaczyć potwierdzenie złożenia zamówienia sprawdź pocztę e-mail
    </div>
    <div>
      <router-link to="/">
        <div class="mt-5 btn btn-primary w-50 mb-3">
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
