<template>
    <LoaderComponent class="w-full h-screen justify-center items-center flex" :is-small="false" />
</template>
<script lang="ts" setup>
definePageMeta({
    layout: false
})
const auth = useAuth()
const route = useRoute()
await useAsyncData(() => auth.login(route.query.code as string), {
    server: false
})
</script>
