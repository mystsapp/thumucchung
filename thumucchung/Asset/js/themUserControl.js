var themUserControl = {
    init: function () {

        themUserControl.registerEvent();
    },

    registerEvent: function () {

        $('#frmThemUser').validate({
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                fullName: {
                    required: true
                }
            },            
            messages: {
                username: {
                    required: "Trường này không được để trống."
                },
                password: {
                    required: "Password không được để trống."
                },
                fullName: {
                    required: "Họ tên không được để trống."
                }
            }

        });
        
        $('#btThem').off('click').on('click', function () {
            if ($('#frmThemUser').valid()) {
                $('#frmThemUser').submit();
            }
        });
        $('#txtusername').blur(function () {
            var username = $('#txtusername').val();
            var phongban = $('#ddphongban').val();
            var url = "/Account/CheckUser";
            $.get(url, { username: username, phongban: phongban }, function (result) {
                if (result == "True") {
                    bootbox.alert({
                        size: "small",
                        title: "THÔNG BÁO",
                        message: "Tên đăng nhập này đã tồn tại."

                    })
                    $('#txtusername').val("");
                    $('#txtusername').focus();
                }
            });
        });
      
}

};
themUserControl.init();