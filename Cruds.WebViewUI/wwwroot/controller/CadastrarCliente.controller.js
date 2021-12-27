sap.ui.define([
	'sap/ui/core/mvc/Controller',
	"sap/ui/core/routing/History",
	"sap/m/MessageBox",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/format/DateFormat",

], function (Controller, History, MessageBox, JSONModel) {
	"use strict";


	var CadastrarClienteController = Controller.extend("cliente.lista.controller.CadastrarCliente", {

		onInit: function (oEvent) {


			var oRouter = this.getOwnerComponent().getRouter();
			oRouter.getRoute("atualizarCliente").attachPatternMatched(this.aoCoincidirComARotaDeAtualizar, this);
			oRouter.getRoute("cadastrarCliente").attachPatternMatched(this.aoCoincidirComARotaDeCadastro, this);


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

		aoCoincidirComARotaDeAtualizar: async function (oEvent) {


			this.codigo = oEvent.getParameter("arguments").codigo;
			const dados = await fetch(`/api/Cliente/${this.codigo}`);
			const cliente = await dados.json();
			const oModel = new JSONModel(cliente);
			this.getView().setModel(oModel, "cliente");

			if (!cliente.nome) {
				var oRouter = this.getOwnerComponent().getRouter();
				oRouter.navTo("listagemName", {}, true);
			}
		},

		aoCoincidirComARotaDeCadastro: function (oEvent) {

			var oModel = new JSONModel()
			this.getView().setModel(oModel, "cliente");

		},

		getClienteModel: function () {
			return this.getView().getModel("cliente").getData();

		},

		verificaSeOsCamposEstaoVazios: function (ModelCliente) {
			let cliente = ModelCliente;
			if (cliente.nome == null
				&& cliente.cpf == null
				&& cliente.dataDeNascimento == null
				&& cliente.email == null
				&& cliente.rendaMensal == null) {

				return null;
			} else {
				return ModelCliente;


			}

		},

		salvarNoBancoDeDados: async function () {

			let cliente = this.verificaSeOsCamposEstaoVazios(this.getClienteModel())

			if (cliente == null) {

				MessageBox.warning("Preencha todos os campos");
				return;
			}

			if (cliente.codigo == null) {

				const uri = await fetch('/api/Cliente', {
					method: 'POST',
					headers: {
						'Accept': 'application/json',
						'Content-Type': 'application/json'

					},

					body: JSON.stringify(cliente)

				});
				console.log(cliente)
				const content = await uri.json();
				var oRouter = this.getOwnerComponent().getRouter();
				MessageBox.alert("Cliente cadastrado com sucesso!", {
					onClose: function () {
						oRouter.navTo("listagemName", {}, true);
					}
				});

			} else {

				const uri = await fetch('/api/Cliente', {
					method: 'PUT',
					headers: {
						'Accept': 'application/json',
						'Content-Type': 'application/json'
					},
					body: JSON.stringify(cliente)

				});
				console.log(cliente)
				const content = await uri.json();
				var oRouter = this.getOwnerComponent().getRouter();
				MessageBox.alert("Cliente alterado com sucesso!", {
					onClose: function () {
						oRouter.navTo("listagemName", {}, true);
					}
				});

			}

		},

		onEventoCancelar: function () {
			var oRouter = this.getOwnerComponent().getRouter();
			MessageBox.confirm("Ao cancelar todos os dados serão perdidos, tem certeza disso?", {
				onClose: function () {

					oRouter.navTo("listagemName", {}, true);
				}
			});
		},

		_setLocalTimeZoneZone: function (datevalue) {
			if (datevalue !== undefined && datevalue !== null && datevalue !== "") {
				datevalue = new Date(datevalue);
				var offSet = datevalue.getTimezoneOffset();
				var offSetVal = datevalue.getTimezoneOffset() / 60;
				var h = Math.floor(Math.abs(offSetVal));
				var m = Math.floor((Math.abs(offSetVal) * 60) % 60);
				datevalue = new Date(datevalue.setHours(h, m, 0, 0));
				return datevalue;
			}
			return null;
		},
		oDateTimew: function (oEvent) {
			oDate = new sap.m.Input({
				value: {
					parts: [
					 {path: "{cliente>/dataDeNascimento}", type: new sap.ui.model.type.DateTime(), formatOptions: {UTC:true} }

					]
                }
            })
		},
		getOriginalDateTime: function (dateTime) {
			if (dateTime !== undefined && dateTime !== null && dateTime !== "") {
				var dateFormat = DateFormat.getInstance({
					UTC: true,
					pattern: "MM/dd/yyyy"
				});
				var originalDate = dateFormat.format(new Date(dateTime));
				return originalDate;
			}
			return null;
		},		
		handleChange: function (oEvent) {
			var oText = this.byId("textResult"),
				oDP = oEvent.getSource(),
				sValue = oEvent.getParameter("value"),
				bValid = oEvent.getParameter("valid");

			this._iEvent++;
			oText.setText("Change - Event " + this._iEvent + ": DatePicker " + oDP.getId() + ":" + sValue);

			if (bValid) {
				oDP.setValueState(ValueState.None);
			} else {
				oDP.setValueState(ValueState.Error);
			}
		},



	});
	return CadastrarClienteController
});