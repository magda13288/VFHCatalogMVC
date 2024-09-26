// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');

        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var dataUrl = $(this).data('url');
        var actionUrl = form.attr('action');
        var url = dataUrl + actionUrl;
        var sendData = form.serialize();
        $.post(url, sendData).done(function (data) {
            var newBody = $('.modal-body', data);
            PlaceHolderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                PlaceHolderElement.find('.modal').modal('hide');
                location.reload();
            }
        });
        //$.post(url, sendData).done(function (data) {
        //    //PlaceHolderElement.find('.modal').modal('hide');
        //    //location.reload();
        //    var isValid = PlaceHolderElement.find('[name="IsValid"]').val() == 'True';
        //    if (isValid) {
        //        PlaceholderElement.find('.modal').modal('hide');
        //        location.reload();
        //    }
        //})
    })

    PlaceHolderElement.on('click', '[data-dismiss="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
        location.reload();
    })
})

