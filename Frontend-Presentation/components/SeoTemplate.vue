<template>
    <div>
        <Html :lang="head.htmlAttrs.lang" :dir="head.htmlAttrs.dir">
            <Head>
                <Link
                    v-for="link in head.link"
                    :id="link.id"
                    :key="link.id"
                    :rel="link.rel"
                    :href="link.href"
                    :hreflang="link.hreflang"
                />

                <Meta v-for="meta in head.meta" :id="meta.id" :key="meta.id" :property="meta.property" :content="meta.content" />
                <Meta name="description" content="USite - Creator of website" />
                <Meta name="keywords" content="Website creator, USite" />
            </Head>
            <Body>
                <slot />
            </Body>
        </Html>
    </div>
</template>

<script setup lang="ts">
const head = useLocaleHead({
    addDirAttribute: true,
    identifierAttribute: "id",
    addSeoAttributes: true
})
</script>
