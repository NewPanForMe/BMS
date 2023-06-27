//cookie
import cookies from 'vue-cookies/vue-cookies'

//通讯组件
import axios from 'axios'

const instance = axios.create({
    //默认url
    baseURL: "https://localhost:7090/api/",
})
//统一设置post请求头
//instance.defaults.headers.post['Content-Type'] = "application/json";

//添加请求拦截器
instance.interceptors.request.use(config => {
    //判断cookie是否存在
    var token = cookies.get("token");
    //存在,放入请求头
    config.headers.token = token;
    window.$loadingBar.start();
    return config;
}, error => {
    window.$loadingBar.error();
    return config;
});
//添加resp拦截器
instance.interceptors.response.use(resp => {
    //如果返回的结果为true
    if (!resp.data.success) {
        window.$loadingBar.error();
        window.$message.error(resp.data.message);
        return Promise.reject(resp.data.Message);
    }
    else {
        window.$loadingBar.finish();
        return resp.data;
    }
}, error => {
    window.$loadingBar.error();
    window.$message.error(error);
});

export default instance