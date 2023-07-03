const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
    transpileDependencies: true,
    devServer: {
        port: 8081,
        host: "localhost",
        open: true,
        proxy: {
            // 高德地图 逆地理编码 跨域问题
            "BmsV1Service": {
                target: "http://localhost:20000/BmsV1Service",
                changeOrigin: true,
                pathRewrite: {
                    "^/BmsV1Service": "",
                },
            },
        },
    },
});
