<template>
    <div class="flex flex-col pt-20 pb-20 lg:flex-row ">
        <div class="flex flex-col justify-center flex-1 px-5 mt-32 lg:ml-5 text-grayusite lg:mt-0">
            <h2>{{ $t('support.title') }}</h2>
            <h5 class="mt-5">
                {{ $t('support.1') }}
            </h5>

            <div class="flex flex-col mt-5">
                <FormsFloatInput v-model="form.name" :label="$t('support.inputs.name')" />
                <FormsFloatInput v-model="form.email" :label="$t('support.inputs.email')" type="email" class="mt-5" />
                <FormsFloatTextarea v-model="form.message" :label="$t('support.inputs.message')" class="mt-5" />

                <div class="flex items-baseline">
                    <input v-model="form.isCheck" type="checkbox">
                    <h6 class="ml-2 text-grayusite">
                        {{ $t('support.rgpd1') }}
                        <a target="_blank" href="https://doc.usite.fr/rgpd/annexes/politique-de-confidentialite.html" class="cursor-pointer text-primary hover:underline">
                            {{ $t('support.rgpd2') }} </a>
                    </h6>
                </div>

                <button class="mt-10 btn-primary" :disabled="checkValidation()" @click="send()">
                    {{ $t('support.btn') }}
                </button>
            </div>
        </div>
        <div class="flex items-center justify-center flex-1">
            <img src="~/assets/images/france.svg" style="height: 70%;">
        </div>
    </div>
</template>
<script setup lang="ts">
const { t } = useI18n()
const { sendEmail } = useUser()

const testMail = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$/

useHead({
    title: `USite - ${t("navbar.support")}`
})

definePageMeta({
    layout: "default"
})

const form = reactive({
    name: "",
    email: "",
    message: "",
    isCheck: false
})

const checkValidation = () => {
    return form.name.trim() === "" || !testMail.test(form.email) || form.message.trim() === "" || !form.isCheck
}

const send = async () => {
    const message = `Message de ${form.name} \r\n Répondre avec le mail ${form.email} \r\n ${form.message} `
    await sendEmail("support@diiage.org", "Support", message)
}

</script>
