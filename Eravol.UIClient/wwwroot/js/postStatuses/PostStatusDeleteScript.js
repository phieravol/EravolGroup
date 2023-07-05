/**
 * Delete Category with confirm dialog
 * */
function confirmDeltePostStatus(postStatusId) {
    var confirmDiaglog = confirm("Are you sure you want to delete this post status?");
    if (confirmDiaglog) {

        const baseUrl = "https://localhost:7259";
        const relativeUrl = "api/PostStatuses";
        const url = `${baseUrl}/${relativeUrl}/${postStatusId}`;
        fetch(`${baseUrl}/${relativeUrl}/${postStatusId}`,
            {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    console.log("delete successfully");
                    // Cập nhật danh sách categories trên giao diện nếu cần thiết
                    var row = document.getElementById(`row-postStatus-${postStatusId}`);
                    row.remove();
                }
            });
    }
}
