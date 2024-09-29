<template>
  <div class="App todo-list-container" :style="{ backgroundColor: backgroundColor }">
    <h1>Todo List</h1>
    <div class="input-container">
      <input
        v-model="newTodo"
        id="new-todo"
        name="newTodo"
        placeholder="Add new todo"
        class="todo-input"
      />
      <button @click="addTodo" class="add-button">Add</button>
    </div>
    <div v-if="loading" class="loading">Yükleniyor...</div>
    <ul class="todo-list">
      <li v-for="todo in todos" :key="todo.id" class="todo-item">
        <span :class="{ completed: todo.isComplete }">{{ todo.title }}</span>
        <div class="button-group">
          <button @click="deleteTodo(todo.id)" class="delete-button">Delete</button>
        </div>
      </li>
    </ul>
    <div v-if="message" class="message">{{ message }}</div>
  </div>
</template>

<script>
import axios from 'axios';
import '../assets/styles/TodoList.css'; 

export default {
  data() {
    return {
      todos: [],
      newTodo: '',
      loading: false,
      message: '',
      backgroundColor: '#f9f9f9', 
    };
  },
  mounted() {
    this.getTodos();
  },
  methods: {
    async getTodos() {
      this.loading = true;
      try {
        const response = await axios.get('http://localhost:5291/api/todo');
        console.log(response.data);
        this.todos = response.data;
      } catch (error) {
        console.error('Error fetching todos:', error);
      } finally {
        this.loading = false;
      }
    },
    async addTodo() {
      if (!this.newTodo) return; 
      this.loading = true;
      try {
        await axios.post('http://localhost:5291/api/todo', { title: this.newTodo });
        this.message = 'Todo added successfully';
        this.backgroundColor = '#d4edda'; 
        this.newTodo = '';
        await this.getTodos();
      } catch (error) {
        console.error('Error adding todo:', error);
      } finally {
        this.loading = false;
        setTimeout(() => {
          this.message = '';
          this.backgroundColor = '#f9f9f9'; 
        }, 3000);
      }
    },
    async deleteTodo(id) {
      this.loading = true;
      try {
        await axios.delete(`http://localhost:5291/api/todo/${id}`);
        this.message = 'ToDo Deleted!';
        this.backgroundColor = '#f8d7da';
        await this.getTodos();
      } catch (error) {
        console.error('Error deleting todo:', error.response ? error.response.data : error.message);
      } finally {
        this.loading = false;
        setTimeout(() => {
          this.message = ''; 
          this.backgroundColor = '#f9f9f9';
        }, 3000);
      }
    },
  }
}
</script>

<style>
.loading {
    text-align: center;
    color: #3498db; 
    font-weight: bold;
}

.message {
    text-align: center;
    margin-top: 20px;
    font-size: 18px;
    font-weight: bold;
}

.todo-list {
    list-style-type: none;
    padding: 0;
}

.todo-item {
    margin-bottom: 10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.completed {
    text-decoration: line-through;
    color: #28a745;
}

.button-group {
    display: flex;
    gap: 10px;
}

.delete-button {
    background-color: #e74c3c;
    color: white;
    border: none;
    cursor: pointer;
    padding: 5px 10px;
    border-radius: 4px;
}

.delete-button:hover {
    background-color: #c0392b;
}
</style>
