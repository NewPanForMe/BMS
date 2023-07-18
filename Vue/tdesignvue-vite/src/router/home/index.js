const router = [
    {
        path: "/home",
        name: "home",
        meta: { title: "首页" },
        component: () => import('@/views/system/home/index.vue')
    },
    {
        path: "/user",
        name: "user",
        meta: { title: "用户" },
        component: () => import('@/views/system/user/index.vue')
    },
    {
        path: "/user_add/:type/:code",
        name: "user_add",
        meta: { title: "用户新增" },
        component: () => import('@/views/system/user/add.vue'),
        append: true
    },
    {
        path: "/user_info",
        name: "user_info",
        meta: { title: "用户个人信息" },
        component: () => import('@/views/system/user/info.vue'),
    },
    {
        path: "/user_bind_phone",
        name: "user_bind_phone",
        meta: { title: "用户手机号绑定" },
        component: () => import('@/views/system/user/phone.vue'),
    },
    {
        path: "/user_password",
        name: "user_password",
        meta: { title: "用户修改密码" },
        component: () => import('@/views/system/user/password.vue'),
    },
    {
        path: "/order",
        name: "order",
        meta: { title: "订单" },
        component: () => import('@/views/system/order/index.vue')
    },
    {
        path: "/role",
        name: "role",
        meta: { title: "角色" },
        component: () => import('@/views/system/role/index.vue')
    },
    {
        path: "/role_add/:type/:code",
        name: "role_add",
        meta: { title: "角色新增" },
        component: () => import('@/views/system/role/add.vue')
    },
    {
        path: "/module",
        name: "module",
        meta: { title: "模块" },
        component: () => import('@/views/system/module/index.vue')
    },
    {
        path: "/module_add/:type/:parentCode/:code",
        name: "module_add",
        meta: { title: "模块新增" },
        component: () => import('@/views/system/module/add.vue'),
        append: true
    },
    {
        path: "/log",
        name: "log",
        meta: { title: "日志" },
        component: () => import('@/views/system/nlog/index.vue')
    },
    {
        path: "/smslog",
        name: "smslog",
        meta: { title: "短信日志" },
        component: () => import('@/views/system/smslog/index.vue')
    },
    {
        path: "/fileupload",
        name: "fileupload",
        meta: { title: "文件上传" },
        component: () => import('@/views/system/file/upload.vue')
    },
    {
        path: "/file",
        name: "file",
        meta: { title: "文件上传" },
        component: () => import('@/views/system/file/index.vue')
    },
];
export default router;
