(function () {
    'use strict';

    var previewElement;
    var contentElement;

    $(document).ready(function () {
        previewElement = $('#preview');
        contentElement = $('#Content');
        $('#btn-preview').on('click', preview);
    });

    function preview() {
        $.ajax({
            url: '/Home/Preview',
            type: 'POST',
            data: { content: contentElement.val() },
            success: function (data) {
                previewElement.html(data);
                prettyPrint();
            }
        });
    }

})();