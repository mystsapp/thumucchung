var upfileControl = {
    init: function () {

        upfileControl.registerEvent();
    },

    registerEvent: function () {
       
        $("#btupfile").off('click').on('click', function () {
            var path = $(this).data('path');
            $('#upfileModal').modal('show');
        });
      
    },
};
upfileControl.init();