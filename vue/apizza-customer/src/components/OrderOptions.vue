<template>
  <div class="order-options">
     <button @click="goBack" class="new-add-to-cart-button shrink-border go-back-button">
      Go Back
    </button>
    <div class="options-container">
      <div v-for="(option, index) in options" :key="index" class="option-box">
        <h2>{{ option.title }}</h2>
        <img :src="option.image" :alt="option.title" class="option-image">
        <div class="options" v-if="option.type === 'radio'">
          <div class="radio-option" v-for="(choice, i) in option.choices" :key="i">
            <input
              type="radio"
              :id="`${option.title}-${choice}`"
              :name="option.title"
              :value="choice"
              v-model="selectedOptions[option.title]"
            >
            <label :for="`${option.title}-${choice}`">{{ choice }}</label>
          </div>
        </div>
        <div class="options" v-else>
          <div class="checkbox-option" v-for="(choice, i) in option.choices" :key="i">
            <input
              type="checkbox"
              :id="`${option.title}-${choice}`"
              :value="choice"
              v-model="selectedOptions[option.title]"
            >
            <label :for="`${option.title}-${choice}`">{{ choice }}</label>
          </div>
        </div>
      </div>
      <!-- toppings selection -->
      <div class="toppings-selection">
        <!-- ... -->
      </div>
      <!-- "Add to Cart" button & the button effect  -->
    <button @click="addToCart" class="new-add-to-cart-button shrink-border" :disabled="isAddToCartDisabled">
  Add to Cart
</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "OrderOptions",
  data() {
    return {
      options: [
        {
          title: "Pizza Size",
          type: "radio",
          image: "https://i.imgur.com/Fkb7upp.png",
          choices: ["SM", "MD", "LG", "XL"],
        },
        {
          title: "Crust Type",
          type: "radio",
          image: "https://i.imgur.com/QKUeJyV.jpeg",
          choices: ["Thin Crust", "Hand Tossed"],
        },
        {
          title: "Sauce Choice",
          type: "radio",
          image: "https://i.imgur.com/DGsgjpY.jpg",
          choices: ["Red", "White", "Azure"],
        },
        {
          title: "Toppings",
          type: "checkbox",
          image: "https://i.imgur.com/1mVtTF7.jpeg",
          choices: [
            "Pepperoni",
            "Extra Cheese",
            "Ham",
            "Onion",
            "Bacon",
            "Mushroom",
            "Pineapple",
            "Spinach",
            "Sausage",
            "Garlic",
            "Chicken",
            "Tomato",
            "Steak",
            "Green Peppers",
            "Gold Flake",
            "Banana Peppers",
          ],
        },
      ],
      selectedOptions: {
        "Pizza Size": "",
        "Crust Type": "",
        "Sauce Choice": "",
        "Toppings": [],
      },
      cart: [],
      pizzaCounter: 0,
    };
  },
  computed: {
    isAddToCartDisabled() {
      // Check if the required options are selected so user can choose 'add to cart'
      return (
        !this.selectedOptions["Pizza Size"] ||
        !this.selectedOptions["Crust Type"] ||
        !this.selectedOptions["Sauce Choice"]
      );
    },
  },
  methods: {
    newItem() {
      return {
        type: "Pizza",
        name: "Custom Pi",
        size: this.selectedOptions["Pizza Size"],
        crust: this.selectedOptions["Crust Type"],
        sauce: this.selectedOptions["Sauce Choice"],
        toppings: this.selectedOptions["Toppings"],
        quantity: 1,
      };
    },
    addToCart() {
      const newItem = this.newItem();
      console.log("Adding to cart:", newItem);
      this.$store.commit("addToCart", newItem); // Use Vuex store to add the item to the cart
      console.log("Cart:", this.$store.state.cart); // Access the cart from the Vuex store
      this.selectedOptions = {
        "Pizza Size": "",
        "Crust Type": "",
        "Sauce Choice": "",
        "Toppings": [],
      };
    },
     goBack() {
      this.$router.push({ name: "HomeView" });
    },
  },
};
</script>

<style scoped>
.order-options {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 20px;
  margin-top: 20px;
  width: 100%;
}

.options-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
  width: 100%;
}

.option-box {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
  max-width: 500px;
  padding: 20px;
  background-color: ivory;
  border-radius: 5px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  text-align: center;
  border: 2px solid red;
}

.option-image {
  width: 100%;
  height: auto;
  object-fit: cover;
  margin-bottom: 10px;
}

.options {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  width: 100%;
  gap: 10px;
}

.add-to-cart-container {
  position: fixed;
  bottom: 20px;
  left: 50%;
  transform: translateX(-50%);
}

.new-add-to-cart-button {
  background-color: red;
  color: white;
  font-size: 2rem;
  padding: 15px 40px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.new-add-to-cart-button:hover {
  background-color: yellow;
  transition: background-color 0.3s ease-in-out;
}

.radio-option {
  display: flex;
  flex-direction: column;
  align-items: center;
  /* Add these lines to make size buttons evenly spaced */
  flex: 1;
  min-width: calc(25% - 20px);
}

.checkbox-option {
  display: flex;
  flex-direction: column;
  align-items: center;
  /* Update these lines to make toppings options in 2 columns */
  flex: 1;
  min-width: calc(50% - 20px);
}

.radio-option input[type="radio"] {
  display: none;
}

.radio-option label {
  display: inline-block;
  padding: 10px 15px;
  width: 100px; /* Set uniform width */
  text-align: center; /* Center text */
  background-color: #fff1d3;
  border: 2px solid rgb(0, 0, 0); /* Add border */
  border-radius: 3px;
  cursor: pointer;
  transition: background-color 0.3s, border-color 0.3s;
  margin: 5px; /* Add margin for spacing */
}

.radio-option input[type="radio"]:checked + label {
  background-color: #d7f5a8;
  border-color: #999; /* Change border color on checked */
}

/* Custom checkbox button style */
.checkbox-option input[type="checkbox"] {
  display: none;
}

.checkbox-option label {
  display: inline-block;
  padding: 10px 15px;
  width: 210px;
  background-color: #fff1d3;
  border: 2px solid rgb(0, 0, 0); /* Add border */
  border-radius: 3px;
  cursor: pointer;
  transition: background-color 0.3s, border-color 0.3s;
  margin: 5px 10px; /* Adjust margin for better spacing */
  white-space: nowrap; /* Prevent text from wrapping to the next line */
}

.checkbox-option input[type="checkbox"]:checked + label {
  background-color: #d7f5a8;
  border-color: #999; /* Change border color on checked */
}


.container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100vh;
  width: 100vw;
  text-align: center;
}

button {
  font-family: 'Montserrat', sans-serif;
  text-transform: uppercase;
  position: relative;
  border: none;
  font-size: 18px;
  transition: color 0.5s, transform 0.2s, background-color 0.2s;
  outline: none;
  border-radius: 3px;
  margin: 0 10px;
  padding: 23px 33px;
  border: 3px solid white;
}

button:active {
  transform: translateY(3px);
}

button::after,
button::before {
  border-radius: 3px;
}

.shrink-border {
  background-color: red;
  color: black;  /* pre hover - text color*/
  font-weight: bold;
}

.shrink-border:hover {
background-color: transparent;
box-shadow: none;
color: black; /* Post hover - text color / */
font-weight: bold;
}

.shrink-border::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border: 3px solid rgb(0, 0, 0); /*Pre hover border color */
  transition: opacity 0.3s, border 0.3s;
}

.shrink-border:hover::before {
  opacity: 0;
}

.shrink-border::after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgb(14, 233, 6); /* Post hover background */
  border: 3px solid black; /* Post hover border*/
  opacity: 0;
  z-index: -1;
  transform: scaleX(1.1) scaleY(1.3);
  transition: transform 0.3s, opacity 0.3s;
}

.shrink-border:hover::after {
  opacity: 1;
  transform: scaleX(1) scaleY(1);
}

.go-back-button {
  position: absolute;
  top: 0;
  left: 0;
  z-index: 1;
}


</style>
 