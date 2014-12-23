function Person() {
    var self = this;
    self.addresses = ko.observableArray();
}

var person = new Person();

person.addresses.subscribe(function () {
    //console.log('person.addresses()', person.addresses());
    $('#addessesContainer').jtable({
        //messages: common.jTableMessages,
        //jqueryuiTheme: false,
        //useBootstrap: false,
        title: 'Endereços'
        , actions: {
            listAction: function (postData, jtParams) {
                return {
                    "Result": "OK",
                    "Records": person.addresses(),
                    "TotalRecordCount": person.addresses().length
                };
            }
            , createAction: '/Person/CreateAddress'
            , updateAction: '/Person/UpdateAddress'
            , deleteAction: '/Person/DeleteAddress'
        }
        , fields: {
            ID: {
                key: true,
                list: false
            }
            , DateIns: {
                list: false,
                type: 'hidden'
            }
            , LastUpdate: {
                list: false,
                type: 'hidden'
            }
            , IsActive: {
                list: false,
                type: 'hidden'
            }
            , Street: {
                title: 'Rua',
                //width: '100%'
            }
            , Number: {
                title: 'Número',
                //width: '100%'
            }
            , Complement: {
                title: 'Complemento',
                //width: '100%'
            }
            , Neighborhood: {
                title: 'Bairro',
                //width: '100%'
            }
            , ZipCode: {
                title: 'CEP',
                //width: '100%'
            }
            , City: {
                title: 'Cidade',
                //width: '100%'
            }
            , State: {
                title: 'UF',
                //width: '100%'
            }
            //, AddressType:{
            //    title: 'Tipo',
            //    //width: '100%'
            //}
        }
    });
    $('#addessesContainer').jtable('load');
});

ko.applyBindings(person, document.getElementById('_personForm'));