<template>
    <t-table
        row-key="index"
        :data="list.data"
        :columns="columns"
        bordered
        hover
        table-layout="fixed"
        size="small"
        :pagination="list.pagination"
        cell-empty-content="-"
        resizable
        @row-click="handleRowClick"
    >
    </t-table>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import {  reactive } from "vue";
var prop=defineProps({
    columns: [],
    listUrl:String,
    list:{
        pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
        data: [],
    }
})
const emit = defineEmits(["rowClick"]);
const getTableList = () => {
    console.log(prop.listUrl)
    $instance.get(prop.listUrl).then((resp) => {
        prop.list.data = resp.result.data;
        prop.list.pagination = resp.result.pagination;
    });
};
getTableList();
const handleRowClick = (e) => {
    emit("rowClick", e);
};
</script>
