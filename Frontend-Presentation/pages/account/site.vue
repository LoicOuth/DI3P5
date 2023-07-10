<template>
    <div class="w-full p-5">
        <div class="flex justify-end m-6">
            <button class="btn-primary" @click="showPopupAdd = true">
                {{ $t('account.site.btns.new') }}
            </button>
        </div>
        <FormsModalComponent v-if="showPopupAdd" :title="$t('account.site.btns.new')" @close="(showPopupAdd = false)">
            <div class="flex flex-wrap">
                <FormsFloatInput v-model="form.name" :label="$t('account.site.inputs.name')" />
                <FormsFloatInput v-model="form.description" :label="$t('account.site.inputs.description')" class="mt-5" />
                <button
                    class="mt-10 btn-primary"
                    :disabled="checkValidation()"
                    @click="manageAdd()"
                >
                    {{ $t('account.site.btns.create') }}
                </button>
            </div>
        </FormsModalComponent>

        <LoaderComponent v-if="sitesPending" class="w-full" />
        <div v-else class="items-center w-full pt-4 border-2 rounded-lg">
            <div v-if="sites !== null && sites.length > 0">
                <FormsCarrousselComponent :sites="sites" @show-popup-edit="managePopupUpdate" />
            </div>
            <div v-else>
                <h1 class="text-center">
                    {{ $t('account.site.anyText') }}
                </h1>
            </div>
            <FormsModalComponent
                v-if="showPopupDelete"
                :title="`Supprimer ${deletedSite.name}`"
                @close="(showPopupDelete = false)"
            >
                <div class="flex flex-column">
                    <div class="w-full mt-5">
                        {{ $t('account.site.deleteText') }}
                    </div>
                    <button class="w-full btn-primary" @click="manageDelete">
                        {{ $t('account.site.btns.delete') }}
                    </button>
                </div>
            </FormsModalComponent>
            <FormsModalComponent
                v-if="showPopupUpdate"
                :title="$t('account.site.btns.edit') + updatedSite.name"
                @close="(showPopupUpdate = false)"
            >
                <div class="flex flex-wrap">
                    <FormsFloatInput v-model="updatedSite.name" :label="$t('account.site.inputs.name')" />
                    <FormsFloatInput v-model="updatedSite.description" :label="$t('account.site.inputs.description')" class="mt-5" />
                    <button
                        class="mt-10 btn-primary"
                        :disabled="!(updatedSite.name != '' && updatedSite.description != '')"
                        @click="manageUpdate"
                    >
                        {{ $t('account.site.btns.edit') }}
                    </button>
                </div>
            </FormsModalComponent>
        </div>
    </div>
</template>

<script setup lang="ts">
import Site from "~~/core/Interfaces/Site.interface"
const { t } = useI18n()

const { remove, create, update, getAllSites } = useSite()

useHead({
    title: `USite - ${t("account.navbar.sites")}`
})

definePageMeta({
    layout: "account",
    middleware: ["auth-middleware"]
})

const showPopupDelete = ref<boolean>(false)
const deletedSite = ref<Site>(null)
const showPopupUpdate = ref<boolean>(false)
const updatedSite = ref<Site>(null)
const showPopupAdd = ref<boolean>(false)
const form = reactive({
    name: "",
    description: ""
})

const manageDelete = async () => {
    await remove(deletedSite.value.id)
    await refreshSites()
    showPopupDelete.value = false
    deletedSite.value = null
}

const managePopupUpdate = (site: Site) => {
    showPopupUpdate.value = true
    updatedSite.value = site
}

const manageUpdate = async () => {
    await update(updatedSite.value.id, updatedSite.value.name, updatedSite.value.description)
    await refreshSites()
    showPopupUpdate.value = false
    updatedSite.value = null
}

const manageAdd = async () => {
    await create(form.name, form.description)
    await refreshSites()
    form.name = ""
    form.description = ""
    showPopupAdd.value = false
}

const checkValidation = () => {
    return form.name.trim() === "" || form.description.trim() === ""
}

const {
    data: sites,
    pending: sitesPending,
    refresh: refreshSites
} = await useAsyncData(() => getAllSites())

</script>
