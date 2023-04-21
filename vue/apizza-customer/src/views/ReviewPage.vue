<template>
  <div class="review-page">
    <h1>Thank you for your order!</h1>
    <Review />
    <PreviousReviews />
  </div>
</template>

<script>
import Review from "@/components/Review.vue";
import PreviousReviews from "@/components/PreviousReviews.vue";

export default {
  name: "ReviewPage",
  components: {
    Review,
    PreviousReviews,
  },
};
</script>

<style scoped>
.review-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 20px;
  margin-top: 20px;
  width: 100%;
}
</style>