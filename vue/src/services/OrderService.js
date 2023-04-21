import axios from 'axios';

const http = axios.create({
    baseURL: "/api/pendingorders"
});

export default{

    list(){
        return http.get()
    },
}