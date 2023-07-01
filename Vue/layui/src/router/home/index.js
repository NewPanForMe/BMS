const home = [
  {
    path: '/home',
    name: 'home',
    meta:{title:"系统管理"},
    component: () => import( '@/views/home/Index.vue'),
    children:[
      {
        path:"/homepage",
        name:"homepage",
        meta:{title:"首页"},
        component:()=>import("@/views/home/homepage.vue") 
      },
      {
        path:"/order",
        name:"order",
        meta:{title:"订单"},
        component:()=>import("@/views/order/Index.vue")
      },
      {
        path:"/orderadd",
        name:"orderadd",
        meta:{title:"订单"},
        component:()=>import("@/views/order/add.vue")
      },
      {
        path:"/user",
        name:"user",
        meta:{title:"用户"},
        component:()=>import("@/views/user/Index.vue")
      }
    ]
  }
]
export default home