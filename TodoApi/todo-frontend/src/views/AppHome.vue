<template>
  <div class="home-container">
    <h1>Welcome to the Todo Application</h1>
    <h2>Features</h2>
    <ul class="features-list">
      <li>✔️ Add new tasks</li>
      <li>✔️ Mark tasks as complete</li>
      <li>✔️ Delete tasks</li>
      <li>✔️ Edit existing tasks</li>
    </ul>
    <div class="author-info">
      <p>Developed by <strong>Burak Dönmez</strong></p>
    </div>
  </div>
</template>

<style scoped>
.home-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  text-align: center;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

h1 {
  color: #3498db;
  margin-bottom: 20px;
}

h2 {
  color: #333;
  margin: 15px 0;
}

.features-list {
  list-style: none;
  padding: 0;
  margin: 0;
  text-align: left;
}

.features-list li {
  margin: 10px 0;
}

.author-info {
  margin-top: 30px;
  font-size: 18px;
  color: #666;
}
</style>
