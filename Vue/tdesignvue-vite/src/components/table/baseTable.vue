<template>
    <t-table row-key="index" :data="list.data" :columns="columns" bordered hover table-layout="fixed" size="small"
        :pagination="list.pagination" cell-empty-content="-" resizable @row-click="handleRowClick" :loading="loading"
      >
    </t-table>
</template>
<script setup >
import $instance from "@/utils/http";
import { ref } from "vue";
let list = ref({
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
})
var prop = defineProps({
    columns: {type:Array,default:()=>[]},
    listUrl: String,
    Param:{}
})
const loading = ref(false)
const emit = defineEmits(["rowClick"]);
const getTableList = () => {
    loading.value = !loading.value
    $instance.get(prop.listUrl,{params:prop.Param}).then((resp) => {
        loading.value = !loading.value
        list.value.data = resp.result.data;
        list.value.pagination = resp.result.pagination;
    });
};
const handleRowClick = (e) => {
    emit("rowClick", e);
};
//暴露属性
defineExpose({
    getTableList,
});
</script>
