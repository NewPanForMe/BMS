<template>
    <div class="content">
        <t-space break-line>
            <t-card>
                <div id="contain" style="height: 400px; width: 480px"></div>
            </t-card>
        </t-space>
    </div>
</template>
<script setup lang="jsx">
import $instance from "@/utils/http";
import $api from "@/api/index";
import $router from "@/router/router";
import { reactive, ref, onMounted } from "vue";
import * as echarts from "echarts";

let chart1Data = ref([]);
const initChart1 = (id) => {
    let newPromise = new Promise((resolve) => {
        resolve();
    });
    //然后异步执行echarts的初始化函数
    newPromise.then(() => {
        //	此dom为echarts图标展示dom
        let chart = echarts.init(document.getElementById(id));
        var option = {
            title: {
                text: "消费类型",
            },
            tooltip: {
                trigger: "item",
            },
            legend: {
                orient: "horizontal",
                type:"scroll",
                　top:"7%"//与上方的距离 可百分比% 可像素px
            },
            series: [
                {
                    name: "消费类型",
                    type: "pie",
                    radius: "50%",
                    data: chart1Data.value,
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: "rgba(0, 0, 0, 0.5)",
                        },
                    },
                },
            ],
        };
        chart.setOption(option);
    });
};
const getChart1Data = () => {
    $instance.get($api.chart.GetTypeChart).then((resp) => {
        console.log(resp);
        if (resp.success) {
            chart1Data.value = resp.result.data;
            initChart1("contain");
        }
    });
};

getChart1Data();
</script>
