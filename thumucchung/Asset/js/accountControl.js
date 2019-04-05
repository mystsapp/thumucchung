var accountControl = {
    init: function () {

        accountControl.registerEvent();
    },

    registerEvent: function () {
        $('#btThemUser').click(function () {
            var url = '/Account/themUser';
            $.get(url, function (data) {
                $('#dsuser').css('display', 'none');
                $('.them_user').css('display', '');
                $('.them_user').html(data);
                //$.cookie("them", "1");
            })
        });
        $('.huychon').click(function () {
            $('#dsuser').css('display', '');
            $('.them_user').css('display', 'none');
            //$.cookie("them", null);
        }),

            $('.updateUser').click(function () {
                var username = $(this).data('username');
                var dataObject = { username: username };
                var url = '/Account/capnhatUser';
                $.get(url, dataObject, function (data) {
                    $('#dsuser').css('display', 'none');
                    $('.them_user').css('display', '');
                    $('.them_user').html(data);
                    // $.cookie("them", "1");
                })
            }),
            $('.btn-active').off('click').on('click', function (e) {
                e.preventDefault();
                var btn = $(this);
                var username = btn.data('username');

                $.ajax({
                    type: 'POST',
                    url: '/Account/changeStatus',
                    datatype: 'Json',
                    data: { username: username },
                    success: function (response) {
                        console.log(response);
                        if (response.status==true) {
                            btn.text('Kích hoạt');
                            //$("#row_" + username).removeClass("danger");
                        }
                        else {
                            btn.text('Khóa');
                            //$("#row_" + username).addClass("danger");
                        }
                    }
                });
            });
        $('.delUser').click(function () {
            var id = $(this).data('username');
            bootbox.confirm({
                title: "Delete Confirm?",
                message: "Bạn muốn xóa account " + id + " này?",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Xóa'
                    }
                },
                callback: function (result) {
                    if (result) {
                        accountControl.deleteUser(id);
                        $("#row_" + id).remove();
                    }
                }
            })
        });


    },
    deleteUser: function (id) {
        $.ajax({
            url: '/Account/deleteUser',
            data: { username: id },
            type: 'POST',
            success: function (response) {
                if (response.status) {
                    bootbox.alert({
                        size: "small",
                        title: "THÔNG BÁO",
                        message: "Đã xóa account: " + id + " thành công.",
                        callback: function () {

                        }

                    })
                }
                else {
                    bootbox.alert({
                        size: "small",
                        title: "DELETE INFO",
                        message: response.message
                    })
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

};
accountControl.init();