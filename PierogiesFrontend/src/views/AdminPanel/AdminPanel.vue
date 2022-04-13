<template>
  <div class="wrapper">
    <!-- Sidebar -->
    <nav class="colorSecond sidebarStyle">
      <div class="sidebar-header mt-4">
        <h3>Folwark</h3>
      </div>
      <p class="mt-3 text-info">{{ userName }}</p>
      <div class="list-group list-group-flush mt-4">
        <router-link class="text-decoration-none" to="/board/dashboard">
          <a
            class="bi bi-house-door list-group-item list-group-item-action p-3"
          >
            Panel</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/orders">
          <a
            class="bi bi-person-lines-fill list-group-item list-group-item-action p-3"
          >
            Zamówienia</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/forms">
          <a
            class="bi bi-list-check list-group-item list-group-item-action p-3"
          >
            Formularze</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/positions">
          <a
            class="bi bi-journal-richtext list-group-item list-group-item-action p-3"
          >
            Pozycje menu</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/analytics">
          <a
            class="bi bi-clipboard-data list-group-item list-group-item-action p-3"
          >
            Raporty</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/invoices">
          <a class="bi bi-receipt list-group-item list-group-item-action p-3">
            Faktury</a
          >
        </router-link>
        <router-link class="text-decoration-none" to="/board/settings">
          <a class="bi bi-gear list-group-item list-group-item-action p-3">
            Ustawienia</a
          >
        </router-link>
      </div>
    </nav>

    <!-- Page Content -->
    <div class="w-100 adminContent textSecond">
      <div>
        <Navbar />
      </div>
      <div>
        <router-view />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Navbar from "@/views/AdminPanel/Navbar.vue";

export default defineComponent({
  name: "AdminPanel",
  components: { Navbar },
  setup: () => {
    return {};
  },
});
</script>
