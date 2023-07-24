<template>
    <baseTable ref="table" :columns="columns" :listUrl="listUrl" @row-click="handleRowClick" />
    <t-drawer v-model:visible="visible" :on-overlay-click="() => (visible = false)" placement="bottom" size="400px" :footer="footer" @cancel="visible = false">
        <t-row :gutter="16">
            <t-col :span="3">
                <t-card header bordered hover-shadow>
                    <t-form :data="formData">
                        <t-form-item label="上传人">
                            <t-input v-model="formData.userName" :readonly="true" />
                        </t-form-item>
                        <t-form-item label="上传年份">
                            <t-input v-model="formData.year" :readonly="true" />
                        </t-form-item>
                        <t-form-item label="上传月份">
                            <t-input v-model="formData.month" :readonly="true" />
                        </t-form-item>
                        <t-form-item label="上传日期">
                            <t-input v-model="formData.createDate" :readonly="true" />
                        </t-form-item>
                        <t-form-item label="上传类型">
                            <t-input v-model="formData.type" :readonly="true" />
                        </t-form-item>
                        <t-form-item label="备注">
                            <t-textarea v-model="formData.remark" :readonly="true" />
                        </t-form-item>
                    </t-form>
                </t-card>
            </t-col>
            <t-col :span="9">
                <baseTable ref="drawTable" :columns="drawColumns"  :param="param"  :listUrl="drawListUrl"  />
            </t-col>
        </t-row>
    </t-drawer>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import baseTable from "@/components/table/baseTable.vue";
import $api from "@/api/index";
import { ref,  onMounted } from "vue";
import moment from "moment";
//=====子表列表

let drawTable = ref(null);
let drawListUrl = ref($api.billDetail.GetList);
const drawColumns = [
    {
        colKey: "date",
        title: "交易时间",
        align: "center",
        cell: (h, { row }) => {
            return moment(row.date).format("yyyy/MM/DD HH:mm:ss");
        },
    },
    { colKey: "type", title: "交易分类", align: "center" },
    { colKey: "bTo", title: "交易对方", align: "center" },
    { colKey: "toAccount", title: "对方账号", align: "center" },
    { colKey: "payMethod", title: "收/付款方式", align: "center" },
    { colKey: "inOut", title: "收/支", align: "center" },
    { colKey: "money", title: "金额", align: "center" },
];

const drawLoadTable = () => {
    param.value = {billCode: formData.value.code};
    console.log("draw.param",param.value)
    drawTable.value.getTableList();
};
const param =ref(null)
//=====抽屉
const visible = ref(false);
const footer = ref(false);
let formData = ref({
    code: "",
    createDate: "",
    month: "",
    type: "",
    userName: "",
    year: "",
    remark: "",
});
//======列表
let table = ref(null);
const listUrl = $api.bill.GetList;
const columns = [
    { colKey: "userName", title: "上传人", align: "center" }, // width: "100"
    { colKey: "year", title: "年份", align: "center" },
    { colKey: "month", title: "月份", align: "center" },
    { colKey: "type", title: "类型", align: "center" },
    {
        colKey: "createDate",
        title: "上传日期",
        cell: (h, { row }) => {
            return moment(row.createDate).format("yyyy/MM/DD HH:mm:ss");
        },
        align: "center",
    },
];

onMounted(() => {
    loadTable();
});
const loadTable = () => {
    table.value.getTableList();
};
const handleRowClick = (e) => {
    let code = e.row.code;
    $instance
        .get($api.bill.GetBillEntityByCode, {
            params: {
                code: code,
            },
        })
        .then((resp) => {
            formData.value = resp.result.data;
            formData.value.createDate = timeFormat(resp.result.data.createDate);
            drawLoadTable();
        });
    visible.value = true;
};
const timeFormat = (time) => {
    var data = new Date(time);
    var year = data.getFullYear();
    var getMonth = data.getMonth() + 1;
    var getDay = data.getDate();
    var getHours = data.getHours();
    var getMinutes = data.getMinutes();
    var getSeconds = data.getSeconds();
    return year + "-" + getMonth + "-" + getDay + " " + getHours + ":" + getMinutes + ":" + getSeconds;
};


</script>   
