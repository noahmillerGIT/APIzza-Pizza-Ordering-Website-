<template>
  <div>
      <h1>assssssssssssssssssssssss</h1>
      <div id="nav">
          <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">surrender</router-link>
          <router-link v-bind:to="{ name: 'available' }"> items </router-link>
          <router-link v-bind:to="{ name: 'orders' }"> orders </router-link>
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