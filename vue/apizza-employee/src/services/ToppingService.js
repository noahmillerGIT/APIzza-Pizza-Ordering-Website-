import axios from 'axios';

const http = axios.create({
  baseURL: ""
});

export default {

  list(){
    //remember to change this 
    //@@@@@@@@@@@@@@@@@@@@@@@@
    //@@@@@@@@@@@@@@@@@@@@@@@@
    return http.get('/api/topping')
  },

  addTopping(topping) {
    return axios.post('/api/topping', topping)
  },

  changeAvailability(topping){
      return axios.put(`/api/topping/${topping.toppingid}`, topping)
  }

}
