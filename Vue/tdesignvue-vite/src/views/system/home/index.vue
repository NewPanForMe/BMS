<template>
   
    <div class="content">
        <baseTable   />
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { reactive, ref,onMounted } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
import { AddIcon } from "tdesign-icons-vue-next";
import baseTable from "@/components/table/baseTable.vue";
</script>
