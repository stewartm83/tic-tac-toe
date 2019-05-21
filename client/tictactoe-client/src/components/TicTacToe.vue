<template>
  <div class="hello">
    <div id="result" v-if="resultMessage">{{resultMessage}}</div>
    <table>
      <tr id="row0" class="row">
        <td ref="cell0" class="spot" @click="handleClick(0)"></td>
        <td ref="cell1" class="spot" @click="handleClick(1)"></td>
        <td ref="cell2" class="spot" @click="handleClick(2)"></td>
      </tr>
      <tr id="row1" class="row">
        <td ref="cell3" class="spot" @click="handleClick(3)"></td>
        <td ref="cell4" class="spot" @click="handleClick(4)"></td>
        <td ref="cell5" class="spot" @click="handleClick(5)"></td>
      </tr>
      <tr id="row2" class="row">
        <td ref="cell6" class="spot" @click="handleClick(6)"></td>
        <td ref="cell7" class="spot" @click="handleClick(7)"></td>
        <td ref="cell8" class="spot" @click="handleClick(8)"></td>
      </tr>
    </table>
    <button @click="resetGame()">Restart</button>
  </div>
</template>
<script>
import axios from "axios";

export default {
  name: "TicTacToe",
  props: {
    startPlayer: String
  },
  data() {
    return {
      resultMessage: "",
      gameId: null,
      apiUrl: process.env.VUE_APP_API_URL
    };
  },
  mounted() {
    this.activePlayer = this.startPlayer || "X";
    this.createNewGame(this.activePlayer);
  },
  methods: {
    resetGame() {
      this.resultMessage = "";
      for (let i = 0; i < 9; i++) {
        const item = "cell" + i;
        this.$refs[item].textContent = "";
      }
      this.createNewGame(this.activePlayer);
    },
    createNewGame(marker) {
      axios({
        method: "POST",
        url: this.apiUrl + "/api/Games?marker=" + marker
      }).then(
        result => {
          this.gameId = result.data.id;
        },
        error => {
          console.error(error);
        }
      );
    },
    updateCell(cellNumber, marker) {
      this.$refs["cell" + cellNumber].textContent = marker;
    },
    handleClick(n) {
      const item = "cell" + n;

      if (this.$refs[item].textContent !== "") {
        alert("Square already taken");
        return;
      }
      this.errorMessage = "";

      this.updateCell(n, this.activePlayer);

      const position = {
        index: n,
        marker: this.activePlayer,
        gameId: this.gameId
      };
      axios({
        method: "POST",
        url: this.apiUrl + "/api/Positions",
        data: JSON.stringify({
          index: n,
          marker: this.activePlayer,
          gameId: this.gameId
        }),
        headers: { "Content-Type": "application/json" }
      }).then(
        result => {
          if (result.data.message) {
            this.resultMessage = result.data.message;
            alert(result.data.message);
            return;
          }
          this.updateCell(result.data.index, result.data.marker);
        },
        error => {
          console.error(error);
        }
      );
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
body {
  background-color: #e0e4cc;
  font-family: "Montserrat", sans-serif;
  margin: 0;
  padding: 0;
  text-align: center;
}

h1 {
  font-size: 3em;
}

table {
  width: 75%;
  margin: auto;
  margin-top: 10px;
  font-family: "Arimo", sans-serif;
  font-size: 70px;
}

td {
  display: inline-block;
  width: 50px;
  height: 50px;
  line-height: 80px;
  background-color: #69d2e7;
  color: white;
  border: none;
  border-radius: 10%;
  box-shadow: 0 0.03em 0.08em rgba(0, 0, 0, 0.5);
  margin: 8px;
  transition: background-color 0.2s ease;
}

td:hover {
  cursor: pointer;
  background-color: #fa6900;
}

.selected {
  background-color: #fa6900;
}

tr {
  list-style-type: none;
  width: 500px;
}

#selection {
  height: 60px;
  font-size: 1.5em;
}

button {
  border: none;
  background: transparent;
  width: 50px;
  height: 50px;
  margin: 0.3em;
  border-radius: 50%;
}

.hover:hover {
  background: #f38630;
  color: white;
}

:active {
  outline: none;
}

:focus {
  outline: none;
}

.seed {
  background: #f38630;
  color: white;
}

#result {
  padding-top: 10px;
  height: 40px;
  font-size: 1.5em;
}

@media only screen and (min-device-width: 1000px) {
  td {
    width: 120px;
    height: 120px;
    line-height: 120px;
    font-size: 120px;
  }
}
</style>