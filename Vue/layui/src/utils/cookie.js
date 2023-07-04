import cookies from 'vue-cookies'
const tokenName = "token";
export default {
    saveToken(token){
       return cookies.set(tokenName,token)
    },
    getToken(){
        return cookies.get(tokenName);
    },
    removeToken(){
        return cookies.remove(tokenName);
    },
    isTokenExits(){
        return cookies.isKey(tokenName);
    }
}
