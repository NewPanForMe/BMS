<template>
    <div class="layout">
        <t-layout>
            <t-aside>
                <t-menu>
                    <t-menu-item :to="list" >
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
                            <t-menu-item value="3-1">三级菜单-1</t-menu-item>
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
                    <div class="layout-content-top">
                        <t-tag variant="outline" theme="primary" closable>标签一</t-tag>
                        <t-tag variant="outline" theme="success" closable>标签二</t-tag>
                        <t-tag variant="outline" theme="warning" closable>标签三</t-tag>
                        <t-tag variant="outline" theme="danger" closable>标签四</t-tag>
                    </div>
                    <router-view ></router-view>
                </t-content>
                <t-footer>Copyright @ 2022-{{ new Date().getFullYear() }} Ys.BMS All Rights Reserved</t-footer>
            </t-layout>
        </t-layout>
    </div>
</template>
<script>
import { RouterView } from 'vue-router'
export default {

    components: { RouterView },
    data() {
        return {
            list: "user"
        }
    },
    watch: {
        $route(to, from) {
            console.log(to)
            console.log(from)
        }
    },
    methods: {
        onChange(x) {
            console.log(x)
        },
        click(x) {
            console.log(x)
        },
    }
}
</script>
