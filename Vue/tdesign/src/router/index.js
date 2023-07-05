import Vue from 'vue'
import VueRouter from 'vue-router'
import layout from './layout/index'
Vue.use(VueRouter)

const routes = [
  ...layout,
  {
    path: '/',
    redirect:'login'
  },
  {
    path: '/login',
    name: 'login',
    component: ()=> import('@/views/login/login.vue') 
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
