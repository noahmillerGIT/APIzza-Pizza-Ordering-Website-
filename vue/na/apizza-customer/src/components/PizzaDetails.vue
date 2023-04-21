<template>
  <div>
    <h1>{{ pizza.name }}</h1>
    <p>{{ pizza.description }}</p>
    <p>Price: ${{ pizza.price }}</p>
    <button @click="addToCart">Add to Cart</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      pizza: {} // Replace this with API data
    };
  },
  methods: {
    addToCart() {
      // Add the selected pizza to the cart and navigate to the checkout page.
    }
  },
  mounted() {
    // Fetch the pizza details from the API using the route parameter (id) and assign it to the 'pizza' data property.
  }
};
</script>