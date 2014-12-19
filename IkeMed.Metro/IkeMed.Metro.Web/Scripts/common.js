function Common() {
    var self = this;

    self.init = function () {
        $(document).ready(function () {
            //fix to input file validation
            $.validator.addMethod('accept', function () { return true; });
        });
    }
}

var common = new Common();
common.init();