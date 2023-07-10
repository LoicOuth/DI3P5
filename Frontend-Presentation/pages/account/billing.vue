<template>
    <div class="w-full m-5">
        a
    </div>
</template>

<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("account.navbar.billing")}`
})

definePageMeta({
    layout: "account",
    middleware: ["auth-middleware"]
})
</script>
