<template>
  <n-layout-header :bordered="true">
    <n-flex id="title" align="center" justify="space-between" style="margin-left: 5px; margin-right: 5px;">
      <n-flex align="center" :wrap="false" :size="2">
        <n-button :quaternary="true" @click="newproject">{{ t('i18xin-jian') }}</n-button>
        <n-button :quaternary="true">{{ t('i18da-kai') }}</n-button>
        <n-button :quaternary="true">{{ t('i18bao-cun') }}</n-button>
        <n-divider vertical />
        <n-menu v-model:value="value" :options="menuOptions" dropdown-placement="left-start" mode="horizontal"
          responsive style="height: 40px" />
      </n-flex>
      <n-select v-model:value="languagelabel" :options="options" @update:value="languageupdate"
        style="width: 120px"></n-select>
      <!-- <n-flex align="center" :size="0">
                  <n-icon size="42" @click="app.minimize()">
                      <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                          viewBox="0 0 24 12">
                          <path d="M9 6h7" fill="none" stroke="currentColor" stroke-width="1" stroke-linecap="round"
                              stroke-linejoin="round"></path>
                      </svg>
                  </n-icon>
                  <n-icon v-if="ismax" size="42" @click="sizechenge">
                      <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                          viewBox="0 0 24 24">
                          <g fill="none">
                              <path path d="M8.2,8.9c0-0.3,0.3-0.6,0.6-0.6h1c0.2,0,0.4-0.2,0.4-0.4s-0.2-0.4-0.4-0.4h-1c-0.8,0-1.4,0.6-1.4,1.4v1
      c0,0.2,0.2,0.4,0.4,0.4s0.4-0.2,0.4-0.4V8.9z M8.2,15.1c0,0.3,0.3,0.6,0.6,0.6h1c0.2,0,0.4,0.2,0.4,0.4c0,0.2-0.2,0.4-0.4,0.4h-1
      c-0.8,0-1.4-0.6-1.4-1.4v-1c0-0.2,0.2-0.4,0.4-0.4s0.4,0.2,0.4,0.4V15.1z M15.1,8.2c0.3,0,0.6,0.3,0.6,0.6v1c0,0.2,0.2,0.4,0.4,0.4
      c0.2,0,0.4-0.2,0.4-0.4v-1c0-0.8-0.6-1.4-1.4-1.4h-1c-0.2,0-0.4,0.2-0.4,0.4s0.2,0.4,0.4,0.4H15.1z M15.8,15.1
      c0,0.3-0.3,0.6-0.6,0.6h-1c-0.2,0-0.4,0.2-0.4,0.4c0,0.2,0.2,0.4,0.4,0.4h1c0.8,0,1.4-0.6,1.4-1.4v-1c0-0.2-0.2-0.4-0.4-0.4
      c-0.2,0-0.4,0.2-0.4,0.4V15.1z" fill="currentColor"></path>
                          </g>
                      </svg>
                  </n-icon>
                  <n-icon v-else size="42" @click="sizechenge">
                      <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                          viewBox="0 0 24 24">
                          <g fill="none">
                              <path d="M8.9,7.5h6.2c0.8,0,1.4,0.6,1.4,1.4v6.2c0,0.8-0.6,1.4-1.4,1.4H8.9c-0.8,0-1.4-0.6-1.4-1.4V8.9C7.5,8.1,8.1,7.5,8.9,7.5z
       M8.9,8.2c-0.3,0-0.6,0.3-0.6,0.6v6.2c0,0.3,0.3,0.6,0.6,0.6h6.2c0.3,0,0.6-0.3,0.6-0.6V8.9c0-0.3-0.3-0.6-0.6-0.6H8.9z"
                                  fill="currentColor"></path>
                          </g>
                      </svg>
                  </n-icon>
                  <n-icon class="close-icon" size="42" @click="close">
                      <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                          viewBox="0 0 24 24">
                          <g>
                              <path stroke="currentColor" d="M16.5,16.5l-9-9" />
                              <path stroke="currentColor" d="M16.5,7.5l-9,9" />
                          </g>
                      </svg>
                  </n-icon>
              </n-flex> -->
    </n-flex>
  </n-layout-header>
</template>

<script setup>
// import file from "@/api/file";
import router, { routemap } from "@/router";
import { onMounted, ref, watch } from "vue";
import { useI18n } from 'vue-i18n'
import tool from "@/utils/tool";
import useprojectst from "@/stores/projectst";

const { locale, t } = useI18n()
const projectst = useprojectst()

const options = [
  {
    label: '中文',
    value: 'zh',
  },
  {
    label: 'English',
    value: 'en',
  },
]

locale.value = tool.language.get() ?? 'zh'

const languagelabel = ref()

const value = ref(null)

const menuOptions = ref([
  {
    label: t('i18ying-jian-zu-tai'),
    key: routemap.valveisland,
  },
  {
    label: t('i18bu-xu-bian-ji'),
    key: routemap.sequence,
  },
])

onMounted(() => {
  let language = tool.language.get()
  if (language == null) {
    language = 'zh'
  }
  languagelabel.value = options.find(item => item.value === language).label
  locale.value = language
  tool.language.set(language)
})
async function newproject() {
  await projectst.newprojectasync(t("i18xin-jian-xiang-mu"))
}


watch(value, (newval) => {
  router.push(newval);
});

function languageupdate(val) {
  if (val === tool.language.get()) {
    return
  }
  tool.language.set(val)
  window.location.reload()
}

</script>

<style scoped>
.n-icon {
  cursor: pointer;
  opacity: 0.6;
}

.n-icon:hover {
  opacity: 1;
}

.close-icon:hover svg g path {
  stroke: red;
}
</style>
