import { ElMessageBox } from "element-plus";
import {App} from "vue";

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