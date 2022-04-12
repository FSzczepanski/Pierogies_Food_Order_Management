<template>
  <div
    class="imga text-center disable-select background-image-home homePage"
  >
    <div class="mt-4 me-3 p-1 h5 mb-5" id="nav">
      <router-link class="m-1" to="/">Zamów</router-link>
      |
      <a class="m-2 myNavItem" href="https://staryfolwark.com.pl/nf/"
        >Strona główna</a
      >
    </div>
    <animated-scroll>
      <div class="float-end logoContainer col-lg-6 mt-3 me-3 ms-3">
        <img class="homeFormImage w-75" src="icon.png" alt="zdjecie" />
        <div class="mt-3 fs-6">
          {{ systemSettings.description }}
        </div>
        <button
          class="mt-3 ms-5 center btn btn-primary mb-4"
          @click.prevent="goto('homeForms')"
        >
          Zamów online
        </button>
      </div>
    </animated-scroll>
  </div>
  <div id="homeForms">
    <HomeForms />
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { ISystemSettings, SystemSettingsClient } from "@/core/api/pierogiesApi";
import HomeForms from "@/views/Home/HomeForms.vue";
import AnimatedScroll from "@/components/AnimatedScroll.vue";

export default defineComponent({
  name: "Home",
  components: {
    HomeForms,
    AnimatedScroll,
  },
  setup: () => {
    const systemSettings = ref<ISystemSettings>({
      id: "",
      created: new Date(),
      identityNumber: 0,
      name: "",
      description: "",
      nip: "",
      phoneNumber: "",
      maxKmFromLocation: 0,
      globalDeliveryPrice: 0,
    });

    const client = new SystemSettingsClient(process.env.VUE_APP_API_BASE_PATH);
    const getSystemSettings = () => {
      client.get().then((response) => {
        systemSettings.value = response;
      });
    };

    const goto = (navEl: any) => {
      const element = document.querySelector(`#${navEl}`);
      element?.scrollIntoView({ behavior: "smooth" });
    };

    onMounted(() => {
      getSystemSettings();
    });

    return {
      goto,
      systemSettings,
    };
  },
});
</script>
