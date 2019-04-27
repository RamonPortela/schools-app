<template>
  <div>
    <!-- <md-button 
      class="md-icon-button md-raised md-primary rounded-button"
      @click="showDialog = true"
    >
      <md-icon>add</md-icon>
    </md-button> -->
    <PaginatedTable
      tableTitle="Escolas"
      searchPlaceHolder="Procure pelo nome da escola..."
      titleEmpty="Nenhuma Escola Encontrada"
      descriptionEmpty="Nenhuma escola foi encontrada com este nome. Faça uma nova busca ou adicione uma nova escola."
      buttonEmptyText="Adicionar Nova Escola"
      :columns="columns"
      endpoint="/school"
      :bus="eventBus"
      @openModalForm="openModalForm"
      @deletedItem="snackBarMessage='Escola Removida'; showSnackbar=true"
    />


    <md-dialog 
      :md-active.sync="showDialog"
      @md-closed="clearForm"
      :md-click-outside-to-close="!sending"
      :md-close-on-esc="!sending"
    >
        <md-dialog-title>{{modalTitle}}</md-dialog-title>

      <form novalidate @submit.prevent="validateSchool" style="padding:0 20px">
          <div class="md-layout">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('name')">
                <label for="name">Nome da Escola</label>
                <md-input name="name" id="name" v-model="form.name" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.name.required">O nome da escola é obrigatório</span>
              </md-field>
            </div>
          </div>

        <md-dialog-actions>
          <md-button class="md-primary" @click="showDialog = false">Cancelar</md-button>
          <md-button type="submit" class="md-primary">Salvar</md-button>
        </md-dialog-actions>
      </form>
    </md-dialog>

    <md-snackbar md-position="center" :md-duration="4000" :md-active.sync="showSnackbar" md-persistent>
      <span>{{snackBarMessage}}</span>
      <md-button class="md-primary" @click="showSnackbar = false">Fechar</md-button>
    </md-snackbar>

  </div>
</template>

<script>
import PaginatedTable from "../../components/PaginatedTable";
import Vue from 'vue';
import { validationMixin } from 'vuelidate'
import { required } from 'vuelidate/lib/validators'
import Request from '../../http-helper/HttpHelper';

export default {
  mixins: [validationMixin],
  components: {
    PaginatedTable
  },
  data() {
    return {
      columns: [
        { label: "ID", value: "id" },
        { label: "Nome", value: "name" },
        { label: "Quantidade de Turmas", value: "classesQuantity" }
      ],
      showDialog: false,
      eventBus: new Vue(),
      modalTitle: '',
      sending: false,
      editing: false,
      form:{
        name: null,
        id: null,
      },
      showSnackbar: false,
      snackBarMessage: null
    };
  },

  validations: {
    form: {
      name:{
        required
      }
    }
  },

  methods:{
    openModalForm(school){
      if(school.id){
        this.editing = true;
        this.modalTitle = 'Editar Escola';
        this.form.name = school.name;
        this.form.id = school.id;
      }
      else{
        this.editing = false;
        this.modalTitle = 'Adicionar Escola'
        this.clearForm();
      }

      this.showDialog = true;
    },
    getValidationClass (fieldName) {
      const field = this.$v.form[fieldName]

      if (field) {
        return {
          'md-invalid': field.$invalid && field.$dirty
        }
      }
    },
    clearForm(){
      this.$v.$reset();
      this.form.name = null;
      this.sending = false;
    },
    validateSchool () {
      this.$v.$touch()

      if (!this.$v.$invalid) {
        if(this.editing)
          this.editSchool();
        else
          this.saveSchool();
      }
    },
    saveSchool(){
      const school = {
        name: this.form.name
      }

      this.sending = true;
      Request.post('school', school)
        .then(() => {
          this.sending = false;
          this.showDialog = false;
          this.snackBarMessage = 'Escola inserida com sucesso.';
          this.showSnackbar = true;
          this.eventBus.$emit('updateTable');
        })
        .catch(e =>{ 
          console.error(e); 
          this.sending = false;
          this.snackBarMessage = 'Erro durante a requisição, tente novamente';
          this.showSnackbar = true;
        });
    },
    editSchool(){
      const school = {
        id: this.form.id,
        name: this.form.name
      }

      this.sending = true;
      Request.put(`school/${school.id}`, school)
        .then(() => {
          this.sending = false;
          this.showDialog = false;
          this.snackBarMessage = 'Escola alterada com sucesso.';
          this.showSnackbar = true;
          this.eventBus.$emit('updateTable');
        })
        .catch(e =>{ 
          console.error(e); 
          this.sending = false;
          this.snackBarMessage = 'Erro durante a requisição, tente novamente';
          this.showSnackbar = true;
        });
    }
  },

  updateTable(){
    
  }
};
</script>

<style lang="scss" scoped>

</style>