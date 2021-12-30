sap.ui.define([
		'sap/ui/core/mvc/Controller',
		'sap/ui/model/Filter',		
		'sap/ui/model/FilterOperator',
		'sap/ui/model/Sorter',
		'sap/ui/model/json/JSONModel',
	    'sap/m/MessageToast'
],
	function (Controller, Filter, FilterOperator, Sorter, JSONModel, MessageToast) {
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
				const oModel = new JSONModel(cliente);
			
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
				
				var aFilter = [];
				var sQuery = oEvent.getParameter("query");
				if (sQuery) {
					aFilter.push(new Filter("nome", FilterOperator.Contains, sQuery));
				}
				
				var oList = this.byId("ListagemCliente");
				var oBinding = oList.getBinding("items");
				oBinding.filter(aFilter);
			}

	    });

	return ListagemController;

});