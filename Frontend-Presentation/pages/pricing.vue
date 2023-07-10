<template>
    <div class="flex flex-col items-center justify-center w-full pt-40 pb-20 lg:flex-row">
        <div class="pricing-card lg:mt-0 mt-pricing-card">
            <div class="text-center border-b py-7 border-secondary">
                <h4>
                    Starter
                </h4>
                <p class="text-primary">
                    5€ / {{ $t('pricing.month') }}
                </p>
            </div>

            <div class="flex flex-col justify-between h-full p-5">
                <ul class="ml-10 space-y-4 list-disc">
                    <li>{{ $t('pricing.studio') }}</li>
                    <li>{{ $t('pricing.https') }}</li>
                    <li>{{ $t('pricing.support') }}</li>
                </ul>

                <button class="mt-5 btn-primary lg:mt-0" @click="useAuth().redirectLogin()">
                    {{ $t('pricing.order') }}
                </button>
            </div>
        </div>
        <div class="relative flex lg:ml-10 pricing-card ">
            <div class="absolute px-12 py-2 font-bold text-white border border-black rounded-full bg-primary -top-5" style="right: 28%;">
                {{ $t('pricing.popular') }}
            </div>
            <div class="text-center border-b py-7 border-secondary">
                <h4>
                    Advanced
                </h4>
                <p class="text-primary">
                    10€ / {{ $t('pricing.month') }}
                </p>
            </div>

            <div class="flex flex-col justify-between h-full p-5">
                <ul class="ml-10 space-y-4 list-disc">
                    <li>{{ $t('pricing.studio') }}</li>
                    <li>{{ $t('pricing.https') }}</li>
                    <li>{{ $t('pricing.support') }}</li>
                    <li>{{ $t('pricing.domain') }}</li>
                    <li>{{ $t('pricing.stat') }}</li>
                </ul>

                <button class="mt-5 btn-primary lg:mt-0" @click="useAuth().redirectLogin()">
                    {{ $t('pricing.order') }}
                </button>
            </div>
        </div>
        <div class="lg:ml-10 pricing-card">
            <div class="text-center border-b py-7 border-secondary">
                <h4>
                    Premium
                </h4>
                <p class="text-primary">
                    15€ / {{ $t('pricing.month') }}
                </p>
            </div>

            <div class="flex flex-col justify-between h-full p-5">
                <ul class="ml-10 space-y-4 list-disc">
                    <li>{{ $t('pricing.studio') }}</li>
                    <li>{{ $t('pricing.https') }}</li>
                    <li>{{ $t('pricing.support') }}</li>
                    <li>{{ $t('pricing.domain') }}</li>
                    <li>{{ $t('pricing.stat') }}</li>
                    <li>{{ $t('pricing.statRef') }}</li>
                </ul>

                <button class="mt-5 btn-primary lg:mt-0" @click="useAuth().redirectLogin()">
                    {{ $t('pricing.order') }}
                </button>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("navbar.pricing")}`
})

definePageMeta({
    layout: "default"
})
</script>
