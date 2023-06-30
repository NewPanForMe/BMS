<template>
  <lay-menu :selected-key="selectedKey" @change-selected-Key="changeSelectedKey" @change-open-keys="changeOpenKeys"
    v-model:openKeys="openKeys2" :tree="true">
    <lay-menu-item id="/homepage">首页</lay-menu-item> 
    <lay-sub-menu id="1">
      <template #title>
        系统管理
      </template>
      <lay-menu-item id="/order">订单</lay-menu-item>
      <lay-menu-item id="/user">用户</lay-menu-item>
    </lay-sub-menu>
  </lay-menu>
</template>
<script>

import { ref } from 'vue'

export default {
  setup() {

    const openKeys2 = ref(["7"])
    const selectedKey = ref("3")

    return {
      openKeys2,
      selectedKey,
    }
  },
  methods: {
    changeSelectedKey(e) {
      this.selectedKey = e;
      this.$router.push(e)
    },
    changeOpenKeys(val) {
      this.openKeys2 = val;
    }
  }
}

</script>