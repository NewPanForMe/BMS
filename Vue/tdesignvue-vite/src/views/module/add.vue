<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @reset="onReset" @submit="onSubmit">
            <t-form-item label="名称" name="Name">
                <t-input v-model="formData.Name" placeholder="请输入名称" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="路径" name="Path">
                <t-input v-model="formData.Path" placeholder="请输入路径" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="图标" name="Icon">
                <icon @dropDownValueChange="dropDownValueChange" v-model="formData.Icon" :value="formData.Icon" />
            </t-form-item>
            <t-form-item label="值" name="Value">
                <t-input v-model="formData.Value" placeholder="请输入值" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="父节点" name="ParentCode">
                <t-input v-model="formData.ParentCode" placeholder="当前父节点" :readonly="true" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="是否展示" name="IsShow">
                <t-select v-model="formData.IsShow2">
                    <t-option key="true" label="是" value="true" />
                    <t-option key="false" label="否" value="false" />
                </t-select>
            </t-form-item>
    

            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">提交</t-button>
                    <t-button theme="default" variant="base" type="reset">重置</t-button>
                    <t-button theme="default" variant="base" @click="back">返回</t-button>
                </t-space>
            </t-form-item>
            <t-input hidden :readonly="true" v-model="formData.Code" />
            <t-input hidden :readonly="true" v-model="formData.Id" />
        </t-form>
    </div>
</template>
<script setup>
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import icon from "./component/icon.vue";
import { useRouter } from "vue-router";
import { ref, reactive, getCurrentInstance } from "vue";
import { MessagePlugin } from "tdesign-vue-next";

// 获取当前路径的code和类型
const route = useRouter();
const parentCode = route.currentRoute.value.params.parentCode;
const type = route.currentRoute.value.params.type;
const code = route.currentRoute.value.params.code;



const FORM_RULES = {
    Path: [{ required: true, message: "路径必填" }],
    // LoginPassword:[{ required: true, message: "登录密码必填" }],
    Name: [{ required: true, message: "名称必填" }],
    IsShow: [{ required: true, message: "请选择是否展示" }],
    ParentCode: [{ required: true, message: "父节点必填" }],
};

let formData = reactive({
    Id: 0,
    Code: "",
    Path: "",
    Icon: "",
    Value: "",
    ParentCode: parentCode,
    IsShow: false,
    IsShow2: "false",
    Module:[]
});

const form = ref(null);

const back = () => {
    $router.push("/module");
};

const onReset = () => {
    MessagePlugin.success("重置成功");
};

const dropDownValueChange = (e) => {
    formData.Icon = e;
};

const onSubmit = ({ validateResult, firstError }) => {
    console.log(formData);
    if (validateResult === true) {
        let url = "";
        if (type == "add") url = $api.module.Add;
        if (type == "edit") url = $api.module.Edit;
        formData.IsShow = JSON.parse(formData.IsShow2);
        $instance.post(url, formData).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
                $router.push("/module");
            }
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};

const getTableEntity = () => {
    if (type == "edit") {
        $instance
            .get($api.module.GetEntityByCode, {
                params: {
                    code: code,
                },
            })
            .then((resp) => {
                formData.Code = resp.result.data.code;
                formData.Id = resp.result.data.id;
                formData.IsShow = resp.result.data.isShow;
                formData.IsShow2 = resp.result.data.isShow.toString();
                formData.Icon = resp.result.data.icon;
                formData.Path = resp.result.data.path;
                formData.ParentCode = resp.result.data.parentCode;
                formData.Value = resp.result.data.value;
                formData.Name = resp.result.data.name;
            });
    }
};

getTableEntity();

const onEnter = (_, { e }) => {
    // 禁用 Input 组件，按下 Enter 键时，触发 submit 事件
    e.preventDefault();
};
</script>
