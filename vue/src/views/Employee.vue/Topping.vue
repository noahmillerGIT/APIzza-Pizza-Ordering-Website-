<template>
<section>
  <p>aaaaaaaaaaaaa</p>
<table>
  <img width="20px" src="@/assets/angry.png" v-on:click="alert('WHAT THE HELL ARE YOU DOING? GET TO WORK!')"/><br/>
    <thead>
      <tr>
        <th>&nbsp;</th>
        <th>Topping Name ||  </th>
        <th>Price ||  </th>
        <th>Employees still worthless? ||  </th>
        <th>Availability ||  </th>
        <th>button</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="topping in toppings" :key="topping.toppingName" v-on:mouseover="alert('Keep your grimy cursor off my toppings!!!')">
        <td class="toppings-icon">
          <img src="@/assets/angry.png" />
        </td>
        <td class="name">{{ topping.toppingName }}</td>
        <td>
          <span class="price">{{ topping.price }}</span>
        </td>
        <td>yes</td>
        <td>{{topping.available == 1 ? 'Available' : 'Not Available'}}</td>
        <td ><button type="submit" v-on:click.prevent="" >{{topping.available == 1 ? 'Make Unavailable' : 'Make Available'}} </button></td>
      </tr>
    </tbody>
  </table>
  </section>
</template>

<script>
import ToppingService from "@/services/ToppingService";
export default {
  name: 'Topping',
  data(){
    return {
      toppings:[]
    };
  },
  created(){
    ToppingService.list().then((response) =>{
    this.toppings = response.data;
    });
  }
}
</script>

<style>
.toppings-class {
  width: 20px;
}
</style>