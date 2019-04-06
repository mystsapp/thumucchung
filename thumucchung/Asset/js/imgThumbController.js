
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

    imgThumb: function () {
        //var imgPath = $('.thumbImage').data('id');

        //$('.thumbImage').wrap('<ul id="etalage2" class="etalage"><li></li></ul>')
        //    .addClass('etalage_thumb_image')
        //    .after('<img class="etalage_source_image target"/>');

       // $('.etalage_source_image').attr('src', imgPath);

        $('.etalage').etalage({
            zoom_element: '#custom_zoom_element',
            thumb_image_width: 100,
            thumb_image_height: 100,
            //source_image_width: 900,
            //source_image_height: 1200
            zoom_area_width: 900,
            zoom_area_height: 1200
        });
    }

};
imgThumbController.init();