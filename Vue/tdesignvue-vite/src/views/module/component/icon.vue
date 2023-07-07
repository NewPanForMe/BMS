<template>
    <t-select
      v-model="value"
      placeholder="请选择"
      :popup-props="{ overlayInnerStyle: { width: '400px' } }"
      :onChange="onChange"
    >
      <t-option v-for="item in options" :key="item.stem" :value="item.stem" class="overlay-options">
        <div>
          <t-icon :name="item.stem" />
        </div>
      </t-option>
      <template #valueDisplay><t-icon :name="value" :style="{ marginRight: '8px' }" />{{ value }}</template>
    </t-select>
  </template>
  <script setup>
  import { ref ,defineEmits} from 'vue';
  import { manifest } from 'tdesign-icons-vue-next';
  // 获取全部图标的列表
  const options = ref(manifest);
  const value = ref('add');
  const emit = defineEmits(['dropDownValueChange'])
  const onChange =(e)=>{
    emit('dropDownValueChange', e)
  }
  </script>
  <style>
  .overlay-options {
    display: inline-block;
    font-size: 20px;
  }
  </style>