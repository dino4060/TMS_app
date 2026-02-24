import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');

  return {
    plugins: [
      react({
        // babel: {
        //   plugins: [['babel-plugin-react-compiler']],
        // },
      }),
    ],
    server: {
      port: parseInt(env.PORT) || 5173,
      strictPort: true,
      open: true,
    },
  }
})
