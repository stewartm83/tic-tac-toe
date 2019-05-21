<template>
  <div class="game">
    <h1>Tic Tac Toe Game</h1>
    <h3>You are playing as: {{startPlayer}}</h3> 
    <TicTacToe :startPlayer='startPlayer'/>
  </div>
</template>
<script>
// @ is an alias to /src
import TicTacToe from "@/components/TicTacToe.vue";

export default {
  name: "game",
  components: {
    TicTacToe
  },
  data() {
    return {
      startPlayer: ""
    };
  },
  created() {
    this.startPlayer = this.$route.params.startPlayer;
  }
};
</script>
