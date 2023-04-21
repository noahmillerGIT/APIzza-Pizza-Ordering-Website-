<template>
  <div class="checkout-container">
      <button @click="goBack" class="new-add-to-cart-button shrink-border go-back-button">
      Go Back
    </button>
    <div class="toggle-container">
      <p>Pickup / Delivery</p>
      <label class="toggle">
        <input type="checkbox" v-model="isDelivery" />
        <span class="slider round"></span>
      </label>
    </div>
    <div class="order-summary">
     <h2>Order Summary</h2>
      <ul>
        <li v-for="(item, index) in formattedCartItems" :key="index" class="order-item">
          <span>{{ item.size }} {{ item.name }}</span>
          <span>${{ item.price.toFixed(2) }}</span><!-- Display the price for each item -->
        </li>
      </ul>
      <hr class="divider" />
      <div class="total-price">
        <span>Total:</span>
        <span>${{ totalPrice.toFixed(2) }}</span> <!-- Display the total price -->
      </div>
    </div>
    <div class="payment-method">
      <h2>Choose Payment Method</h2>
      <div class="payment-image-placeholder">
        <img src="https://i.imgur.com/oc9rhHe.jpeg" alt="Image Placeholder" class="payment-image">
      </div>
      <div class="radio-buttons">
        <label>
          <input type="radio" name="payment" value="cash" v-model="selectedPayment" />
          Cash
        </label>
        <label>
          <input type="radio" name="payment" value="card" v-model="selectedPayment" />
          Card
        </label>
      </div>
      <div v-if="selectedPayment === 'card'" class="credit-card-input">
        <label for="card-number">Card Number:</label>
        <input type="text" id="card-number" maxlength="16" inputmode="numeric" pattern="[0-9]*" v-model="cardNumber" />
        <label for="expiry-date">Expiry Date:</label>
        <input type="text" id="expiry-date" placeholder="MM/YYYY" v-model="expiryDate" />
        <label for="cvv">CVV:</label>
        <input type="text" id="cvv" maxlength="3" inputmode="numeric" pattern="[0-9]*" v-model="cvv" />
      </div>
    </div>
    <div class="order-details">
  <h2>Order Details</h2>
      <div>
        <span>Type:</span>
        <span>{{ getOrderType }}</span>
     </div>
      <div>
        <span>Total Cost:</span>
        <span>${{ totalPrice.toFixed(2) }}</span> <!-- Shows total cost in order details -->
      </div>
      <div>
        <span>Payment Method:</span>
        <span v-if="selectedPayment === 'cash'">Straight Cash $</span>
        <span v-else-if="selectedPayment === 'card'">Card: **** **** **** {{ cardNumber.slice(12) }}</span>
      </div>
      <hr class="divider" />
      <div class="input-container">
        <label for="HomeType">Home Type:</label>
        <input type="text" id="homeType" required v-model="address.homeType" />
        <label for="streetAddress">Street Address:</label>
        <input type="text" id="streetAddress" required v-model="address.streetAddress"/>
      </div>
      <div class="input-container">
        <label for="city">City:</label>
        <input type="text" id="city" required v-model="address.city" />
        <label for="zip-code">Zip Code:</label>
        <input type="text" id="zip-code" required v-model="address.zip" />
        <label for="state">State:</label>
        <input type="text" id="stateAbbreviation" required v-model="address.stateAbbreviation" />
      </div>
      <div class="input-container">
        <label for="phone-number">Phone Number:</label>
        <input type="tel" id="phone-number" required v-model="customerOrder.phoneNumber" />
        <label for="email">Email Address:</label>
        <input type="email" id="email" required v-model="customerOrder.email" />
      </div>
      <label for="special-instructions">Special Instructions:</label>
      <textarea id="special-instructions" rows="4" v-model="address.instructions"></textarea>
    </div>
     <button
      @click="placeOrder"
      class="btn btn-primary place-order-button"
      :disabled="!isFormComplete"
      :class="{ 'hover-effect': isFormComplete }"
    >
      Place Order
    </button>
  </div>
</template>


<script>

export default {
  name: 'FinalCheckOut',
  computed: {
    cartItems() {
      return this.$store.state.cart;
    },
    // Add a new computed property to format cart items with their prices
  formattedCartItems() {
  return this.cartItems.map(item => {
    let price = 0;

    const basePrices = {
      'PEP-C': 3,
      'MTN DEW-WHILE LOOP': 3,
      'SPRITES': 3,
      'ROUTER BEER': 3,

      'Bread Bits': 7,
      'C-ZER SALAD': 7,
      'BONELESS BASH WINGS': 7,
      'C#OCOLATE C#IP COOKIE': 7,
      

      'OOP': 12.99,
      '2 STRAWZ': 12.99,
      'Meaty Marshall': 12.99,
      'Bacon Byte': 12.99,
       'C# - CHEESE': 12.99,
      'Polymushroomism': 12.99,
      'Gold Miller': 12.99,
      'Template Tula': 12.99,
       'PAT GPT': 12.99,
      'The Elizzah': 12.99,
      'TWO TOM-MATO PI': 12.99,
      'Walts Golden Fungi': 12.99,
      
    };

     if (item.name.includes('Custom Pi')) {
      const basePrice = {
        SM: 8.99,
        MD: 10.99,
        LG: 12.99,
        XL: 15.99,
      };
      price = basePrice[item.size] || 0;
      price += (item.toppings || []).length * 1.5;
    } else {
      price = basePrices[item.name] || 0;
    }

    return {
      ...item,
      price,
    };
  });
},
    // Add a new computed property to calculate the total price
    totalPrice() {
      return this.formattedCartItems.reduce((total, item) => total + item.price, 0);
    },
    isFormComplete() {
  const requiredFieldsFilled =
    this.address.streetAddress &&
    this.address.city &&
    this.address.zip &&
    this.customerOrder.phoneNumber;

  const creditCardInfoRequired =
    this.selectedPayment === 'card' && this.cardNumber && this.expiryDate && this.cvv;

  return (
    requiredFieldsFilled && (this.selectedPayment === 'cash' || creditCardInfoRequired)
  );
},
      getOrderType() {
      return this.isDelivery ? 'Delivery' : 'Pickup';
    }
  },
  data() {
    return {
      address: {
        homeType: '',
        streetAddress: '',
        apartment: '',
        city: '',
        stateAbbreviation: '',
        zip: '',
        addressID: 0,
        instructions: ''
      },
      customerOrder: {
        orderId: 0,
        orderCost: 0,
        email: '',
        phoneNumber: '',
        orderStatus: '',
        orderTime: '',
        orderType: '',
        addressId: ''
      },
      pizzas: [{
        pizzaId: 0,
        size: {
          size: 0
        },
        toppings: [
          {
            topping: 0
          }
        ],
        _Price: 0,
        crusts: {
          crusts: 0
        },
        sauces: {
          sauces: 0
        },
        pizzaQuantity: 0
      }
      ],
       isDelivery: false,
       selectedPayment: null,
       cardNumber: '',
       expiryDate: '',
       cvv: '',
    };
  },

  watch: {
    isDelivery(newVal) {
      this.toggleDelivery(newVal);
    },
  },
  methods: {
    toggleDelivery(isDelivery) {
      if (isDelivery) {
        console.log("Delivery selected");
      } else {
        console.log("Pickup selected");
      }
    },
    goBack() {
      this.$router.go(-1);
    },
    placeOrder() {
      if (this.isFormComplete) {
        this.$router.push({ name: 'ReviewPage' });
      } else {
        console.log("Form is not complete");
      }
    },
  },
};
</script>

<style scoped>
.checkout-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
  max-width: 800px; /* Adjust this value according to your preference */
  margin: 0 auto;
  padding: 20px;
  box-sizing: border-box;
}

.toggle-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
}

.toggle {
  position: relative;
  display: inline-block;
  width: 60px;
  height: 34px;
  margin-bottom: 0.5rem;
}

.toggle input {
  display: none;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}

input:checked + .slider {
  background-color: #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

.order-summary,
.payment-method {
  background-color: white;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  width: 100%;
  margin-bottom: 20px;
}

.order-item {
  display: flex;
  justify-content: space-between;
  width: 100%;
  margin-bottom: 10px;
}

.divider {
  border: none;
  border-top: 1px solid #ccc;
  margin: 10px 0;
}

.total-price {
  display: flex;
  justify-content: space-between;
  width: 100%;
  font-weight: bold;
  margin-top: 10px;
}

.payment-image-placeholder {
  width: 100%;
  height: 150px; /* Adjust the height according to your preference */
  background-color: white;
  margin-bottom: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: hidden;
}

.payment-image {
  max-width: 100%;
  max-height: 100%;
}

.radio-buttons {
  display: flex;
  justify-content: space-around;
  width: 100%;
  margin-bottom: 20px;
}

.credit-card-input {
  display: flex;
  flex-direction: column;
}

.credit-card-input label {
  margin-bottom: 5px;
}

.credit-card-input input {
  margin-bottom: 10px;
}

.order-details {
  background-color: white;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  width: 100%;
  margin-bottom: 20px;
}

.order-details > div {
  display: flex;
  justify-content: space-between;
  width: 100%;
  margin-bottom: 10px;
}

.order-details input,
.order-details textarea {
  width: 100%;
  padding: 6px 12px;
  margin: 4px 0 12px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  font-size: 14px;
}

.order-details label {
  display: block;
  margin-bottom: 4px;
  font-weight: bold;
}

textarea {
  resize: vertical;
}

.place-order-button {
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
  border: 3px solid black; /* another Pre hover border */
  background-color: red; /* Pre hover background */
  color: black;/* Pre hover text */
  font-weight: bold;
}

.place-order-button:active {
  transform: translateY(3px);
}

.place-order-button::after, .place-order-button::before {
  border-radius: 3px;
}

.place-order-button::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border: 3px solid black;/* Pre hover border*/
  transition: opacity 0.3s, border 0.3s;
}

.place-order-button:hover::before {
  opacity: 0;
}

.place-order-button::after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: transparent;
  border: 3px solid red;/* Post hover border */
  opacity: 0;
  z-index: -1;
  transform: scaleX(1.1) scaleY(1.3);
  transition: transform 0.3s, opacity 0.3s;
}

.place-order-button:hover::after {
  opacity: 1;
  transform: scaleX(1) scaleY(1);
}

.place-order-button:hover {
  background-color: green;
  box-shadow: none;
  color: black; /* Post hover text */
}

 .go-back-button {
  font-family: 'Montserrat', sans-serif;
  text-transform: uppercase;
  position: fixed; /* Change from 'relative' to 'fixed' */
  top: 20px; /* Add space from the top */
  left: 20px; /* Add space from the left */
  border: none;
  font-size: 18px;
  transition: color 0.5s, transform 0.2s, background-color 0.2s;
  outline: none;
  border-radius: 3px;
  margin: 0 10px;
  padding: 23px 33px;
  border: 3px solid black; /* Pre hover border */
  background-color: red; /* Pre hover background */
  color: black;/* Pre hover text */
  font-weight: bold;
  z-index: 1000; /* Add z-index to ensure button stays on top */
}

  .go-back-button:active {
    transform: translateY(3px);
  }

  .go-back-button::after, .go-back-button::before {
    border-radius: 3px;
  }

  .go-back-button::before {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    border: 3px solid red;/* Pre hover border*/
    transition: opacity 0.3s, border 0.3s;
  }

  .go-back-button:hover::before {
    opacity: 0;
  }

  .go-back-button::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: transparent;
    border: 3px solid red;/* Post hover border */
    opacity: 0;
    z-index: -1;
    transform: scaleX(1.1) scaleY(1.3);
    transition: transform 0.3s, opacity 0.3s;
  }

  .go-back-button:hover::after {
    opacity: 1;
    transform: scaleX(1) scaleY(1);
  }

  .go-back-button:hover {
    background-color: red;
    box-shadow: none;
    color: black; /* Post hover text */
  }

  .input-container {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 10px;
  }
  
  .input-container label {
    flex: 1;
  }
  
  .input-container input {
    flex: 3;
    margin-left: 10px;
  }
  
  #address {
    flex: 6;
  }
  
  #zip-code {
    flex: 1;
  }
  
  #special-instructions {
    width: 100%;
    margin-top: 10px;
    margin-bottom: 20px;
  }
  
</style>


