//通讯组件
import axios from "axios";
import cookie from '@/utils/cookie'
import layer from '@layui/layui-vue'
import {useRouter} from 'vue-router'

const instance = axios.create({
    //默认url
    baseURL: "/BmsV1Service",
});
//统一设置post请求头
instance.defaults.headers.post["Content-Type"] = "application/json";
//添加请求拦截器
instance.interceptors.request.use((config) => {
        console.log( cookie);
        //判断cookie是否存在
        var token =cookie.getToken();
        if (token) {
            //存在,放入请求头
            config.headers.Authorization ="Bearer "+ token;
            return false;
        }
        return config;
    },
    (error) => {
        layer.msg(error);
        console.log(error);
        return;
    }
);
//添加resp拦截器
instance.interceptors.response.use(
    (resp) => {
        //如果返回的结果为true
        if (resp.data.success == true) {
            return resp.data;
        } 
        else if(resp.data.result=="400"){
            useRouter().push("/login")
             cookie.removeToken();
             return false;
        }
        else {
            this.$layer.msg(resp.data.result);
            return false;
        }
    },
    (error) => {
        console.log(error);
    }
);

export default instance;