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
    path: "/Zamowienie/Potwierdzono",
    name: "OrderConfirmed",
    component: () => import("../views/Home/OrderConfirmedView.vue"),
  },
  {
    path: "/oNas",
    name: "About",
    component: () => import("../views/About.vue"),
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/AdminPanel/Login.vue"),
    props: { messageInfo: true },
  },
  {
    path: "/board",
    name: "Board",
    component: () => import("../views/AdminPanel/AdminPanel.vue"),
    children: [
      {
        path: "dashboard",
        name: "Dashboard",
        component: () => import("../views/AdminPanel/components/Dashboard.vue"),
      },
      {
        path: "forms",
        name: "Forms",
        component: () =>
          import("../views/AdminPanel/components/Forms/Forms.vue"),
      },
      {
        path: "createForm",
        name: "CreateForm",
        component: () =>
          import("../views/AdminPanel/components/Forms/CreateForm.vue"),
      },
      {
        path: "updateForm/:formId",
        name: "UpdateForm",
        component: () =>
          import("../views/AdminPanel/components/Forms/EditForm.vue"),
        props: { formId: true },
      },

      {
        path: "editForm",
        name: "EditForm",
        component: () =>
          import("../views/AdminPanel/components/Forms/EditForm.vue"),
      },
      {
        path: "settings",
        name: "Settings",
        component: () => import("../views/AdminPanel/components/Settings.vue"),
      },
      {
        path: "analytics",
        name: "Analytics",
        component: () => import("../views/AdminPanel/components/Analytics.vue"),
      },
      {
        path: "invoices",
        name: "Invoices",
        component: () => import("../views/AdminPanel/components/Invoices.vue"),
      },
      {
        path: "orders",
        name: "Orders",
        component: () =>
          import("../views/AdminPanel/components/Orders/Orders.vue"),
      },
      {
        path: "orders/details/:orderId",
        name: "OrderDetails",
        component: () =>
          import("../views/AdminPanel/components/Orders/OrderDetails.vue"),
        props: { orderId: true },
      },
      {
        path: "orders/summary/:formId",
        name: "OrderSummary",
        component: () =>
            import("../views/AdminPanel/components/Orders/OrdersSummarized.vue"),
        props: { formId: true },
      },
      {
        path: "positions",
        name: "Positions",
        component: () =>
          import("../views/AdminPanel/components/Positions/Positions.vue"),
      },
    ],
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

export default router;
