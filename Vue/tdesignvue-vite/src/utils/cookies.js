import cookies from "vue-cookies";
const tokenName = "token";
const jwtVersion = "JwtVersion";
const refreshToken = "RefreshToken";
const userName = "Name";
const cookie = {
    saveToken(resp) {
        const token =resp.token;
        const version = resp.jwtVersion;
        const refresh = resp.refreshToken;
        const name = resp.name;
        cookies.set(tokenName, token);
        cookies.set(jwtVersion, version);
        cookies.set(refreshToken, refresh);
        cookies.set(userName, name);
    },
    getToken() {
        var param = {
            tokenName: cookies.get(tokenName),
            jwtVersion: cookies.get(jwtVersion),
            refreshToken: cookies.get(refreshToken),
            userName: cookies.get(userName),
        };
        return param;
    },
    getRefreshToken() {
        return cookies.get(refreshToken);
    },
    removeToken() {
        cookies.remove(tokenName);
        cookies.remove(jwtVersion);
    },
    removeRefreshToken() {
        cookies.remove(refreshToken);
    },
    isTokenExits() {
        return cookies.isKey(tokenName);
    },
};
export default cookie;
