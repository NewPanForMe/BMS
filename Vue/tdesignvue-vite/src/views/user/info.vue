<template>
    <div class="add">
        <t-form ref="form" :rules="FORM_RULES" :data="formData" :colon="true" @reset="onReset" @submit="onSubmit">
            <t-form-item label="登录账号" name="LoginName">
                <t-input  :readonly="true"  v-model="formData.LoginName" placeholder="请输入登录账号" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="账号名称" name="Name">
                <t-input v-model="formData.Name" placeholder="请输入账号名称" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="是否删除" name="IsDelete">
                <t-select v-model="formData.IsDelete2" :readonly="true">
                    <t-option key="true" label="是" value="true" />
                    <t-option key="false" label="否" value="false" />
                </t-select>
            </t-form-item>
            <t-form-item label="角色" name="IsDelete">
                <t-select :readonly="true" v-model="roleStrings" :options="roleList" placeholder="请选择角色" multiple />
            </t-form-item>
            <t-form-item label="身份证号" name="IdCard">
                <t-input v-model="formData.IdCard" placeholder="请输入身份证号" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item label="性别" name="Gender">
                <t-select v-model="formData.Gender" >
                    <t-option key="男" label="男" value="男" />
                    <t-option key="女" label="女" value="女" />
                </t-select>
            </t-form-item>
            <t-form-item label="邮件" name="Mail"  help="邮件可以接收系统发出的通知邮件">
                <t-input v-model="formData.Mail" placeholder="请输入邮件" @enter="onEnter"></t-input>
            </t-form-item>
            <t-form-item>
                <t-space size="small">
                    <t-button theme="primary" type="submit">提交</t-button>
                    <!-- <t-button theme="default" variant="base" type="reset">重置</t-button> -->
                    <!-- <t-button theme="default" variant="base" @click="back">返回</t-button> -->
                </t-space>
            </t-form-item>
            <t-input v-model="formData.Code" hidden></t-input>
            <t-input v-model="formData.Id" hidden></t-input>
        </t-form>
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $cookies from "@/utils/cookies";
import $api from "@/api/index";
import $router from "@/router/router";
import { useRouter } from "vue-router";
import { ref, reactive } from "vue";
import { MessagePlugin } from "tdesign-vue-next";
const route = useRouter();
// 获取当前路径的code和类型
const code =$cookies.getUserCode();
const type = route.currentRoute.value.params.type;
const roleStrings = ref([]); //角色选择结果
let roleList = ref([{ label: "全选", checkAll: true }]); //角色列表
const form = ref(null);

const GetRoleOptions = () => {
    $instance.get($api.role.GetRoleOptions).then((resp) => {
        resp.result.data.forEach((element) => {
            roleList.value.push(element);
        });
    });
};
GetRoleOptions();

const formData = reactive({
    Id: 0,
    Code: "",
    Name: "",
    LoginName: "",
    LoginPassword: "",
    IsDelete: false,
    IsDelete2: "false",
    Roles: [],
    IdCard:"",
    Gender:"",
    Mail:""
});

const FORM_RULES = {
    Phone: [{ telnumber:true,message: '请输入正确的手机号码'}],
    Mail:[ {email:true, ignore_max_length: true , message: '请输入正确的邮箱地址'}],
    IdCard:[{ idcard: true, message: '请输入正确的身份证号码' }]
};
const onReset = () => {
    MessagePlugin.success("重置成功");
};

const onSubmit = ({ validateResult, firstError }) => {
    formData.Roles = roleStrings.value;
    formData.IsDelete = JSON.parse(formData.IsDelete2);
    console.log(formData);
    if (validateResult === true) {
        let url = "";
        $instance.post($api.user.Edit, formData).then((resp) => {
            if (resp.success) {
                MessagePlugin.success("成功");
            }
        });
    } else {
        console.log("Validate Errors: ", firstError, validateResult);
        MessagePlugin.warning(firstError);
    }
};

const getTableEntity = () => {
    
    $instance
        .get($api.user.GetEntityByCode, {
            params: {
                code: code,
            },
        })
        .then((resp) => {
            formData.Code = resp.result.data.code;
            formData.Id = resp.result.data.id;
            formData.IsDelete = resp.result.data.isDelete;
            formData.IsDelete2 = resp.result.data.isDelete.toString();
            formData.LoginName = resp.result.data.loginName;
            formData.Name = resp.result.data.name;
            formData.Roles = resp.result.data.roles;
            formData.IdCard = resp.result.data.idCard;
            formData.Gender = resp.result.data.gender;
            roleStrings.value = resp.result.data.roles;
            console.log(resp.result.data);
            console.log(formData);
            console.log(roleList.value);
        });
};

getTableEntity();

const onEnter = (_, { e }) => {
    // 禁用 Input 组件，按下 Enter 键时，触发 submit 事件
    e.preventDefault();
};


</script>
