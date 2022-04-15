<template>
  <div></div>
  <router-view />
  <page-footer/>
</template>

<style>
@import "helpers/styles.css";
</style>
<script>
import PageFooter from "@/components/PageFooter";
export default {
  components: {PageFooter}
}
</script>