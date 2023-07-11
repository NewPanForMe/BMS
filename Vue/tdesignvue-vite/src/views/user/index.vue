<template>
    <div class="content-top">
        <t-button theme="primary" @click="adduser">
            <template #icon><add-icon /></template>
            新建
        </t-button>
    </div>
    <div class="content">
        <baseTable :list="list" :columns="columns" :listUrl="listUrl"  @row-click="handleRowClick" />
    </div>

    <t-drawer v-model:visible="visible" :header="drawName" size="medium" :confirmBtn="drawName" :onConfirm="onConfirm" :onCancel="onCancel">
        <t-form ref="form" :data="data" :onSubmit="onSubmit">
            <t-form-item label="登录名称" name="LoginName">
                <t-input placeholder="请输入登录名称" v-model="data.LoginName" />
            </t-form-item>
            <t-form-item label="密码" name="LoginPassword">
                <t-input placeholder="请输入密码" v-model="data.LoginPassword" />
            </t-form-item>
            <t-form-item label="账号名称" name="Name">
                <t-input placeholder="请输入账号名称" v-model="data.Name" />
            </t-form-item>
            <t-form-item label="是否删除" name="IsDelete">
                <t-select v-model="data.IsDelete" :data="data.IsDelete">
                    <t-option key="true" label="是" value="true" />
                    <t-option key="false" label="否" value="false" />
                </t-select>
            </t-form-item>
            <t-input hidden :readonly="true" v-model="data.Code" />
            <t-input hidden :readonly="true" v-model="data.Id" />
        </t-form>
    </t-drawer>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from '@/router/router'
import { ref, reactive, defineComponent } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
import baseTable from "@/components/table/baseTable.vue";
let list = reactive({
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
});
const listUrl = $api.user.GetUserList;
let visible = ref(false);
let drawName = ref("");
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
                <div class="tdesign-table-demo__table-operations">
                    <t-button variant="outline" theme="default" onClick={() => onEdit(row)}>
                        修改
                    </t-button>
                    &nbsp;
                    <t-popconfirm content="确认删除吗" theme="danger" onConfirm={() => onDelete(row)}>
                        <t-button variant="text" theme="danger">
                            删除
                        </t-button>
                    </t-popconfirm>
                </div>
            );
        },
        align: "center",
    },
];
const adduser = () => {
    $router.push("/user_add")
    // visible.value = true;
    // drawName.value = "新增";
    // data.value.Type = "Add";
};
const onEdit = (row) => {
    getTableEntity(row.code);
    visible.value = true;
    drawName.value = "修改";
    data.value.Type = "Edit";
};
const onDelete = (row) => {
    console.log(row);
    $instance.post($api.user.Delete, row).then((resp) => {
        if (resp.success) {
            MessagePlugin.success("成功");
            visible.value = false;
            getTableList();
            data = ref({
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
        }
    });
};
let data = ref({
    Id: 0,
    Code: "",
    Name: "",
    LoginName: "",
    LoginPassword: "",
    IsDelete: false,
    Type: "",
});
const handleRowClick = (e) => {
    console.log(e)
};

const onConfirm = () => {
    onSubmit();
};
const onCancel = () => {
    visible.value = false;
};
const onSubmit = () => {
    console.log(data.value);
    if (data.value.Name == "") {
        MessagePlugin.error("账号名称必填");
        return;
    }
    if (data.value.IsDelete == "") {
        MessagePlugin.error("是否删除必填");
        return;
    }
    data.value.IsDelete = JSON.parse(data.value.IsDelete);
    if (data.value.Type == "Add") {
        $instance.post($api.user.Add, data.value).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
                visible.value = false;
                getTableList();
                data = ref({
                    Id: 0,
                    Code: "",
                    Name: "",
                    LoginName: "",
                    LoginPassword: "",
                    Type: "",
                });
            }
        });
    }
    if (data.value.Type == "Edit") {
        $instance.post($api.user.Edit, data.value).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
                visible.value = false;
                getTableList();
                data = ref({
                    Id: 0,
                    Code: "",
                    Name: "",
                    LoginName: "",
                    LoginPassword: "",
                    Type: "",
                });
            }
        });
    }
};
const getTableEntity = (entityCode) => {
    $instance
        .get($api.user.GetEntityByCode, {
            params: {
                code: entityCode,
            },
        })
        .then((resp) => {
            data.value.Name = resp.result.data.name;
            data.value.Code = resp.result.data.code;
            data.value.Id = resp.result.data.id;
            data.value.LoginName = resp.result.data.loginName;
            data.value.IsDelete = resp.result.data.isDelete;
        });
};
</script>
