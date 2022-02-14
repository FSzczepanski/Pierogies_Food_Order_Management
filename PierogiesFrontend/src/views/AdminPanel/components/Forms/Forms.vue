<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="panelPath"/>
    <router-link class="text-decoration-none" to="/board/createForm">
      <button class="btn btn-primary me-5 float-end" @click="createForm">
        Dodaj nowy formularz
      </button>
    </router-link>
    <div class="mt-5 tableShape bg-light p-4">
      <div class="row fw-bold">
        <div class="col-md-2">#</div>
        <div class="col">Nazwa</div>
        <div class="col">Typ</div>
        <div class="col">Miejsce na liście</div>
        <div class="col-md-2">Aktywny</div>
        <div class="col">Akcje</div>
      </div>
      <div class="tableRow" v-for="(item, index) in formsList.items" :key="index">
        <hr />
        <div class="row m-auto">
          <div class="col-md-2">
            {{ item.identityNumber }}
          </div>
          <div class="col">
            {{ item.name }}
          </div>
          <div class="col">
            {{ item.formType }}
          </div>
          <div class="col">
            {{ item.placeOnList }}
          </div>
          <div class="col-md-2">
            <el-checkbox v-model="item.isActive" :value="item.isActive" />
          </div>
          <div class="col-md-2">
            <button class="btn btn-primary ms-4 bi bi-pencil-square" @click="goToUpdateFormView(item.id)"/> 
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
import {
  FormsClient,
  IFormDetailListAm,
} from "@/core/api/pierogiesApi";
import PanelPath from "@/components/PanelPath.vue";
import {useRouter} from "vue-router";

export default defineComponent({
  name: "Forms",
  components: {PanelPath},
  props: {},
  setup: function (props, { emit }) {
    const router = useRouter();
    const panelPath = ref<Array<any>>([{label: "Formularze",path: "/board/forms"}])
    
    const formsList = reactive({ items: [] as Array<IFormDetailListAm> });
    const client = new FormsClient(process.env.VUE_APP_API_BASE_PATH);
    client.getForms().then((response) => {
      formsList.items = response.items as Array<IFormDetailListAm>;
    }).catch(err => {
      console.log(err)
    });

    const createForm = () => {
      console.log("createForm");
    };
    
    const goToUpdateFormView = (id: string) => {
      router.push({
        name: "UpdateForm", params: {formId: id}
      });
    }

    return {
      formsList,
      createForm,
      panelPath,
      goToUpdateFormView,
    };
  },
});
</script>
