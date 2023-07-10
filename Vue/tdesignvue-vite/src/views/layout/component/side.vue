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
import { ref ,defineEmits} from "vue";
import $instance from "@/utils/http";
import $api from "@/api/index";
const menu = ref([]);
const GetMenu = ()=>{
    $instance.get($api.menu.GetMenu ).then((resp) => {
        menu.value = resp.result.menuNode;
    });
}
GetMenu();

</script>
