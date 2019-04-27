<template>
  <div :class="{content: loadingPage}">
    <md-progress-spinner
      :md-diameter="100"
      :md-stroke="10"
      md-mode="indeterminate"
      v-if="loadingPage"
    ></md-progress-spinner>
    <md-table v-model="data" md-card v-else>
      <md-table-toolbar>
        <div class="md-primary rounded-icon">
          <md-icon>search</md-icon>
        </div>


        <md-field md-clearable class="md-toolbar-section-end">
          <md-input placeholder="Pesquisar" v-model="search"/>
        </md-field>
      </md-table-toolbar>
      <md-table-empty-state :md-label="titleEmpty" :md-description="descriptionEmpty">
        <md-button class="md-primary md-raised" @click="openModalForm">{{buttonEmptyText}}</md-button>
      </md-table-empty-state>

      <md-table-row slot="md-table-row" slot-scope="{ item }">
        <md-table-cell
          v-for="column in columns"
          :md-label="column.label"
          :key="column.id"
        >{{ item[column.value] }}</md-table-cell>
        <md-table-cell md-label="Ações" md-numeric>
          <md-button class="md-icon-button" @click="openDetailModal(item)">
            <md-icon>remove_red_eye</md-icon>
          </md-button>
          <md-button class="md-icon-button" @click="openModalForm(item)">
            <md-icon>edit</md-icon>
          </md-button>
          <md-button class="md-icon-button" @click="deleteItem(item.id)">
            <md-icon>delete</md-icon>
          </md-button>
        </md-table-cell>
      </md-table-row>
    </md-table>

    <vue-ads-pagination
      :total-items="totalItemsCurrent"
      :max-visible-pages="3"
      :page="currentPage"
      :items-per-page="5"
      :loading="loadingData"
      v-if="!loadingPage"
    >
      <template slot-scope="props">
        <div
          class="vue-ads-pr-2 vue-ads-leading-loose"
        >Exibindo {{ props.start }} - {{ props.end }} de um total de {{ props.total }} registros</div>
      </template>
      <template slot="buttons" slot-scope="props">
        <vue-ads-page-button
          v-for="(button, key) in props.buttons"
          :key="key"
          v-bind="button"
          :class="{'bg-yellow-dark': button.active}"
          @page-change="currentPage = button.page; getData()"
          @range-change="start = button.start; end = button.end"
        />
      </template>
    </vue-ads-pagination>

    <md-dialog-confirm
      :md-active.sync="showConfirm"
      md-title="Apagar Registro"
      md-content="Você tem certeza que deseja apagar esse registro?."
      md-confirm-text="Confirmar"
      md-cancel-text="Cancelar"
      @md-cancel="onCancel"
      @md-confirm="onConfirm" />


    <md-dialog :md-active.sync="showDialog" v-if="selectedItem">
      <md-dialog-title>{{selectedItem.name}}</md-dialog-title>
      <div style="padding:0 20px">
        <md-table v-model="selectedItem.classes" md-card md-fixed-header v-if="selectedItem.classes.length > 1">
          <md-table-row slot="md-table-row" slot-scope="{ item }">
            <md-table-cell md-label="ID" md-numeric>{{ item.id }}</md-table-cell>
            <md-table-cell md-label="Name">{{ item.name }}</md-table-cell>
          </md-table-row>
        </md-table>
        <div v-else>
          Não há turma associada a essa escola
        </div>
        <div v-if="selectedItem.school">
          Escola: {{selectedItem.school.name}}
        </div>
      </div>
      <md-dialog-actions>
        <md-button class="md-primary" @click="showDialog = false">Fechar</md-button>
      </md-dialog-actions>
    </md-dialog>
  </div>
</template>

<script>
import Request from "../http-helper/HttpHelper";
import VueAdsPagination, { VueAdsPageButton } from "vue-ads-pagination";
import "../../node_modules/@fortawesome/fontawesome-free/css/all.css";
import "../../node_modules/vue-ads-pagination/dist/vue-ads-pagination.css";

export default {
  components: {
    VueAdsPagination,
    VueAdsPageButton
  },
  props: {
    tableTitle: {
      type: String,
      required: true
    },
    searchPlaceHolder: {
      type: String,
      required: true
    },
    titleEmpty: {
      type: String,
      required: true
    },
    descriptionEmpty: {
      type: String,
      required: true
    },
    buttonEmptyText: {
      type: String,
      required: true
    },
    columns: {
      type: Array,
      required: true
    },
    endpoint: {
      type: String,
      required: true
    },
    bus: {
      type: Object,
      required: true
    }
  },

  data() {
    return {
      loadingPage: true,
      loadingData: false,
      currentPage: 0,
      totalItems: 0,
      totalItemsCurrent: 0,
      search: "",
      data: [],
      searchTimer: null,
      showConfirm: false,
      idToDelete: null,
      showDialog: false,
      selectedItem: null,
    };
  },

  methods: {
    openModalForm(item) {
      this.$emit("openModalForm", item);
    },

    getData() {
      this.loadingData = true;
      Request.get(
        `${this.endpoint}?page=${this.currentPage + 1}${
          this.search ? "&search=" + this.search : ""
        }`
      )
        .then(resp => {
          this.data = resp.data;
          this.loadingPage = false;
          this.loadingData = false;
        })
        .catch(e => console.error(e));
    },

    deleteItem(id){
      this.showConfirm = true;
      this.idToDelete = id;
    },
    
    onCancel() {
      this.showConfirm = false;
    },
    onConfirm(){
      Request.delete(`${this.endpoint}/${this.idToDelete}`)
        .then(response => {
          Request.get(`${this.endpoint}/count?search=${this.search}`)
            .then(response => {
                this.totalItems = parseInt(response.data);
                this.totalItemsCurrent = this.totalItems;
                this.$emit('deletedItem');
                this.getData(this.search);
            })
            .catch(e => console.error(e));
        })
        .catch(e => console.error(e));
    },
    openDetailModal(item){
      this.selectedItem = item;
      this.showDialog = true;
    }
  },

  watch: {
    search(newSearch) {
      if (newSearch) {
        clearTimeout(this.searchTimer);
        this.searchTimer = setTimeout(() => {
          this.currentPage = 0;
          Request.get(`${this.endpoint}/count?search=${this.search}`)
            .then(response => {
              this.totalItemsCurrent = parseInt(response.data);
              this.getData(newSearch);
            })
            .catch(e => console.error(e));
          this.getData(newSearch);
        }, 500);
        return;
      }

      this.totalItemsCurrent = this.totalItems;
      this.getData();
    }
  },

  mounted() {
    Request.get(`${this.endpoint}/count`)
      .then(response => {
        this.totalItems = parseInt(response.data);
        this.totalItemsCurrent = this.totalItems;
        this.getData();
      })
      .catch(e => console.error(e));

    this.bus.$on("updateTable", () => {
        Request.get(`${this.endpoint}/count?search=${this.search}`)
            .then(response => {
                this.totalItems = parseInt(response.data);
                this.totalItemsCurrent = this.totalItems;
                this.getData(this.search);
            })
            .catch(e => console.error(e));
    });
  }
};
</script>

<style lang="scss" scoped>
.content {
  height: calc(100vh - 100px);
  display: flex;
  align-items: center;
  justify-content: center;
}
.vue-ads-bg-teal-dark {
  background-color: var(--md-theme-default-primary, #69f0ae);
}

.md-toolbar-section-end{
  flex: 20% 0 0;
}

.rounded-icon {
  border-radius: 50%;
  background: var(--md-theme-default-primary);
  padding: 5px;
  margin-right: 10px;
}

.vue-ads-m-2{
  margin: .5rem 1rem;
}
</style>