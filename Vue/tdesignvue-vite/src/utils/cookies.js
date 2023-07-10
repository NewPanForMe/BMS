import cookies from "vue-cookies";
const tokenName = "token";
const jwtVersion = "JwtVersion";
const refreshToken = "RefreshToken";
const cookie = {
    saveToken(token, version) {
        cookies.set(tokenName, token);
        cookies.set(jwtVersion, version);
    },
    saveRefreshToken(refresh) {
        cookies.set(refreshToken, refresh);
    },
    getToken() {
        var param = {
            tokenName: cookies.get(tokenName),
            jwtVersion: cookies.get(jwtVersion),
            refreshToken: cookies.get(refreshToken),
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
    isTokenExits() {
        return cookies.isKey(tokenName);
    },
};
export default cookie;
