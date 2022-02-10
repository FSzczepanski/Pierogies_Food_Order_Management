<template>
  <div>
    <h1 class="mt-5">Zaloguj się do panelu</h1>
  </div>
  <div class="text-danger rounded-3 m-2 mt-5 col-md-3 m-auto">
    {{ messageInfo }}
  </div>
  <div>
    <div>
      <div
        class="text-center bg-light col-md-3 myInput cartPositions mt-5 p-3 rounded-3"
      >
        <form>
          <div>
            <el-input class="p-4 pb-3" v-model="login" placeholder="Login" />
            <el-input class="p-4 pt-2" type="password" v-model="password" placeholder="Hasło" />
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
import {useRoute, useRouter} from "vue-router";

export default defineComponent({
  name: "AdminPanel",
  components: {},
  setup: () => {
    const router = useRouter();
    const route = useRoute();
    const login = ref("");
    const password = ref("");

    const messageInfo = ref("");
    messageInfo.value = route.params.messageInfo as string;


    const handleSubmit = () => {
      const client = new UserClient(process.env.VUE_APP_API_BASE_PATH);
      const request: IAuthenticateRequest = {
        username: login.value,
        password: password.value,
      };
      client
        .authenticate(request as AuthenticateRequest)
        .then((response) => {
          if (response.token != undefined && response.username != null) {
            localStorage.setItem("auth", response.token.toString());
            localStorage.setItem("user", response.username.toString());
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
      messageInfo,
    };
  },
});
</script>
