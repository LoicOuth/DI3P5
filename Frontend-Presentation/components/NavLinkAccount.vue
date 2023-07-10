<template>
    <div class="flex flex-col items-center justify-center w-full">
        <NuxtLink
            :to="props.to"
            class="flex flex-col items-center my-6 cursor-pointer"
            @click="emits('link-clicked')"
        >
            <slot />
            <h6 ref="link" class="p-2 text-center" :class="$route.path === props.to ? 'text-primary' : ''">
                {{ props.text }}
            </h6>
        </NuxtLink>
        <span class="w-1/2 h-0.5 bg-slate-200" />
    </div>
</template>

<script lang="ts" setup>
const emits = defineEmits(["link-clicked"])
const props = defineProps({
    to: {
        type: String,
        required: true
    },

    text: {
        type: String,
        required: true
    }
})
</script>
