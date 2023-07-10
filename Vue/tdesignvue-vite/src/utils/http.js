//通讯组件
import axios from "axios";
import router from "../router/router";
import cookie from "./cookies";
import api from "../api/index";
import { MessagePlugin } from "tdesign-vue-next";
const instance = axios.create({
    //默认url
    baseURL: "/BmsV1Service",
});
//统一设置post请求头
instance.defaults.headers.post["Content-Type"] = "application/json";
instance.defaults.headers.get["Content-Type"] = "application/json";
//添加请求拦截器
instance.interceptors.request.use(
    (config) => {
        console.log(config);
        //判断cookie是否存在
        var token = cookie.getToken();
        if (token) {
            //存在,放入请求头
            config.headers.Authorization = "Bearer " + token.tokenName;
            config.headers.JwtVersion = token.jwtVersion;
        }
        return config;
    },
    (error) => {
        console.log(error);
        return Promise.reject(error);
    }
);
//添加resp拦截器
instance.interceptors.response.use(
    (resp) => {
        console.log(resp);
        //如果返回的结果为true
        if (resp.data.success == true) {
            if (resp.data != null) {
                return resp.data;
            }
        } else {
            MessagePlugin.error(resp.data.result);
        }

        return Promise.reject(resp.data.message);
    },
    (error) => {
        console.log(error);
        if (error.response.status == "401") {
            let refreshToken = cookie.getRefreshToken();
            instance
                .post(api.refreshToken.RefreshToken, {
                    token: refreshToken,
                })
                .then((resp) => {
                    console.log(resp);
                    cookie.removeToken();
                    const token = resp.result.token;
                    const jwtVersion = resp.result.jwtVersion;
                    cookie.saveToken(token, jwtVersion);
                });
        }
    }
);
export default instance;
