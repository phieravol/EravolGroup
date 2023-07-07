/**
 * Delete Skills with confirm dialog
 * */
function confirmDelteSkill(skillId) {
    var confirmDiaglog = confirm("Are you sure you want to delete this skill?", "Delete skill");
    if (confirmDiaglog) {

        const baseUrl = "https://localhost:7259";
        const relativeUrl = "api/Admin/Skills";
        const url = `${baseUrl}/${relativeUrl}/${skillId}`;
        fetch(`${baseUrl}/${relativeUrl}/${skillId}`,
            {
                method: 'DELETE'
            })
            .then(response => {
                if (response.ok) {
                    console.log("delete successfully");
                    // Cập nhật danh sách categories trên giao diện nếu cần thiết
                    var row = document.getElementById(`row-skills-${skillId}`);
                    row.remove();
                }
            });
    }
}
