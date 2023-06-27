import { createRouter, createWebHistory } from 'vue-router'
import LoginView  from '../views/Login/Login.vue'
import LoginRoute  from './base/login.js'



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Login',
      component: LoginView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    ...LoginRoute
  ]
})

export default router
