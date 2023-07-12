<template>
    <div class="content-top">
        <t-button theme="primary" @click="adduser">
            <template #icon><add-icon /></template>
            新建
        </t-button>
    </div>
    <div class="content">
        <baseTable :columns="columns" :listUrl="listUrl" @row-click="handleRowClick" />
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { reactive, ref } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
import baseTable from "@/components/table/baseTable.vue";
const listUrl = $api.user.GetUserList;
const columns = [
    { colKey: "name", title: "名称", align: "center" },
    { colKey: "loginName", title: "登录名称", align: "center" },
    {
        colKey: "isDelete",
        title: "是否删除",
        cell: (h, { row }) => {
            if (row.isDelete) {
                return "是";
            } else {
                return "否";
            }
        },
        align: "center",
    },
    {
        colKey: "isLock",
        title: "是否锁定",
        cell: (h, { row }) => {
            if (row.isLock) {
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
const adduser = () => {
    $router.push({ path: "/user_add/add/code" });
};
const onEdit = (row) => {
    $router.push({ path: "/user_add/edit/" + row.code });
};
const onDelete = (row) => {
    console.log(row);
    $instance.post($api.user.Delete, row).then((resp) => {
        if (resp.success) {
            MessagePlugin.success("成功");
            getTableList();
        }
    });
};
const handleRowClick = (e) => {
    console.log(e);
};
</script>
