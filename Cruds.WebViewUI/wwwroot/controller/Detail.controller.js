sap.ui.define([
	'sap/ui/core/mvc/Controller',
	"sap/ui/core/routing/History",
	"sap/ui/model/json/JSONModel",
	"sap/m/MessageBox",
	'sap/m/MessageToast'
],
	function (Controller,History,JSONModel,MessageBox,MessageToast) {
	"use strict";

		return Controller.extend("clientes.lista.controller.Detail", {

			onInit: function () {

				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.getRoute("detail").attachPatternMatched(this._onObjectMatched, this);
			},
			
			_onObjectMatched: async function (oEvent) {

				this.codigo = oEvent.getParameter("arguments").codigo;
				const dados = await fetch(`/api/Cliente/${this.codigo}`);
				const cliente = await dados.json();
				const oModel = new JSONModel(cliente);
				this.getView().setModel(oModel, "cliente");
			
				if (!cliente.nome){
					var oRouter = this.getOwnerComponent().getRouter();
					oRouter.navTo("listagemName", {}, true);
				}

			},

			onNavBack: function () {
				var oHistory = History.getInstance();
				var sPreviousHash = oHistory.getPreviousHash();
			
				if (sPreviousHash !== undefined) {
					window.history.go(-1);
				} else {
					var oRouter = this.getOwnerComponent().getRouter();
					oRouter.navTo("listagemName", {}, true);
				}
			},
		
			deletarCliente:async function (oEvent){

				const cliente = this.getView().getModel("cliente").getData();
				var oRouter = this.getOwnerComponent().getRouter();
			
				MessageBox.warning("Deseja realmente remover este cliente?.", {
					actions: [MessageBox.Action.OK, MessageBox.Action.CANCEL],
					emphasizedAction: MessageBox.Action.OK,
					onClose: async function (sAction) {
						if (sAction === "OK") {
							const uri = await fetch(`/api/Cliente/${cliente.codigo}`, {
								method: 'DELETE',
								headers: {
									'Accept': 'application/json',
									'Content-Type': 'application/json'
								}
							});						
							const content = await uri.json();
							oRouter.navTo("listagemName", {}, true);						
						} else {
							MessageToast.show("Operação cancelada");
						}
					}
				});			
			},		
			onIrparaAtualizar: function(oEvent){
				console.log("Aqui")
				var oItem = oEvent.getSource();
				var oRouter = this.getOwnerComponent().getRouter();
				var cliente = this.getView().getModel("cliente").getData();
				oRouter.navTo("atualizarCliente", {
					codigo: window.encodeURIComponent(cliente.codigo)
				});
			}
	   });
});