<template>
    <div class="w-full m-5">
        <div class="flex flex-col items-center justify-center w-full lg:flex-row" :style="'height: calc(100vh - 140px)'">
            <div class="w-full">
                <div class="lg:w-2/3" :style="'margin: 0 auto;'">
                    <h2 class="mb-5 text-center">
                        {{ $t('account.personalInfo.part1.title') }}
                    </h2>
                    <div class="w-full">
                        <div class="flex ">
                            <FormsFloatInput v-model="info.username" :label="$t('account.personalInfo.part1.inputs.username')" class="w-1/2 mr-5" />
                        </div>
                        <FormsFloatInput v-model="info.email" type="email" :label="$t('account.personalInfo.part1.inputs.email')" :disabled="getIsExteralUser()" />
                        <button class="w-full mt-10 btn-primary" :disabled="checkValidation()" @click="handleUpdatePersonalInfo()">
                            {{ $t('account.personalInfo.part1.btn') }}
                        </button>
                    </div>
                </div>
            </div>
            <div class="w-full m-5 border-2 lg:h-full lg:w-1" />
            <div class="w-full">
                <h2 class="mb-5 text-center">
                    {{ $t('account.personalInfo.part2.title') }}
                </h2>
                <div class="flex justify-center w-full">
                    <button class="mr-5 btn-primary" @click="downloadPersonalInfo()">
                        {{ $t('account.personalInfo.part2.btn1') }}
                    </button>
                    <button v-if="getIsExteralUser()" class="btn-primary">
                        {{ $t('account.personalInfo.part2.btn2') }}
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const { t } = useI18n()
const { downloadPersonalInfo, updatePersonalInfo, getUserInfo } = useUser()
const { getIsExteralUser } = useAuth()
const testMail = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$/

useHead({
    title: `USite - ${t("account.navbar.infos")}`
})

definePageMeta({
    layout: "account",
    middleware: ["auth-middleware"]
})

const handleUpdatePersonalInfo = async () => {
    await updatePersonalInfo(info.value.username, info.value.email)
}

const {
    data: info
} = await useAsyncData(async () => await getUserInfo())

const checkValidation = () => {
    return info.value.username.trim() === "" || !testMail.test(info.value.email)
}
</script>
