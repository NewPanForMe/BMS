<template>
    <t-space>
        <!-- allow-upload-duplicate-file: false  不允许上传名称相同的文件 -->
        <!-- autoUpload: false  是否在选择文件后自动发起请求上传文件 -->
        <upload @Resp="resp"  :codeStrings="codeStrings"  />
    </t-space>
</template>
<script setup>
import { ref, watch } from "vue";
import upload from "@/components/file/upload.vue";
const theme=ref("file-flow")
const resp = (param) => {
    console.log(param);
};

let codeStrings=ref([
    "42095be3-712e-4bde-89ec-dbbefd66a402",
    "4fbbbe35-6894-43ab-a649-f3021dddc64d",
    "50eb7f1c-4a5c-4715-99f4-8cb895d2f1cf",
    "875c68c6-1835-4988-87c8-69cf0c2ff755",
])


</script>
