<template>
  <div class="ms-5 me-5 mt-4 disable-select">
    <PanelPath :paths="panelPath" />
    <div class="tableShape bg-light mt-5">
      <div class="row textMain dashboardItem" @click="goToAnalytics">
        <div
          class="col analiticsPane m-5"
          style="background-color: #3caae5; opacity: 0.75"
        >
          <div class="row mt-2">
            <div class="col-lg-4 mt-4">
              <i class="bi bi-basket2 textSecond h1"></i>
            </div>
            <div class="col-lg-7">
              <h2>25</h2>
              <div>Zamówień dzisiaj</div>
            </div>
          </div>
        </div>
        <div
          class="col analiticsPane m-5"
          style="background-color: #30c8cd; opacity: 0.75"
        >
          <div class="row mt-2">
            <div class="col-lg-4 mt-4">
              <i class="bi bi-calendar-week textSecond h1"></i>
            </div>
            <div class="col-lg-7">
              <h2>102</h2>
              <div>Zamówień w tym miesiącu</div>
            </div>
          </div>
        </div>
        <div
          class="col analiticsPane m-5"
          style="background-color: #b04ddb; opacity: 0.75"
        >
          <div class="row mt-2">
            <div class="col-lg-3 mt-4">
              <i class="bi bi-card-list textSecond h1"></i>
            </div>
            <div class="col-lg-7">
              <h4>Menu restauracyjne</h4>
              <div>Najpopularniejszy formularz</div>
            </div>
          </div>
        </div>
        <img
          class="homeFormImage col-lg-3"
          src="graph.jpg"
          alt="zdjecie"
          style="max-height: 30vh; opacity: 0.2"
        />
      </div>
    </div>
    <div class="mt-2 tableShape bg-light  p-4 text-center pb-5">
      <h2 class="mt-2 mb-5">Ostatnie zamówienia</h2>
      <div class="tableShape p-2">
        <div class="row fw-bold">
          <div class="col-md-1">#</div>
          <div class="col">Formularz</div>
          <div class="col">Klient</div>
          <div class="col">Data</div>
          <div class="col-md-1">Kwota</div>
          <div class="col-md-1">Płatność</div>
        </div>
        <div
          class="tableRow"
          v-for="(item, index) in ordersList.items"
          :key="index"
        >
          <hr />
          <div class="row m-auto text-center">
            <div class="col-md-1">
              {{ item.identityNumber }}
            </div>
            <div class="col">
              {{ item.formName }}
            </div>
            <div class="col">
              {{ item.purchaserName }}
            </div>
            <div class="col">
              {{ new Date(item.date).toLocaleString() }}
            </div>
            <div class="col-md-1">{{ item.fullPrice }} zł</div>
            <div class="col-md-1">
              <el-checkbox v-model="item.isPaid" :value="item.isPaid" />
            </div>
          </div>
        </div>
      </div>
      <router-link to="/board/orders">
        <button
          class="btn btn-primary col-lg-4 me-4 float-end p-1"
          @click="createOrder"
        >
          Przejdź do zamówień
        </button>
      </router-link>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
import { IOrderDetailsListAm, OrderClient } from "@/core/api/pierogiesApi";
import PanelPath from "@/components/PanelPath.vue";
import {useRouter} from "vue-router";
import ApiService from "@/core/api/ApiService";

export default defineComponent({
  name: "Dashboard",
  components: { PanelPath },
  props: {},
  setup: function (props, { emit }) {
    const panelPath = ref<Array<any>>([
      { label: "Panel", path: "/board/dashboard" },
    ]);
    
    const router = useRouter();
    
    const ordersList = reactive({ items: [] as Array<IOrderDetailsListAm> });
    const client = new OrderClient(process.env.VUE_APP_API_BASE_PATH, ApiService.instance);

    client
      .getOrders()
      .then((response) => {
        ordersList.items = response.items as Array<IOrderDetailsListAm>;
      })
      .catch((err) => {
        console.log(err);
      });

    const goToAnalytics = () => {
      router.push({
        name: "Analytics"
      });
    };
    

    return {
      ordersList,
      panelPath,
      goToAnalytics,
    };
  },
});
</script>
