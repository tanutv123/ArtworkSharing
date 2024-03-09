$(document).on('click', '.like-state', function () {
    const artworkId = $(this).data('artwork-id');
    const isLiked = $(this).data('like-state');

    // AJAX call to the server to toggle the like state
    // Update the button display based on the server's response...
});