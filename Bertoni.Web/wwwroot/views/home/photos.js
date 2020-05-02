$(document).ready(function() {
    $('.comment-handler').click(function() {
        var photoId = $(this).attr('photo-id');
        $.ajax({
            type: 'GET',
            url: `/Photos/${photoId}/Comments`,
            contentType: 'json',
            success: function(response) {
                $("#comments-section").html(response);
            },
            failure: function(error) {
                console.log(error);
                alert("Something went wrong loading the comments...");
            },
        });
    });
});