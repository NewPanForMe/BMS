<template>
    <div class="content-top">
        </div>
    <div class="content">
        <baseTable   ref="table" :columns="columns" :listUrl="listUrl" @row-click="handleRowClick" @refreshTable="refresh"  />
    </div>
</template>
<script setup lang="jsx">
import $api from "@/api/index";
import { reactive, ref,onMounted } from "vue";
import baseTable from "@/components/table/baseTable.vue";
import moment from 'moment'
const listUrl = $api.sms.GetSmsList;
const columns = [
    { colKey: "phone", title: "手机号", align: "center" },
    { colKey: "type", title: "类型", align: "center" },
    { colKey: "verifyCode", title: "验证码", align: "center" },
    { colKey: "sendTime", title: "发送时间", align: "center" , cell: (h, { row }) => {
        return  moment(row.sendTime).format("yyyy/MM/DD HH:mm:ss")
    } },
    { colKey: "smsResult", title: "反馈结果", align: "center", ellipsis: true },

];
let table = ref(null)
onMounted(() => {
  loadTable()
})
const loadTable=()=>{
    table.value.getTableList()
}
const handleRowClick = (e) => {
    console.log(e);
};
</script>
