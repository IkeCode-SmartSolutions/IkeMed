function RegisterPerson() {
    var self = this;

    self.saveResult = function (e) {
        var result = $.parseJSON(e.responseText);
        //console.log(result);

        if (result.success) {
            $.notification({
                //title: "Title",
                message: "Registro salvo com sucesso!",
                position: 'topcenter',
                theme: 'green',
                interval: 3000,
                close: true
            });

        } else {
            $.alert({
                title: "Ops...",
                message: "Ocorreu um erro ao processar sua solicitação!",
                position: 'topcenter',
                theme: 'orange',
                interval: 3000,
                close: true
            });
        }
    }
}

var registerPerson = new RegisterPerson();