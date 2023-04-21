import { createRouter, createWebHistory } from "../router"
import PizzaList from '../components/PizzaList.vue'
import PizzaDetails from '../components/PizzaDetails.vue'
import CreatePizza from '../components/CreatePizza.vue'
import Checkout from '../components/CheckOut.vue'

const routes = [
  { path: '/', component: PizzaList },
  { path: '/pizza/:id', component: PizzaDetails },
  { path: '/create-pizza', component: CreatePizza },
  { path: '/checkout', component: Checkout }
]

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
