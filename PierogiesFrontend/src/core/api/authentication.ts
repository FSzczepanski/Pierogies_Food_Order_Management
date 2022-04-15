export const getToken = (): string | null => {
  return localStorage.getItem("auth");
};

export const getUser = (): string | null => {
  return localStorage.getItem("user");
};

export const saveToken = (token: string, user: string) => {
  localStorage.setItem("auth", token);
  localStorage.setItem("user", user);
};

export const removeToken = () => {
  localStorage.removeItem("auth");
  localStorage.removeItem("user");
};