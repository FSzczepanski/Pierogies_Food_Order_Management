<template>
  <VueRecaptcha
    :sitekey="siteKey"
    :load-recaptcha-script="true"
    @verify="handleSuccess"
    @error="handleError"
  ></VueRecaptcha>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import { VueRecaptcha } from "vue-recaptcha";

export default defineComponent({
  name: "ReCaptcha",
  components: {
    VueRecaptcha,
  },
  emits: ["success"],
  setup(props, {emit}) {
    const siteKey = computed(() => {
      return process.env.VUE_APP_GOOGLE_CAPTCHA_API_CODE_PUB;
    });

    const handleError = () => {
      // Do some validation
    };

    const handleSuccess = (response: string) => {
      if (response)
      emit("success");
    };

    return {
      handleSuccess,
      handleError,
      siteKey,
    };
  },
});
</script>
