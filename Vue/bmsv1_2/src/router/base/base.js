const router = [
    {
        path: "/",
        name: "/",
        component: (() => import("@/views/login.vue"))
    },
    {
        path: "/login",
        name: "login",
        component: (() => import("@/views/login.vue"))
    },
    {
        path: "/home",
        name: "home",
        component: (() => import("@/views/home/index.vue"))
    }
]

export default router
