import axios from "axios";

const Api = axios.create({
  baseURL: "https://balltalkapi.azurewebsites.net/api",
});

const authConfig = {
  headers: {
    Authorization:  `Bearer ${sessionStorage.getItem("accessToken")}`,
  }
};

const isLoggedIn = () => sessionStorage.getItem("accessToken");

const isAdmin = () => JSON.parse(sessionStorage.getItem("isAdmin")) === true;

const userId = () => sessionStorage.getItem("userId");

export { Api, authConfig, isLoggedIn, isAdmin, userId };