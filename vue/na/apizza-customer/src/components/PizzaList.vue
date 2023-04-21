<template>
  <div>
    <h1>APIzza Menu</h1>
    <div v-for="pizza in pizzas" :key="pizza.id">
      <h2>{{ pizza.name }}</h2>
      <p>{{ pizza.description }}</p>
      <button @click="selectPizza(pizza.id)">Order Now</button>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      pizzas: [] // Replace this with API data
    };
  },
  methods: {
    selectPizza(pizzaId) {
      this.$router.push(`/pizza/${pizzaId}`);
    }
  },
  mounted() {
    // Fetch pizza data from the API and assign it to the 'pizzas' data property.
  }
};
</script>