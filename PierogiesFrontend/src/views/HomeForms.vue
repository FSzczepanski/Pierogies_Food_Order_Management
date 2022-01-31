<template>
  <div style="min-height: 900px">
    <h1 class="mt-5">Hej, wybierz menu!</h1>

    <animated-scroll>
    <div class="d-flex justify-content-center">
      <div v-for="(item, index) in formsList" @click="loadFormData(item.id)" :key="index"
           class="btn btn-dark m-5" style="font-size: 30px">{{item.name}}</div>
    </div>
      <div>
        <div class="m-5 mx-auto w-50">
          {{formDescription}}
        </div>
        <div v-for="(item, index) in positionsList.items" :key="index">
          <div class="m-2 mt-5 row mx-auto border-white positionItem">
            <div class="col-md-5">
              <div class="textThird mt-4 fs-3 fw-bold">
                {{item.name}}
              </div>
              <div class="mt-2" style="font-size: 16px">
                {{item.description}}
              </div>
              <div>
                <button class="mt-4 btn btn-primary w-50" @click="goto('div1')">Dodaj</button>
              </div>
            </div>
            <div class="col-md-3 mt-5">
              <div class="colorThird fs-4 w-75 h-25 mt-3 rounded-circle">
                {{item.price}} zł
              </div>
              <div class="mt-2" style="font-size: 16px">
                porcja: {{item.portionSize}}
              </div>
            </div>
            <div class="col-md-4">
              <div class="imga text-center mt-1" :style="{
                'background-image': `url(${require('/public/kluska.jpg')})`,
                width: '40vh',
                height: '30vh'
              }"/>
            </div>
          </div>
        </div>
      </div>
    </animated-scroll>
  </div>


</template>

<script lang="ts">
import { defineComponent, ref, reactive} from "vue";
import {FormsClient, IFormDetailListAm, IFormPosition} from "@/core/api/pierogiesApi";
import AnimatedScroll from "@/components/AnimatedScroll.vue";


export default defineComponent({
  name: "HomeForms",
  components: {
    AnimatedScroll
  },setup: () => {
    const formsList = ref(Array<any>([]));
    const positionsList = reactive({ items: [] as Array<IFormPosition> });
    const client = new FormsClient(process.env.VUE_APP_API_BASE_PATH);
    const formDescription = ref("");
    
    client.getForms().then((response) => {
      formsList.value = response.items as Array<IFormDetailListAm>
    })
    
    const loadFormData = (id : string) => {
      client.get(id).then(response => {
        positionsList.items = response.positions as Array<IFormPosition>
        formDescription.value = response.description!;
      })
      
    };
    

    return{
      formsList,
      positionsList,
      loadFormData,
      formDescription
    }

  }
});
</script>