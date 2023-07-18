<template>
    <baseTable ref="table" :columns="columns" :listUrl="listUrl"  />
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import baseTable from "@/components/table/baseTable.vue";
import $api from "@/api/index";
import { ref, reactive, defineComponent, onMounted } from "vue";
import moment from 'moment'
let table = ref(null);
const listUrl = $api.log.GetLogList;
const columns = [
    { colKey: "id", title: "主键" },
    { colKey: "application", title: "应用", align: "center" }, // width: "100"
    { colKey: "level", title: "日志级别" },
    { colKey: "logged", title: "日期", cell: (h, { row }) => {
        console.log(row)
        return  moment(row.logged).format("yyyy/MM/DD HH:mm:ss")
    } },
    { colKey: "message", title: "具体信息", ellipsis: true },
    { colKey: "exception", title: "错误", ellipsis: true },
    { colKey: "callSite", title: "应用位置", align: "center", ellipsis: true },
    { colKey: "logger", title: "问题位置", ellipsis: true },
];

onMounted(() => {
    loadTable();
});
const loadTable = () => {
    table.value.getTableList();
};
</script>
