sap.ui.define([
		'sap/ui/core/mvc/Controller',
		'sap/ui/model/Filter',		
		'sap/ui/model/FilterOperator',
		'sap/ui/model/Sorter',
		'sap/ui/model/json/JSONModel',
		'sap/m/MessageToast'
  ], function(Controller, Filter, FilterOperator, Sorter, JSONModel, MessageToast) {
	"use strict";
	 
	var ListagemController = Controller.extend("clientes.lista.controller.Listagem", 
	{

		getRouter: function () {
		 return this.getOwnerComponent().getRouter();
		},


		attachRouter(routerName, func) {
		    const router = this.getRouter();

			if (!!routerName) {
				router.getRoute(routerName).attachPatternMatched(func, this)
			} else {
				router.attachRouter(func, this);
			}
		},

		   onInit: function (evt) {
			 
				this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
				this.attachRouter("listagemName", this.buscarNoServidor)
				let numeroDeClientes = {
					nome: "",
				};
				const oModel = new JSONModel(numeroDeClientes)

			   this.bGrouped = false;
			   this.bDescending = false;
			   this.sSearchQuery = 0;
		},

			buscarNoServidor: async function () {

				const dados = await fetch(`/api/cliente/getall`);
				const cliente = await dados.json();
				const oModel = new JSONModel(cliente)
				this.getView().setModel(oModel, "cliente");
			
			},
			onPress: function (oEvent) {
				var oItem = oEvent.getSource();
				var oRouter = this.getOwnerComponent().getRouter();
		     	oRouter.navTo("detail", {
					codigo: window.encodeURIComponent(oItem.getBindingContext("cliente").getProperty("codigo"))
				});
			},

			onAdicionar: function (oEvent) {
				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("cadastrarCliente")			 
		   },
		    onFilter: function (oEvent) {
				this.sSearchQuery = 0;
				this.sSearchQuery = oEvent.getSource().getValue();
				this.fnApplyFiltersAndOrdering();
			},

     		fnApplyFiltersAndOrdering: function (oEvent) {
				var aFilters = [],
					aSorters = [];

				if (this.bGrouped) {
					aSorters.push(new Sorter("cliente>/nome", this.bDescending, this._fnGroup));
				} else {
					aSorters.push(new Sorter("cliente>/codigo", this.bDescending));
				}

				if (this.sSearchQuery) {
					var oFilter = new Filter("cliente>/codigo", FilterOperator.Contains, this.sSearchQuery);
					aFilters.push(oFilter);
				}

					this.byId("ListagemCliente").getBinding("items").filter(aFilters).sort(aSorters);
			},

				onReset: function (oEvent) {
					this.bGrouped = false;
					this.bDescending = false;
					this.sSearchQuery = 0;
					this.byId("cliente>/nome").setValue("");

					this.fnApplyFiltersAndOrdering();
				},
			

	    });

	return ListagemController;

});