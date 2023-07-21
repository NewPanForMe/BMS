import home from '../home/index'
import bill from '../bill/index'
const router = [
    {
        path: "/index",
        name: "index",
        meta:{title:"框架"},
        component: () => import('@/views/system/layout/index.vue'),
        children:[
           ...home,
           ...bill
        ]
    },
];
export default router;
