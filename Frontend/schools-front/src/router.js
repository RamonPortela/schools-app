import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/schools",
      name: "schools",
      component: () => import("./views/school/Schools.vue")
    },
    {
      path: "/classes",
      name: "classes",
      component: () => import("./views/class/Classes.vue")
    }
  ]
});
