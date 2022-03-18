<template>
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="path" />
    <div class="mt-5 tableShape colorFourth row">
      <div class="tableShape m-3 col-lg-6">
        <div class="mt-4">
          <label class="form-label required fw-bold"> Imie i nazwisko </label>
          <div>{{ order.item.name }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Email </label>
          <div>{{ order.item.email }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Numer telefonu </label>
          <div>{{ order.item.phone }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Data odbioru </label>
          <div>{{ new Date(order.item.date).toLocaleString() }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Adres odbioru </label>
          <div>{{ order.item.locationString }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold">
            Całkowity koszt (Zamówienie + dostawa)
          </label>
          <div>{{ order.item.fullPrice }}</div>
        </div>
      </div>

      <div class="tableShape m-3 col-lg-4">
        <div class="mt-4">
          <label class="form-label required fw-bold">
            Data złożenia zamówienia
          </label>
          <div>{{ new Date(order.item.created).toLocaleString() }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Metoda płatności </label>
          <div>
            {{ PaymentMethodEnumTranslation[order.item.paymentMethod] }}
          </div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Czy opłacone? </label>
          <div>{{ order.item.isPaid ? "Tak" : "Nie" }}</div>
        </div>
        <div class="mt-4" v-if="order.item.isPaid">
          <label class="form-label required fw-bold"> Data płatności </label>
          <div>{{ new Date(order.item.paymentDate).toLocaleString() }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Faktura? </label>
          <div>{{ order.item.needInvoice ? "Tak" : "Nie" }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold"> Koszt dostawy </label>
          <div>{{ order.item.deliveryPrice }}</div>
        </div>
        <div class="mt-4">
          <label class="form-label required fw-bold">
            Dodatkowe informacje od klienta
          </label>
          <div>{{ order.item.description }}</div>
        </div>
      </div>

      <div class="tableShape m-3">
        <div class="col-lg-10 m-auto mt-4">
          <div class="h3 mb-5">Pozycje</div>
          <table class="table table-rounded table-hover border gy-2 gs-2">
            <thead>
              <tr>
                <th>#</th>
                <th>Pozycja</th>
                <th>Cena</th>
                <th>Liczba porcji</th>
                <th>Porcja</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in order.item.positions" :key="index">
                <td>
                  {{ index + 1 }}
                </td>
                <td>
                  {{ item.name }}
                </td>
                <td>
                  {{ item.price }}
                </td>
                <td>
                  {{ item.amount }}
                </td>
                <td>
                  {{ item.portionSize }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div>
          <div class="bg-light pt-3 mt-5">
            <button
              class="m-auto mb-3 btn btn-primary col-lg-10"
              @click="generateRaport"
            >
              Wygeneruj raport
              <i class="bi bi-card-checklist h4"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref } from "vue";
import { useRoute } from "vue-router";
import PanelPath from "@/components/PanelPath.vue";
import { IOrderAm, OrderClient } from "@/core/api/pierogiesApi";
import { showToast } from "@/helpers/confirmationsAdapter";
import { PaymentMethodEnumTranslation } from "@/helpers/enums";
import { save } from "@/helpers/fileHandling";

export default defineComponent({
  name: "OrderDetails",
  components: { PanelPath },
  setup: () => {
    const route = useRoute();
    const orderId = route.params.orderId;

    const path = ref<Array<any>>([
      { label: "Zamówienia", path: "/board/orders" },
      { label: "Generuj", path: "" },
    ]);

    const order = reactive({ item: {} as IOrderAm });

    const client = new OrderClient(process.env.VUE_APP_API_BASE_PATH);
    const getOrder = () => {
      client
        .get(orderId as string)
        .then((response) => {
          order.item = response as IOrderAm;
        })
        .catch((err) => {
          showToast("Coś poszło nie tak, " + err);
        });
    };

    const generateRaport = () => {
      save(
        order.item as unknown as Blob,
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "Zamówienie: " + order.item.name
      );
    };

    onMounted(() => {
      getOrder();
    });

    return {
      path,
      order,
      PaymentMethodEnumTranslation,
      generateRaport,
    };
  },
});
</script>
