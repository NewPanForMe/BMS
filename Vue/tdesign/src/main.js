import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import TDesign from 'tdesign-vue';
import instance from './utils/http';
import cookie from './utils/cookies';
import api from './api/index'

// 引入组件库的少量全局样式变量
import 'tdesign-vue/es/style/index.css';
Vue.prototype.$instance = instance;
Vue.prototype.$cookies = cookie;
Vue.prototype.$api = api;
Vue.config.productionTip = false
Vue.use(TDesign)
Vue.use(cookie)
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
