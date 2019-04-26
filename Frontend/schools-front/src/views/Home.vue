<template>
  <div class="home">
    <md-card md-with-hover @click.native="goToSchools">
      <md-ripple>
        <md-card-header>
          <div class="md-title">Escolas</div>
          <div class="md-subhead">Quantidade de escolas cadastradas</div>
        </md-card-header>

        <md-card-content>
          <AnimatedCounter :value="schoolsQuantity" :cssClass="{'md-display-4': true}"/>
        </md-card-content>
      </md-ripple>
    </md-card>
    <md-card md-with-hover>
      <md-ripple>
        <md-card-header>
          <div class="md-title">Turmas</div>
          <div class="md-subhead">Quantidade de turmas cadastradas</div>
        </md-card-header>

        <md-card-content>
          <AnimatedCounter :value="classesQuantity" :cssClass="{'md-display-4': true}"/>
        </md-card-content>
      </md-ripple>
    </md-card>  
  </div>
</template>

<script>
import AnimatedCounter from "@/components/AnimatedCounter.vue";

export default {
  name: "home",
  data() {
    return {
      schoolsQuantity: 0,
      classesQuantity: 0,
    }
  },
  components: {
    AnimatedCounter
  },
  mounted(){
    fetch('http://localhost:64552/api/school/count')
      .then(response => response.text())
      .then(data => this.schoolsQuantity = parseInt(data))
      .catch(e => console.error(e));
  },
  methods:{
    goToSchools(){
      this.$router.push('/escolas');
    }
  }
}
</script>

<style lang="scss" scoped>
  .home{
    display: flex;
    align-items: center;
    justify-content: center;

    .md-card {
      width: 320px;
      margin: 4px;
      display: inline-block;
      vertical-align: top;

      .md-card-content{
        text-align: center;

        .md-display-4{
          color: var(--md-theme-default-primary, #69f0ae);
        }
      }
    }
  }
</style>
