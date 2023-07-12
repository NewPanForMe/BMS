const router = [
    {
        path: "/home",
        name: "home",
        meta:{title:"首页"},
        component: () => import('@/views/home/index.vue')
    },
    {
        path: "/user",
        name: "user",
        meta:{title:"用户"},
        component: () => import('@/views/user/index.vue')
    },
    {
        path: "/user_add/:type/:code",
        name: "user_add",
        meta:{title:"用户新增"},
        component: () => import('@/views/user/add.vue'),
        append:true
    },
    {
        path: "/order",
        name: "order",
        meta:{title:"订单"},
        component: () => import('@/views/order/index.vue')
    },
    {
        path: "/role",
        name: "role",
        meta:{title:"角色"},
        component: () => import('@/views/role/index.vue')
    },
    {
        path: "/role_add/:type/:code",
        name: "role_add",
        meta:{title:"角色新增"},
        component: () => import('@/views/role/add.vue')
    },
    {
        path: "/module",
        name: "module",
        meta:{title:"模块"},
        component: () => import('@/views/module/index.vue')
    },
    {
        path: "/module_add/:type/:parentCode/:code",
        name: "module_add",
        meta:{title:"模块新增"},
        component: () => import('@/views/module/add.vue'),
        append:true
    },
    {
        path: "/log",
        name: "log",
        meta:{title:"日志"},
        component: () => import('@/views/nlog/index.vue')
    },
];
export default router;
