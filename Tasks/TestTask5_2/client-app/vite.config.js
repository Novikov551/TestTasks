import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  build: {
    outDir: '../src/TestTask5_2.Api/wwwroot',
    emptyOutDir: true
  },
  server: {
    proxy: {
      '/api': 'https://localhost:5001'
    }
  }
});
