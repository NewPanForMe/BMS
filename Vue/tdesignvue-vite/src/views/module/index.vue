<template>
    <div class="content-top">
        <t-button variant="outline" theme="success" size="small" @click="addModule">
            <template #icon>
                <t-icon name="add-circle" />
            </template>
        </t-button>
    </div>
    <t-table row-key="index" :data="list.data" :columns="columns" bordered hover table-layout="fixed" size="small"
        :pagination="list.pagination" cell-empty-content="-" resizable @row-click="handleRowClick">
    </t-table>
    <t-drawer v-model:visible="visible" :header="drawName" size="medium" :confirmBtn="drawName" :onConfirm="onConfirm"
    :onCancel="onCancel">
        <t-form ref="form" :data="data" :onSubmit="onSubmit">
            <t-form-item label="名称" name="Name">
                <t-input placeholder="请输入名称" v-model="data.Name" />
            </t-form-item>
            <t-form-item label="路径" name="Path">
                <t-input placeholder="请输入路径" v-model="data.Path" />
            </t-form-item>
            <t-form-item label="图标" name="Icon">
                <icon @dropDownValueChange="dropDownValueChange" v-model="data.Icon" />
            </t-form-item>
            <t-form-item label="值" name="Value">
                <t-input placeholder="请输入值" v-model="data.Value" />
            </t-form-item>
            <t-form-item label="父节点" name="ParentCode">
                <t-input placeholder="当前父节点" :readonly="true" v-model="data.ParentCode" />
            </t-form-item>
            <t-form-item label="是否展示" name="IsShow" v-model="data.IsShow">
                <t-select>
                    <t-option key="true" label="是" value="true" />
                    <t-option key="false" label="否" value="false" />
                </t-select>
            </t-form-item>
        </t-form>
    </t-drawer>
</template>
<script setup lang="jsx">
import $instance from '@/utils/http'
import $api from '@/api/index'
import { ref, reactive, defineComponent } from "vue";
import icon from "./component/icon.vue";
import { MessagePlugin } from "tdesign-vue-next";
let list = {
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 20 },
    data: []
}
let visible = false;
let drawName = ref("");
const columns = ([
    { colKey: "path", title: "路径" }, // width: "100"
    {
        colKey: "icon",
        title: "图标",
        cell: (h, { row }) => {
            return row.icon;
        },
    },
    { colKey: "value", title: "值" },
    { colKey: "name", title: "名称" },
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
    },
    {
        title: '操作',
        cell: (h, { row }) => {
            return (<div class="tdesign-table-demo__table-operations">
                <t-button variant="outline" theme="default" onClick={() => onEdit(row)}>
                    修改
                </t-button>
                &nbsp;
                <t-popconfirm content="确认删除吗">
                    <t-button variant="text" theme="danger" >删除</t-button>
                </t-popconfirm>
            </div>);
        },
    },
]);
const onEdit = (row) => {
    console.log(row)
    getTableEntity(row.code);
    visible = true;
    drawName = "修改";
}
const data = reactive({
    Name: "",
    Path: "",
    Icon: "",
    Value: "",
    ParentCode: "",
    ParentName: "",
    IsShow: true,
});
const getTableList = () => {
    $instance.get($api.module.GetModuleList).then((resp) => {
        list = resp.result;
    })
}
getTableList();
const addModule = () => {
    console.log("新增")
    visible = true;
    drawName = "新增";
};
const handleRowClick = (e) => {
    console.log(e);
    getTableEntity(e.row.code);
};
const dropDownValueChange = (e) => {
    console.log(e);
    data.Icon = e;
};
const onConfirm = () => {
    onSubmit()
};
const onCancel=()=>{
    console.log("01111")
    visible = false;
};
const onSubmit = () => {
    if (data.Name == "") {
        MessagePlugin.error("名称必填");
        return;
    }
    if (data.Path == "") {
        MessagePlugin.error("路径必填");
        return;
    }
    if (data.Icon == "") {
        MessagePlugin.error("图标必填");
        return;
    }
    if (data.IsShow == "") {
        MessagePlugin.error("是否展示必填");
        return;
    }
    $instance.post($api.module.Add, data).then((resp) => {
        if (resp.success) {
            MessagePlugin.success("成功");
            visible = false;
        }
    })
};
const getTableEntity = (entityCode) => {
    $instance.get($api.module.GetEntityByCode, {
        params: {
            code: entityCode
        }
    }).then((resp) => {
        data = resp.result.data;
    })
};
</script>
