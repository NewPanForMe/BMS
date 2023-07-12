<template>
    <div class="content-top">
        <t-button theme="primary" @click="addModule">
            <template #icon><add-icon /></template>
            新建
        </t-button>
    </div>
    <div class="content">
        <t-row :gutter="16">
            <t-col :span="2">
                <t-card title="所有模块" header bordered hover-shadow>
                    <tree @click-node="TreeNodeCick" />
                </t-card>
            </t-col>
            <t-col :span="10">
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
            </t-col>
        </t-row>
    </div>


</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from '@/router/router'
import { ref, reactive, defineComponent } from "vue";
import tree from "./component/tree.vue";
import baseTable from "@/components/table/baseTable.vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
let list = reactive({
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
});
let TreeNodeValue=ref("")
const columns = [
    { colKey: "path", title: "路径", align: "center" }, // width: "100"
    {
        colKey: "icon",
        title: "图标",
        cell: (h, { row }) => {
            return <t-icon name={row.icon}> </t-icon>;
        },
        align: "center",
    },
    { colKey: "value", title: "值", align: "center" },
    { colKey: "name", title: "名称", align: "center" },
    {
        colKey: "IsShow",
        title: "是否展示",
        cell: (h, { row }) => {
            if (row.isShow) {
                return "是";
            } else {
                return "否";
            }
        },
        align: "center",
    },
    {
        title: "操作",
        cell: (h, { row }) => {
            return (
                <div class="">
                    <t-space>
                        <t-button variant="outline" theme="default" onClick={() => onEdit(row)}>
                            修改
                        </t-button>
                        <t-popconfirm content="确认删除吗" theme="danger" onConfirm={() => onDelete(row)}>
                            <t-button variant="text" theme="danger">
                                删除
                            </t-button>
                        </t-popconfirm>
                    </t-space>
                </div>
            );
        },
        align: "center",
    },
];
const addModule = () => {
    if (TreeNodeValue.value == "") {
        MessagePlugin.error("请选择节点");
        return;
    }
  $router.push({path:"/module_add/add/"+TreeNodeValue.value +"/code"});
};
const onEdit = (row) => {
    $router.push({name:"module_add" ,params:{
        code:row.code,
        parentCode:"parentCode",
        type:"edit",
    }});
};
const onDelete = (row) => {
    console.log(row);
    $instance.post($api.module.Delete, row).then((resp) => {
        if (resp.success) {
            MessagePlugin.success("成功");
            getTableList();
        }
    });
};
let data = ref({
    Id: 0,
    Code: "",
    Name: "",
    Path: "",
    Icon: "",
    Value: "",
    ParentCode: "",
    ParentName: "",
    IsShow: true,
    Type: "",
});
const getTableList = (url) => {
    if (url == null) url = $api.module.GetModuleList;
    $instance.get(url).then((resp) => {
        list.data = resp.result.data;
        list.pagination = resp.result.pagination;
    });
};
getTableList($api.module.GetModuleList);
const handleRowClick = (e) => {};

const TreeNodeCick = (e) => {
    TreeNodeValue.value = e.value;
    getTableList($api.module.GetModuleList + "?value=" + TreeNodeValue.value);
};

</script>
