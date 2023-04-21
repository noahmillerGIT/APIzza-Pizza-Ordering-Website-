<template>
<section>
  <h2>Toppings</h2>
  <span>Showing Toppings which are currently {{displayAvailabilityThatIs ? "available" : "unavailable"}}</span><br><button v-on:click="toggleAvailabilityView()">{{displayAvailabilityThatIs ? "Show unavailable Toppings" : "Show available Toppings"}}</button>
<table>
  <img width="20px" src="@/assets/angry.png" v-on:click="alert('WHAT THE HELL ARE YOU DOING? GET TO WORK!')"/><br/>
    <thead>
      <tr>
        <th>&nbsp;</th>
        <th>Topping Name </th>
        <th>Price </th>
        <th>Employees still worthless?  </th>
        <th>Availability  </th>
        <th>button</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="topping in filterToppings" :key="topping.itemId" >
        <td class="toppings-icon">
          <img @click="alert('GET BACK TO WORK')" width=50px src="@/assets/angry.png" />
        </td>
        <td class="name" v-on:mouseover="alert('Keep your grimy cursor off my toppings!!!')">{{ topping.itemName }}</td>
        <td>
          <span class="price">{{ topping.price }}</span>
        </td>
        <td v-on:change="alert('think you\'re pretty clever, huh? well guess what? YOU\'RE FIRED!!!')">yes</td>
        <td>{{topping.availability == 1 ? 'Available' : 'Not Available'}}</td>
        <td ><button type="submit" v-on:click="changeAvailability(topping)" >{{topping.availability == 1 ? 'Make Unavailable' : 'Make Available'}} </button></td>
      </tr>
    </tbody>
  </table>
  </section>
</template>

<script>
import axios from 'axios';
export default {
  name: 'Topping',
  methods: {
    populateToppings(){
      axios.get('api/topping')
      .then(response => {
        this.toppings = response.data;
      })
      .catch(error =>{
        console.log('you messed up', error);
      })
    },
    changeAvailability(item){
      
      axios.put('api/topping', item)
      .then(this.populateToppings())
      .catch(error => {
        alert(error);
      })
      
    },
    toggleAvailabilityView(){
      this.displayAvailabilityThatIs = !this.displayAvailabilityThatIs;
    },
    alert(string){
      alert(string)
    },
  },
  data(){
    return {
      toppings:[],
      displayAvailabilityThatIs: true,
    };
  },
  computed: {
      filterToppings(){
        return this.toppings.filter(topping => (topping.availability == this.displayAvailabilityThatIs) && (true))
      },
  },
  created(){
    this.populateToppings();
    }
  }

</script>

<style>
.toppings-class {
  width: 20px;
}
</style>