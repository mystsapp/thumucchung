var capnhatUserControl = {
    init: function () {

        capnhatUserControl.registerEvent();
    },

    registerEvent: function () {
        $('#frmCapnhatUser').validate({
            rules: {
              
                fullName: {
                    required: true
                }
            },
            messages: {
                
                fullName: {
                    required: "Họ tên không được để trống."
                }
            }

        });
        $('#btCapnhatUser').off('click').on('click', function () {
            if ($('#frmCapnhatUser').valid()) {
                $('#frmCapnhatUser').submit();               
            }
        });
    }

};
capnhatUserControl.init();