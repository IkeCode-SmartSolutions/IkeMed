function ikeNotify() {
    this.show = function (config) {
        var newConfig = jQuery.parseJSON(config);
        //console.log(newConfig);

        //console.log('newConfig.type', newConfig.type);
        if (newConfig.type === undefined || newConfig.type == '')
            this.newConfig.type = "notification";

        //console.log('newConfig.theme', newConfig.theme);
        if (newConfig.theme === undefined || newConfig.theme == '')
            this.newConfig.theme = "green";

        //console.log('newConfig.message', newConfig.message);
        if (newConfig.message === undefined || newConfig.message == '')
            return;

        newConfig.notify = 'always';

        //console.log('this.newConfig', newConfig);
        if (newConfig.type == 'notification') {
            $.notification(newConfig);
        } else if (newConfig.type == 'alert') {
            $.alert(newConfig);
        }
    };
}
var ikeNotify = new ikeNotify();