<template>
  <div class="container">
    <n-layout has-sider style="height: 100%;">
      <!----侧边栏开始-->
      <n-layout-sider bordered :collapsed-width="64" :width="240" :native-scrollbar="false" collapse-mode="width"
        show-trigger :inverted="inverted">
        <n-menu :inverted="inverted" :options="menuOptions" />
      </n-layout-sider>
      <!----侧边栏结束-->
      <n-layout>
        <!----头部开始-->
        <n-layout-header :inverted="inverted" style="height: 5%;" bordered>
          <!-- <div>
            <n-button text size="large" @click="SideCollapseClick()">
              <n-icon :size="45" v-if="collapsed">
                <LayoutSidebarLeftExpand />
              </n-icon>
              <n-icon :size="45" v-else="collapsed==false">
                <LayoutSidebarLeftCollapse />
              </n-icon>
            </n-button>
          </div> -->
          <div class="flex">
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div>
              <n-dropdown :options="menu" :inverted="inverted" size="huge">
                <n-button text class=" flex-button  ">欢迎你，xxxx</n-button>
              </n-dropdown>
              <n-button text @click="showDrawer()" style="margin: 15px;">
                <template #icon>
                  <n-icon size="40px">
                    <MoreVertical28Filled />
                  </n-icon>
                </template>
              </n-button>
            </div>

          </div>
        </n-layout-header>
        <!----头部结束-->
        <!----内容开始-->
        <n-layout-content style="height: 90%;" :inverted="inverted">
          <n-scrollbar bordered style="height: 5%;border: 1px">
            <n-tag closable>
              爱在西元前
            </n-tag>
          </n-scrollbar>
          <iframe src="https://www.naiveui.com/zh-CN/os-theme/components/button"
            style="width: 100%;height: 93%;border: none;"></iframe>
        </n-layout-content>
        <!----内容结束-->
        <!----foot开始-->
        <n-layout-footer :inverted="inverted" style="height: 5%;">成府路</n-layout-footer>
        <!----foot结束-->
      </n-layout>
    </n-layout>
  </div>
  <n-drawer v-model:show="active" :width="502" :placement="placement">
    <n-drawer-content title="个性设置">
      <div>
        对比度:
      </div>
    </n-drawer-content>
  </n-drawer>
</template>
<script>
import { RouterLink, RouterView } from 'vue-router'

import { h, defineComponent, ref } from "vue";
import { NIcon, darkTheme } from "naive-ui";
import {
  BookOutline as BookIcon,
  PersonOutline as PersonIcon,
  WineOutline as WineIcon,
  PersonCircleOutline as UserIcon,
  Pencil as EditIcon,
  LogOutOutline as LogoutIcon
} from "@vicons/ionicons5";

import { LayoutSidebarLeftExpand, LayoutSidebarLeftCollapse } from '@vicons/tabler'
import { MoreVertical28Filled } from '@vicons/fluent'

import menu from '../system/menu'

function renderIcon(icon) {
  return () => h(NIcon, null, { default: () => h(icon) });
}

const menuOptions = [
  {
    label: "且听风吟",
    key: "hear-the-wind-sing",
    icon: renderIcon(BookIcon)
  },
  {
    label: "1973年的弹珠玩具",
    key: "pinball-1973",
    icon: renderIcon(BookIcon),
    disabled: true,
    children: [
      {
        label: "鼠",
        key: "rat"
      }
    ]
  },
  {
    label: "寻羊冒险记",
    key: "a-wild-sheep-chase",
    disabled: true,
    icon: renderIcon(BookIcon)
  },
  {
    label: "舞，舞，舞",
    key: "dance-dance-dance",
    icon: renderIcon(BookIcon),
    children: [
      {
        type: "group",
        label: "人物",
        key: "people",
        children: [
          {
            label: "叙事者",
            key: "narrator",
            icon: renderIcon(PersonIcon)
          },
          {
            label: "羊男",
            key: "sheep-man",
            icon: renderIcon(PersonIcon)
          }
        ]
      },
      {
        label: "饮品",
        key: "beverage",
        icon: renderIcon(WineIcon),
        children: [
          {
            label: "威士忌",
            key: "whisky"
          }
        ]
      },
      {
        label: "食物",
        key: "food",
        children: [
          {
            label: "三明治",
            key: "sandwich"
          }
        ]
      },
      {
        label: "过去增多，未来减少",
        key: "the-past-increases-the-future-recedes"
      }
    ]
  }
];


export default defineComponent({
  setup() {
    const placement = ref("right");
    return {
      inverted: ref(false),
      collapsed: ref(false),
      active: ref(false),
      darkTheme,
      theme: ref(null),
      menu,
      menuOptions
    };
  },
  components: {
    LayoutSidebarLeftExpand,
    LayoutSidebarLeftCollapse,
    MoreVertical28Filled,
  },
  mounted() {
    let hash = window.location
    console.log(hash)

    this.loadHome();
    console.log("页面最高：" + window.innerHeight)
  },
  methods: {
    SideCollapseClick: function () {
      if (this.collapsed) this.collapsed = false;
      else this.collapsed = true;
    },
    showDrawer: function () {
      this.active = true

    },
    loadHome: function () {
      let inver = this.$cookies.get("inverted");
      console.log(inver)
      this.inverted = JSON.parse(inver);;
    },
    invertedChecked: function (value) {
      this.inverted = JSON.parse(value);
      this.$cookies.remove("inverted");
      this.$cookies.set("inverted", value)
    }
  }
});
</script>