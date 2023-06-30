<template>
    <div class="body-top">
        <template v-for="item in list" :key="item.name">
            <lay-tag closable :color="color" variant="light" @close="closeTag(item.name)" @click="clickTag(item.path)"
                path="item.path">{{ item.name }}</lay-tag>
            &nbsp;
        </template>
    </div>
</template>
<script>
import { ref } from 'vue'

export default {
    setup() {
        return {
            color: "#165DFF",
            list: ref([{
                name: "homepage",
                path: "/homepage"
            }]),
            newlist: ref([]),
        }
    },
    watch: {
        $route(to, from) {
            console.log(to)
            console.log(from)
            var param = {
                name: to.name,
                path: to.path
            }
            if (this.list.findIndex(x => x.name === param.name) === -1) {
                this.list.push(param)
            }


            console.log(this.list.findIndex(x => x.name === param.name) === -1)
            console.log(this.list)
        }
    },
    methods: {
        closeTag(e) {
            if (this.list.length < 2) {
                this.$layer.msg("不可关闭最后一个标签页")
                this.$router.push("/homepage")
                return;
            }

            for (let index = 0; index < this.list.length; index++) {
                const element = this.list[index];
                if (element.name === e) {
                    this.list.splice(index, 1)
                    this.$router.push(this.list[index-1].path)
                }
                else { }
            }
        },
        clickTag(tag) {
            this.$router.push(tag)
        }
    }
}
</script>