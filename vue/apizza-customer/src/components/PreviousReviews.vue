<template>
  <div class="previous-reviews-container">
    <div class="previous-reviews">
      
      <div class="average-review-box">
        <h2>Avg APIzza Review Score</h2>
        <img src="https://i.imgur.com/xfLZhsO.png" alt="Five stars" class="review-stars">
        <p>From 5043 reviews</p>
      </div>
      
      <h2>Previous 5-star reviews:</h2>
      <div class="previous-reviews-scrollable">
        <ul>
          <li v-for="(review, index) in previousReviews" :key="index" class="previous-review-box">
            <div class="review-header">
              <h3 class="review-name">{{ names[index] }}</h3>
              <img src="https://i.imgur.com/xfLZhsO.png" alt="Five stars" class="review-stars"> <!-- Move the image here -->
            </div>
            <div class="review-content-container">
              <p class="review-content">{{ review }}</p>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "PreviousReviews",
  data() {
    return {
      previousReviews: [
        "APIzza is simply the best pizza store out there! Their pizzas are delicious and always arrive hot and fresh.",
        "I have ordered from APIzza multiple times, and I have never been disappointed. Marshall is a smart guy with a ton of potential!",
        "I highly recommend APIzza to anyone who loves pizza. Tula was the most kind and compassionate person I've ever met",
        "APIzza has become my go-to pizza place. Their pizzas are consistently delicious. Joe goes above and beyond to make your experience special",
        "I cannot say enough good things about APIzza. Their pizzas are always fresh, their prices are unbeatable and Noah is a cutie-patootie with a humongous brain.",
        "If you're looking for the best pizza in town, look no further than APIzza. Their pizzas are simply amazing, and their customer service is second to none.",
      ],
      names: [
        "Mike Smith",
        "Jenna Gardner",
        "David Johnson",
        "Cameron Haywood",
        "Jennifer Cowens",
        "Lia Ford",
      ]
    };
  },
  methods: {
    addNewReview(review) {
      this.previousReviews.unshift(review.comment);
      this.names.unshift(review.name);
    },
  },
  created() {
    this.$root.$on("review-submitted", this.addNewReview);
  },
  beforeDestroy() {
    this.$root.$off("review-submitted", this.addNewReview);
  },
};
</script>

<style scoped>
html, body {
  height: 100%;
}

body {
  display: flex;
  flex-direction: column;
}

.previous-reviews-container {
  flex: none;
  position: fixed;
  bottom: 0;
  left: 50%; /* Change this from 'left: 0' to 'left: 50%' */
  background-color: transparent;
  padding: 20px;
  box-sizing: border-box;
  transform: translateX(-50%); /* Add this line to center the container horizontally */
}

.previous-reviews {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 20px;
  width: 100%;
  max-width: 1200px;
  margin: 0 auto;
}

.previous-reviews-scrollable {
  max-height: 300px;
  overflow-y: auto;
  scrollbar-color: #ec0d0d #ff0000; /* Set the color of the scrollbar */
  scrollbar-width: thick; /* Set the thickness of the scrollbar */
}

.previous-reviews-scrollable::-webkit-scrollbar {
  width: 15px; /* Set the width of the scrollbar */
}

.previous-reviews-scrollable::-webkit-scrollbar-track {
  background-color: #ffc956; /* Set the color of the track */
}

.previous-reviews-scrollable::-webkit-scrollbar-thumb {
  background-color: #ff0202; /* Set the color of the thumb */
  border-radius: 4px; /* Add some border radius to the thumb */
}

.previous-review-box {
  background-color: ivory;
  border: 1px solid black;
  padding: 20px;
  border-radius: 5px;
  font-size: 16px;
  line-height: 1.5;
  text-align: center;
  position: relative;
}

.review-header {
  display: flex;
  flex-direction: column; /* Change to column layout */
  justify-content: center;
  align-items: center;
  margin-bottom: 10px;
}

.review-stars {
  width: 25%; /*Size of stars in review */
  margin-top: 10px;
  margin-bottom: 10px; /* Add margin to separate from the review content */
}

.review-name {
  font-size: 20px;
  font-weight: bold;
  margin: 0;
}

.review-content {
  margin: 0;
}

.review-content-container {
  position: relative;
}

 .average-review-box {
    background-color: ivory;
    border: 1px solid black;
    padding: 20px;
    margin-right: -400px;
    margin-top: 150px;
    border-radius: 5px;
    font-size: 16px;
    line-height: 1.5;
    text-align: center;
    position: absolute;
    right: 0;
    top: 0;
    width: 300px; /* Set the width of the box */
  }

</style>