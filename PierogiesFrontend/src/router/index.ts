import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/about",
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
