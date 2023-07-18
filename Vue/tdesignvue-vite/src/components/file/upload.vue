<template>
    <t-space  direction="vertical">
        <!-- allow-upload-duplicate-file: false  不允许上传名称相同的文件 -->
        <!-- autoUpload: false  是否在选择文件后自动发起请求上传文件 -->
        <t-upload
            ref="uploadRef1"
            method="post"
            v-model="files1"
            :action="action"
            :headers="header"
            :multiple="multiple"
            :auto-upload="autoUpload"
            :response="response"
            :size-limit="{ size: 300, unit: 'MB' , message: '图片大小不超过 {sizeLimit} MB' }"
            :allow-upload-duplicate-file="false"
            @select-change="handleSelectChange"
            @fail="handleFail"
            @success="handleSuccess"
            @validate="onValidate"
            :before-upload="beforeUpload"
            theme="file-flow"
            :codeStrings="codeStrings"
        />
        <table style="min-width: 498px;max-width: 960px;text-align: center;">
            <thead>
                <tr>
                    <td>文件名称</td>
                    <td>操作</td>
                </tr>
            </thead>
            <tbody>
                
            </tbody>
        </table>
    </t-space>
</template>
<script setup>
import { ref, watch } from "vue";
import $api from "@/api/index";
import { MessagePlugin } from "tdesign-vue-next";
import $cookies from "@/utils/cookies";
const uploadRef1 = ref();
const files1 = ref([]);
const multiple = ref(true);
const autoUpload = ref(true);
const emit = defineEmits(["Resp"]);

let action = ref($api.file.FileUpload);
let header = ref({});
let response = ref(null);
const prop=defineProps({
    codeStrings: {type:Array,default:()=>[]},
});
const handleFail = ({ file }) => {
    MessagePlugin.error(`文件 ${file.name} 上传失败`);
    return;
};

const handleSelectChange = (files) => {
    console.log("onSelectChange", files);
};

const handleSuccess = (params) => {
    let res = params.response[0]
    emit("Resp", res);
    console.log(res)
    if (res.success) {
        MessagePlugin.success("上传成功");
        return;
    }else{
        MessagePlugin.warning(res.result);
        return;
    }
};

const onValidate = (params) => {
    const { files, type } = params;
    console.log("onValidate", type, files);
    const messageMap = {
        FILE_OVER_SIZE_LIMIT: "文件大小超出限制，已自动过滤",
        FILES_OVER_LENGTH_LIMIT: "文件数量超出限制，仅上传未超出数量的文件",
        FILTER_FILE_SAME_NAME: "不允许上传同名文件",
        BEFORE_ALL_FILES_UPLOAD: "beforeAllFilesUpload 方法拦截了文件",
        CUSTOM_BEFORE_UPLOAD: "beforeUpload 方法拦截了文件",
    };
    // you can also set Upload.tips and Upload.status to show warning message.
    messageMap[type] && MessagePlugin.warning(messageMap[type]);
};

/** 单个文件校验方法，示例代码有效，勿删 */
const beforeUpload = (file) => {
    var token = $cookies.getToken();
    header.value = {
        Authorization: "Bearer " + token.tokenName,
        JwtVersion: token.jwtVersion,
        RefreshToken: token.refreshToken,
    };
};

const dealFileCode=()=>{
    console.log(prop.codeStrings)
}
dealFileCode();
</script>
