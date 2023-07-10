<template>
    <div class="flex flex-col w-full p-5">
        <div class="flex w-full md:w-2/5">
            <DashboardCard :title="$t('account.dashboard.deployment.title')">
                <template #icon>
                    <svg xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48"><path d="M450-160v-371L330-411l-43-43 193-193 193 193-43 43-120-120v371h-60ZM160-597v-143q0-24 18-42t42-18h520q24 0 42 18t18 42v143h-60v-143H220v143h-60Z" /></svg>
                </template>
                <div class="flex flex-col">
                    <div class="flex items-center pb-3 mb-5 font-bold border-b">
                        <div class="w-1/3">
                            {{ $t('account.dashboard.deployment.header1') }}
                        </div>
                        <div class="flex w-1/3">
                            {{ $t('account.dashboard.deployment.header2') }}
                        </div>
                        <div class="flex justify-end w-1/3 text-center">
                            {{ $t('account.dashboard.deployment.header3') }}
                        </div>
                    </div>
                    <DashboardDeploymentItem v-for="(deployment, index) in deployments" :key="index" :deployment="deployment" />
                </div>
            </DashboardCard>
        </div>
    </div>
</template>

<script setup lang="ts">
const { t } = useI18n()

useHead({
    title: `USite - ${t("account.navbar.dashboard")}`
})

definePageMeta({
    layout: "account",
    middleware: ["auth-middleware"]
})

const { getLastDeployments } = useSite()

const {
    data: deployments
} = await useAsyncData(() => getLastDeployments())

</script>
