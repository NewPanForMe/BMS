const api = {
    user: {
        GetUserList: window.config.baseUrl + "/User/GetList",
    },
    module: {
        GetModuleList: window.config.baseUrl + "/Module/GetList",
        Add: window.config.baseUrl + "/Module/Add",
        Edit: window.config.baseUrl + "/Module/Update",
        Delete: window.config.baseUrl + "/Module/Delete",
        GetEntityByCode: window.config.baseUrl + "/Module/GetEntityByCode",
    },
    login: {
        checkUserName: window.config.baseUrl + "/Login/Check",
    },
    refreshToken:{
        RefreshToken: window.config.baseUrl + "/RefreshToken/RefreshToken",
    },
    tree:{
        GetModuleTreeNode: window.config.baseUrl + "/Tree/ModuleTree",
    },
    menu:{
        GetMenu: window.config.baseUrl + "/Menu/Menu",
    },
    log:{
        GetLogList:window.config.baseUrl + "/NLog/GetLogList",
    }
}
export default api