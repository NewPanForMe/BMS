<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @reset="onReset" @submit="onSubmit">
            <t-form-item label="登录账号" name="LoginName">
                <t-input v-model="formData.LoginName" placeholder="请输入登录账号" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="登录密码" name="LoginPassword">
                <t-input v-model="formData.LoginPassword" type="password" placeholder="请输入登录密码" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="账号名称" name="Name">
                <t-input v-model="formData.Name" placeholder="请输入账号名称" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="是否删除" name="IsDelete">
                <t-select v-model="formData.IsDelete2">
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
            <t-input v-model="formData.Code"  hidden></t-input>
            <t-input v-model="formData.Id"  hidden></t-input>
        </t-form>
    </div>
</template>
<script setup>
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from '@/router/router'
import {useRouter} from 'vue-router'
import { ref, reactive ,getCurrentInstance} from "vue";
import { MessagePlugin } from "tdesign-vue-next";
// 获取当前路径的code和类型
const route= useRouter()
const code=route.currentRoute.value.params.code
const type=route.currentRoute.value.params.type
const FORM_RULES = { 
    LoginName: [{ required: true, message: "登录名称必填" }] ,
    LoginPassword:[{ required: true, message: "登录密码必填" }],
    Name:[{ required: true, message: "账号名称必填" }],
    IsDelete:[{ required: true, message: "请选择是否删除" }]
};

const formData = reactive({
    Id: 0,
    Code: "",
    Name: "",
    LoginName: "",
    LoginPassword: "",
    IsDelete: false,
    IsDelete2: "false",
});
const form = ref(null);

const back = () => {
    $router.go(-1)
};

const onReset = () => {
    MessagePlugin.success("重置成功");
};

const onSubmit = ({ validateResult, firstError }) => {
    console.log(formData)
    if (validateResult === true) {
        let url=""
        if(type=="add") url=$api.user.Add
        if(type=="edit") url=$api.user.Edit
        formData.IsDelete = JSON.parse(formData.IsDelete2)
        $instance.post(url, formData).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
                $router.push("/user")
            }
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};
const getTableEntity = () => {
    if(type=="edit"){
        $instance
        .get($api.user.GetEntityByCode, {
            params: {
                code: code,
            },
        })
        .then((resp) => {
            formData.Code=resp.result.data.code
            formData.Id=resp.result.data.id
            formData.IsDelete=resp.result.data.isDelete
            formData.IsDelete2=resp.result.data.isDelete.toString()
            formData.LoginName=resp.result.data.loginName
            formData.Name=resp.result.data.name
           console.log(resp.result.data)
           console.log(formData)
        });
    }
};

getTableEntity();





// eslint-disable-next-line @typescript-eslint/no-unused-vars
const submitForm = async () => {
    console.log("1,执行了submitForm")
    console.log(form.value)
    // form.value.submit();
    form.value.submit({ showErrorMessage: true });

    // 校验数据，代码有效，勿删
    // form.value.validate();

    // 校验数据：只提交和校验，不在表单中显示错误文本信息。下方代码有效，勿删
    // form.value.validate({ showErrorMessage: false }).then((validateResult) => {
    //   if (validateResult && Object.keys(validateResult).length) {
    //     const firstError = Object.values(validateResult)[0]?.[0]?.message;
    //     MessagePlugin.warning(firstError);
    //   }
    // });
};

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const resetForm = () => {
    form.value.reset();
    // 下方为示例代码，有效，勿删
    // form.value.reset({ type: 'initial' });
    // form.value.reset({ type: 'empty' });
    // form.value.reset({ type: 'initial', fields: ['name'] });
    // form.value.reset({ type: 'empty', fields: ['name'] });
};

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const validateOnly = async () => {
    const result = await form.value.validateOnly();
    MessagePlugin.success("打开控制台查看校验结果");
    console.log("validateOnly", result);
};

// 禁用 Input 组件，按下 Enter 键时，触发 submit 事件
const onEnter = (_, { e }) => {
    e.preventDefault();
};
</script>
