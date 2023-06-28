$(document).ready(function () {

});

function handleSubmitPosts() {
    createServiceByFormData();
    
}
function createServiceByFormData() {
    const postUrl = "https://localhost:7259/api/Posts";
    var token = $("#token_Js").val();
    console.log("token: " + token);

    //Get Service informations
    var postTitle = $("#postTitle_Js").val();
    var expirationDate = $("#expirationDate_JS").val();
    var categoryId = $("#categoryId_Js").val();
    var budget = $("#budget_Js").val();
    var sortDesc = $("#sortDesc_Js").val();
    var postDetails = tinymce.get('wt-ckeditor').getContent();
    var select = document.getElementById("postSkillId_Js");
    var selectedSkills = Array.from(select.selectedOptions).map(option => option.value);
    var postStatusId = 2;
    
    $.ajax({
        url: postUrl,
        type: "POST",
        data: JSON.stringify({
            postTitle: postTitle,
            expirationDate: expirationDate,
            budget: budget,
            sortDesc: sortDesc,
            postDetails: postDetails,
            categoryId: categoryId,
            postStatusId: postStatusId,
            levelRequired: "Beginer"
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            var postId = response;
            var select = document.getElementById("postSkillId_Js");
            var selectedSkills = Array.from(select.selectedOptions).map(option => option.value);
            var createSkillRequires = [];
            selectedSkills.forEach(function (skillId) {
                console.log(skillId); // in ra giá trị từng phần tử trong mảng
                var skill = {
                    postId: postId,
                    skillId: skillId
                };
                createSkillRequires.push(skill);
            });
            createPostSkillRequired(createSkillRequires);
            location.href = "https://localhost:7053/Posts/Clients";
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
    const createPostButton = document.getElementById('create-post-button');
    createPostButton.addEventListener('click', handleCreatePostClick);
}
function createPostSkillRequired(createSkillRequires) {
    const postSkillUrl = "https://localhost:7259/api/PostSkillRequire";
    $.ajax({
        url: postSkillUrl,
        type: "POST",
        data: JSON.stringify(createSkillRequires),
        contentType: 'application/json',
        success: function (response) {
            console.log(response);

        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}
//function handleCreatePostClick() {
//    const postUrl = "https://localhost:7259/api/Posts";
//    var postTitle = $("#postTitle_Js").val();
//    var expirationDate = $("#expirationDate_JS").val();
//    var categoryId = $("#categoryId_Js").val();
//    var budget = $("#budget_Js").val();
//    var sortDesc = $("#sortDesc_Js").val();
//    var postDetails = tinymce.get('wt-ckeditor').getContent();
//    var select = document.getElementById("postSkillId_Js");
//    var selectedSkills = Array.from(select.selectedOptions).map(option => option.value);
//    var postStatusId = 2;
//    // Gọi API để tạo bài viết mới
//    fetch(postUrl, {
//        method: 'POST',
//        body: JSON.stringify({
//            // Điền dữ liệu của bài viết vào đây
//            postTitle: postTitle,
//            expirationDate: expirationDate,
//            budget: budget,
//            sortDesc: sortDesc,
//            postDetails: postDetails,
//            categoryId: categoryId,
//            postStatusId: postStatusId,
//            levelRequired: "Beginer"
//        }),
//        headers: {
//            'Content-Type': 'application/json'
//        }
//    })
//        .then(response => {
//            if (response.ok) {
//                // Nếu thành công, hiển thị thông báo thành công
//                const successMessage = 'Post created successfully!';
//                alert(successMessage);
//            } else {
//                // Nếu không thành công, hiển thị thông báo lỗi
//                const errorMessage = 'Failed to create post. Please try again.';
//                alert(errorMessage);
//            }
//        })
//        .catch(error => {
//            // Nếu xảy ra lỗi, hiển thị thông báo lỗi
//            const errorMessage = 'An error occurred while creating the post. Please try again later.';
//            alert(errorMessage);
//            console.error(error);
//        });
//}