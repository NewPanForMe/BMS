//cookie
import cookies from 'vue-cookies'
//通讯组件
import axios from 'axios'
import { layer } from "@layui/layui-vue"
const instance = axios.create({
    //默认url
    baseURL: "/BmsV1Service",
})
//统一设置post请求头
instance.defaults.headers.post['Content-Type'] = "application/json";

//设置axios为form-data
//instance.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';
//instance.defaults.headers.get['Content-Type'] = 'application/x-www-form-urlencoded';
//instance.defaults.transformRequest = [function (data) {
//     let ret = ''
//     for (let it in data) {
//       ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&'
//     }
//     return ret
// }]
//添加请求拦截器
instance.interceptors.request.use(config => {
    console.log(config)
    //判断cookie是否存在
    var token = cookies.get("token");
    //存在,放入请求头
    config.headers.token = token;
    return config;
}, error => {
    window.$layer.msg(error);
    console.log(error);
    return;
});
//添加resp拦截器
instance.interceptors.response.use(resp => {
    //如果返回的结果为true
    console.log( window.$layer)
    if (!resp.data.success) {
        window.$layer.msg(resp.data.message);
        return ;
    }
    else {
        return resp.data;
    }
}, error => {
   console.log(error)
});

export default instance