import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home/Home.vue";
import ConfirmOrder from "@/views/Home/ConfirmOrder.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/Zamowienie",
    name: "ConfirmOrder",
    component: ConfirmOrder,
    props: { orderPositions: true, form: true },
  },
  {
    path: "/oNas",
    name: "About",
    component: () =>
      import("../views/About.vue"),
  },
  {
    path: "/panel/login",
    name: "Login",
    component: () =>
        import("../views/AdminPanel/Login.vue"),
  },
  {
    path: "/panel",
    name: "Panel",
    component: () =>
        import("../views/AdminPanel/AdminPanel.vue"),
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

export default router;
