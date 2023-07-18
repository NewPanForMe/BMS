const router = [
    {
        path: "/",
        name: "login",
        component: () => import('@/views/system/login/login.vue')
    },
];
export default router;
