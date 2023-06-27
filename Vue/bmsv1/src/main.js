import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'


//模板组件
import   naive from  'naive-ui'

//系统默认get/post方法
import instance from "./assets/base/http.js"

//通讯组件
import axios from 'axios'

//cookie
import cookies from 'vue-cookies'

const app = createApp(App)

app.use(router)
app.use(naive)
app.config.globalProperties.$router = router;
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$cookies = cookies;
app.config.globalProperties.$instance = instance;
app.mount('#app')
