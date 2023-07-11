const router = [
    {
        path: "/home",
        name: "home",
        component: () => import('@/views/home/index.vue')
    },
    {
        path: "/user",
        name: "user",
        component: () => import('@/views/user/index.vue')
    },
    {
        path: "/user_add",
        name: "user_add",
        component: () => import('@/views/user/add.vue')
    },
    {
        path: "/order",
        name: "order",
        component: () => import('@/views/order/index.vue')
    },
    {
        path: "/role",
        name: "role",
        component: () => import('@/views/role/index.vue')
    },
    {
        path: "/module",
        name: "module",
        component: () => import('@/views/module/index.vue')
    },
    {
        path: "/log",
        name: "log",
        component: () => import('@/views/log/index.vue')
    },
];
export default router;
