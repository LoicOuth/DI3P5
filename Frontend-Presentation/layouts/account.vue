<template>
    <div class="h-full">
        <NavBarAccount
            :contrast-enable="false"
            :burger-open="burgerOpen"
            @burger-value="(value: boolean) => burgerOpen = value"
        />

        <div class="z-20 flex flex-row h-container">
            <div :class="navBarClass" class="flex flex-col items-center overflow-auto shadow-md">
                <NavLinkAccount
                    :to="localePath('/account/dashboard')"
                    :text="$t('account.navbar.dashboard')"
                    @link-clicked="burgerOpen = false"
                >
                    <img alt="gauge logo" src="/images/gauge.svg">
                </NavLinkAccount>
                <NavLinkAccount
                    :to="localePath('/account/personal-info')"
                    :text="$t('account.navbar.infos')"
                    @link-clicked="burgerOpen = false"
                >
                    <img alt="info logo" src="/images/circle-info.svg">
                </NavLinkAccount>
                <NavLinkAccount v-if="!getIsExteralUser()" :to="localePath('/account/security')" :text="$t('account.navbar.security')" @link-clicked="burgerOpen = false">
                    <img
                        alt="key logo"
                        src="/images/key.svg"
                    >
                </NavLinkAccount>
                <NavLinkAccount
                    :to="localePath('/account/billing')"
                    :text="$t('account.navbar.billing')"
                    @link-clicked="burgerOpen = false"
                >
                    <img alt="file invoice logo" src="/images/file-invoice.svg">
                </NavLinkAccount>
                <NavLinkAccount
                    :to="localePath('/account/site')"
                    :text="$t('account.navbar.sites')"
                    @link-clicked="burgerOpen = false"
                >
                    <img alt="website logo" src="/images/website.svg">
                </NavLinkAccount>
            </div>
            <div class="w-full navBarWidth">
                <slot />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
const localePath = useLocalePath()
const { getIsExteralUser } = useAuth()
const burgerOpen = ref(false)

const navBarClass = computed(() => {
    if (burgerOpen.value) {
        return "top-18 absolute bg-white w-full"
    } else {
        return "hidden lg:flex"
    }
})
</script>
<style>
.navBarWidth {
    max-width: 100%
}

.h-container {
    height: calc(100% - 72px);
}

@media (min-width: 1024px) {
    .navBarWidth {
        max-width: calc(100% - 12rem);
    }
}
</style>
