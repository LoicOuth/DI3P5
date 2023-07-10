<template>
    <NuxtLink
        :to="props.to"
        class="flex flex-col items-center"
    >
        <h6
            class="relative border-third nav-link"
            :class="{'active': $route.path === props.to}"
        >
            {{ props.text }}
        </h6>
    </NuxtLink>
</template>

<script lang="ts" setup>
const props = defineProps({
    to: {
        type: String,
        required: true
    },

    text: {
        type: String,
        required: true
    }
})
</script>

<style scoped>
.nav-link::before {
    content: "";
    position: absolute;
    width: 0;
    height: 2px;
    background-color: rgb(200 177 0);
    border-radius: 15px;
    transition: width 300ms;
    bottom: -3px;
}

.nav-link.active::before {
    width: 100%;
}

.nav-link:hover::before {
    width: 100%;
}
</style>
