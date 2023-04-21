<template>
<section>
    <h1>@@@@@@@@@@@@@@@@@@@@@</h1>
  <table>
      <thead>
      <tr>
      <th> order number </th>
      <th> status </th>
      </tr>
      </thead>
      <tbody>
          <tr v-for="order in orders" :key="order.orderId">
              <td>{{order.orderId}}</td>
              <td><span>{{order.orderStatus}}</span></td>
              </tr>
        </tbody>
      </table>
</section>
</template>

<script>
import OrderService from "@/services/OrderService";
export default {
    name: 'Order',
    data(){
        return {
            orders: []
        };
    },
    created(){
        OrderService.list().then((response) => {
            this.orders = response.data;
        });
    }

}
</script>

<style>

</style>