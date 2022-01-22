<template>
  <div class="home">
    
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import {FormsClient, PositionsClient} from "@/core/api/swiftApi";

export default defineComponent({
  name: "Home",
  components: {
  },setup: () => {
    const formsList = ref(Array<any>([]));
      const client = new PositionsClient(process.env.VUE_APP_API_BASE_PATH);
    
    client.getPositions().then((response) => {
      formsList.value = response as any
      console.log(formsList.value);
    })
  }
});
</script>
