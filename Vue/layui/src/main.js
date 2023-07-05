import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Layui from '@layui/layui-vue'
import '@layui/layui-vue/lib/index.css'
import  {layer} from "@layui/layui-vue"
import instance from '../src/utils/http'
import api from '../src/api/index'
import cookie from './utils/cookie'
var app=createApp(App);
app.use(store)
app.use(router)
app.use(Layui)
app.config.globalProperties.$router =router;
app.config.globalProperties.$layer =layer;
app.config.globalProperties.$cookies =cookie;
app.config.globalProperties.$instance =instance;
app.config.globalProperties.$api =api;
app.mount('#app')
