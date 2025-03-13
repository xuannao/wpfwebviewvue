import './assets/main.css'

import "vfonts/Lato.css";
import "vfonts/FiraCode.css";

import { createApp } from 'vue'
import App from './App.vue'
import router from "./router";
import { createPinia } from 'pinia'
import i18n from './i18n'

createApp(App)
  .use(i18n)
  .use(createPinia())
  .use(router)
  .mount('#app')
