<template>
    <div class="relative w-full p-5 overflow-hidden">
        <div
            class="flex transition-all duration-500"
            :style="{ transform: 'translateX(-' + 100 * currentSlide + '%)' }"
        >
            <div
                v-for="(site, index) in props.sites"
                :key="index"
                class="relative flex justify-center flex-grow-0 flex-shrink-0 text-center basis-full"
            >
                <div class="relative flex justify-center w-3/4 rounded-md carousel-item">
                    <iframe v-if="site.domain && site.subDomain" style="pointer-events: none; height: 32rem; width: 100%; overflow: hidden;" :src="`https://${site.subDomain}.${site.domain}`" />
                    <img v-else src="~/assets/images/preview.svg" style="height: 32rem; width: 100%;">

                    <div role="overlay" class="absolute top-0 left-0 hidden w-full h-full p-6 space-y-5 text-white">
                        <h3 class="border-b">
                            {{ site.name }}
                        </h3>
                        <div class="flex items-center">
                            <p class="mr-2 text-base">
                                Url :
                            </p>
                            <a
                                v-if="site.domain && site.subDomain"
                                target="_blank"
                                :href="`https://${site.subDomain}.${site.domain}`"
                                class="text-left text-white hover:text-primary hover:underline"
                            >
                                {{ `https://${site.subDomain}.${site.domain}` }}
                            </a>
                            <p v-else class="text-base">
                                {{ $t('account.site.carroussel.anyDomain') }}
                            </p>
                        </div>
                        <div class="flex flex-col w-1/2">
                            <div class="flex justify-between">
                                <button class="flex-1 btn btn-primary" @click="emits('showPopupEdit', site)">
                                    {{ $t('account.site.carroussel.btns.editInfo') }}
                                </button>
                            </div>
                            <button class="mt-3 btn btn-primary" @click="redirectLogin(site.id)">
                                {{ $t('account.site.carroussel.btns.edit') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div
            class="absolute flex items-center justify-center w-10 h-10 text-2xl rounded-full cursor-pointer text-secondary left-1 top-1/2 hover:bg-primary/10"
            @click="prevSlide"
        >
            &#10094;
        </div>
        <div
            class="absolute flex items-center justify-center w-10 h-10 text-2xl rounded-full cursor-pointer text-secondary right-1 top-1/2 hover:bg-primary/10"
            @click="nextSlide"
        >
            &#10095;
        </div>

        <div class="flex justify-center mt-4">
            <div
                v-for="(_, index) in props.sites"
                :key="index"
                class="w-2 h-2 mx-2 bg-gray-300 rounded-full cursor-pointer"
                :class="{ 'bg-primary': currentSlide === index }"
                @click="showSlide(index)"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import Site from "~~/core/Interfaces/Site.interface"
const { redirectLogin } = useAuth()

const props = defineProps<{
    sites: Site[]
}>()

const emits = defineEmits(["showPopupEdit"])

const currentSlide = ref(0)

const showSlide = (index) => {
    currentSlide.value = index
}

const prevSlide = () => {
    if (currentSlide.value === 0) {
        currentSlide.value = props.sites.length - 1
    } else {
        currentSlide.value--
    }
}

const nextSlide = () => {
    if (currentSlide.value === props.sites.length - 1) {
        currentSlide.value = 0
    } else {
        currentSlide.value++
    }
}
</script>
