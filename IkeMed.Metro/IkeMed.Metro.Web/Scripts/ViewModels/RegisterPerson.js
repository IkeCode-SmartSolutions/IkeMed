function RegisterPerson() {
    var self = this;

    self.saveResult = function (e) {
        var result = $.parseJSON(e.responseText);
        //console.log(result);

        if (result.success) {
            $.Notify({ style: { background: 'green', color: 'white' }, content: "Registro salvo com sucesso!" });
        } else {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "Ocorreu um erro ao processar sua solicitação!" });
        }
    }
}

var registerPerson = new RegisterPerson();