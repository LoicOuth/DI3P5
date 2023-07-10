import createBundler from "@bahmutov/cypress-esbuild-preprocessor"
import { addCucumberPreprocessorPlugin } from "@badeball/cypress-cucumber-preprocessor"
import { createEsbuildPlugin } from "@badeball/cypress-cucumber-preprocessor/esbuild"
import { defineConfig } from "cypress"

export default defineConfig({
    e2e: {
        specPattern: "**/*.feature",
        setupNodeEvents (on: Cypress.PluginEvents, config: Cypress.PluginConfigOptions): Cypress.PluginConfigOptions {
            addCucumberPreprocessorPlugin(on, config)

            on(
                "file:preprocessor",
                createBundler({
                    plugins: [createEsbuildPlugin(config)]
                })
            )

            return config
        }
    }
})
