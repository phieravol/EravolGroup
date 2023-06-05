/**
 * Delete Category with confirm dialog
 * */
function confirmDelteCategory(categoryId) {
    var confirmDiaglog = confirm("Are you sure you want to delete this category?");
    if (confirmDiaglog) {

        const baseUrl = "https://localhost:7259";
        const relativeUrl = "api/Admin/Categories";
        const url = `${baseUrl}/${relativeUrl}/${categoryId}`;
        fetch(`${baseUrl}/${relativeUrl}/${categoryId}`,
            {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    console.log("delete successfully");
                    // Cập nhật danh sách categories trên giao diện nếu cần thiết
                    var row = document.getElementById(`row-category-${categoryId}`);
                    row.remove();
                }
            });
    }
}