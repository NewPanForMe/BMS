<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @reset="onReset" @submit="onSubmit">
            <t-form-item label="角色名称" name="Name">
                <t-input v-model="formData.Name" placeholder="请输入角色名称" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="排序" name="OrderBy">
                <t-input v-model="formData.OrderBy" placeholder="请输入排序" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="配置模块" name="Module">
                <t-select v-model="moduleStrings" :options="moduleOptions" placeholder="请选择" filterable multiple />
            </t-form-item>
            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">提交</t-button>
                    <t-button theme="default" variant="base" type="reset">重置</t-button>
                    <t-button theme="default" variant="base" @click="back">返回</t-button>
                </t-space>
            </t-form-item>

            <t-input v-model="formData.Code" hidden></t-input>
            <t-input v-model="formData.Id" hidden></t-input>
        </t-form>
    </div>
</template>
<script setup>
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { useRouter } from "vue-router";
import { ref, reactive, getCurrentInstance } from "vue";
import { MessagePlugin } from "tdesign-vue-next";

// 获取当前路径的code和类型
const route = useRouter();
const code = route.currentRoute.value.params.code;
const type = route.currentRoute.value.params.type;
const FORM_RULES = {
    Name: [{ required: true, message: "请输入角色名称" }],
    OrderBy: [{ required: true, message: "请输入排序" }],
};

const moduleStrings = ref([]);
const moduleOptions = ref([]);

const formData = reactive({
    Id: 0,
    Code: "",
    Name: "",
    OrderBy: "",
    Modules:[]
});
const form = ref(null);

const back = () => {
    $router.push("/role");
};

const onReset = () => {
    MessagePlugin.success("重置成功");
};

const onSubmit = ({ validateResult, firstError }) => {
    formData.Modules=moduleStrings.value
    console.log(formData);
    if (validateResult === true) {
        let url = "";
        if (type == "add") url = $api.role.Add;
        if (type == "edit") url = $api.role.Edit;
        $instance.post(url, formData).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
                $router.push("/role");
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
            .get($api.role.GetEntityByCode, {
                params: {
                    code: code,
                },
            })
            .then((resp) => {
                formData.Code = resp.result.data.code;
                formData.Id = resp.result.data.id;
                formData.Name = resp.result.data.name;
                formData.OrderBy = resp.result.data.orderBy;
                formData.Modules = resp.result.data.modules;
                moduleStrings.value = resp.result.data.modules;
                console.log(resp.result.data);
                console.log(formData);
            });
    }
};

getTableEntity();

const onEnter = (_, { e }) => {
// 禁用 Input 组件，按下 Enter 键时，触发 submit 事件
    e.preventDefault();
};

const GetGroupSelectOptions = () => {
    $instance.get($api.module.GetGroupSelectOptions).then((resp) => {
        resp.result.data.forEach((element) => {
            moduleOptions.value.push(element);
        });
    });
};
GetGroupSelectOptions();


</script>
