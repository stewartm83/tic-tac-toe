<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <h1>Welcome to Your Vue.js App</h1>
    <div id="selection">
      Play as:
      <input style="font-size: 22px" type="radio" id="x" value="X" v-model="picked">
      <label for="x">X</label>
      <input style="font-size: 22px" type="radio" id="o" value="O" v-model="picked">
      <label for="o">O</label>
      <br>
      <span>Picked: {{ picked }}</span>
    </div>
    <!-- <router-link to="game">Start Game</router-link> -->
    <router-link :to="{ name: 'game', params: { startPlayer: picked }}">Start Game</router-link>
  </div>
</template>

<script>
export default {
  name: "",
  data() {
    return {
      picked: "X"
    };
  }
};
</script>