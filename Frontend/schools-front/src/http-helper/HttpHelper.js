import axios from "axios";

const Request = axios.create({
  baseURL: "http://localhost:64552/api/"
});

export default Request;
