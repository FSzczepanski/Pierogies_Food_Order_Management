<template>
  <div class="imga text-center " :style="{
        'background-image': `url(${require('/public/background.png')})`,
        width: '300vh',
        height: '100vh',
        overflowX: 'auto'
      }">
    <div class="p-5 h4 me-5" id="nav">
      <router-link class="m-1" to="/">Zamów</router-link>
      |
      <a class="m-2" href="https://staryfolwark.com.pl/nf/">Folwark</a> |
      <router-link class="m-1" to="/about">O nas</router-link>
    </div>
    <animated-scroll>
      <div class="float-end logoContainer w-50 mt-3 me-5">
        <div class="icon text-center " :style="{
        'background-image': `url(${require('/public/icon.png')})`,
        width: '90vh',
        height: '30vh',
        overflowX: 'auto'
      }"/>
        <div class="mt-3">
          {{ systemSettings.description }}
        </div>
        <button class="mt-3 ms-5 center btn btn-primary mb-4" @click="goto('div1')">Zamów online</button>

      </div>
    </animated-scroll>
  </div>
  <div ref="div1">
    <HomeForms/>
  </div>

</template>

<script lang="ts">
import {defineComponent, ref} from "vue";
import {ISystemSettings, SystemSettingsClient} from "@/core/api/pierogiesApi";
import HomeForms from "@/views/Home/HomeForms.vue";
import AnimatedScroll from "@/components/AnimatedScroll.vue";

export default defineComponent({
  name: "Home",
  components: {
    HomeForms,
    AnimatedScroll
  }, setup: () => {
    const systemSettings = ref<ISystemSettings>({
      id: "",
      created: new Date,
      identityNumber: 0,
      name: "",
      description: "",
      nip: "",
      phoneNumber: "",
      maxKmFromLocation: 0,
      globalDeliveryPrice: 0
    });
    const client = new SystemSettingsClient(process.env.VUE_APP_API_BASE_PATH);

    client.get().then((response) => {
      systemSettings.value = response
    })


    const goto = (refName: any) => {
      window.scrollTo(0, 750);
    }

    return {
      goto,
      systemSettings
    }

  }
});
</script>
