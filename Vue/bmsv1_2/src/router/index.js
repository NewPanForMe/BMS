import { createRouter, createWebHistory } from 'vue-router'
import login from '../views/login.vue'
import home from '../views/home/index.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
      {
        path:"/",
        name:"/",
        component:login
      },
      {
        path:"/login",
        name:"login",
        component:login
      },
      {
          path:"/home",
          name:"home",
          component:home,
      }
  ]
})

export default router
