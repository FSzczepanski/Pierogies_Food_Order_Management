import { ElMessageBox } from "element-plus";

export function confirmDelete(success: any): void {
  ElMessageBox.confirm("Wybrana pozycja zostanie usunięta", {
    confirmButtonText: "Potwierdź",
    cancelButtonText: "Anuluj",
    type: "warning",
  }).then(() => {
    success();
  });
}
