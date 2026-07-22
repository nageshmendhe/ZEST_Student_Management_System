import axios from "axios";

const API_BASE_URL = (import.meta.env.VITE_API_BASE_URL || "").replace(/\/$/, "");
const TOKEN_KEY = "zestStudentJwt";

const api = axios.create({
  baseURL: API_BASE_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem(TOKEN_KEY);
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem(TOKEN_KEY);
      window.location.href = "/login";
    }
    return Promise.reject(error);
  }
);

export function setToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function removeToken() {
  localStorage.removeItem(TOKEN_KEY);
}

export function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export async function login(username, password) {
  return api.post("/api/Auth/login", { username, password });
}

export async function fetchStudents() {
  return api.get("/api/Student/GetAll");
}

export async function addStudent(student) {
  return api.post("/api/Student/Add", student);
}

export async function updateStudent(id, student) {
  return api.put(`/api/Student/Update/${id}`, student);
}

export async function deleteStudent(id) {
  return api.delete(`/api/Student/${id}`);
}

export default api;
