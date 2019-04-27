<template>
  <div></div>
</template>

<script>
import Request from '../../http-helper/HttpHelper';

export default {
  mounted() {
      const {id} = this.$route.params;

      Request.get(`school/${id}`)
        .then(response => console.log(response))
        .catch(e => console.error(e));
  }
};
</script>

<style lang="scss" scoped>
</style>