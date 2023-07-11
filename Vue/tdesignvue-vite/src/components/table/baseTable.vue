<template>
    <t-table row-key="index" :data="list.data" :columns="columns" bordered hover table-layout="fixed" size="small"
        :pagination="list.pagination" cell-empty-content="-" resizable @row-click="handleRowClick" :loading="loading"
        :refreshTable="refreshTable">
    </t-table>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import { ref } from "vue";
let list = {
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
}
var prop = defineProps({
    columns: [],
    listUrl: String,
})
const loading = ref(false)
const emit = defineEmits(["rowClick", "refreshTable"]);
const getTableList = () => {
    loading.value = !loading.value
    $instance.get(prop.listUrl).then((resp) => {
        loading.value = !loading.value
        list.data = resp.result.data;
        list.pagination = resp.result.pagination;
    });
};
getTableList();
const handleRowClick = (e) => {
    emit("rowClick", e);
};
</script>
