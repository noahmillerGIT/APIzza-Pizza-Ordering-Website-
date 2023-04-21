import axios from 'axios';

const http = axios.create({
  baseURL: "https://localhost:44315"
});

export default {
  postOrder(order) {
    console.log('Sending order data:', order);
    return axios.post('api/cart/', order);
  },
  //post() {
  //  return http.get('api/custompizza');
  //},
  GetListOfSpecialtyPizza(){
    return http.get('api/specialty');
  },
  GetListOfSides(){
    return http.get('api/sides');
  },
  GetListOfBeverages(){
    return http.get('get');
  }
};
