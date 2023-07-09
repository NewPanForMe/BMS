import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import {resolve} from 'path'
export default defineConfig({
  plugins: [vue(),vueJsx()],
  resolve:{
    alias:{
      "@":resolve(__dirname,"./src")
    }
  },  
  server: {
    //方式二:设置多个代理
    proxy: {
      "/BmsV1Service": {
        target: "http://localhost:20000",
        changeOrigin: true, //必须要开启跨域
        rewrite: (path) => path.replace(/\/BmsV1Service/, "BmsV1Service"), // 路径重写
      },
    },
  },
})
