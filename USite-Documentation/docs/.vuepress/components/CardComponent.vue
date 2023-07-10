<template>
   <div class="container-home">
      <img src="/assets/images/usite-logo-only-red.svg" height="100" />
      <h1 class="title">Bienvenue sur la base de connaissance de USite</h1>

      <div class="container-card">
         <div class="card" @click="navigate('/rgpd')">
            <h5>RGPD</h5>
         </div>
         <div class="card" @click="navigate('/documentation/base')">
            <h5>Documentation de base</h5>
         </div>
         <div class="card" @click="navigate('/documentation/dat')">
            <h5>Document d'architecture technique</h5>
         </div>
         <div class="card" @click="navigate('/documentation/dc')">
            <h5>Document de conception</h5>
         </div>
      </div>
   </div>
   
</template>

<script>

export default {
   methods: {
      navigate(url) {
         this.$router.push(url)
      }
   }
}

</script>

<style scoped>

.container-card {
   display: flex;
   flex-wrap: wrap;
   width: 100%;
   justify-content: center;
}

.card {
   position: relative;
   background: white;
   border-radius: 15px;
   padding: 2rem;
   box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
   width: 40%;
   text-align: center;
   display: flex;
   align-items: center;
   justify-content: center;
   height: 10rem;
   margin: 0.3rem;
   cursor: pointer;
}

.card h5 {
   font-size: 2rem;
   z-index: 2;
}

.card:hover {
   color: white;
}

.card:hover::before {
   width: 100%;
}

.card::before {
   content: "";
   height: 100%;
   width: 0;
   position: absolute;
   background-color: #E71D36;
   border-radius: 15px;
   transition: all 300ms;
   left: 0;
} 

.container-home {
   display: flex;
   justify-content: center;
   flex-direction: column;
}

.title {
   text-align: center;
}

</style>