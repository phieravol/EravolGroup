/**
 * Delete Category with confirm dialog
 * */
function confirmDeletePost(postId) {
    var confirmDiaglog = confirm("Are you sure you want to delete this Post?");
    var token = $("#token_Js").val();
    if (confirmDiaglog) {

        const baseUrl = "https://localhost:7259";
        const relativeUrl = "api/Posts";
        const url = `${baseUrl}/${relativeUrl}/${postId}`;
        $.ajax({
            url: url,
            type: "DELETE",
            headers: {
                'Authorization': 'Bearer ' + token
            },
            contentType: 'application/json',
            success: function (response) {
                location.href = "https://localhost:7053/Posts/Clients";
            },
            error: function (xhr, status, error) {
                console.log(status);
                console.log(xhr);
                console.log(error);
            }
        });
    }
}

