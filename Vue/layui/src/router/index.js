import { createRouter, createWebHistory } from 'vue-router'
  import home from './home/index'
  import login from './login/index'
const routes = [
...home,
...login
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

//全局守卫
router.beforeEach((to, from, next) => {
  // //用户访问的是'/notes'
  // if(to.path === '/homepage') {
  //   //查看一下用户是否保存了登录状态信息
  //   let user = 
  //   JSON.parse(localStorage.getItem('user'))
  //   if(user) {
  //     //如果有，直接放行
  //     next();
  //   }else {
  //     //如果没有，用户跳转登录页面登录
  //     next('/login')
  //   }
  // }else {
    next();
 // }
})
export default router
