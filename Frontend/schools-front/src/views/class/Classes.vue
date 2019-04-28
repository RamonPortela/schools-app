<template>
  <div>
    <PaginatedTable
      titleEmpty="Nenhuma Turma Encontrada"
      descriptionEmpty="Nenhuma Turma foi encontrada com este nome. Faça uma nova busca ou adicione uma nova turma."
      buttonEmptyText="Adicionar Nova Turma"
      :columns="columns"
      endpoint="/class"
      :bus="eventBus"
      @openModalForm="openModalForm"
      @deletedItem="snackBarMessage='Turma Removida'; showSnackbar=true"
    />


    <md-dialog 
      :md-active.sync="showDialog"
      @md-closed="clearForm"
      :md-click-outside-to-close="!sending"
      :md-close-on-esc="!sending"
    >
        <md-dialog-title>{{modalTitle}}</md-dialog-title>

      <form novalidate @submit.prevent="validateClass" style="padding:0 20px">
          <div class="md-layout">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('name')">
                <label for="name">Nome da Turma</label>
                <md-input name="name" id="name" v-model="form.name" :disabled="sending" />
                <span class="md-error" v-if="!$v.form.name.required">O nome da turma é obrigatório</span>
              </md-field>

            <md-field :class="getValidationClass('schoolId')">
                <label for="schoolId">Escola</label>
                <md-select name="schoolId" id="schoolId" v-model="form.schoolId" :disabled="sending">
                    <md-option></md-option>
                    <md-option v-for="school in schools" :key="school.id" :value="school.id">{{school.name}}</md-option>
                </md-select>
                <span class="md-error" v-if="!$v.form.schoolId.required">O campo escola é obrigatório</span>
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
        { label: "Nome", value: "name" }
      ],
      showDialog: false,
      eventBus: new Vue(),
      modalTitle: '',
      sending: false,
      editing: false,
      form:{
        name: null,
        id: null,
        schoolId: null,
      },
      showSnackbar: false,
      snackBarMessage: null,
      schools: null
    };
  },

  validations: {
    form: {
      name:{
        required
      },
      schoolId:{required}
    }
  },

  methods:{
    openModalForm(clazz){
      if(clazz.id){
        this.editing = true;
        this.modalTitle = 'Editar Turma';
        this.form.name = clazz.name;
        this.form.schoolId = clazz.school.id;
        this.form.id = clazz.id;
      }
      else{
        this.editing = false;
        this.modalTitle = 'Adicionar Turma'
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
      this.form.id = null;
      this.form.schoolId = null;
      this.sending = false;
    },
    validateClass () {
      this.$v.$touch()

      if (!this.$v.$invalid) {
        if(this.editing)
          this.editClass();
        else
          this.saveClass();
      }
    },
    saveClass(){
      const clazz = {
        name: this.form.name,
        schoolId: this.form.schoolId
      }

      this.sending = true;
      Request.post('class', clazz)
        .then(() => {
          this.sending = false;
          this.showDialog = false;
          this.snackBarMessage = 'Turma inserida com sucesso.';
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
    editClass(){
      const clazz = {
        id: this.form.id,
        name: this.form.name,
        schoolId: this.form.schoolId
      }

      this.sending = true;
      Request.put(`class/${clazz.id}`, clazz)
        .then(() => {
          this.sending = false;
          this.showDialog = false;
          this.snackBarMessage = 'Turma alterada com sucesso.';
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
  },
    mounted(){
        Request.get('school')
        .then(response => this.schools = response.data.map(school => {
            return {
                id: school.id,
                name: school.name,
            }
        }))
    }
};
</script>

<style lang="scss" scoped>

</style>