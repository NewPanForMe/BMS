const menu = [
    {
        path: "",
        meta: { title: "系统管理" },
        icon: "setting",
        value: "system",
        name: "system",
        child: [
            {
                path: "/user",
                meta: { title: "用户信息" },
                value: "user",
                icon: "user",
                name: "user",
            },
            {
                path: "/order",
                meta: { title: "订单信息" },
                value: "order",
                icon: "bulletpoint",
                name: "order",
            },
        ],
    },
    {
        path: "",
        meta: { title: "基础配置" },
        icon: "add-rectangle",
        value: "base",
        name: "base",
        child: [
            {
                path: "/role",
                meta: { title: "角色信息" },
                value: "role",
                icon: "usergroup-add",
                name: "role",
            },
        ],
    },
    {
        path: "/home",
        meta: { title: "家庭" },
        icon: "add-rectangle",
        value: "home",
        name: "home",
        child: [],
    },
];
export default menu