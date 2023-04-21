<template>
  <section>
    <h2>Sides</h2>
    <span
      >Showing sides which are currently
      {{ displayAvailabilityThatIs ? "available" : "unavailable" }}</span
    >
    <br />
    <button v-on:click="toggleAvailabilityView()">
      {{
        displayAvailabilityThatIs ? "Show unavailable sides" : "Show available sides"
      }}
    </button>
    <table>
      <thead>
        <tr>
          <th>Side Name</th>
          <th>Price</th>
          <th>Availability</th>
          <th>
            <img
              width="50px"
              src="@/assets/angry.png"
              v-on:click="alert('WHAT THE HELL ARE YOU DOING? GET TO WORK!')"
            />
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="side in filterSides" :key="side.sideId">
          <td>{{ side.itemName }}</td>
          <td>{{ side.price }}</td>
          <td>{{ side.availability == 1 ? "Available" : "Not Available" }}</td>
          <td>
            <button type="submit" v-on:click="changeAvailability(side)">
              {{
                side.availability == 1 ? "Make Unavailable" : "Make Available"
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
  name: "Sides",

  methods: {
    populateSides() {
      axios
        .get("api/side")
        .then((response) => {
          this.sides = response.data;
        })
        .catch((error) => {
          console.log("asd", error);
        });
    },
    changeAvailability(side) {
      axios
        .put("api/side", side)
        .then(this.populateSides())
        .catch((error) => {
          alert(error);
        });
    },
    toggleAvailabilityView() {
      this.displayAvailabilityThatIs = !this.displayAvailabilityThatIs;
    },
    alert(string) {
      alert(string);
    },
  },
  data() {
    return {
      sides: [],
      displayAvailabilityThatIs: true,
    };
  },
  computed: {
    filterSides: function () {
      return this.sides.filter(
        (side) => side.availability == this.displayAvailabilityThatIs
      );
    },
  },
  created() {
    this.populateSides();
  },
};
</script>

<style>
</style>