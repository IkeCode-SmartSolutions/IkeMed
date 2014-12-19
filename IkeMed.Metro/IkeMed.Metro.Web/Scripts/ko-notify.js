function koNotify() {
    this.config = ko.observable({});

    this.config.extend({ notify: 'always' });

    this.show = function () {

    };
}
var koNotify = new koNotify();

koNotify.config.subscribe(function () {
    config = jQuery.parseJSON(koNotify.config());
    //console.log(config);

    //console.log('config.type', config.type);
    if (config.type === undefined || config.type == '')
        koNotify.type = "notification";

    //console.log('config.theme', config.theme);
    if (config.theme === undefined || config.theme == '')
        config.theme = "green";

    //console.log('config.message', config.message);
    if (config.message === undefined || config.message == '')
        return;

    if (config.type == 'notification') {
        $.notification(config);
    } else if (config.type == 'alert') {
        $.alert(config);
    }
});

//ko.applyBindings(koNotify);