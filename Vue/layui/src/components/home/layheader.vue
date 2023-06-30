<template>
    <div id="header">
        <div class="header-left">BMS后台管理系统</div>
        <div class="header-right">
            <lay-button radius @click="openDrawer"><lay-icon type="layui-icon-more-vertical"></lay-icon></lay-button>
        </div>
    </div>
</template>
<script>

import { ref } from 'vue'
import { layer } from "@layui/layui-vue"
export default {
    setup() {
        return {
        }
    },
    methods: {
        openDrawer() {
            layer.drawer({
                title: "系统信息",
                content: 'Yuan设计与建造 \r\n   Ⓒ 2022~2023',
                offset: "r"
            })
        }
    }
}

</script>