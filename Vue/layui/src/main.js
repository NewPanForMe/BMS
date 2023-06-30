import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Layui from '@layui/layui-vue'
import '@layui/layui-vue/lib/index.css'
import { layer } from "@layui/layui-vue"
var app=createApp(App);
app.use(store)
app.use(router)
app.use(Layui)
app.use(layer)

app.config.globalProperties.$layer = layer;
app.mount('#app')
