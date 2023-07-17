<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @submit="onSubmit">
            <t-form-item label="旧密码" name="OldPassword">
                <t-input v-model="formData.OldPassword" type="password"></t-input>
            </t-form-item>

            <t-form-item label="新密码" name="Password">
                <t-input v-model="formData.Password" type="password"></t-input>
            </t-form-item>

            <t-form-item label="确认密码" name="rePassword">
                <t-input v-model="formData.rePassword" type="password"></t-input>
            </t-form-item>

            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">提交</t-button>
                </t-space>
            </t-form-item>
        </t-form>
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $cookies from "@/utils/cookies";
import $api from "@/api/index";
import $router from "@/router/router";
import { ref, reactive } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
// 获取当前路径的code和类型
const code = $cookies.getUserCode();
const form = ref(null);
const formData = reactive({
    Id: 0,
    Code: code,
    Password: "",
    OldPassword: "",
    rePassword: "",
});

const rePassword = (val) => 
    new Promise((resolve) => {
        const timer = setTimeout(() => {
            resolve(formData.Password === val);
            clearTimeout(timer);
        });
    });

const FORM_RULES = {
    OldPassword: [{ required: true, message: "旧密码必填", type: "error" }],
    Password: [{ required: true, message: "新密码必填", type: "error" }],
    rePassword: [
        { required: true, message: "确认密码必填", type: "error" },
        { validator: rePassword, message: "两次密码不一致" },
    ],
};

const onSubmit = ({ validateResult, firstError }) => {
    console.log(formData);
    if (validateResult === true) {
        $instance.post($api.user.EditPassword, formData).then((resp) => {
            if (!resp.success) {
                MessagePlugin.warning(resp.result);
            } else {
                MessagePlugin.success("修改成功");
                $cookies.removeToken();
                $cookies.removeRefreshToken();
                $router.push({ name: "login", replace: true });
            }
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};
</script>
