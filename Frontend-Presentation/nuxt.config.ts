// https://v3.nuxtjs.org/api/configuration/nuxt.config
export default defineNuxtConfig({
    css: ["~/assets/css/tailwind.css"],
    build: {
        postcss: {
            postcssOptions: {
                plugins: {
                    tailwindcss: {},
                    autoprefixer: {}
                }
            }
        }
    },

    modules: [
        "@nuxtjs/i18n"
    ],

    loading: {
        color: "#E71D36"
    },

    i18n: {
        locales: [
            {
                code: "en",
                file: "en-US.json",
                iso: "en-US",
                name: "English",
                flag: "gb"
            },
            {
                code: "fr",
                file: "fr-FR.json",
                iso: "fr-FR",
                name: "Français",
                flag: "fr"
            },
            {
                code: "es",
                file: "es-ES.json",
                iso: "es-ES",
                name: "Español",
                flag: "es"
            }
        ],
        strategy: "prefix_except_default",
        lazy: true,
        langDir: "lang",
        defaultLocale: "en",
        baseUrl: process.env.BASE_URL
    },

    imports: {
        dirs: ["composables/**"]
    },

    publicRuntimeConfig: {
        API_BASE_URL: process.env.API_BASE_URL,
        BASE_URL: process.env.BASE_URL,
        CMS_CALLBACK_URL: process.env.CMS_CALLBACK_URL,
        AUTH_SCOPE: process.env.AUTH_SCOPE,
        AUTH_CLIENT_ID: process.env.AUTH_CLIENT_ID,
        AUTH_CLIENT_SECRET: process.env.AUTH_CLIENT_SECRET
    }
})
