﻿<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="panelPath" />
    <div class="text-start mt-5 ms-2">
      <label class="form-label required"> Formularz </label>
      <div>
        <el-select v-model="searchModel" class="col-lg-2">
          <el-option
            v-for="(item, index) in formsList.items"
            :value="item.id"
            :label="item.name"
            :key="index"
          >
            {{ item.name }}
          </el-option>
        </el-select>
      </div>
      <button class="btn btn-primary me-5 float-end" @click="generateSummarizedOrders">
        Generuj podsumowanie formularza
      </button>
    </div>
    <div class="mt-5 tableShape bg-light p-4 text-center">
      <div class="row fw-bold">
        <div class="col-md-1">#</div>
        <div class="col">Formularz</div>
        <div class="col">Klient</div>
        <div class="col">Adres odbioru</div>
        <div class="col">Data odbioru</div>
        <div class="col-md-1">Kwota</div>
        <div class="col-md-1">Płatność</div>
        <div class="col">Akcje</div>
      </div>
      <div
        class="tableRow"
        v-for="(item, index) in ordersSearched.items"
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
            {{ item.locationString }}
          </div>
          <div class="col">
            {{ new Date(item.date).toLocaleString() }}
          </div>
          <div class="col-md-1">{{ item.fullPrice }} zł</div>
          <div class="col-md-1">
            <el-checkbox v-model="item.isPaid" :value="item.isPaid" />
          </div>
          <div class="col">
            <button class="btn btn-primary ms-4 bi bi-pencil-square" @click="editOrder(item.id)"></button>
            <button
              class="btn btn-primary ms-4 bi-card-checklist"
              @click="showOrderDetails(item.id)"
            ></button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import {defineComponent, onMounted, reactive, ref, watch} from "vue";
import {
  FormsClient,
  IFormDetailListAm,
  IOrderDetailsListAm,
  OrderClient,
} from "@/core/api/pierogiesApi";
import PanelPath from "@/components/PanelPath.vue";
import { useRouter } from "vue-router";
import {showToast} from "@/helpers/confirmationsAdapter";
import apiService from "@/core/api/ApiService";

export default defineComponent({
  name: "Orders",
  components: { PanelPath },
  props: {},
  setup: function (props, { emit }) {
    const router = useRouter();

    const panelPath = ref<Array<any>>([
      { label: "Zamówienia", path: "/board/orders" },
    ]);

    const ordersList = reactive({ items: [] as Array<IOrderDetailsListAm> });
    const ordersSearched = reactive({
      items: [] as Array<IOrderDetailsListAm>,
    });
    const client = new OrderClient(process.env.VUE_APP_API_BASE_PATH, apiService.instance);
    const searchModel = ref<string>("");

    watch(
      () => searchModel.value,
      () => {
        if (searchModel.value) {
          ordersSearched.items = ordersList.items.filter(
            (o) => o.formId == searchModel.value
          );
        }
      }
    );

    const getOrders = () => {
      client
        .getOrders()
        .then((response) => {
          ordersList.items = response.items as Array<IOrderDetailsListAm>;
          ordersSearched.items = ordersList.items;
        })
        .catch((err) => {
          console.log(err);
        });
    };

    const formsList = reactive({ items: [] as Array<IFormDetailListAm> });
    const formsClient = new FormsClient(process.env.VUE_APP_API_BASE_PATH, apiService.instance);
    formsClient
      .getForms(false)
      .then((response) => {
        formsList.items = response.items as Array<IFormDetailListAm>;
      })
      .catch((err) => {
        console.log(err);
      });

    const showOrderDetails = (id: string) => {
      router.push({
        name: "OrderDetails",
        params: { orderId: id },
      });
    };

    const editOrder = (id: string) => {
      router.push({
        name: "OrderEdit",
        params: { orderId: id },
      });
    };
    
    const generateSummarizedOrders = () => {
      if (searchModel.value != ""){
        router.push({
          name: "OrderSummary",
          params: { formId: searchModel.value },
        });
      }else{
        showToast("Aby wygenerować podsumowanie wybierz formularz");
      };
      
    };

    onMounted(() => {
      getOrders();
    });

    return {
      panelPath,
      searchModel,
      ordersSearched,
      formsList,
      showOrderDetails,
      editOrder,
      generateSummarizedOrders,
    };
  },
});
</script>
