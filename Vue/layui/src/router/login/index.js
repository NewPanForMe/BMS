const login = [
    {
        path: '/',
        redirect:"/login"
    },
    {
        path: '/login', 
        name: 'login',
        component: () => import("@/views/login/Login.vue")
    }
]
export default login