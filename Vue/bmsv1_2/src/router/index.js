import { createRouter, createWebHistory } from 'vue-router'
import base from './base/base'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    ...base
  ]
})

export default router
