import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import AutoImport from 'unplugin-auto-import/vite'
import { NaiveUiResolver } from 'unplugin-vue-components/resolvers'
import Components from 'unplugin-vue-components/vite'
import VueDevtools from 'vite-plugin-vue-devtools'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    VueDevtools(),
    AutoImport({
      imports: [
        'vue',
        {
          'naive-ui': [
            'useDialog',
            'useMessage',
            'useNotification',
            'useLoadingBar'
          ]
        }
      ]
    }),
    Components({
      resolvers: [NaiveUiResolver()]
    })
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
    server: {
        port: 6549,
        // open: true,
        // cors: true,
        // strictPort: true,
        https: false,
        proxy: {
            '/api': {
                target: 'http://localhost:6078',
                changeOrigin: true,
                // rewrite: (path) => path.replace(/^\/api/, '')
            }
        }
  },
  build: {
    outDir: '../ba.config/Assets/wwwroot',
    emptyOutDir: true,
    //大小限制
    assetsInlineLimit: 4096000,
    chunkSizeWarningLimit: 4096000,
  }
})
