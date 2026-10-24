import { defineConfig } from "vitest/config";

export default defineConfig({
  test: {
    environment: "jsdom",
    setupFiles: ["./tests/setup/setup.ts"],
    include: ["tests/**/*.test.tsx"],
    fileParallelism: false,
    maxWorkers: 1,
    restoreMocks: true,
  },
});
