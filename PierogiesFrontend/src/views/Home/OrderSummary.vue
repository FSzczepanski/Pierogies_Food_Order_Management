<template>
  <div>
    <h2>Podsumowanie zamówienia</h2>
    <div>
      <button
          class="mt-4 btn btn-primary w-50 mb-3"
          @click="SendOrder"
      >
        Zamawiam!
      </button>
    </div>
  </div>
</template>
<script lang="ts">
import {defineComponent, PropType, ref} from "vue";
import {ICreateOrderCommand} from "@/core/api/pierogiesApi";

export default defineComponent({
  name: "OrderSummary",
  props: {
    order: { type: Object as PropType<ICreateOrderCommand> },
  },
  setup: function (props, { emit }) {
    const sendOrder = () => {
      console.log(props.order);
    }
    return {sendOrder};
  },
});
</script>
