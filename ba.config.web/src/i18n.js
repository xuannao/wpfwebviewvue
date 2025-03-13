// src/i18n.js
import { createI18n } from 'vue-i18n'
import zh from './assets/locales/zh.json'
import en from './assets/locales/en.json'

const i18n = createI18n({
  legacy: false, // 禁用 legacy 模式
  globalInjection: true, // 全局注入 $t 等函数

  locale: 'zh', // 设置默认语言
  messages: {
    zh,
    en,
  },
})

export default i18n
