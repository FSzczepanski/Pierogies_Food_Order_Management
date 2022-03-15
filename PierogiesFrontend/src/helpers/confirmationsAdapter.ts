import { ElMessageBox } from "element-plus";

export function confirmDelete(success: any): void {
  ElMessageBox.confirm("Wybrana pozycja zostanie usunięta", {
    confirmButtonText: "Potwierdź",
    cancelButtonText: "Anuluj",
    type: "warning",
    customClass: "customMessageBoxClass",
  }).then(() => {
    success();
  });
}

export function showToast(message: string): void {
  ElMessageBox.alert(message, 'Uwaga!',{
    confirmButtonText: "Ok",
    type: "warning",
    customClass: "customMessageBoxClass",
  });
}
