<template>
  <div id="app">
    <md-app md-mode="reveal">
      <md-app-toolbar class="md-primary">
        <router-link tag="h3" to="/" class="md-title" style="flex: 1">Sistema de Registro de Escolas</router-link>
        <md-button to="/schools">Escolas</md-button>
        <md-button to="/classes">Turmas</md-button>
      </md-app-toolbar>
      <md-app-content>
        <router-view/>
      </md-app-content>
    </md-app>
  </div>
</template>

<script>
  export default{
    
  }
</script>

<style lang="scss">
@import "~vue-material/dist/theme/engine";

@include md-register-theme(
  "default",
  (
    primary: md-get-palette-color(green, A200),
    accent: md-get-palette-color(pink, 500)
  )
);

@import "~vue-material/dist/theme/all";
</style>
