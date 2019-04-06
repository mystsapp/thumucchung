/// <reference path="../../scripts/jquery-3.3.1.min.js" />
/// <reference path="../../scripts/jquery-3.3.1.min.js" />
//var createFolderControl = {
//    init: function () {

//        createFolderControl.registerEvent();
//    },

//    registerEvent: function () {

//        $('#frmCreateFolder').validate({
//            rules: {
//                foldername: {
//                    required: true
//                }
                
//            },
//            messages: {
//                foldername: {
//                    required: "Trường này không được để trống."
//                }
//            }

//        });
//        $("#btCreateFolder").off('click').on('click', function () {
//            var path = $(this).data('path');
//            $('#createFolderModal').modal('show');
//        });
//        $('#btAddFolder').off('click').on('click', function () {
//            if ($('#frmCreateFolder').valid()) {
//                $('#frmCreateFolder').submit();
//            }
//        });

//    },
//};
//createFolderControl.init();

var createFolderControl = {
    init: function () {

        createFolderControl.registerEvent();
    },

    registerEvent: function () {

        $('#frmCreateFolder').validate({
            rules: {
               
                foldername: {
                    required: true
                }
            },
            messages: {                
                foldername: {
                    required: "Tên thư mục không được trống."
                }
            }

        });

        $("#btCreateFolder").off('click').on('click', function () {
            var path = $(this).data('path');
            $('#createFolderModal').modal('show');
        });

        $('#btAddFolder').off('click').on('click', function () {
            if ($('#frmCreateFolder').valid()) {
                $('#frmCreateFolder').submit();
            }
        });
        $("#txtFolder").blur(function () {
            var text = $(this).val();
            var text_create;
            text_create = text.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a").replace(/\ /g, ' ').replace(/đ/g, "d").replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y").replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u").replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o").replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e").replace(/ì|í|ị|ỉ|ĩ/g, "i");
            $('#txtFolder').val(text_create);
        });
    },

    

};
createFolderControl.init();