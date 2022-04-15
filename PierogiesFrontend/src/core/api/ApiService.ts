import axios, { AxiosInstance } from "axios";
import { getToken, removeToken } from "@/core/api/authentication";
import router from "@/router";

class ApiService {
  public static instance: AxiosInstance;

  public static init() {
    this.instance = axios.create();

    this.instance.interceptors.response.use(
      (response) => {
        return response;
      },
      (error) => {
        if (error.response.status === 401) {
          if (getToken() != null) {
            removeToken();
          }
          router.push({ name: "Login" });
        }

        return Promise.reject(error);
      }
    );

    this.instance.interceptors.request.use(
      (config) => {
        const accessToken = getToken();
        if (accessToken) {
          config.headers = Object.assign(
            {
              Authorization: `Token ${accessToken}`,
            },
            config.headers
          );
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }
}

export default ApiService;
