<template>
  <div>
    <nav class="bg-light navbar">
      <div class="container-fluid">
        <div class="navbar-brand textThird">
          <h3 class="ms-4 fw-bolder" style="font-family: Cursive,serif">Pierogi system</h3>
        </div>
        <div>
          <router-link class="text-decoration-none" to="/board/settings">
            <button class="btn btn-primary me-3">
              <i class="bi bi-gear-fill"></i>
            </button>
          </router-link>
          <button class="btn btn-primary" @click="logout">
            <i class="bi bi-box-arrow-right"></i>
          </button>
        </div>
      </div>
    </nav>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";

export default defineComponent({
  name: "Navbar",
  setup: () => {
    const router = useRouter();

    const logout = () => {
      localStorage.setItem("auth", "");
      localStorage.setItem("user", "");

      router.push({
        name: "Login",
        params: { messageInfo: "Nastąpiło wylogowanie, zaloguj się ponownie" },
      });
    };

    return { logout };
  },
});
</script>
