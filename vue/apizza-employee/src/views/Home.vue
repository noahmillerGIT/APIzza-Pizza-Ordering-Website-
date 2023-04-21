<template>
  <div class="home">
    <h1>Welcome, {{ username }}</h1>
  </div>
</template>

<script>
export default {
  name: "home",
  computed: {
    username() {
      return this.$store.state.user.username;
    }
  },
};
</script>
<style scoped>
    .home{
      text-align: center;
    }
</style>
