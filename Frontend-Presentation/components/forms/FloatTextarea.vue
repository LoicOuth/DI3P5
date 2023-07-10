<template>
    <div class="relative z-0 w-full mb-5">
        <textarea
            placeholder=" "
            :value="props.modelValue"
            class="block w-full px-0 pt-3 pb-2 mt-0 bg-transparent border-2 border-gray-200 rounded-md appearance-none focus:outline-none focus:ring-0 focus:border-primary"
            rows="5"
            @input="handleChangeValue(($event.target as HTMLInputElement).value)"
        />
        <label class="absolute ml-2 text-gray-500 duration-300 origin-center top-2 z-1 origin-0">{{ props.label }}</label>
        <span v-if="error" class="text-sm text-red-600">{{ error }}</span>
    </div>
</template>

<script lang="ts" setup>
const props = defineProps({
    modelValue: {
        type: [String, Number],
        required: true
    },
    label: {
        type: String,
        required: true
    },
    error: {
        type: String,
        default: ""
    }
})

const emit = defineEmits(["update:modelValue"])

const handleChangeValue = (value: HTMLInputElement["value"]) => {
    emit("update:modelValue", value)
}
</script>
