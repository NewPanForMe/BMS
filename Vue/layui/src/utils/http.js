//cookie
import cookies from 'vue-cookies'
//通讯组件
import axios from 'axios'

const instance = axios.create({
    //默认url
    baseURL: window.config.baseUrl,
})
//统一设置post请求头
//instance.defaults.headers.post['Content-Type'] = "application/json";
//instance.defaults.headers.get['Access-Control-Allow-Origin'] = "*";
//instance.defaults.headers.post['Access-Control-Allow-Origin'] = "*";

//添加请求拦截器
instance.interceptors.request.use(config => {
    console.log("1111")
    console.log(config)
    //判断cookie是否存在
    var token = cookies.get("token");
    //存在,放入请求头
    config.headers.token = token;
    return config;
}, error => {
    return config;
});
//添加resp拦截器
instance.interceptors.response.use(resp => {
    //如果返回的结果为true
    if (!resp.data.success) {
        window.$layer.msg(resp.data.message);
        return Promise.reject(resp.data.Message);
    }
    else {
        return resp.data;
    }
}, error => {
   console.log(error.message)
});

export default instance