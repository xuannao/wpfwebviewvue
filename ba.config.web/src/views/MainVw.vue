<template>
  <n-config-provider :theme="theme" :theme-overrides="{ common: { fontWeightStrong: '600' } }">
    <n-dialog-provider>
      <n-layout style="height: 100vh;">
          <HeaderCp />
          <n-message-provider>
            <RouterView />
          </n-message-provider>
        </n-layout>
    </n-dialog-provider>
  </n-config-provider>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { darkTheme } from "naive-ui";
import HeaderCp from "@/components/HeaderCp.vue";

const theme = ref(null);

onMounted(async () => {
  const t = matchMedia('(prefers-color-scheme:dark)');
  theme.value = t.matches ? darkTheme : null;
  t.addEventListener('change', (e) => {
    theme.value = e.matches ? darkTheme : null;
  })




});
</script>

<style scoped></style>
