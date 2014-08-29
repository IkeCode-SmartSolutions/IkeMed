(function ($, _window, _element) {
    var metroAlert = {
        init: function (options, defaultOptions) {
            var self = this;
            self.options = $.extend({}, defaultOptions, options);
            self.options.localize = {
                yes: 'Sim',
                no: 'N&atilde;o',
                ok: 'OK',
                cancel: 'Cancelar',
                close: 'Fechar'
            };
            self.setLocalize();
            if (self.options.type != "notification") self.show();
            else self.showNotification()
        },

        show: function () {
            var self = this;
            var d = $(_window).height();
            self.shade = $(self.template.shade);
            self.notif = $(self.template.alert);
            self.wrap = $(self.template.wrap);
            self.wrap.css({
                paddingTop: self.options.vspace,
                paddingBottom: self.options.vspace,
                paddingLeft: self.options.hspace,
                paddingRight: self.options.hspace
            });
            self.content = $(self.template.content);
            if (self.options.maxheight != null) self.content.css({
                maxHeight: self.options.maxheight,
                overflowY: 'auto',
                overflowX: 'hidden',
            });
            self.foot = $(self.template.foot);
            if (self.options.title != null) {
                self.title = $(self.template.title);
                self.title.html(self.options.title);
                self.title.prependTo(self.wrap)
            }
            self.content.html(self.options.message);
            self.content.appendTo(self.wrap);
            switch (self.options.type) {
                case 'confirm':
                    {
                        self.yesBtn = $(self.template.buttons.yes);
                        if (typeof self.options.yes == "string") self.yesBtn.html(self.options.yes);
                        self.yesBtn.appendTo(self.foot);
                        self.yesBtn.bind("click", function () {
                            self.hide(true)
                        });
                        self.noBtn = $(self.template.buttons.no);
                        if (typeof self.options.no == "string") self.noBtn.html(self.options.no);
                        self.noBtn.appendTo(self.foot);
                        self.noBtn.bind("click", function () {
                            self.hide(false)
                        })
                    }
                    break;
                case 'prompt':
                    {
                        self.input = $(self.template.input);
                        if (self.options.placeholder != '') self.input.find("input").attr("placeholder", self.options.placeholder);
                        self.input.appendTo(self.wrap);
                        self.okBtn = $(self.template.buttons.ok);
                        if (typeof self.options.yes == "string") self.okBtn.html(self.options.yes);
                        self.okBtn.appendTo(self.foot);
                        self.okBtn.bind("click", function () {
                            if (self.input.find("input").val() == '') self.input.find("input").focus();
                            else self.hide(self.input.find("input").val())
                        });
                        self.cancelBtn = $(self.template.buttons.cancel);
                        if (typeof self.options.no == "string") self.cancelBtn.html(self.options.no);
                        self.cancelBtn.appendTo(self.foot);
                        self.cancelBtn.bind("click", function () {
                            self.hide(false)
                        })
                    }
                    break;
                default:
                    {
                        self.customButton = new Array();
                        $.each(self.options.buttons, function (i, button) {
                            self.customButton[i] = $(self.template.buttons.btn);
                            self.customButton[i].html(button);
                            self.customButton[i].attr("id", i);
                            self.customButton[i].bind("click", function () {
                                self.hide(i)
                            });
                            self.customButton[i].appendTo(self.foot)
                        });
                        if (self.options.close) {
                            self.closeBtn = $(self.template.buttons.close);
                            if (typeof self.options.close == "string") self.closeBtn.html(self.options.close);
                            self.closeBtn.appendTo(self.foot);
                            self.closeBtn.bind("click", function () {
                                self.hide(false)
                            })
                        }
                    }
                    break
            }
            if (!self.foot.is(':empty')) self.foot.appendTo(self.wrap);
            if ($(_window).width() > self.options.width && self.options.width !== null) self.wrap.css({
                width: self.options.width
            });
            self.wrap.appendTo(self.notif);
            if (self.options.theme != null) {
                self.notif.addClass(self.options.theme);
                self.shade.addClass(self.options.theme)
            }
            self.shade.appendTo("body");
            self.shade.css({
                height: $(_element).height(),
                width: '100%'
            }).fadeIn("slow");
            self.notif.appendTo("body");
            self.notif.css({
                top: (d / 2 - self.notif.height() / 2)
            });
            self.notif.fadeIn();
            if (self.options.backdrop === true) self.shade.bind("click", function () {
                self.hide(false)
            });
            if (self.options.esc === true) self.hide_on_esc()
        },

        showNotification: function () {
            var self = this;
            if ($("#ikecode-notify-" + self.options.position).length == 0) {
                self.notify = $(self.template.notify);
                self.notify.addClass(self.options.position);
                self.notify.attr("id", "ikecode-notify-" + self.options.position);
                self.notify.appendTo("body")
            } else {
                self.notify = $("#ikecode-notify-" + self.options.position)
            }
            self.message = $(self.template.message);
            self.wrap = $(self.template.wrap);
            self.wrap.css({
                paddingTop: self.options.vspace,
                paddingBottom: self.options.vspace,
                paddingLeft: self.options.hspace,
                paddingRight: self.options.hspace
            });
            self.content = $(self.template.content);
            if (self.options.maxheight != null) self.content.css({
                maxHeight: self.options.maxheight,
                overflowY: 'auto',
                overflowX: 'hidden',
            });
            if (self.options.title != null) {
                self.title = $(self.template.title);
                self.title.html(self.options.title);
                self.title.prependTo(self.wrap)
            }
            self.content.html(self.options.message);
            self.content.appendTo(self.wrap);
            self.wrap.appendTo(self.message);
            if (self.options.width !== null) self.message.css({
                width: self.options.width
            });
            if (self.options.theme != null) self.message.addClass(self.options.theme);
            self.message.appendTo(self.notify);
            switch (self.options.position) {
                case 'topleft':
                    self.notify.css({
                        top: self.options.margin,
                        left: 0,
                        borderLeftWidth: 0
                    });
                    break;
                case 'bottomleft':
                    self.notify.css({
                        bottom: self.options.margin,
                        left: 0,
                        borderLeftWidth: 0
                    });
                    break;
                case 'bottomright':
                    self.notify.css({
                        bottom: self.options.margin,
                        right: 0,
                        borderRightWidth: 0
                    });
                    break;
                case 'bottomcenter':
                    self.notify.css({
                        bottom: 0,
                        left: ($(_window).width() - self.message.width()) / 2,
                        borderBottomWidth: 0
                    });
                    break;
                case 'topcenter':
                    self.notify.css({
                        top: 0,
                        left: ($(_window).width() - self.message.width()) / 2,
                        borderTopWidth: 0
                    });
                    break;
                case 'center':
                    self.notify.css({
                        top: ($(_window).height() - self.message.height()) / 2,
                        left: ($(_window).width() - self.message.width()) / 2
                    });
                    break;
                default:
                    self.notify.css({
                        top: self.options.margin,
                        right: 0,
                        borderRightWidth: 0
                    });
                    break
            }
            self.message.slideDown("normal", function () {
                setTimeout(function () {
                    self.message.animate({
                        height: 0,
                        opacity: 0
                    }, "normal", function () {
                        self.message.remove()
                    })
                }, self.options.interval)
            })
        },

        hide: function (a) {
            var self = this;
            self.notif.fadeOut();
            self.shade.fadeOut("slow", function () {
                self.notif.remove();
                self.shade.remove();
                self.options.callback(a)
            });
            if (self.options.esc === true) $("body").unbind("keypress")
        },

        hide_on_esc: function () {
            var self = this;
            $("body").bind("keypress", function (a) {
                if (a.keyCode == 27) self.hide(false);
                else if (self.options.type != 'prompt')
                    return false
            })
        },

        template: {
            shade: '<div class="ikecode-alert-shade"></div>',
            alert: '<div class="ikecode-alert-alert"></div>',
            wrap: '<div class="ikecode-alert-wrap"></div>',
            notify: '<div class="ikecode-alert-notify"></div>',
            message: '<div class="ikecode-alert-message"></div>',
            title: '<div class="ikecode-alert-title"></div>',
            content: '<div class="ikecode-alert-content"></div>',
            foot: '<div class="ikecode-alert-foot"></div>',
            input: '<div class="ikecode-alert-input"><input type="text" id="ikecode-promt-input"/></div>',
            buttons: {
                yes: '<button id="ikecode-button-yes">|lang|yes|/lang|</button>',
                no: '<button id="ikecode-button-not">|lang|no|/lang|</button>',
                ok: '<button id="ikecode-button-ok">|lang|ok|/lang|</button>',
                cancel: '<button id="ikecode-button-cancel">|lang|cancel|/lang|</button>',
                close: '<button id="ikecode-button-close">|lang|close|/lang|</button>',
                btn: '<button class="ikecode-custom-button"></button>'
            }
        },

        setLocalize: function () {
            var self = this;
            $.each(self.options.localize, function (i, lang) {
                $.each(self.template.buttons, function (j, button) {
                    self.template.buttons[j] = button.replace('|lang|' + i + '|/lang|', lang)
                })
            })
        }
    };

    var _defaultOptions = {
        type: 'alert',
        width: null,
        maxheight: null,
        vspace: 10,
        hspace: 20,
        backdrop: false,
        title: null,
        position: 'topright',
        margin: 20,
        interval: 2000,
        message: '[mensagem vazia]',
        buttons: {},
        theme: null,
        close: true,
        placeholder: '',
        esc: true,
        callback: function (a) {
            return true
        }
    };

    $.alert = function (options) {
        var defaultOptions = _defaultOptions;
        defaultOptions.type = "alert";
        var self = Object.create(metroAlert);
        self.init(options, defaultOptions)
    };

    $.confirm = function (options) {
        var defaultOptions = _defaultOptions;
        defaultOptions.type = "confirm";
        var self = Object.create(metroAlert);
        self.init(options, defaultOptions)
    };

    $.prompt = function (options) {
        var defaultOptions = _defaultOptions;
        defaultOptions.type = "prompt";
        var self = Object.create(metroAlert);
        self.init(options, defaultOptions)
    };

    $.notification = function (options) {
        var defaultOptions = _defaultOptions;
        defaultOptions.type = "notification";
        var self = Object.create(metroAlert);
        self.init(options, defaultOptions)
    }

})(jQuery, window, document)