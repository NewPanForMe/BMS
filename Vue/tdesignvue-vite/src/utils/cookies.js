import cookies from "vue-cookies";
const tokenName = "AccessToken";
const jwtVersion = "JwtVersion";
const refreshToken = "RefreshToken";
const userName = "Name";
const userCode = "UserCode";

const cookie = {
    saveToken(resp) {
        const token =resp.token;
        const version = resp.jwtVersion;
        const refresh = resp.refreshToken;
        const name = resp.name;
        const code = resp.code;
        cookies.set(tokenName, token);
        cookies.set(jwtVersion, version);
        cookies.set(refreshToken, refresh);
        cookies.set(userName, name);
        cookies.set(userCode, code);
    },
    getToken() {
        var param = {
            tokenName: cookies.get(tokenName),
            jwtVersion: cookies.get(jwtVersion),
            refreshToken: cookies.get(refreshToken),
            userName: cookies.get(userName),
            userCode: cookies.get(userCode),
        };
        return param;
    },
    getRefreshToken() {
        return cookies.get(refreshToken);
    },
    getUserCode() {
        return cookies.get(userCode);
    },
    removeToken() {
        cookies.remove(tokenName);
        cookies.remove(jwtVersion);
        cookies.remove(refreshToken);
        cookies.remove(userName);
        cookies.remove(userCode);
    },
    removeRefreshToken() {
        cookies.remove(refreshToken);
    },
};
export default cookie;
