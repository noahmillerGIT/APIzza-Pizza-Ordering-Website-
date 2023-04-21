<template>
  <div class="container">
    <h1>Add Side</h1>
    <form @submit.prevent="addSide" class="form">
      <div class="form-group">
        <label for="itemName">Side Name</label>
        <input type="text" v-model="itemName" required>
      </div>
      <div class="form-group">
        <label for="itemType">Item Type</label>
        <input type="text" v-model="itemType" required>
      </div>
      <div class="form-group">
        <label for="price">Price</label>
        <input type="number" step="0.01" v-model="price" required>
      </div>
      <div class="form-group">
        <label for="available">Available</label>
        <input type="checkbox" v-model="available">
      </div>
      <div class="form-group">
        <label for="imageUrl">Image URL</label>
        <input type="text" v-model="imageUrl">
      </div>
      <div class="form-group">
        <label for="description">Description</label>
        <textarea v-model="description"></textarea>
      </div>
      <button type="submit">Add Side</button>
    </form>
  </div>
</template>

<script>
import addService from '../services/AuthService';

export default {
  name: 'addItems',
  data() {
    return {
      itemName: '',
      itemType: '',
      price: 0,
      available: true,
      imageUrl: '',
      description: '',
      formErrors: []
    };
  },
  methods: {
    async addSide() { 
      try {
        const sideData = {
          itemName: this.itemName,
          itemType: this.itemType,
          price: parseFloat(this.price),
          available: this.available,
          imageUrl: this.imageUrl,
          description: this.description
        };
        const response = await addService.addSide(sideData);
        console.log('Side added successfully:', response.data);
        this.itemName = '';
        this.itemType = '';
        this.price = 0;
        this.available = true;
        this.imageUrl = '';
        this.description = '';
      } catch (error) {
        console.error('Failed to add side:', error);
      }
    },
  },
};
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: 0 auto;
}

.form {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
}

.form-group {
  display: flex;
  flex-direction: column;
}

.label {
  font-weight: bold;
  margin-bottom: 0.5rem;
}

input, textarea {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button[type="submit"] {
  margin-top: 1rem;
  padding: 0.5rem 1rem;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button[type="submit"]:hover {
  background-color: #0056b3;
}
</style>

