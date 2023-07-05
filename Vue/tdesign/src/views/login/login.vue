<template>
    <div class="login">
        <div class="login-content">
            <p class="login-text"><strong>BMS后台管理系统</strong></p>
            <t-form :data="form.user" ref="form"  @submit="onSubmit" :colon="true" :labelWidth="0">
                <t-form-item name="account">
                    <t-input clearable v-model="form.user.username" autocomplete="false"  placeholder="请输入账户名">
                        <desktop-icon slot="prefix-icon"></desktop-icon>
                    </t-input>
                </t-form-item>
                <t-form-item name="password">
                    <t-input type="password" clearable v-model="form.user.password" autocomplete="false"  placeholder="请输入密码">
                        <lock-on-icon slot="prefix-icon"></lock-on-icon>
                    </t-input>
                </t-form-item>
                <t-form-item>
                    <t-button theme="primary" :loading="form.model.loading" type="submit" block>登录</t-button>
                </t-form-item>
            </t-form>
        </div>
    </div>
</template>
<script>
import { DesktopIcon, LockOnIcon } from 'tdesign-icons-vue';
export default {
    components:{DesktopIcon, LockOnIcon },
    data() {
        return {
            form: {
                user: {
                    username: "",
                    password: "",
                },
                model: {
                    loading: false,
                },
            },
        };
    },
    methods: {
        onSubmit: function ({ validateResult, firstError }) {
            console.log(111);
            console.log(this.form)
            console.log(this.$instance)
            
        },
    },
};
</script>
