import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },
  addSide(itemData) {
    return axios.post('/api/sides', itemData)
  },
  addBeverage(beverageData){
    return axios.post('/api/beverage', beverageData);
  }
}
