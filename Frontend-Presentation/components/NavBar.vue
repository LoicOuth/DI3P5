<template>
    <div
        class="fixed z-50 flex flex-row-reverse justify-between w-full transition-all duration-200 lg:items-center lg:flex-row"
        :class="bgNavbar"
    >
        <div class="ml-8">
            <img
                v-if="checkContrast"
                alt="logo"
                class="h-12 m-3"
                src="/images/logo_white.svg"
            >
            <img v-else alt="logo" class="h-12 m-3" src="/images/logo.svg">
        </div>
        <div
            :class="navBarClass"
            class="flex-col items-center justify-center w-full transition-all duration-200 shadow-md lg:shadow-none lg:flex-row"
        >
            <div v-if="haveLinks" class="relative flex flex-col w-full lg:flex-row lg:w-3/4 justify-evenly">
                <NavLink :to="localePath('/')" :text="$t('navbar.home')" />
                <NavLink :to="localePath('/template')" :text="$t('navbar.template')" />
                <NavLink :to="localePath('/pricing')" :text="$t('navbar.pricing')" />
                <!-- <NavLink :to="localePath('/project')" :text="$t('navbar.project')" /> -->
                <NavLink :to="localePath('/support')" :text="$t('navbar.support')" />
            </div>
            <div class="flex items-center justify-center m-5 lg:justify-end" :class="haveLinks ? 'lg:w-2/4' : 'lg:w-full'">
                <button
                    v-if="!useAuth().isConnected()"
                    class="w-full mr-5 lg:w-auto btn-primary"
                    @click="useAuth().redirectLogin()"
                >
                    {{ $t('navbar.connexion') }}
                </button>
                <ChangeLanguage :contrast="checkContrast" />
                <img
                    v-if="useAuth().isConnected() && checkContrast"
                    alt="account logo"
                    class="w-10 h-10 ml-5 rounded-full cursor-pointer lg:h-14 lg:w-14"
                    src="/images/user-connected-white.svg"
                    @click="useAuth().redirectManage()"
                >
                <img
                    v-else-if="useAuth().isConnected()"
                    alt="account logo"
                    class="w-10 h-10 ml-5 rounded-full cursor-pointer lg:h-14 lg:w-14"
                    src="/images/user-connected.svg"
                    @click="useAuth().redirectManage()"
                >
            </div>
        </div>

        <div v-if="haveLinks" class="flex items-center m-3 lg:hidden">
            <button
                class="relative w-10 h-10 focus:outline-none"
                :class="topPage && checkRoute ? 'text-white' : 'text-secondary'"
                @click="(burgerOpen = !burgerOpen)"
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
const props = defineProps({
    contrastEnable: {
        type: Boolean,
        default: true
    },
    haveLinks: {
        type: Boolean,
        default: true
    }
})
const burgerOpen = ref(false)
const topPage = ref(true)
const localePath = useLocalePath()
const route = useRoute()

const checkRoute = computed(() =>
    (route.path === localePath("/") || route.path === localePath("/template"))
)

const checkContrast = computed(() =>
    (topPage.value && checkRoute.value) && props.contrastEnable
)

const navBarClass = computed(() => {
    if (!props.haveLinks) {
        return "block bg-white lg:relative lg:top-0 lg:flex"
    }
    if (burgerOpen.value) {
        return checkContrast.value ? "absolute top-16 bg-secondary lg:relative lg:top-0 lg:flex" : "absolute top-16 bg-white lg:relative lg:top-0 lg:flex"
    } else {
        return "hidden lg:flex"
    }
})

const bgNavbar = computed(() =>
    checkContrast.value ? "bg-secondary text-white" : "shadow-md text-secondary bg-white"
)

const checkTopPage = () => {
    if (window.pageYOffset > 0) {
        topPage.value = false
    } else {
        topPage.value = true
    }
}

onMounted(() => {
    checkTopPage()
    window.addEventListener("scroll", checkTopPage)
})
</script>
