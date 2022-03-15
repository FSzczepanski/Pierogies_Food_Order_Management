import { createApp } from "vue";
import App from "./App.vue";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import router from "./router";
import ElementPlus from "element-plus";
import plPl from "element-plus/es/locale/lang/pl";
import * as yup from "yup";
import yupLocalePL from "yup-locale-pl";

const app = createApp(App);

app.use(router).mount("#app");

yup.setLocale(yupLocalePL);

app.use(ElementPlus, {
  locale: plPl,
});
