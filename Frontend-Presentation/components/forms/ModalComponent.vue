<template>
    <div class="fixed inset-0 z-10 overflow-y-auto" aria-labelledby="modal-title" role="dialog" aria-modal="true">
        <div class="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:block sm:p-0">
            <div class="fixed inset-0 bg-black bg-opacity-50" aria-hidden="true" />

            <span class="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>

            <div
                class="inline-block w-full overflow-hidden text-left align-bottom transform bg-white rounded-lg shadow-xl sm:my-8 sm:align-middle sm:w-[50%]"
            >
                <div class="px-4 pt-5 pb-4 bg-white sm:p-6 sm:pb-4">
                    <div
                        class="flex items-center w-full mb-5"
                        :class="props.title !== null ? 'justify-between' : 'justify-end' "
                    >
                        <h1 v-if="props.title !== null" class="text-lg lg:text-2xl">
                            {{ props.title }}
                        </h1>

                        <img
                            alt="close"
                            src="~~/assets/icons/xmark-solid.svg"
                            class="h-6 cursor-pointer"
                            @click="emits('close')"
                        >
                    </div>

                    <slot />
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
const props = defineProps({
    title: {
        type: String,
        required: false,
        default: null
    }
})

const emits = defineEmits(["close"])
</script>
