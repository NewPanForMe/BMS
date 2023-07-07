<template>
    <div class="content-top">
        <t-button theme="default" variant="outline" size="small" @click="addModule">
            <template #icon>
                <t-icon name="add-circle" />
            </template>
            新建
        </t-button>
    </div>
    <t-table row-key="index" :data="list" :columns="columns" bordered hover table-layout="fixed" size="small" :pagination="pagination" cell-empty-content="-" resizable @row-click="handleRowClick">
    </t-table>
    <t-drawer v-model:visible="visible" :header="drawName" size="medium" :footer="false"  :rules="rules">
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
            <t-form-item label="父节点" name="ParentName">
                <t-input placeholder="当前父节点" readonly="true" v-model="data.ParentName" />
            </t-form-item>
            <t-form-item label="是否展示" name="IsShow" v-model="data.IsShow">
                <t-select>
                    <t-option key="true" label="是" value="true" />
                    <t-option key="false" label="否" value="false" />
                </t-select>
            </t-form-item>
            <t-form-item>
                <t-button theme="primary" type="submit">提交</t-button>
            </t-form-item>
            <t-input readonly="true" hidden v-model="data.ParentName" />
        </t-form>
    </t-drawer>
</template>
<script>
import { ref,reactive } from "vue";
import icon from "./component/icon.vue";
const pagination = {
    defaultCurrent: 1,
    defaultPageSize: 5,
    total: 20,
};
const columns = ref([
    { colKey: "Path", title: "路径" }, // width: "100"
    { colKey: "Icon", title: "图标" },
    { colKey: "Value", title: "值" },
    { colKey: "Name", title: "名称" },
    { colKey: "IsShow", title: "是否展示" },
    {
        colKey: "IsShow",
        title: "是否展示",
        cell: (h, { row }) => {
            console.log(row);
            return "<t-tag ></t-tag>";
        },
    },
]);

const data = reactive({
    Name: "",
    Path: "",
    Icon: "",
    Value: "",
    ParentCode: "",
    ParentName: "",
    IsShow: true,
});

const rules = {
    Name: [
        { required: true, message: "名称必填", type: "error", },
    ],
    Path: [
        { required: true, message: "路径必填", type: "error",},
    ],
    Icon: [
        { required: true, message: "图标必填", type: "error", },
    ],
    Value: [
        { required: true, message: "值必填", type: "error", },
    ],
    ParentCode: [
        { required: true, message: "父节点必填", type: "error", },
    ],
    IsShow: [
        { required: true, message: "是否展示必填", type: "error" },
    ],
};

export default {
    components: { icon },
    data() {
        return {
            list: ref([]),
            pagination: pagination,
            columns: columns,
            visible: false,
            drawName: "",
            data: data,
            rules: rules,
        };
    },
    methods: {
        handleRowClick(e) {
            console.log(e);
        },
        addModule() {
            this.visible = true;
            this.drawName = "新增";
        },
        dropDownValueChange(e) {
            console.log(e);
            this.data.Icon = e;
        },
        onSubmit() {
            if (validateResult === true) {
                this.$message.warning("成功");
            } else {
                console.log("Validate Errors: ", firstError, validateResult);
                this.$message.warning(firstError);
            }
        },
    },
};
</script>
