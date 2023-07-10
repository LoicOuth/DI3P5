<template>
    <div class="flex flex-wrap items-center pb-3 mb-5 border-b">
        <div class="flex flex-col w-1/3">
            <h6>{{ props.deployment.siteName }}</h6>
            <a class="text-primary hover:underline url-truncate" :href="`https://${props.deployment.siteUrl}`" target="_blank">{{ `https://${props.deployment.siteUrl}` }}</a>
        </div>
        <div class="flex w-1/3">
            <span v-if="props.deployment.result" class="badge" :class="`badge-buildResult-${props.deployment.result}`">{{ $t(`account.dashboard.deployment.buildResult.${props.deployment.result}`) }}</span>
            <span class="ml-2 badge" :class="`badge-buildStatus-${props.deployment.status}`">{{ $t(`account.dashboard.deployment.buildStatus.${props.deployment.status}`) }}</span>
        </div>
        <div class="flex flex-col items-end w-1/3">
            <div class="flex items-center justify-between w-full">
                <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 -960 960 960" width="48"><path d="M360-860v-60h240v60H360Zm90 447h60v-230h-60v230Zm30 332q-74 0-139.5-28.5T226-187q-49-49-77.5-114.5T120-441q0-74 28.5-139.5T226-695q49-49 114.5-77.5T480-801q67 0 126 22.5T711-716l51-51 42 42-51 51q36 40 61.5 97T840-441q0 74-28.5 139.5T734-187q-49 49-114.5 77.5T480-81Zm0-60q125 0 212.5-87.5T780-441q0-125-87.5-212.5T480-741q-125 0-212.5 87.5T180-441q0 125 87.5 212.5T480-141Zm0-299Z" /></svg>
                <p class="text-sm">
                    {{ elapsedTimeString(props.deployment.startTime, props.deployment.finishTime) }}
                </p>
            </div>
            <div class="flex items-center justify-between w-full mt-2">
                <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 -960 960 960" width="48"><path d="M352.817-310Q312-310 284-338.183q-28-28.183-28-69T284.183-476q28.183-28 69-28T422-475.817q28 28.183 28 69T421.817-338q-28.183 28-69 28ZM180-80q-24 0-42-18t-18-42v-620q0-24 18-42t42-18h65v-60h65v60h340v-60h65v60h65q24 0 42 18t18 42v620q0 24-18 42t-42 18H180Zm0-60h600v-430H180v430Zm0-490h600v-130H180v130Zm0 0v-130 130Z" /></svg>
                <p class="text-sm text-end">
                    {{ stringToLocaleDate(props.deployment.finishTime) }}
                </p>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import Deployment from "~~/core/Interfaces/Deployment.interface"

const { stringToLocaleDate, elapsedTimeString } = useDate()

const props = defineProps<{
   deployment: Deployment
}>()
</script>

<style>
.url-truncate {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.badge {
    display: inline-block;
    padding: 0.3rem 0.6em;
    font-size: 0.8rem;
    line-height: 1;
    text-align: center;
    border-radius: 0.40rem;
}

.badge-buildStatus-0 {
    background-color: #4B4B4B;  /* Gris pour "None" */
    color: white;
}

.badge-buildStatus-1 {
    background-color: #007bff;  /* Bleu pour "In Progress" */
    color: white;
}

.badge-buildStatus-2 {
    background-color: #22c55e;  /* Vert pour "Completed" */
    color: white;
}

.badge-buildStatus-4 {
    background-color: #ffc107;  /* Jaune pour "Cancelling" */
    color: black;
}

.badge-buildStatus-8 {
    background-color: #17a2b8;  /* Cyan pour "Postponed" */
    color: white;
}

.badge-buildStatus-32 {
    background-color: #4B4B4B;  /* Gris pour "Not Started" */
    color: white;
}

.badge-buildResult-0 {
    background-color: #4B4B4B;  /* Gris pour "None" */
    color: white;
}

.badge-buildResult-2 {
    background-color: #22c55e;  /* Vert pour "Succeeded" */
    color: white;
}

.badge-buildResult-4 {
    background-color: #ffc107;  /* Jaune pour "Partially Succeeded" */
    color: black;
}

.badge-buildResult-8 {
    background-color: #8E2C2C;  /* Rouge pour "Failed" */
    color: white;
}

.badge-buildResult-32 {
    background-color: #4B4B4B;  /* Gris pour "Canceled" */
    color: white;
}
</style>
