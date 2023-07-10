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

    <t-drawer v-model:visible="visible" :header="drawName" size="medium" :confirmBtn="drawName" :onConfirm="onConfirm" :onCancel="onCancel">
        <t-form ref="form" :data="data" :onSubmit="onSubmit">
            <t-form-item label="名称" name="Name">
                <t-input placeholder="请输入名称" v-model="data.Name" />
            </t-form-item>
            <t-form-item label="路径" name="Path">
                <t-input placeholder="请输入路径" v-model="data.Path" />
            </t-form-item>
            <t-form-item label="图标" name="Icon">
                <icon @dropDownValueChange="dropDownValueChange" v-model="data.Icon" :value="data.Icon" />
            </t-form-item>
            <t-form-item label="值" name="Value">
                <t-input placeholder="请输入值" v-model="data.Value" />
            </t-form-item>
            <t-form-item label="父节点" name="ParentCode">
                <t-input placeholder="当前父节点" :readonly="true" v-model="data.ParentCode" />
            </t-form-item>
            <t-form-item label="是否展示" name="IsShow"  >
                <t-select v-model="data.IsShow" v-model:value="data.IsShow">
                    <t-option key="true" label="是true" value="true" />
                    <t-option key="false" label="否false" value="false" />
                </t-select>
            </t-form-item>
            <t-input hidden :readonly="true" v-model="data.Code" />
        </t-form>
    </t-drawer>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import { ref, reactive, defineComponent } from "vue";
import icon from "./component/icon.vue";
import tree from "./component/tree.vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
let list = reactive({
    pagination: { defaultCurrent: 1, defaultPageSize: 5, total: 0 },
    data: [],
});
let visible = ref(false);
let drawName = ref("");
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
const addModule = () => {
    if(data.value.ParentCode==""){
        MessagePlugin.error("请选择节点")
        return ;
    }
    visible.value = true;
    drawName.value = "新增";
    data.value.Type = "Add";
};
const onEdit = (row) => {
    getTableEntity(row.code);
    visible.value = true;
    drawName.value = "修改";
    data.value.Type = "Edit";
};
const onDelete = (row) => {
    console.log(row);
    $instance.post($api.module.Delete, row).then((resp) => {
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
    console.log(e);
    let value = e.value;
    getTableList($api.module.GetModuleList + "?value=" + value);
    data.value.ParentCode = value;
};
const dropDownValueChange = (e) => {
    data.Icon = e;
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
        MessagePlugin.error("名称必填");
        return;
    }
    if (data.value.Path == "") {
        MessagePlugin.error("路径必填");
        return;
    }
    if (data.value.Icon == "") {
        MessagePlugin.error("图标必填");
        return;
    }
    if (data.value.IsShow == "") {
        MessagePlugin.error("是否展示必填");
        return;
    }

    if (data.value.Type == "Add") {
        $instance.post($api.module.Add, data.value).then((resp) => {
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
    }
    if (data.value.Type == "Edit") {
        $instance.post($api.module.Edit, data.value).then((resp) => {
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
    }
};
const getTableEntity = (entityCode) => {
    $instance
        .get($api.module.GetEntityByCode, {
            params: {
                code: entityCode,
            },
        })
        .then((resp) => {
            data.value.Icon = resp.result.data.icon;
            data.value.Name = resp.result.data.name;
            data.value.Path = resp.result.data.path;
            data.value.Value = resp.result.data.value;
            data.value.ParentCode = resp.result.data.parentCode;
            data.value.IsShow = resp.result.data.isShow;
            data.value.Code = resp.result.data.code;
            data.value.Id = resp.result.data.id;
        });
};
</script>
