import home from '../home/index'
const router = [
    {
        path: "/index",
        name: "index",
        component: () => import('@/views/layout/index.vue'),
        children:[
           ...home
        ]
    },
];
export default router;
