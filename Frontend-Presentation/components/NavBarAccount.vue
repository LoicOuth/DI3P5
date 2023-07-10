<template>
    <div
        class="z-50 flex flex-row-reverse justify-between w-full transition-all duration-200 bg-white shadow-md lg:items-center lg:flex-row text-secondary"
    >
        <img alt="logo" class="h-12 m-3" src="/images/logo.svg">
        <div class="flex mt-4">
            <ChangeLanguage :contrast="false" />
            <img
                alt="account logo"
                class="w-10 h-10 mx-5 rounded-full cursor-pointer"
                src="/images/user-connected.svg"
                @click="useAuth().redirectManage()"
            >
            <img
                alt="logout logo"
                class="w-10 h-8 mx-5 cursor-pointer hover:bg-secondary/20"
                src="/images/logout.svg"
                @click="showPopup = true"
            >

            <FormsModalComponent v-if="showPopup" :title="$t('account.popupLogout.btn')" @close="(showPopup = false)">
                <h2 class="text-center">
                    {{ $t('account.popupLogout.text') }}
                </h2>

                <button
                    class="mt-5 btn-primary"
                    @click="useAuth().logout()"
                >
                    {{ $t('account.popupLogout.btn') }}
                </button>
            </FormsModalComponent>
        </div>

        <div class="flex items-center m-3 lg:hidden">
            <button
                class="relative w-10 h-10 focus:outline-none"
                :class="checkRoute ? 'text-white' : 'text-secondary'"
                @click="emits('burger-value', !props.burgerOpen)"
            >
                <div class="absolute block w-5 transform -translate-x-1/2 -translate-y-1/2 left-1/2 top-1/2">
                    <span
                        class="block absolute h-0.5 w-6 bg-current transform transition duration-500 ease-in-out"
                        :class="{ 'rotate-45': burgerOpen, ' -translate-y-1.5': !burgerOpen }"
                    />
                    <span
                        class="block absolute  h-0.5 w-6 bg-current transform transition duration-500 ease-in-out"
                        :class="{ 'opacity-0': burgerOpen }"
                    />
                    <span
                        class="block absolute  h-0.5 w-6 bg-current transform  transition duration-500 ease-in-out"
                        :class="{ '-rotate-45': burgerOpen, ' translate-y-1.5': !burgerOpen }"
                    />
                </div>
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
const showPopup = ref<boolean>(false)
const props = defineProps({
    contrastEnable: {
        type: Boolean,
        default: true
    },
    burgerOpen: {
        type: Boolean,
        default: false
    }
})
const emits = defineEmits(["burger-value"])
const localePath = useLocalePath()
const route = useRoute()

const checkRoute = computed(() => {
    return (route.path === localePath("/") || route.path === localePath("/template"))
})
</script>
