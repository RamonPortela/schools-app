import Vue from "vue";
import VueMaterial from "vue-material";
import App from "./App.vue";
import router from "./router";
import "vue-material/dist/vue-material.min.css";

Vue.config.productionTip = false;

Vue.use(VueMaterial);
Vue.component("router-link", Vue.options.components.RouterLink);
Vue.component("router-view", Vue.options.components.RouterView);

new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
