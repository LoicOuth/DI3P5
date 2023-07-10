<template>
    <div id="top" class="flex flex-col items-center">
        <div class="flex items-center justify-between w-full template-header bg-secondary">
            <div class="m-0 ml-2 text-center text-white">
                <h2>
                    {{ $t('template.header.1') }}
                </h2>
                <h5 class="mt-3">
                    {{ $t('template.header.2') }}
                </h5>
                <h5 class="mt-10">
                    {{ $t('template.header.3') }}
                </h5>

                <a href="#nextSection" class="flex flex-col items-center mt-20 cursor-pointer">
                    <h5>{{ $t('template.header.btn') }}</h5>
                    <img src="~/assets/icons/arrow-down.svg" class="mt-5 animate-bounce">
                </a>
            </div>

            <img alt="mockups-header" src="~~/assets/images/template.svg">
        </div>

        <div id="nextSection" class="flex justify-between w-full px-5 mt-20 text-grayusite">
            <div class="flex flex-col">
                <h2>{{ $t('template.part1.title') }}</h2>

                <h5 class="mt-10">
                    {{ $t('template.part1.1') }}
                </h5>

                <h5 class="mt-10">
                    {{ $t('template.part1.2') }}
                </h5>

                <div class="flex flex-col ml-14">
                    <div class="flex items-center mt-5">
                        <div class="p-1 mr-3 rounded-full bg-primary/30" style="width: 30px;">
                            <img src="~/assets/icons/check-primary.svg" width="30" height="30">
                        </div>
                        <h4>{{ $t('template.part1.list.1') }}</h4>
                    </div>
                    <div class="flex items-center mt-5">
                        <div class="p-1 mr-3 rounded-full bg-primary/30" style="width: 30px;">
                            <img src="~/assets/icons/check-primary.svg" width="30" height="30">
                        </div>
                        <h4>{{ $t('template.part1.list.2') }}</h4>
                    </div>
                    <div class="flex items-center mt-5">
                        <div class="p-1 mr-3 rounded-full bg-primary/30" style="width: 30px;">
                            <img src="~/assets/icons/check-primary.svg" width="30" height="30">
                        </div>
                        <h4>{{ $t('template.part1.list.3') }}</h4>
                    </div>
                </div>
            </div>

            <img src="~/assets/images/rectangle.svg" class="w-2/3">
        </div>
        <div class="flex justify-between w-full mt-20 text-grayusite">
            <img src="~/assets/images/mockup.svg" class="w-2/3">

            <div class="flex flex-col px-5">
                <h2>{{ $t('template.part2.title') }}</h2>

                <h5 class="mt-10">
                    {{ $t('template.part2.1') }}
                </h5>

                <h5 class="mt-10">
                    {{ $t('template.part2.2') }}
                </h5>
                <div class="flex flex-col ml-14">
                    <div class="flex items-center mt-5">
                        <div class="p-2 mr-3 rounded-full bg-primary/30" style="width: 40px;">
                            <img src="~/assets/icons/drag&drop.svg" width="30" height="30">
                        </div>
                        <h4>{{ $t('template.part1.list.1') }}</h4>
                    </div>
                    <div class="flex items-center mt-5">
                        <div class="p-2 mr-3 rounded-full bg-primary/30" style="width: 40px;">
                            <img src="~/assets/icons/star.svg" width="30" height="30">
                        </div>
                        <h4>{{ $t('template.part2.list.2') }}</h4>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("navbar.template")}`
})

definePageMeta({
    layout: "default"
})
</script>
