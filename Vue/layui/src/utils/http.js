
//通讯组件
import axios from "axios";
import { layer } from "@layui/layui-vue";
const instance = axios.create({
    //默认url
    baseURL: "/BmsV1Service",
});
//统一设置post请求头
instance.defaults.headers.post["Content-Type"] = "application/json";

//添加请求拦截器
instance.interceptors.request.use((config) => {
        console.log(config);
        //判断cookie是否存在
        var token = this.$cookies.getToken();
        if (token) {
            //存在,放入请求头
            config.headers.Authorization ="Bearer "+ token;
        }else{
            this.$router.push("/login")
            this.$cookies.removeToken();
            return false;
        }
        return config;
    },
    (error) => {
        window.$layer.msg(error);
        console.log(error);
        return;
    }
);
//添加resp拦截器
instance.interceptors.response.use(
    (resp) => {
        //如果返回的结果为true
        console.log(window.$layer);
        if (resp.data.success == true) {
            return resp.data;
        } 
        else if(resp.data.result=="400"){
             this.$router.push("/login")
             this.$cookies.removeToken();
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
