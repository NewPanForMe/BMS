<template>
    <div style="margin-top: 50px;text-align: center;">
        <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />
        <br>
        <n-gradient-text :size="40" type="danger">
            Vue.js
        </n-gradient-text>
    </div>
    <div style="background: rgb(255, 255, 255);
                                            width: 300px;
                                            margin-right: auto;
                                            margin-bottom: auto;
                                            padding: 50px;
                                            margin-top: 30px;
                                            margin-left: auto;
                                            margin-right: auto;">
        <div style="margin-bottom: 40px;">
            <n-gradient-text :size="40" type="danger">
                欢迎登录
            </n-gradient-text>
        </div>

        <div style="margin-bottom: 20px;">
            <div style="color: black;font-weight: bold;">
                <n-input type="text" size="large" placeholder="用户名" v-model:value="username" />
            </div>
        </div>
        <div style="margin-bottom: 40px;">
            <div style="color: black;font-weight: bold;">
                <n-input type="password" show-password-on="mousedown" size="large" placeholder="密码"
                    v-model:value="password" />
            </div>
        </div>
        <div>
            <n-button type="info" @click="loginClick()" size="large" style="width: 100%;">
                登录
            </n-button>
        </div>
    </div>
</template>
<script>
import { ref } from 'vue';

export default {
    setup() {
        return {
            username: ref(null),
            password: ref(null),
        }
    },
    data() {
    }, methods: {
        loginClick: function () {
            this.checkLogin();
        },
        checkLogin: function () {
            let param = {
                username: this.username,
                password: this.password,
            }
            this.$instance.post(this.$loginapi.checkUserName, param).then(resp => {
                console.log(resp)
                let token = resp.result.accessToken;
                this.$cookies.set("token", token);

                this.$router.push("/home");
            }).catch(error => {
                console.log(error)
            })
        }
    },
    mounted() {
        let hash = window.location
        console.log(hash)
    }
}
</script>