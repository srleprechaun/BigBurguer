import axios from 'axios';

const headers = {
    'Content-Type': 'application/json'
};

const api = axios.create({ baseURL: 'http://localhost:5000/api', headers: headers });

export default api;