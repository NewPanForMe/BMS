<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @submit="onSubmit">
            <t-form-item label="手机号" name="Phone">
                <t-input v-model="formData.Phone" placeholder="请输入手机号" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="验证码" name="VerifyCode">
                <t-input v-model="formData.VerifyCode" placeholder="请输入验证码" @enter="onEnter"></t-input>
                <t-button theme="default" variant="outline" :disabled="IsSendMsgButton" @click="SendMsg">{{ bttonText }}
                </t-button>
            </t-form-item>
            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">绑定</t-button>
                </t-space>
            </t-form-item>
        </t-form>
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $cookies from "@/utils/cookies";
import $api from "@/api/index";
import { ref, reactive } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
// 获取当前路径的code和类型
const code = $cookies.getUserCode();
const form = ref(null);
let time = ref(60);
let IsSendMsgButton = ref(false);
let bttonText = ref("发送")
let timer = null
const formData = reactive({
    Id: 0,
    Code: code,
    Phone: "",
    VerifyCode: ""
});

const FORM_RULES = {
    Phone: [{ telnumber: true, message: '请输入正确的手机号码' }, { required: true, message: "请输入手机号码" }],
    VerifyCode:[ { required: true, message: "请输入验证码" }]
};

const SendMsg = () => {
    if (formData.Phone == "") {
        MessagePlugin.warning("请输入手机号码")
        return;
    }
    $instance.post($api.sms.SendBindCode, { phone: formData.Phone }).then(resp => {
        console.log(resp)
        if (resp.success) {
            MessagePlugin.success("发送成功")
            timer = setInterval(timeLess, 1000)
            IsSendMsgButton.value = true;
        }
        else {
            reset();
        }
    })
}

const timeLess = () => {
    time.value = parseInt(time.value);
    time.value--;
    bttonText.value = "重新发送(" + time.value + "s)"
    if (time.value == 0) {
        clearInterval(timer);
        reset();
    }
}

const reset = () => {
    time.value = 60;
    IsSendMsgButton.value = false;
    bttonText.value = "发送"
}

const onSubmit = ({ validateResult, firstError }) => {
    console.log(formData);
    if (validateResult === true) {
        $instance.post($api.user.BindPhone, formData).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
            }
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};

const onEnter = (_, { e }) => {
    // 禁用 Input 组件，按下 Enter 键时，触发 submit 事件
    e.preventDefault();
};


</script>
