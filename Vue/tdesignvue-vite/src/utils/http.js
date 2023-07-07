//通讯组件
import axios from "axios";
import cookie from "./cookies";
import { MessagePlugin } from "tdesign-vue-next";
const instance = axios.create({
    //默认url
    baseURL: "/BmsV1Service",
    timeout: 5000,
});
//统一设置post请求头
instance.defaults.headers.post["Content-Type"] = "application/json";
//添加请求拦截器
instance.interceptors.request.use(
    (config) => {
        console.log(config);
        //判断cookie是否存在
        var token = cookie.getToken();
        if (token) {
            //存在,放入请求头
            config.headers.Authorization = "Bearer " + token.token;
            config.headers.JwtVersion = token.jwtVersion;
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
            if (resp.data.result != null) {
                return resp.data;
            }
        } else if (resp.data.result == "401") {
            this.$router.push("/login");
            cookie.removeToken();
        } else {
            MessagePlugin.error(resp.data.result);
        }
        return Promise.reject(resp.data.message);
    },
    (error) => {
        console.log(error);
    }
);
export default instance;