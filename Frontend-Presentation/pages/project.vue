<template>
    <div>
        <p>Project</p>
    </div>
</template>
<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("navbar.project")}`
})
definePageMeta({
    layout: "default"
})
</script>
