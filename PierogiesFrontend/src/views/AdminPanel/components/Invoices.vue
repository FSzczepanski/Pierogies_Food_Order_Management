<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="panelPath" />
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";
import PanelPath from "@/components/PanelPath.vue";

export default defineComponent({
  name: "Invoices",
  components: {PanelPath},
  props: {},
  setup: function (props, { emit }) {
    const panelPath = ref<Array<any>>([{label: "Faktury",path: "/board/invoices"}])
    return {
      panelPath
    };
  },
});
</script>
