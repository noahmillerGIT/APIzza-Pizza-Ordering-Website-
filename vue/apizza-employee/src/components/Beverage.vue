<template>
  <section>
    <h2>Beverages</h2>
    <span
      >Showing Beverages which are currently
      {{ displayAvailabilityThatIs ? "available" : "unavailable" }}</span
    ><br /><button v-on:click="toggleAvailabilityView()">
      {{
        displayAvailabilityThatIs
          ? "Show unavailable Beverages"
          : "Show available Beverages"
      }}
    </button>
    <table>
      <thead>
        <tr>
          <th>Beverage Name</th>
          <th>Price</th>
          <th>Availability</th>
          <th>button</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="beverage in filterBeverages" :key="beverage.itemId">
          <td>{{ beverage.itemName }}</td>
          <td>{{ beverage.price }}</td>
          <td>
            {{ beverage.availability == 1 ? "Available" : "Not Available" }}
          </td>
          <td>
            <button type="submit" v-on:click="changeAvailability(beverage)">
              {{
                beverage.availability == 1
                  ? "Make Unavailable"
                  : "Make Available"
              }}
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </section>
</template>

<script>
import axios from "axios";
export default {
  name: "Beverage",
  methods: {
    populateBeverages() {
      axios
        .get("api/beverage")
        .then((response) => {
          this.beverages = response.data;
        })
        .catch((error) => {
          alert("you messed up", error);
        });
    },
    changeAvailability(item) {
      axios
        .put("api/beverage", item)
        .then(this.populateBeverages())
        .catch((error) => {
          alert(error);
        });
      this.populateBeverages();
    },
    toggleAvailabilityView() {
      this.displayAvailabilityThatIs = !this.displayAvailabilityThatIs;
    },
    alert(string) {
      alert(string);
    },
  },
  created() {
    this.populateBeverages();
  },
  data() {
    return {
      beverages: [],
      displayAvailabilityThatIs: true,
    };
  },
  computed: {
    filterBeverages() {
      return this.beverages.filter((beverage) => beverage.availability == this.displayAvailabilityThatIs);
    },
  },
};
</script>

<style>
</style>