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
import $api from "@/api/index";
import { ref, reactive, defineComponent } from "vue";
let list = reactive({
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
});
const columns = [
    { colKey: "id", title: "主键" },
    { colKey: "application", title: "应用", align: "center" }, // width: "100"
    { colKey: "level", title: "日志级别" },
    { colKey: "logged", title: "日期" },
    { colKey: "message", title: "具体信息",ellipsis: true, },
    { colKey: "exception", title: "错误",ellipsis: true, },
    { colKey: "callSite", title: "应用位置", align: "center",ellipsis: true, },
    { colKey: "logger", title: "问题位置",ellipsis: true, },
];

const getTableList = () => {
    $instance.get($api.log.GetLogList).then((resp) => {
        list.data = resp.result.data;
        list.pagination = resp.result.pagination;
    });
};
getTableList();
</script>
