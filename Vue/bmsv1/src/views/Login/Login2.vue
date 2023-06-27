<template>
  <n-space vertical>
    <n-input v-model:value="value" type="text" placeholder="基本的 Input" />
    <n-input v-model:value="value" type="textarea" placeholder="基本的 Textarea" />
    <n-button type="info" size="large" attr-type="button" @click="loginClick">
      登录
    </n-button>
  </n-space>
</template>

<script>
import { defineComponent, ref } from "vue";

export default defineComponent({
  setup() {
    return {
      value: ref(null),
      loginClick() {
        console.log(this.value)
      }
    };
  }
});
</script>