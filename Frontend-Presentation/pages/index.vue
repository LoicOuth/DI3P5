<template>
    <div class="flex flex-col items-center">
        <div class="flex justify-end w-full home-header bg-secondary">
            <img
                alt="mockups-header"
                src="~~/assets/images/Mockup-header.svg"
                class="absolute hidden xl:block left-24 top-24"
            >

            <div class="absolute m-0 text-center text-white lg:mr-20 top-20 lg:top-36">
                <h2>
                    {{ $t('home.header.1') }}
                </h2>
                <h5 class="mt-3">
                    {{ $t('home.header.2') }}
                </h5>
                <h5 class="mt-10">
                    {{ $t('home.header.3') }}
                </h5>

                <NuxtLink :to="localePath('/template')">
                    <button class="mt-20 btn-primary">
                        {{ $t('home.header.btn') }}
                    </button>
                </NuxtLink>
            </div>
        </div>
        <img alt="waves" src="~~/assets/images/waves.svg">

        <div class="flex justify-between mt-20">
            <div class="flex flex-col mt-20 ml-5 text-left text-grayusite reveal fade-left">
                <h2>
                    {{ $t('home.body.part1.1') }}
                </h2>
                <h5 class="mt-10">
                    {{ $t('home.body.part1.2') }}
                </h5>
                <h5 class="mt-10">
                    {{ $t('home.body.part1.3') }}
                </h5>
            </div>
            <img alt="dots" src="~~/assets/images/dots.svg" class="hidden lg:block box">
        </div>
        <div class="flex justify-between mt-20">
            <img alt="dots-2" src="~~/assets/images/dots2.svg" class="hidden lg:block">
            <div class="flex flex-col mt-10 text-right lg:mr-5 text-grayusite reveal fade-top">
                <h2>
                    {{ $t('home.body.part2.1') }}
                </h2>
                <h5 class="mt-10">
                    {{ $t('home.body.part2.2') }}
                </h5>
                <h5 class="mt-10">
                    {{ $t('home.body.part2.3') }}
                </h5>
            </div>
        </div>

        <div class="flex flex-col justify-center p-2 mt-20 text-white lg:p-5 bg-secondary">
            <h2 class="mt-5 text-center">
                {{ $t('home.body.part3.title') }}
            </h2>

            <div class="flex flex-col mt-10 lg:flex-row lg:justify-center">
                <div class="w-full p-10 border rounded-md shadow-xl lg:mr-20 lg: lg:w-1/4 border-secondary">
                    <h4 class="pb-3 border-b-2 border-white">
                        {{ $t('home.body.part3.card1.title') }}
                    </h4>

                    <p class="mt-10">
                        {{ $t('home.body.part3.card1.body') }}
                    </p>
                    <NuxtLink :to="localePath('/template#top')">
                        <button class="mt-16 btn-third">
                            {{ $t('home.body.part3.card1.btn') }}
                        </button>
                    </NuxtLink>
                </div>

                <div class="w-full p-10 border rounded-md shadow-xl lg:mr-20 lg: lg:w-1/4 border-secondary">
                    <h4 class="pb-3 border-b-2 border-white">
                        {{ $t('home.body.part3.card2.title') }}
                    </h4>

                    <p class="mt-10">
                        {{ $t('home.body.part3.card2.body') }}
                    </p>
                    <NuxtLink :to="localePath('/template#top')">
                        <button class="mt-16 btn-third">
                            {{ $t('home.body.part3.card2.btn') }}
                        </button>
                    </NuxtLink>
                </div>
            </div>
            <div class="relative flex flex-col items-center mb-10 text-center lg:mb-20 pt-28 lg:text-left">
                <h3>{{ $t('home.body.part3.1') }}</h3>

                <p class="mt-3">
                    {{ $t('home.body.part3.2') }}
                </p>

                <p class="mt-3">
                    {{ $t('home.body.part3.3') }}
                </p>

                <img src="~~/assets/images/france.svg" class="absolute left-0 hidden lg:block top-2">
            </div>
        </div>

        <div class="flex justify-center w-full mt-20 lg:w-3/4 lg:mt-72 text-grayusite">
            <div class="flex flex-col">
                <h2 class="text-center">
                    {{ $t('home.body.part4.1') }}
                </h2>

                <h5 class="mt-20 text-center">
                    {{ $t('home.body.part4.2') }}
                </h5>

                <h5 class="mt-20 text-center">
                    {{ $t('home.body.part4.3') }}
                </h5>
            </div>

            <div class="hidden w-1 mx-48 lg:block bg-primary" />

            <img src="~~/assets/images/server.svg" class="hidden lg:block">
        </div>

        <div
            class="flex justify-around w-full mt-20 text-white rounded-lg lg:w-5/6 lg:pt-10 lg:px-20 bg-secondary lg:mt-52 mb-36"
        >
            <div class="flex flex-col mt-20 lg:w-2/3 lg:mx-20">
                <h3>{{ $t('home.body.part5.title') }}</h3>

                <h5 class="mt-10">
                    {{ $t('home.body.part5.body') }}
                </h5>

                <div class="mt-20 mb-10">
                    <NuxtLink :to="localePath('/support')">
                        <button class="btn-third">
                            {{ $t('home.body.part5.btn') }}
                        </button>
                    </NuxtLink>
                </div>
            </div>

            <div class="hidden w-1/3 lg:block">
                <img src="~~/assets/images/phone.svg">
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("navbar.home")}`
})
definePageMeta({
    layout: "default"
})

const { reveal } = useAnimation()
const localePath = useLocalePath()

onMounted(() => {
    window.addEventListener("scroll", reveal)
})
</script>
