<template>
  <div>
    <h1>bool timeToClean = timeToLean;</h1>
      <label>Choose category to view</label>
      <select v-model="showMe"
      name="viewsDropdown" id="viewsDropdown">
      <option value="Toppings">Toppings</option>
      <option value="Beverage">Beverages</option>
      <option value="Sides">Sides</option>
        </select>
      <topping v-if="showMe=='Toppings'" > </topping>
      <beverage v-if="showMe=='Beverage'"></beverage>
      <sides v-if="showMe=='Sides'"></sides>
      </div>
</template>

<script>
import Topping from "../components/Topping.vue"
import Beverage from "../components/Beverage.vue"
import Sides from "../components/Sides.vue"


export default {
  components: {Topping, Beverage, Sides,},
  name: 'available',

  data(){
    return{

    showMe: "Toppings",
    };
  },
  computed:{
    
  }
}
</script>

<style>

</style>