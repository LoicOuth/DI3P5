<template>
    <div>
        <div v-if="props.isBar" class="loader bg-secondary/30">
            <div class="loaderBar bg-secondary" />
        </div>
        <img v-else alt="loading" class="animate-spin h-6" :class="{'!h-14' : !isSmall}" src="/images/spinner-solid.svg">
    </div>
</template>

<script setup lang="ts">
const props = defineProps({
    isBar: {
        type: Boolean,
        default: false
    },
    isSmall: {
        type: Boolean,
        default: true
    }
})
</script>

<style scoped>
.loader {
  width: 100%;
  margin:0 auto;
  position:relative;
  padding:3px;
}

.loader:before {
  content:'';
  position:absolute;
  top:-4px;
  right:-4px;
  bottom:-4px;
  left:-4px;
}
.loader .loaderBar {
  position:absolute;
  top:0;
  right:100%;
  bottom:0;
  left:0;
  width:0;
  animation:borealisBar 2s linear infinite;
}

@keyframes borealisBar {
  0% {
    left:0%;
    right:100%;
    width:0%;
  }
  10% {
    left:0%;
    right:75%;
    width:25%;
  }
  90% {
    right:0%;
    left:75%;
    width:25%;
  }
  100% {
    left:100%;
    right:0%;
    width:0%;
  }
}
</style>
