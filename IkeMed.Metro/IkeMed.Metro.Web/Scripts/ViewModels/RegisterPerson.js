function RegisterPerson() {
    var self = this;
    self.addresses = ko.observableArray();
}

var registerPerson = new RegisterPerson();

registerPerson.addresses.subscribe(function () {
    $('#addessesContainer').jtable({
        title: 'Endereços'
        , actions: {
            listAction: function (postData, jtParams) {
                return {
                    "Result": "OK",
                    "Records": registerPerson.addresses(),
                    "TotalRecordCount": registerPerson.addresses().length
                };
            }
            //, createAction: '/GettingStarted/CreatePerson'
            //, updateAction: '/GettingStarted/UpdatePerson'
            //, deleteAction: '/GettingStarted/DeletePerson'
        }
        , fields: {
            ID: {
                key: true,
                list: false
            }
            , Street: {
                title: 'Rua',
                width: '100%'
            }
        }
    });
    $('#addessesContainer').jtable('load');
});

ko.applyBindings(registerPerson, document.getElementById('_personForm'));