<template>
    <div class="content-top">
        <t-form layout="inline" ref="form" :data="formData" :rules="FORM_RULES" @submit="onSubmit">
            <t-form-item label="年份" name="Year">
                <t-select v-model="formData.Year">
                    <t-option v-for="item in yearList" :key="item" :label="item" :value="item">{{ item }}</t-option>
                </t-select>
            </t-form-item>
            <t-form-item label="月份" name="Month">
                <t-select v-model="formData.Month">
                    <t-option v-for="item in monthList" :key="item" :label="item" :value="item">{{ item }}</t-option>
                </t-select>
            </t-form-item>

            <t-form-item label="类型" name="Type">
                <t-select v-model="formData.Type">
                    <t-option key="微信" label="微信" value="微信">微信</t-option>
                    <t-option key="支付宝" label="支付宝" value="支付宝">支付宝</t-option>
                </t-select>
            </t-form-item>
            <t-form-item label="上传" name="tel">
                <upload @Resp="resp" :tableDisplay="false" :accept="accept" />
            </t-form-item>
            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">数据提交</t-button>
                </t-space>
            </t-form-item>
        </t-form>
    </div>
    <div class="content" style="margin: 40px 0px 0px 0px">
        <baseTable ref="table" :columns="columns" :listUrl="listUrl" :Param="params" @row-click="handleRowClick" />
    </div>
   
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { reactive, ref, onMounted } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
import baseTable from "@/components/table/baseTable.vue";
import upload from "@/components/file/upload.vue";
import moment from "moment";



//=======列表
let table = ref(null);
const listUrl = $api.billDetail.GetList;
let params = ref({});
const columns = [
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

const loadTable = () => {
    table.value.getTableList();
};

//===========基础表单==================
let yearList = ref([]);
let monthList = ref([]);
const form = ref(null);
const formData = reactive({
    Year: "",
    Month: "",
    Location: "",
    Type: "",
});
const getYear = () => {
    var date = new Date();
    var year = date.getFullYear();
    for (let index = 0; index < 3; index++) {
        yearList.value.push(year - index);
    }
};
getYear();
const getMonth = () => {
    var date = new Date();
    var month = date.getMonth();
    if (month == 12) {
        for (let index = 0; index < 12; index++) {
            monthList.value.push(month - index);
        }
    } else {
        for (let index = 0; index <= month; index++) {
            let val = month - index + 1;
            monthList.value.push(val);
        }
    }
};
getMonth();

const FORM_RULES = {
    Month: [{ required: true, message: "请选择月份" }],
    Year: [{ required: true, message: "请选择年份" }],
    Type: [{ required: true, message: "请选择类型" }],
};

const onSubmit = ({ validateResult, firstError }) => {
    if (validateResult === true) {
        $instance.post($api.bill.BillUpload, formData).then((resp) => {
            form.value.reset();
            params.value = { billCode: resp.result.code };
            loadTable();
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};
//==============上传====================
const resp = (param) => {
    formData.Location = param.result.location;
    
};
const accept = ref(".csv");
</script>
