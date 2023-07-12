<template>
    <div class="content-top">
        <t-button theme="primary" @click="addrole">
            <template #icon><add-icon /></template>
            新建
        </t-button>
    </div>
    <div class="content">
        <baseTable   ref="table" :columns="columns" :listUrl="listUrl" @row-click="handleRowClick" @refreshTable="refresh"  />
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { reactive, ref,onMounted } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
import baseTable from "@/components/table/baseTable.vue";
const listUrl = $api.role.GetRoleList;

const columns = [
    { colKey: "name", title: "名称", align: "center" },
    { colKey: "orderBy", title: "排序", align: "center" },
   
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
let table = ref(null)
onMounted(() => {
  loadTable()
})
const loadTable=()=>{
    table.value.getTableList()
}
let refresh = ref(false)
const addrole = () => {
  $router.push("/role_add/add/code");
};
const onEdit = (row) => {
    console.log(row)
    $router.push({ path: "/role_add/edit/" + row.code });
};
const onDelete = (row) => {
    console.log(row);
    $instance.post($api.role.Delete, row).then((resp) => {
        if (resp.success) {
            MessagePlugin.success("成功");
            loadTable();
        }
    });
};
const handleRowClick = (e) => {
    console.log(e);
};
</script>
