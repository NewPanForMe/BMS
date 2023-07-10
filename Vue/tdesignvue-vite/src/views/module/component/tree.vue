<template>
    <t-tree :data="items" activable hover transition :onClick="actived" />
</template>
<script setup>
import { ref ,defineEmits} from "vue";
import $instance from "@/utils/http";
import $api from "@/api/index";
let items = ref([]);
const emit = defineEmits(["ClickNode"]);
const GetModule = ()=>{
    $instance.get($api.tree.GetModuleTreeNode ).then((resp) => {
            items.value = resp.result.treeNode;
    });
}
GetModule();
const actived = (e) => {
    console.log(e.node);
    emit("ClickNode", e.node);
};
</script>
