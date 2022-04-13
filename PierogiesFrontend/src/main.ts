import { createApp } from "vue";
import App from "./App.vue";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import router from "./router";
import ElementPlus from "element-plus";
import plPl from "element-plus/es/locale/lang/pl";
import * as yup from "yup";
import yupLocalePL from "yup-locale-pl";
import Vue3SimpleHtml2pdf from "vue3-simple-html2pdf";
import ApiService from "@/core/api/ApiService";

const app = createApp(App);

app.use(router).mount("#app");
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-ignore
app.use(Vue3SimpleHtml2pdf);

yup.setLocale(yupLocalePL);
ApiService.init();

app.use(ElementPlus, {
  locale: plPl,
});
