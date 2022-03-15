<template>
  <div class="row col-lg-4 fs-5">
    <router-link
      class="col-lg-1 text-decoration-none me-4"
      to="/board/dashboard"
    >
      <i class="btn btn-info rounded-circle bi bi bi-house-door"></i>
    </router-link>
    <div class="col mt-2" v-for="(item, index) in pathValues" :key="index">
      <div class="col-lg-1 textFourth float-lg-start">/</div>
      <router-link
        class="ms-4 textFourth text-decoration-none float-lg-start"
        :to="item.path"
      >
        {{ item.label }}
      </router-link>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";
import { PanelPath } from "@/helpers/inferfaces";

export default defineComponent({
  props: {
    paths: Array,
  },
  setup: function (props, { emit }) {
    const pathValues = ref<Array<PanelPath>>(props.paths as Array<PanelPath>);
    return {
      pathValues,
    };
  },
});
</script>
