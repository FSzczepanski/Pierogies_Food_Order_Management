<template>
  
    <div class="imga  text-center " :style="{
        'background-image': `url(${require('/public/background.jpg')})`,
        width: '300vh',
        height: '100vh',
        overflowX: 'auto'
      }">
      <button class="center btn btn-primary btn-lg position-absolute top-50 " @click="goto('div1')">Zobacz menu</button>
    </div>
  <div ref="div1" style="height: 1100px">
    <h1 class="mt-5">Hej!</h1>
    
    <div class="d-flex justify-content-center">
      <div v-for="(item, index) in formsList" :key="index"
         class="btn btn-dark rounded-circle m-5" style="font-size: 30px">{{item.name}}</div>

    </div>
    </div>

  
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import {FormsClient, IFormDetailListAm} from "@/core/api/swiftApi";


export default defineComponent({
  name: "Home",
  components: {
  },setup: () => {
    const formsList = ref(Array<any>([]));
      const client = new FormsClient(process.env.VUE_APP_API_BASE_PATH);
    
    client.getForms().then((response) => {
      formsList.value = response.items as Array<IFormDetailListAm>
      console.log(formsList);
    })


    const goto = (refName :any) => {
      window.scrollTo(0, 1200);
    }
    
    return{
      goto, 
      formsList
    }
    
  }
});
</script>
<style>
.imga{
  max-width: 100%;
  max-height: 100%;
  background-position:center;
  
}
</style>