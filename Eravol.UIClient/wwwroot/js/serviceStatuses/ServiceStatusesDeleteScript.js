/**
 * Delete Service Status with confirm dialog
 * */
function confirmDelteServiceStatus(serviceStatusId) {
    var confirmDiaglog = confirm("Are you sure you want to delete this service status?", "Delete service status");
    if (confirmDiaglog) {

        const baseUrl = "https://localhost:7259";
        const relativeUrl = "api/Admin/ServiceStatusesAdmin";
        const url = `${baseUrl}/${relativeUrl}/${serviceStatusId}`;
        fetch(`${baseUrl}/${relativeUrl}/${serviceStatusId}`,
            {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    console.log("delete successfully");
                    // Cập nhật danh sách categories trên giao diện nếu cần thiết
                    var row = document.getElementById(`row-serviceStatus-${serviceStatusId}`);
                    row.remove();
                }
            });
    }
}
