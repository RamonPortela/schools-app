<template>
  <div id="app">

    <md-app md-mode="reveal">
      <md-app-toolbar class="md-primary">
      <router-link tag="h3" to="/" class="md-title" style="flex: 1">Sistema de Registro de Escolas</router-link>
      <md-button to="/escolas">Escolas</md-button>
      <md-button to="/turmas">Turmas</md-button>
      </md-app-toolbar>
      <md-app-content>
        <router-view />
      </md-app-content>
    </md-app>    
  </div>
</template>

<style lang="scss">
@import "~vue-material/dist/theme/engine"; // Import the theme engine

@include md-register-theme("default", (
  primary: md-get-palette-color(green, A200), // The primary color of your application
  accent: md-get-palette-color(pink, 500) // The accent or secondary color
));

@import "~vue-material/dist/theme/all"; // Apply the theme
// #app {
//   font-family: "Avenir", Helvetica, Arial, sans-serif;
//   -webkit-font-smoothing: antialiased;
//   -moz-osx-font-smoothing: grayscale;
//   text-align: center;
//   color: #2c3e50;
// }
// #nav {
//   padding: 30px;
//   a {
//     font-weight: bold;
//     color: #2c3e50;
//     &.router-link-exact-active {
//       color: #42b983;
//     }
//   }
// }
</style>
