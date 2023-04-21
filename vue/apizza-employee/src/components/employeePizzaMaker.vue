<template>
  <div class="container">
    <h2>Create a Specialty Pizza</h2>
    <form @submit.prevent="createPizza">
      <div class="form-group">
        <label>Name:</label>
        <input type="text" v-model="newPizza.name" class="form-control" required />
      </div>
      <div class="form-group">
        <label>Description:</label>
        <textarea v-model="newPizza.description" class="form-control" required></textarea>
      </div>
      <div class="form-group">
        <label>Size:</label>
        <select v-model="newPizza.size.size" class="form-control" required>
          <option value="0">Small</option>
          <option value="1">Medium</option>
          <option value="2">Large</option>
          <option value="3">Extra Large</option>
        </select>
      </div>
      <div class="form-group">
        <label>Toppings:</label>
        <div v-for="(topping, index) in newPizza.toppings" :key="index">
          <div class="form-check">
            <input type="checkbox" :id="'topping' + index" :value="index" v-model="newPizza.toppings[index].isChecked" class="form-check-input">
            <label class="form-check-label" :for="'topping' + index">{{ topping.name }}</label>
          </div>
        </div>
      </div>
      <div class="form-group">
        <label>Crust:</label>
        <select v-model="newPizza.crusts.crusts" class="form-control" required>
          <option value="0">HandCrust</option>
          <option value="1">ThinCrust</option>
        </select>
      </div>
      <div class="form-group">
        <label>Sauce:</label>
        <select v-model="newPizza.sauces.sauces" class="form-control" required>
          <option value="0">Red</option>
          <option value="1">White</option>
          <option value="2">Azure</option>
        </select>
      </div>
      <div class="form-group">
        <label>Image URL:</label>
        <input type="text" v-model="newPizza.imageUrl" class="form-control" required />
      </div>
      <button type="submit" class="btn btn-primary">Create Pizza</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      newPizza: {
        name: '',
        description: '',
        size: {
          size: 0
        },
        toppings: [
          { name: 'Pepperoni', quantity: 0 },
          { name: 'Mushrooms', quantity: 0 },
          { name: 'Onions', quantity: 0 },
          { name: 'Sausage', quantity: 0 },
          { name: 'Bacon', quantity: 0 },
          { name: 'Extra cheese', quantity: 0 },
          { name: 'Pineapple', quantity: 0 },
          { name: 'GoldFlake', quantity: 0 }
        ],
        crusts: {
          crusts: 0
        },
        sauces: {
          sauces: 0
        },
        imageUrl: ''
      }
    }
  },
  methods: {
    createPizza() {
      this.newPizza.toppings = this.newPizza.toppings.filter(topping => topping.isChecked).map(topping => {
        return {
          value: topping.value,
          isChecked: topping.isChecked
        };
      });

      // parse the size, crusts, sauces, and toppings values as integers
      this.newPizza.size.size = parseInt(this.newPizza.size.size);
      this.newPizza.crusts.crusts = parseInt(this.newPizza.crusts.crusts);
      this.newPizza.sauces.sauces = parseInt(this.newPizza.sauces.sauces);
      this.newPizza.toppings = this.newPizza.toppings.map(topping => {
        return {
          value: parseInt(topping.value),
          isChecked: topping.isChecked
        };
      });

      console.log('Sending data:', this.newPizza);
      axios.post('https://localhost:44315/api/createspecialty', this.newPizza)
        .then(response => {
          console.log('Specialty pizza created:', response.data);
          // Do something with the created specialty pizza, e.g. show a success message
        })
        .catch(error => {
          console.error('Error creating specialty pizza:', error);
          console.log(error.response.data); // log the error response data
        });
    },
    created() {
      this.newPizza.toppings.forEach(topping => {
        topping.isChecked = false;
      });
    }
  }
}
</script>