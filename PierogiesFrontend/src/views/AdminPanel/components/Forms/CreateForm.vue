<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="createPath"/>
    <div class="mt-5 tableShape bg-light p-4">
      tabelka
    </div>
  </div>
</template>
<script lang="ts">
import {defineComponent, ref} from "vue";
import PanelPath from "@/components/PanelPath.vue";

export default defineComponent({
  name: "CreateForm",
  components: {PanelPath},
  props: {},
  setup: function (props, { emit }) {
    const createPath = ref<Array<any>>([{label: "Formularze",path: "/board/forms"}, {label: "Nowy",path: "/board/createForm"}])
    return {
      createPath
    };
  },
});
</script>
