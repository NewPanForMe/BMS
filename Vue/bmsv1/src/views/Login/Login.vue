<template>
    <n-card>
        <n-form :model="formValue">
            <n-form-item path="user.age" label="账号">
                <n-input type="text" v-model="username" placeholder="请输入账号">
                </n-input>
            </n-form-item>
            <n-form-item path="user.name" label="密码">
                <n-input type="password" show-password-on="click" placeholder="请输入密码" v-model="password">
                    <template #password-visible-icon>
                        <n-icon :size="16" :component="Alarm" />
                    </template>
                    <template #password-invisible-icon>
                        <n-icon :size="16" :component="Glasses" />
                    </template>
                </n-input>
            </n-form-item>
            <n-button type="info" size="large" attr-type="button" @click="loginClick">
                登录
            </n-button>
        </n-form>

    </n-card>
</template>
  
<script>
import { defineComponent, ref } from "vue";
import { Alarm, Glasses } from "@vicons/ionicons5";

export default defineComponent({
    setup() {
        return {
            Alarm, Glasses,
            username: ref(null),
            password: ref(null),
            formValue: ref({
                user: {
                    name: "",
                    age: ""
                },
                phone: ""
            }),
        }
    },
    data() { return {} },
    methods: {
        loginClick(e) {
            console.log(this.password)
            console.log(this.formValue.user)
        }
    }
});
</script>