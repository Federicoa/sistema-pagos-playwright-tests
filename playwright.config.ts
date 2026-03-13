import { defineConfig, devices } from '@playwright/test';

/**
 * Read environment variables from file.
 * https://github.com/motdotla/dotenv
 */
// import dotenv from 'dotenv';
// import path from 'path';
// dotenv.config({ path: path.resolve(__dirname, '.env') });

/**
 * See https://playwright.dev/docs/test-configuration.
 */
export default defineConfig({

  testDir: './tests',

  globalSetup: require.resolve('./tests/setup/global.setup'),

  fullyParallel: false,

  retries: process.env.CI ? 2 : 0,

  workers: process.env.CI ? 1 : 1,
  reporter: 'html',

  use: {
    baseURL: 'http://localhost:8080',
    trace: 'on-first-retry',
    screenshot: 'only-on-failure', 
    video: 'retain-on-failure'
  },

  projects: [

    {
      name: 'chromium-gerente',
      use: {
        ...devices['Desktop Chrome'],
        storageState: 'playwright/.auth/gerente.json'
      }
    },

    {
      name: 'chromium-empleado',
      use: {
        ...devices['Desktop Chrome'],
        storageState: 'playwright/.auth/empleado.json'
      }
    }

  ]

});
