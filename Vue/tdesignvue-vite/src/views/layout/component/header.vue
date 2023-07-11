<template>
    <div class="header">
        <div class="header-left">
            <div class="header-left-logout">
                <t-space>
                    <t-dropdown trigger="click" @click="clickHandler" :options="options">
                    <t-button> {{ $cookies.getToken().userName }}</t-button>
                </t-dropdown>
                <t-button shape="circle" theme="primary" variant="outline" @click="onClick">
                    <template #icon><t-icon name="logout" /></template>
                </t-button>
        </t-space>
            
            </div>
        </div>
    </div>
    <t-dialog v-model:visible="visible" :confirm-on-enter="true" :on-confirm="onConfirmAnother">
        <h2><t-icon name="info-circle" style="color: red" /> 确定要退出吗</h2>
    </t-dialog>
</template>
<script setup>
import { ref } from "vue";
import $cookies from "@/utils/cookies";
import $router from "@/router/router";
import { MessagePlugin } from 'tdesign-vue-next';
const visible = ref(false);
const onClick = (context) => {
    console.log("点击了确认按钮，弹出弹窗", context);
    visible.value = true;
};
const onConfirmAnother = (context) => {
    console.log("点击了确认按钮", context);
    visible.value = false;
    $cookies.removeToken();
    $cookies.removeRefreshToken();
    $router.push({ path: "/", replace: true });
};

const options = [
    { content: "操作一", value: 1 },
    { content: "操作二", value: 2 },
    { content: "操作三", value: 3 },
    { content: "操作四", value: 4 },
];
const clickHandler = (data) => {
    console.log(data);
    MessagePlugin.success(`选中【${data.content}】`);
};
</script>
