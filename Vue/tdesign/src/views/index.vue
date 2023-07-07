<template>
    <div class="layout">
        <t-layout>
            <t-aside>
                <t-menu>
                    <t-menu-item  href="/user">
                        <template #icon>
                            <t-icon name="app" />
                        </template>
                        仪表盘
                    </t-menu-item>

                    <t-submenu value="1" title="资源列表">
                        <template #icon>
                            <t-icon name="code" />
                        </template>
                        <t-menu-item value="1-1">
                            <span>菜单二</span>
                        </t-menu-item>
                    </t-submenu>
                    <t-submenu value="2" title="调度平台">
                        <template #icon>
                            <t-icon name="file" />
                        </template>
                        <t-submenu value="2-1" title="二级菜单-1">
                            <t-menu-item value="3-1" to="homepage">三级菜单-1</t-menu-item>
                            <t-menu-item value="3-2">三级菜单-2</t-menu-item>
                            <t-menu-item value="3-3">三级菜单-3</t-menu-item>
                        </t-submenu>
                        <t-menu-item value="2-2">
                            <span>二级菜单-2</span>
                        </t-menu-item>
                    </t-submenu>
                </t-menu>
            </t-aside>
            <t-layout>
                <t-header>Header</t-header>
                <t-content>
                    <div class="layout-content">
                        <router-view></router-view>
                    </div>
                </t-content>
                <t-footer>Copyright @ 2022-{{ new Date().getFullYear() }} Ys.BMS All Rights Reserved</t-footer>
            </t-layout>
        </t-layout>
    </div>
</template>
<script>
import { RouterView } from "vue-router";
export default {
    components: { RouterView },
    data() {
        return {
            list: "user",
            route:{
                "user":"/user"
            }
        };
    },
    methods: {
        onChange(x) {
            console.log(x);
        },
        click(x) {
            console.log(x);
        },
    },
};
</script>
