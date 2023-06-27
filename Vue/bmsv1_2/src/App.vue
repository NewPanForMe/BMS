<template>
  <n-message-provider>
    <meassage />
  </n-message-provider>
  <n-loading-bar-provider>
    <loadingbar />
  </n-loading-bar-provider>
  <RouterView />
</template>
<script >
import { RouterLink, RouterView } from 'vue-router'
//message组件
import meassage from './components/message/message_api.vue'

//loadingbar组件
import loadingbar from './components/message/loadingBar.vue'

//模板组件
import native from 'naive-ui'


export default {
  components: { meassage, native, loadingbar }
}
</script>

