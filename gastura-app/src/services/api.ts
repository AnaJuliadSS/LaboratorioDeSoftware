 import axios from 'axios';

 const apiUrl = import.meta.env.VITE_API_URL;

 export const api = axios.create({
   baseURL: apiUrl
 });

 (window as any).api = api;


// import axios from 'axios';

//  export const api = axios.create({
//    baseURL: 'https://localhost:7218/api/'
//  });
