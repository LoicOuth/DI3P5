<template>
    <div class="w-full overflow-hidden bg-white rounded-lg shadow-lg">
        <div class="justify-between w-full px-6 py-4 sm:flex sm:items-center">
            <div class="text-center sm:text-left sm:flex-grow">
                <h4 class="leading-tight">
                    {{ props.title }}
                </h4>
            </div>
            <slot name="icon" />
        </div>
        <div class="px-6 py-4">
            <div class="mb-4">
                <p class="text-sm leading-normal">
                    <slot />
                </p>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const props = defineProps({
    title: {
        type: String,
        required: true
    }
})
</script>
