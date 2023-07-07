const route = [
    {
        path: 'homepage',
        name: 'homepage',
        meta:{title:"首页"},
        component: () => import('@/views/home/homepage.vue'),
    },
    {
        path: 'user',
        name: 'user',
        meta:{title:"用户信息"},
        component: () => import('@/views/user/index.vue'),
    }
]
export default route