const router = [
    {
        path: "/",
        name: "login",
        component: () => import('@/views/login/login.vue')
    },
];
export default router;
