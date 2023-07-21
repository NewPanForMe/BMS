const router = [
    {
        path: "/billupload",
        name: "billupload",
        meta: { title: "账单导入" },
        component: () => import('@/views/bill/upload/upload.vue')
    },
    {
        path: "/billindex",
        name: "billindex",
        meta: { title: "账单列表" },
        component: () => import('@/views/bill/upload/index.vue')
    },
    
];
export default router;
