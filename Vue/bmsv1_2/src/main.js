import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import './assets/main.css'

//模板组件
import   native from  'naive-ui'

//系统默认get/post方法
import instance from "./utils/http"

//通讯组件
import axios from 'axios'

//登录api
import loginapi from './api/login'

//cookie
import cookies from 'vue-cookies'


const app = createApp(App)
app.use(router)
app.use(native)
//全局配置组件
app.config.globalProperties.$router = router;
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$loginapi = loginapi;
app.config.globalProperties.$cookies = cookies;
app.config.globalProperties.$instance = instance;


app.mount('#app')
