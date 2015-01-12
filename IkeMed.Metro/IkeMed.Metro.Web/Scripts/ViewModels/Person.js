function Person() {
    var self = this;
    self.ID = 0;
    self.addresses = ko.observableArray();
    self.phones = ko.observableArray();
    self.documents = ko.observableArray();
}

var person = new Person();

person.addresses.subscribe(function () {
    //console.log('person.addresses()', person.addresses());
    var addressesLength = person.addresses().length;
    $('#addessesContainer').jtable({
        title: 'Endereços'
        , actions: {
            listAction: function (postData, jtParams) {
                return {
                    "Result": "OK",
                    "Records": person.addresses(),
                    "TotalRecordCount": addressesLength
                };
            }
            , createAction: function (postData) {
                return $.Deferred(function ($dfd) {
                    $.ajax({
                        url: '/Person/PostAddress?personId=' + person.ID,
                        type: 'POST',
                        dataType: 'json',
                        data: postData,
                        success: function (data) {
                            $dfd.resolve(data);
                        },
                        error: function () {
                            $dfd.reject();
                        }
                    });
                });
            }
            , updateAction: '/Person/PostAddress?personId=' + person.ID
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
            }
            , Number: {
                title: 'Número',
            }
            , Complement: {
                title: 'Complemento',
            }
            , Neighborhood: {
                title: 'Bairro',
            }
            , ZipCode: {
                title: 'CEP',
            }
            , City: {
                title: 'Cidade'
            }
            , State: {
                title: 'UF'
            }
            , AddressType: {
                title: 'Tipo',
                options: '/helpers/GetJsonFromEnum?enumName=AddressTypeEnum&enumNamespace=Enums&assemblyName=IkeMed.Model'
            }
        }
    });
    $('#addessesContainer').jtable('load');
});

person.phones.subscribe(function () {
    //console.log('person.addresses()', person.addresses());
    var phonesLength = person.phones().length;
    $('#phonesContainer').jtable({
        title: 'Telefones'
        , actions: {
            listAction: function (postData, jtParams) {
                return {
                    "Result": "OK",
                    "Records": person.phones(),
                    "TotalRecordCount": phonesLength
                };
            }
            , createAction: function (postData) {
                return $.Deferred(function ($dfd) {
                    $.ajax({
                        url: '/Person/PostPhone?personId=' + person.ID,
                        type: 'POST',
                        dataType: 'json',
                        data: postData,
                        success: function (data) {
                            $dfd.resolve(data);
                        },
                        error: function () {
                            $dfd.reject();
                        }
                    });
                });
            }
            , updateAction: '/Person/PostPhone?personId=' + person.ID
            , deleteAction: '/Person/DeletePhone'
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
            , Number: {
                title: 'Número',
            }
            , PhoneType: {
                title: 'Tipo',
                options: '/helpers/GetJsonFromEnum?enumName=PhoneTypeEnum&enumNamespace=Enums&assemblyName=IkeMed.Model'
            }
        }
    });
    $('#phonesContainer').jtable('load');
});

person.documents.subscribe(function () {
    //console.log('person.documents()', person.documents());
    var documentsLength = person.documents().length;
    $('#documentsContainer').jtable({
        title: 'Documentos'
        , actions: {
            listAction: function (postData, jtParams) {
                return {
                    "Result": "OK",
                    "Records": person.documents(),
                    "TotalRecordCount": documentsLength
                };
            }
            , createAction: function (postData) {
                return $.Deferred(function ($dfd) {
                    $.ajax({
                        url: '/Person/PostDocument?personId=' + person.ID,
                        type: 'POST',
                        dataType: 'json',
                        data: postData,
                        success: function (data) {
                            $dfd.resolve(data);
                        },
                        error: function () {
                            $dfd.reject();
                        }
                    });
                });
            }
            , updateAction: '/Person/PostDocument?personId=' + person.ID
            , deleteAction: '/Person/DeleteDocument'
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
            , Value: {
                title: 'Valor'
            }
            , DocumentType: {
                title: 'Tipo'
                , display: function (data) {
                    return data.record.DocumentType.Name;
                }
                , options: '/Person/GetDocumentTypes'
            }
        }
    });
    $('#documentsContainer').jtable('load');
});

ko.applyBindings(person, document.getElementById('_personForm'));