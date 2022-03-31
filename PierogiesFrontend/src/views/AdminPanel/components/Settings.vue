<template>
  <div class="ms-5 me-5 mt-4 mb-5">
    <PanelPath :paths="panelPath" />
    <div class="mt-5 tableShape bg-light mb-5">
      <h4 class="mt-4">Ustawienia</h4>
      <div class="p-4 row">
        <div class="tableShape col-lg-7 m-3 ms-5">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="form-label required"> Nazwa restauracji </label>
              <el-input
                v-model="systemSettings.name"
                placeholder="Wprowadź nazwę restauracji"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Opis </label>
              <el-input
                v-model="systemSettings.description"
                :rows="4"
                type="textarea"
                placeholder="Wprowadź opis wyświetlany na stronie głównej"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Globalna cena dostawy </label>
              <el-input
                v-model="systemSettings.globalDeliveryPrice"
                type="number"
                placeholder="Wprowadź cenę dostawy"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required">
                Maksymalna liczba kilometrów od lokalizacji
              </label>
              <el-input
                v-model="systemSettings.maxKmFromLocation"
                type="number"
                placeholder="Określ maksymalną liczbe kilometrów pozwalającą na złożenie zamówienia"
              />
            </div>
          </div>
        </div>
        <div class="tableShape col-lg-4 m-3 mb-5">
          <div class="col-lg-8 m-auto">
            <div class="mt-4">
              <label class="form-label required"> NIP </label>
              <el-input
                v-model="systemSettings.nip"
                placeholder="Wprowadź nip do faktury"
              />
            </div>
            <div class="mt-3">
              <label class="form-label required"> Ulica i numer </label>
              <el-input
                v-model="location.street"
                placeholder="Wprowadź ulicę i numer"
              />
            </div>
            <div>
              <label class="form-label required"> Kod pocztowy </label>
              <el-input
                v-model="location.zipCode"
                placeholder="Wprowadź kod pocztowy"
              />
            </div>
            <div>
              <label class="form-label required"> Miejscowość </label>
              <el-input
                v-model="location.cityName"
                placeholder="Wprowadź nazwe miejscowości"
              />
            </div>
            <div class="mt-4">
              <label class="form-label required"> Numer telefonu </label>
              <el-input
                  v-model="systemSettings.phoneNumber"
                  type="number"
                  placeholder="Wprowadź numer kontaktowy"
              />
            </div>
          </div>
        </div>
        <button class="btn btn-primary col-lg-11 m-auto" @click="editSettings">
          Zapisz ustawienia
        </button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";
import PanelPath from "@/components/PanelPath.vue";
import {
  Location,
  ILocation,
  ISaveSystemSettingsCommand,
  SystemSettingsClient, SaveSystemSettingsCommand,
} from "@/core/api/pierogiesApi";

export default defineComponent({
  name: "Settings",
  components: { PanelPath },
  props: {},
  setup: function (props, { emit }) {
    const panelPath = ref<Array<any>>([
      { label: "Ustawienia", path: "/board/settings" },
    ]);

    const location = ref<ILocation>({
      name: "Global",
      description: "",
      street: "",
      cityName: "",
      countryName: "",
      isDefault: true,
      zipCode: "",
    });
    
    const systemSettings = ref<ISaveSystemSettingsCommand>({
      name: "",
      description: "",
      nip: "",
      phoneNumber: "",
      location: undefined,
      maxKmFromLocation: 0,
      globalDeliveryPrice: 0,
    });
    const client = new SystemSettingsClient(process.env.VUE_APP_API_BASE_PATH);

    client.get().then((response) => {
      systemSettings.value.name = response.name;
      systemSettings.value.description = response.description;
      systemSettings.value.nip = response.nip;
      systemSettings.value.phoneNumber = response.phoneNumber;
      systemSettings.value.location = response.location as Location;
      location.value.name = response.location?.name;
      location.value.description = response.location?.description;
      location.value.street = response.location?.street;
      location.value.zipCode = response.location?.zipCode;
      location.value.cityName = response.location?.cityName;
      location.value.countryName = response.location?.countryName;
      systemSettings.value.maxKmFromLocation = response.maxKmFromLocation;
      systemSettings.value.globalDeliveryPrice = response.globalDeliveryPrice;
    });

    

    const editSettings = () => {
      client.update(systemSettings.value as SaveSystemSettingsCommand)
      .then(() => {
        console.log("udało się!")
      }).catch(err => {
        console.log(err)
      })
    };

    return {
      panelPath,
      systemSettings,
      editSettings,
      location,
    };
  },
});
</script>
