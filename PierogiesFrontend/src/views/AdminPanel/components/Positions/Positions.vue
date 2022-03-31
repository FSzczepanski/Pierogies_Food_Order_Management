<template>
  <position-modal
    @ok="refreshList"
    ref="PositionModalRef"
  />
  <div class="ms-5 me-5 mt-4">
    <PanelPath :paths="panelPath" />
    <button class="btn btn-primary me-5 float-end" @click="createPosition">
      Dodaj nową pozycje
    </button>
    <div class="mt-5 tableShape bg-light p-4">
      <div class="row fw-bold">
        <div class="col-md-2">#</div>
        <div class="col">Nazwa</div>
        <div class="col">Kategoria</div>
        <div class="col">Cena</div>
        <div class="col">Wielkość porcji</div>
        <div class="col-md-2">Akcje</div>
      </div>
      <div
        class="tableRow"
        v-for="(item, index) in positionsList.items"
        :key="index"
      >
        <hr />
        <div class="row m-auto">
          <div class="col-md-2">
            {{ item.identityNumber }}
          </div>
          <div class="col">
            {{ item.name }}
          </div>
          <div class="col">
            {{ PositionCategoryEnumTranslation[item.positionCategory] }}
          </div>
          <div class="col">{{ item.price }} zł</div>
          <div class="col">
            {{ item.portionSize }}
          </div>
          <div class="col-md-2">
            <button class="btn btn-primary ms-4 bi bi-pencil-square"
            @click="updatePosition(item.id)"></button>
            <button
              class="btn btn-danger rounded-circle ms-2 bi bi-trash"
              @click="openConfirmDeleteModal(item.id)"
            ></button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
import { IPositionAm, PositionsClient } from "@/core/api/pierogiesApi";
import { confirmDelete } from "@/helpers/confirmationsAdapter";
import PanelPath from "@/components/PanelPath.vue";
import { PositionCategoryEnumTranslation } from "@/helpers/enums";
import PositionModal from "@/views/AdminPanel/components/Positions/PositionModal.vue";

export default defineComponent({
  name: "Forms",
  components: { PositionModal, PanelPath },
  props: {},
  setup: function () {
    const panelPath = ref<Array<any>>([
      { label: "Pozycje", path: "/board/positions" },
    ]);
    const positionsList = reactive({ items: [] as Array<IPositionAm> });
    const client = new PositionsClient(process.env.VUE_APP_API_BASE_PATH);

    const getPositions = () => {
      client
        .getPositions()
        .then((response) => {
          positionsList.items = response.items as Array<IPositionAm>;
        })
        .catch((err) => {
          console.log(err);
        });
    };

    getPositions();

    const PositionModalRef = ref<typeof PositionModal>();

    const createPosition = () => {
      PositionModalRef.value?.openCreate();
    };

    const updatePosition = (id: string) => {
      PositionModalRef.value?.openUpdate(id);
    };

    let idToDelete = "";
    const deletePosition = () => {
      if (idToDelete != "")
        client
          .delete(idToDelete)
          .then(() => {
            getPositions();
          })
          .catch((err) => {
            console.log(err);
          });
    };

    const openConfirmDeleteModal = (id: string) => {
      idToDelete = id;
      confirmDelete(deletePosition);
    };

    const refreshList = () => {
      getPositions();
    };

    return {
      positionsList,
      createPosition,
      openConfirmDeleteModal,
      panelPath,
      refreshList,
      PositionCategoryEnumTranslation,
      PositionModalRef,
      updatePosition,
    };
  },
});
</script>
