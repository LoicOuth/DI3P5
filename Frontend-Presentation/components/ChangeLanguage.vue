<template>
    <div class="font-normal text-black">
        <img
            v-if="!props.contrast"
            class="w-10 h-10 p-1 rounded-full cursor-pointer hover:bg-secondary/20"
            src="~~/assets/icons/language-solid-primary.svg"
            @click="(showPopup = true)"
        >
        <img
            v-else
            class="w-10 h-10 p-1 rounded-full cursor-pointer hover:bg-white/20"
            src="~~/assets/icons/language-solid.svg"
            @click="(showPopup = true)"
        >

        <FormsModalComponent v-if="showPopup" :title="$t('popupLanguage.title')" @close="(showPopup = false)">
            <div class="flex flex-wrap">
                <div
                    v-for="loc in (locales as Array<LocaleObject>)"
                    :key="loc.code"
                    class="relative flex items-center justify-center w-full px-10 py-5 my-3 rounded-md cursor-pointer lg:w-auto lg:ml-5 lg:my-10 hover:bg-black/20"
                    :class="{'bg-primary/10' : locale === loc.code}"
                    @click="switchLang(loc.code)"
                >
                    <CountryFlag :country="loc.flag" class="mr-5 rounded-md" size="big" />
                    <h5 class="ml-5">
                        {{
                            loc.name
                        }}
                    </h5>
                    <img v-if="(locale === loc.code)" src="~~/assets/icons/check-solid.svg" class="absolute h-4 top-2 right-2">
                </div>
            </div>
        </FormsModalComponent>
    </div>
</template>

<script setup lang="ts">
import { LocaleObject } from "@nuxtjs/i18n/dist/runtime/composables"

const props = defineProps({
    contrast: {
        type: Boolean,
        required: true
    }
})

const showPopup = ref<boolean>(false)

const { locales, locale } = useI18n()
const switchLocalePath = useSwitchLocalePath()

const cultureCookie = useCookie(".AspNetCore.Culture")
const switchLang = (to: string) => {
    switch (to) {
    case "fr":
        cultureCookie.value = "c=fr-FR|uic=fr-FR"
        break
    case "en":
        cultureCookie.value = "c=en-US|uic=en-US"
        break
    case "es":
        cultureCookie.value = "c=es-ES|uic=es-ES"
        break
    default:
        cultureCookie.value = "c=fr-FR|uic=fr-FR"
        break
    }

    showPopup.value = false
    navigateTo(switchLocalePath(to))
}
</script>
