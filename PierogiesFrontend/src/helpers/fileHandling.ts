export const save = (data: Blob, type: string, fileName: string) => {
  const blob = new Blob([data], { type: type });
  const link = document.createElement("a");
  link.href = window.URL.createObjectURL(blob);
  link.download = fileName;
  link.click();
};

export const openInNewTab = (data: Blob, type: string) => {
  const blob = new Blob([data], { type: type });
  const _url = window.URL.createObjectURL(blob);
  window.open(_url, "_blank")?.focus();
};
