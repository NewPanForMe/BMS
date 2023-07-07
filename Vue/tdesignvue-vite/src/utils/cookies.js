import cookies from "vue-cookies";
const tokenName = "token";
const jwtVersion = "JwtVersion";
const cookie = {
    saveToken(token, version) {
        cookies.set(tokenName, token);
        cookies.set(jwtVersion, version);
    },
    getToken() {
        var param = {
            tokenName: cookies.get(tokenName),
            jwtVersion: cookies.get(jwtVersion),
        };
        return param;
    },
    removeToken() {
        cookies.remove(tokenName);
        cookies.remove(jwtVersion);
    },
    isTokenExits() {
        return cookies.isKey(tokenName);
    },
};
export default cookie;
