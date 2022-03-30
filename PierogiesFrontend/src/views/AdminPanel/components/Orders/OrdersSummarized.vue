<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="panelPath" />
    <vue3-simple-html2pdf
      ref="vue3SimpleHtml2pdf"
      :options="pdfOptions"
      :filename="'Formularz_' + summarizedOrders.item.formName"
    >
      <div class="bg-white mt-5" style="font-size: 12px">
        <div class="h2 mt-3">Podsumowanie formularza</div>
        <div class="h4">{{ summarizedOrders.item.formName }}</div>
        <div class="mt-5 tableShape bg-white p-4 text-center">
          <div class="row fw-bold">
            <div class="col">#</div>
            <div class="col-md-5">Nazwa</div>
            <div class="col">Cena</div>
            <div class="col-md-3">Wielkość porcji</div>
            <div class="col">Ilość</div>
          </div>
          <div
            class="tableRow"
            v-for="(item, index) in summarizedOrders.item.positions"
            :key="index"
          >
            <hr />
            <div class="row m-auto text-center" style="font-size: 12px">
              <div class="col">
                {{ index + 1 }}
              </div>
              <div class="col-md-5">
                {{ item.name }}
              </div>
              <div class="col">{{ item.price }} zł</div>
              <div class="col-md-3">
                {{ item.portionSize }}
              </div>
              <div class="col">
                {{ item.amount }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </vue3-simple-html2pdf>
    <div class="bg-light pt-3 mb-5">
      <button
          class="m-auto mb-3 btn btn-primary col-lg-10"
          @click="generateRaport"
      >
        Wygeneruj raport 
        <i class="bi bi-card-checklist h4"></i>
      </button>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, onMounted, reactive, ref } from "vue";
import { ISummarizedOrdersAm, OrderClient } from "@/core/api/pierogiesApi";
import PanelPath from "@/components/PanelPath.vue";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "SummarizedOrders",
  components: { PanelPath },
  props: {},
  setup: function () {
    const route = useRoute();
    const formId = route.params.formId;
    const panelPath = ref<Array<any>>([
      { label: "Zamówienia", path: "/board/orders" },
      { label: "Podsumowanie", path: "" },
    ]);

    const summarizedOrders = reactive({
      item: {} as ISummarizedOrdersAm,
    });
    const client = new OrderClient(process.env.VUE_APP_API_BASE_PATH);

    const getSummarizedOrders = () => {
      client
        .getSummarizedOrders(formId as string)
        .then((response) => {
          summarizedOrders.item = response as ISummarizedOrdersAm;
        })
        .catch((err) => {
          console.log(err);
        });
    };

    const pdfOptions = {
      margin: 5,
      image: {
        type: 'jpeg',
        quality: 1,
      },
      html2canvas: { scale: 2 },
      jsPDF: {
        unit: 'mm',
        format: 'a4',
        orientation: 'p',
      },
    };

    const vue3SimpleHtml2pdf = ref<any>();
    const generateRaport = () => {
      vue3SimpleHtml2pdf.value.download();
    };

    onMounted(() => {
      getSummarizedOrders();
    });

    return {
      summarizedOrders,
      panelPath,
      pdfOptions,
      generateRaport,
      vue3SimpleHtml2pdf,
    };
  },
});
</script>
