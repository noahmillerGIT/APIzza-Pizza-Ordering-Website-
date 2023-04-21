<template>
  <section>
    <h1 v-on:click="resetForm()">CLEAR FORM {{ $store.currentuser }}</h1>
    <table>
      <thead>
        <tr>
          <th>order number</th>
          <th>status</th>
          <th>customer email</th>
          <th>customer phone</th>
          <th v-show="false">Assigned to Emp #</th>
          <th>employee has time to lean?</th>
          <th>order time</th>
          <th>change status</th>
        </tr>
        <td>
          <input
            type="number"
            id="numberSearch"
            placeholder="Search by order Number"
            v-model="ONS"
          /><br /><button v-on:click="ONS = ''">Clear</button>
        </td>
        <td>
          <select v-model="filterStatus" name="filterStatus" id="dropdown">
            <option value="Pending">Pending</option>
            <option value="In Progress">In Progress</option>
            <option value="Completed">Completed</option>
            <option value="Cancelled">Cancelled</option>
          </select>
        </td>
        <td></td>
        <td></td>
        <td v-show="false">
          <input
            type="number"
            id="empIDSearch"
            placeholder="Search by Emp #"
            v-model="ENS"
          /><button v-on:click="ENS = ''">Clear</button>
        </td>
        <td></td>
        <td>
          <div v-show="this.invalidTime">Understand linear time much?</div>
          From<input type="date" id="timeFrom" v-model="timeFrom" />To<input
            type="date"
            id="timeTo"
            v-model="timeTo"
          />
        </td>
      </thead>
      <tbody>
        <tr
          v-for="order in filterOrders"
          :key="order.orderID"
          :value="order.value"
        >
          <td
            :class="{
              ultrapriority: order.orderCost > 100,
              priority: order.orderCost > 50,
            }"
          >
            {{ order.orderId }}
          </td>
          <td>
            <span>{{ order.orderStatus }}</span>
          </td>
          <td>
            <span>{{ order.email }}</span>
          </td>
          <td>{{ order.phoneNumber }}</td>
          <td v-show="false">{{ order.employeeId }}</td>
          <td style="color: black; font-size: 30px; font-weight: bold">NO</td>
          <td>{{ formatDate(order.orderTime) }}</td>
          <td>
            <div>
              <button
                v-on:click="markCancelled(order)"
                v-show="order.orderStatus != 'Cancelled'"
              >
                Cancel
              </button>
              <button
                v-on:click="markCompleted(order)"
                v-show="order.orderStatus != 'Completed'"
              >
                Complete
              </button>
              <button
                v-on:click="markInProgress(order)"
                v-show="order.orderStatus != 'In Progress'"
              >
                In Progress
              </button>
              <button
                v-on:click="markPending(order)"
                v-show="order.orderStatus != 'Pending'"
              >
                Pending
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </section>
</template>

<script>
import axios from "axios";
import moment from "moment";

export default {
  name: "order-list",

  methods: {
    resetForm() {
      this.filterStatus = "Pending";
      this.ONS = "";
      this.ENS = "";
      this.timeFrom = "";
      this.timeTo = "";
    },
    populateTable() {
      axios
        .get("employee/api/order")
        .then((response) => {
          this.orders = response.data;
        })
        .catch((error) => {
          console.log("FAILED TO GET ORDERS. WHAT DID YOU DO", error);
        });
    },
    markCancelled(order) {
      axios.put(
        `/employee/api/order/${order.orderId}/status?updateStatus=Cancelled`)
      .then((response) => {
        this.order2 = response.data;
        if (this.order2){
          order.orderStatus = "Cancelled";
        }
      })
    },
    async markCompleted(order) {
      axios.put(
        `/employee/api/order/${order.orderId}/status?updateStatus=Completed`)
      .then((response) => {
        this.order2 = response.data;
        if (this.order2){
          order.orderStatus = "Completed";
        }
      })
    },
    async markInProgress(order) {
      await axios.put(
        `/employee/api/order/${order.orderId}/status?updateStatus=In%20Progress`)
      .then((response) => {
        this.order2 = response.data;
        if (this.order2){
          order.orderStatus = "In Progress";
        }
      })
    },
    async markPending(order) {
      await axios.put(
        `/employee/api/order/${order.orderId}/status?updateStatus=Pending`)
      .then((response) => {
        this.order2 = response.data;
        if (this.order2){
          order.orderStatus = "Pending";
        }
      })
    },
    formatDate(date) {
      return moment(date).format("MMM D, YYYY h:mm A");
    },
  },
  computed: {
    filterOrders: function () {
      if (
        this.invalidTime == false &&
        this.timeFrom != "" &&
        this.timeTo != ""
      ) {
        return this.orders.filter(
          (order) =>
            order.orderStatus == this.filterStatus &&
            (this.ONS == "" || order.orderId == this.ONS) &&
            (this.ENS == "" || order.employeeId == this.ENS) &&
            order.orderTime >= this.timeFrom &&
            order.orderTime <= this.timeTo
        );
      } else
        return this.orders.filter(
          (order) =>
            order.orderStatus == this.filterStatus &&
            (this.ONS == "" || order.orderId == this.ONS) &&
            (this.ENS == "" || order.employeeId == this.ENS)
        );
    },
    invalidTime: function () {
      if (this.timeFrom != "" && this.timeTo != "") {
        return this.timeFrom > this.timeTo;
      } else return false;
    },
  },

  data() {
    return {
      order2: false,
      orders: [],
      filterStatus: "Pending",
      ONS: "",
      ENS: "",
      timeFrom: "",
      timeTo: "",
      currentUser: "",
      selectedStatus: "",
    };
  },

  created() {
    this.populateTable();
  },
};
</script>

<style>
table,
th,
td {
  border: 5px solid;
  border-color: palegoldenrod;
  background-color: rosybrown;
  padding: 20px, 10px;
  text-align: center;
}
th {
  width: 100px;
}
.priority {
  border: 10px solid;
  background-color: red;
  padding: 30px;
}
.ultrapriority {
  border: 50px double;
  background-color: gold;
  padding: 100px;
}
</style>