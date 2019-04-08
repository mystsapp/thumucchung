
var imgThumbController = {
    init: function () {

        imgThumbController.registerEvent();
        //imgThumbController.imgThumb();
    },

    registerEvent: function () {

        $('.thumbImage').hover(function () {
            //alert('a');
            $(this).toggleClass('thumbImage1');
            $(this).parents('div').toggleClass('colmd91');
        });
    },


};
imgThumbController.init();