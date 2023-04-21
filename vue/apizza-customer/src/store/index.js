import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    cart: [],
    customPizzaCount: 0,
    customerOrder: {
      orderType: null,
      deliveryTime: null,
    },
    reviews: [
      {
        name: 'Mike Smith',
        rating: 5,
        comment: "APIzza is simply the best pizza store out there! Their pizzas are delicious and always arrive hot and fresh.",
      },
      {
        name: 'Jenna Gardner',
        rating: 5,
        comment: "I have ordered from APIzza multiple times, and I have never been disappointed. The quality of their ingredients and the taste of their pizzas is always top-notch.",
      },
      {
        name: 'David Johnson',
        rating: 5,
        comment: "I highly recommend APIzza to anyone who loves pizza. Their selection is amazing, and their customer service is fantastic.",
      },
      {
        name: 'Hugh Janus',
        rating: 5,
        comment: "APIzza has become my go-to pizza place. Their pizzas are consistently delicious, and their delivery is always on time.",
      },
      {
        name: 'Michael Davis',
        rating: 5,
        comment: "I cannot say enough good things about APIzza. Their pizzas are always fresh and full of flavor, and their prices are unbeatable.",
      },
    ],
    names: [
      "John Gotti",
      "Mike Oxbrown",
      "David Johnson",
      "Hugh Janus",
      "Michael Davis",
      "Laura Wilson",
    ],
  },
  mutations: {
    addToCart(state, payload) {
      const itemIndex = state.cart.findIndex(
        (item) => item.id === payload.id && item.size === payload.size && item.options === payload.options
      );
      if (itemIndex !== -1) {
        state.cart[itemIndex].quantity++;
      } else {
        if (payload.type === 'custom') {
          payload.name = "Custom Pi";
        }
        state.cart.push({ ...payload, quantity: 1 });
      }
    },
    removeItemFromCart(state, index) {
      state.cart.splice(index, 1);
    },
    setOrderType(state, orderType) {
      state.customerOrder.orderType = orderType;
    },
    addReview(state, review) {
      state.reviews.unshift(review);
    },
  },
  actions: {
    updateOrderType({ commit }, orderType) {
      commit('setOrderType', orderType);
    },
  },
});