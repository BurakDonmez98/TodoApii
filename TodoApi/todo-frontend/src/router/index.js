import { createRouter, createWebHistory } from 'vue-router';
import TodoList from '../views/TodoList.vue';
import Home from '../views/AppHome.vue'; 


const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/todos',
    name: 'TodoList',
    component: TodoList
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
