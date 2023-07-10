<template>
    <div class="bottom-0 w-full text-grayusite">
        <hr>
        <div class="flex p-4 bg-white justify-evenly">
            <div class="flex flex-col w-64">
                <img alt="logo" class="h-14" src="/images/logo.svg">
                <p class="w-64 mt-2 break-words">
                    {{ $t('navbar.description') }}
                </p>
            </div>
            <div class="flex flex-col">
                <h6 class="p-2 mb-2 text-center border-b-2 border-primary">
                    {{ $t('navbar.information') }}
                </h6>
                <a class="text-center hover:underline underline-offset-2 hover:text-primary" style="font-size: 20px;" href="https://doc.usite.fr/rgpd/annexes/politique-de-confidentialite.html">
                    {{ $t('navbar.mention') }}
                </a>
                <a class="text-center hover:underline underline-offset-2 hover:text-primary" style="font-size: 20px;" href="https://doc.usite.fr/rgpd/annexes/politique-de-confidentialite.html">
                    {{ $t('navbar.cookie') }}
                </a>
            </div>
            <div>
                <h6 class="w-auto p-2 mb-2 text-center border-b-2 border-primary">
                    {{ $t('navbar.contact') }}
                </h6>
                <p class="text-right break-words w-44">
                    69 avenue Aristide Briand 21000 Dijon
                </p>
                <div class="flex justify-center">
                    <button class="mt-4 btn-primary" @click="redirectSupport()">
                        {{ $t('navbar.support') }}
                    </button>
                </div>
            </div>
        </div>
        <div class="flex items-center p-2 justify-evenly">
            <span class="w-64" />
            <p class="flex items-center">
                <span class="mr-2">Made in</span>
                <img src="~~/assets/images/france-flag.svg">
            </p>
            <p>{{ $t('navbar.copyright') }}</p>
        </div>
    </div>
</template>
<script lang="ts" setup>
const router = useRouter()

const redirectSupport = () => {
    router.push("support")
}

</script>
