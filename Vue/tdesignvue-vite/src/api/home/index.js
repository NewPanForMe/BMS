const api = {
    user: {
        GetUserList: window.config.baseUrl + "/User/GetList",
    },
    module: {
        GetModuleList: window.config.baseUrl + "/Module/GetList",
        Add: window.config.baseUrl + "/Module/Add",
        GetEntityByCode: window.config.baseUrl + "/Module/GetEntityByCode",
    },
    login: {
        checkUserName: window.config.baseUrl + "/Login/Check",
    }
}
export default api