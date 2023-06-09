$(document).ready(function () {
    $('#addCategoryBtn').on('click', function () {

        var categoryName = $("#categoryName").val();
        var isActiveCategory = $("#isActiveCategory").val();
        var categoryDesc = $("#categoryDesc").val();
        var categoryImage = $("#categoryImage").val();

        var formData = new FormData();

        formData.append('categoryName', categoryName);
        formData.append('isCategoryActive', isActiveCategory);
        formData.append('categoryDesc', categoryDesc);
        var imageFile = document.getElementById('categoryImage').files[0];
        formData.append('categoryImage', imageFile);


        const BASE_URL = "https://localhost:7259";
        const RELATIVE_PATH_URL = "/api/Admin/Categories";
        const Url = BASE_URL + RELATIVE_PATH_URL;
        console.log(Url);
        $.ajax({
            url: BASE_URL + RELATIVE_PATH_URL,
            type: 'POST',
            data: formData,
            contentType: 'multipart/form-data',
            success: function (response) {
                console.log(response);
            },
            error: function (error) {
                console.log(error);
                console.log("");
            }
        });
    });
});