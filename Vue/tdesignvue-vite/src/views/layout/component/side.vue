<template>
    <t-menu>
        <template v-for="item in menu">
            <template v-if="item.child.length == 0">
                <t-menu-item value="{{ item.value }}" :to="item.path" :content="item.meta.title" :change="onMenuClick">
                    <template #icon>
                        <t-icon :name="item.icon" />
                    </template>
                </t-menu-item>
            </template>
            <template v-if="item.child.length > 0">
                <t-submenu :value="item.value" :title="item.meta.title">
                    <template #icon>
                        <t-icon :name="item.icon" />
                    </template>
                    <template v-for="itemChild in item.child">
                        <t-menu-item :value="itemChild.value" :to="itemChild.path" :change="onMenuClick">
                            <template #icon>
                                <t-icon :name="itemChild.icon" />
                            </template>
                            <span>{{ itemChild.meta.title }}</span>
                        </t-menu-item>
                    </template>
                </t-submenu>
            </template>
        </template>
    </t-menu>
</template>
<script setup>
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
            {
                path: "/module",
                meta: { title: "模块信息" },
                value: "module",
                icon: "view-module",
                name: "module",
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
</script>
