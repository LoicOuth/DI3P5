<template>
   <ParentLayout>
      <div class="additional-info space-x-6" slot="page-top">
         <span>📅 {{ dateFormatted }}</span>
         <span>✍️ {{ this.$page.frontmatter.author }}</span>
         <span>⏱️ {{ $page.readingTime.text.split(' ')[0] }} minutes</span>
      </div>
   </ParentLayout>
</template>
 
<script>
import ParentLayout from '@parent-theme/layouts/Layout.vue'

export default {
   components: { ParentLayout },

   computed: {
      dateFormatted() {
         if (this.$page.lastUpdated) {
            return this.$page.lastUpdated
         }
         else {
            return "Pas encore à jour"
         }
      }
   },
}
</script>

<style scoped>
.additional-info {
   margin-top: 80px;
   margin-left: 1.5rem;
   font-weight: 500;
}

.space-x-6 > * + * {
   margin-left: 1.5rem;
}
</style>