export default defineNuxtPlugin(() => {
    const nuxtApp = useNuxtApp()

    nuxtApp.hooks.hook("page:finish", async () => {
        window.scrollTo(0, 0)
    })
})
