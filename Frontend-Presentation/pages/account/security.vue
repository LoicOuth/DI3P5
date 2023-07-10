<template>
    <div class="w-full m-5">
        <div class="flex flex-col items-center justify-center w-full lg:flex-row" :style="'height: calc(100vh - 140px)'">
            <div class="w-full">
                <div class="lg:w-2/3" :style="'margin: 0 auto;'">
                    <h2 class="mb-5 text-center">
                        {{ $t('account.security.title') }}
                    </h2>
                    <div class="w-full">
                        <FormsFloatInput v-model="form.oldPassword" type="password" :label="$t('account.security.inputs.old')" />
                        <FormsFloatInput v-model="form.newPassword" type="password" :label="$t('account.security.inputs.new')" />
                        <FormsFloatInput v-model="form.confirmNewpassword" type="password" :label="$t('account.security.inputs.confirm')" />
                        <button class="w-full mt-10 btn-primary" :disabled="checkValidation()" @click="handleUpdatePassword()">
                            {{ $t('account.security.btn') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const { t } = useI18n()
const { updatePassword } = useUser()
const checkPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

useHead({
    title: `USite - ${t("account.navbar.security")}`
})

definePageMeta({
    layout: "account",
    middleware: ["auth-middleware"]
})

const handleUpdatePassword = async () => {
    await updatePassword(form.oldPassword, form.newPassword)
}

const form = reactive({
    oldPassword: "",
    newPassword: "",
    confirmNewpassword: ""
})

const checkValidation = () => {
    return !checkPassword.test(form.oldPassword) ||
    !checkPassword.test(form.newPassword) ||
    !checkPassword.test(form.confirmNewpassword) ||
    form.newPassword !== form.confirmNewpassword ||
    form.oldPassword === form.newPassword
}
</script>
