<template>
  <div>
    <h1 class="mt-5">Zaloguj się do panelu</h1>
  </div>
  <div>
    <div>
      <div
        class="text-center bg-light col-md-3 myInput cartPositions mt-5 p-3 rounded-3"
      >
        <form>
          <div>
            <el-input class="p-4 pb-3" v-model="login" placeholder="Login" />
            <el-input
              class="p-4 pt-2"
              type="password"
              v-model="password"
              placeholder="Hasło"
            />
            <button class="btn btn-info" @click="handleSubmit">Zaloguj</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import {
  AuthenticateRequest,
  IAuthenticateRequest,
  UserClient,
} from "@/core/api/pierogiesApi";
import { useRoute, useRouter } from "vue-router";
import { saveToken } from "@/core/api/authentication";
import apiService from "@/core/api/ApiService";

export default defineComponent({
  name: "Login",
  components: {},
  setup: () => {
    const router = useRouter();
    const login = ref("");
    const password = ref("");

    const handleSubmit = () => {
      const client = new UserClient(
        process.env.VUE_APP_API_BASE_PATH,
        apiService.instance
      );
      const request: IAuthenticateRequest = {
        username: login.value,
        password: password.value,
      };
      client
        .authenticate(request as AuthenticateRequest)
        .then((response) => {
          if (response.token != undefined && response.username != null) {
            saveToken(response.token.toString(), response.username.toString());
            router.push({ name: "Dashboard" });
          }
        })
        .catch((err) => {
          console.log(err);
        });
    };

    return {
      login,
      password,
      handleSubmit,
    };
  },
});
</script>
