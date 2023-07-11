<template>
    <div class="login">
        <div class="login-content">
            <p class="login-text"><strong>BMS后台管理系统</strong></p>
            <t-form ref="form" :data="formData" :colon="true" :label-width="0" @reset="onReset" @submit="onSubmit">
                <t-form-item name="username">
                    <t-input v-model="formData.username" clearable placeholder="请输入账户名">
                        <template #prefix-icon>
                            <desktop-icon />
                        </template>
                    </t-input>
                </t-form-item>

                <t-form-item name="password">
                    <t-input v-model="formData.password" type="password" clearable placeholder="请输入密码">
                        <template #prefix-icon>
                            <lock-on-icon />
                        </template>
                    </t-input>
                </t-form-item>
                <div class="login-button">
                    <t-form-item>
                        <t-button theme="primary" type="submit" block :loading="loadding">登录</t-button>
                    </t-form-item>
                </div>
            </t-form>
        </div>
    </div>
</template>
<script>
import { reactive } from "vue";
import { DesktopIcon, LockOnIcon } from "tdesign-icons-vue-next";
export default {
    components: { DesktopIcon, LockOnIcon },
    data() {
        return {
            formData: reactive({
                username: "",
                password: "",
            }),
            loadding:false
        };
    },
    methods: {
        onReset() {
            this.$message.success("重置成功");
        },
        onSubmit() {
            if (this.formData.username == null || this.formData.password == null) {
                return false;
            }
            this.$instance.post(this.$api.login.checkUserName, this.formData).then((resp) => {
           
                this.$cookies.saveToken(resp.result);
                this.$router.push("/index");
            });
        },
    },
};
</script>
