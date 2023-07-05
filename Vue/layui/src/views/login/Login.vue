<template>
  <div class="login">
    <div class="login-content">
      <lay-form :model="model">
        <lay-form-item label="账户" prop="username">
          <lay-input v-model="model.username"></lay-input>
        </lay-form-item>
        <lay-form-item label="密码" prop="password">
          <lay-input v-model="model.password" type="password"></lay-input>
        </lay-form-item>
        <lay-form-item style="text-align: center;">
          <lay-button type="primary" @click="submit">登录</lay-button>
        </lay-form-item>
      </lay-form>
    </div>
  </div>
</template>

<script >
import { ref, reactive } from 'vue'
import { layer } from '@layui/layer-vue'

export default {
  setup() {
    const model = reactive({})
    return {
      model
    }
  },
  methods: {
    submit() {
      console.log(this.model)
      this.checkLogin()
      //  this.$router.push("/homepage")
    },
    checkLogin() {
      this.$instance.post(this.$api.checkUserName, this.model).then(resp => {
        console.log(resp)
      })
    }
  }
}
</script>