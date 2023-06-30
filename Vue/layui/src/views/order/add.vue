<template>
    <lay-card>
        <lay-form :model="model">
            <lay-form-item label="账户" prop="username">
                <lay-input v-model="model.username"></lay-input>
            </lay-form-item>
            <lay-form-item label="密码" prop="password">
                <lay-input v-model="model.password" type="password"></lay-input>
            </lay-form-item>
            <lay-form-item label="描述" prop="desc">
                <lay-textarea placeholder="请输入描述" v-model="model.describe"></lay-textarea>
            </lay-form-item>
            <lay-form-item style="text-align: center;">
                <lay-button type="primary" @click="submit">提交</lay-button>
                <lay-button type="default" @click="">重置</lay-button>
            </lay-form-item>
        </lay-form>
    </lay-card>
</template>
  
<script setup>
import { ref, reactive } from 'vue'
import { layer } from '@layui/layer-vue'

const model = reactive({})

const submit = () => {
    layer.msg(`${JSON.stringify(model)}`, { time: 2000 });
};
</script>