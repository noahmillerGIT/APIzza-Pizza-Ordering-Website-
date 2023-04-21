<template>
  <div>
      <h1>DON'T FORGET: YOU'RE HERE FOREVER</h1>
      <div id="nav">
          <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">surrender</router-link><br><br>
          <router-link v-bind:to="{ name: 'addItems' }"> Add Side </router-link><br><br>
          <router-link v-bind:to="{ name: 'addItemBeverage' }"> Add Beverage </router-link><br><br>
         <div> <router-link v-bind:to="{ name: 'available' }"> View Items </router-link></div><br>
          <div><router-link v-bind:to="{ name: 'orders' }"> View Orders </router-link> </div>
          </div>
          </div>
</template>

<script>
export default {
    name: 'emp-home',

}
</script>

<style>

</style>