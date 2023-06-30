<template>
  <div id="module">
    <lay-layout>
      <!-- 头部开始 -->
      <lay-header>
        <layheader />
      </lay-header>
      <!-- 头部结束 -->
      <lay-layout>
        <!-- 菜单开始 -->
        <lay-side>
          <layside />
        </lay-side>
        <!-- 菜单结束 -->
        <!-- body开始 -->
        <lay-body>
          <tag />
          <router-view />
        </lay-body>
        <!-- body结束 -->
      </lay-layout>
    </lay-layout>
  </div>
</template>

<script>
import layside from '@/components/home/layside.vue';
import layheader from '@/components/home/layheader.vue';
import tag from '@/components/home/tag.vue';

export default {
  setup() {
    return {

    }
  },
  methods: {
 
  },
  components: { layside, layheader, tag }
}
</script>
