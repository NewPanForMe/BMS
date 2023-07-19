<template>
    <t-space>
        <!-- allow-upload-duplicate-file: false  不允许上传名称相同的文件 -->
        <!-- autoUpload: false  是否在选择文件后自动发起请求上传文件 -->
        <upload @Resp="resp"  :codeStrings="codeStrings" />
    </t-space>
</template>
<script setup>
import { ref, watch } from "vue";
import upload from "@/components/file/upload.vue";
//:theme="theme"   开启多文件上传
const theme=ref("file-flow")
const resp = (param) => {
    console.log(param);
};

let codeStrings=ref([
   
])


</script>
