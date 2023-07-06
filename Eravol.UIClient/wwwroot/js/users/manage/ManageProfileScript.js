$(document).ready(function () {

    //Display images after select from computer
    $("#filep").change(function () {
        insertProfileImages();
    });

    //Display thumbnail images after select from computer
    $("#filew").change(function () {
        insertAvatarImages();
    });
});


/**
 * Display user profile images
 * */
function insertProfileImages() {
    var token = $("#token_Js").val();
    var url = 'https://localhost:7259/api/UserProfile/ProfileImages';
    var fileInput = document.getElementById('filep');
    var files = fileInput.files;
    var currentIndex = 0;

    //Add Images to formdata
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append("profileImages", files[i]);
    }
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log("image response: " + response);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(xhr);
        }
    });

    function processFile(index) {
        if (index >= files.length) {
            return;
        }

        var file = files[index];
        var filename = file.name;
        var fileSize = (file.size / 1024).toFixed(2);
        var reader = new FileReader();
        reader.onload = function (e) {
            var imageUrl = e.target.result;

            // Hiển thị ảnh trên frontend
            var html = `<li class="wt-uploadingholder">
                    <div class="wt-uploadingbox">
                        <div class="wt-designimg">
                            <label for="demoz"><img src="${imageUrl}" alt="img description"><i class="fa fa-check"></i></label>
                        </div>
                        <div class="wt-uploadingbar wt-uploading">
                            <span class="uploadprogressbar"></span>
                            <span>${filename}</span>
                            <em>File size: ${fileSize} kb<button type="button" class="lnr lnr-cross" style="color:red"></button></em>
                        </div>
                    </div>
                </li>`;
            // Thêm khối thẻ HTML vào phần tử mục tiêu
            $("#profileImgContainer").append(html);

            // Xử lý file tiếp theo
            processFile(index + 1);
        };

        reader.readAsDataURL(file);
    }

    // Bắt đầu xử lý file đầu tiên
    processFile(currentIndex);
}


/**
 * Remove UserProfile Images
 * */
function removeProfileImage(imageId) {
    var token = $("#token_Js").val();
    var url = `https://localhost:7259/api/UserImages?imageId=${imageId}`;
    var token = $("#token_Js").val();

    $.ajax({
        url: url,
        type: "DELETE",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(xhr);
        }
    }).done(function (response) {
        // Xử lý dữ liệu khi yêu cầu thành công
        var imageElementName = `#item-profileImg_${imageId}`;
        var imageElement = $(imageElementName);
        imageElement.remove();
    }).fail(function () {
        // Xử lý phản hồi lỗi
    });

}

/**
 * Insert avatar photo
 * */
function insertAvatarImages() {
    var url = "https://localhost:7259/api/UserImages/UserAvatar"
    var token = $("#token_Js").val();
    var fileInput = document.getElementById('filew');
    var file = fileInput.files[0];
    //Add Images to formdata
    var formData = new FormData();
    formData.append("userAvatar", file);

    //Send ajax request api
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(xhr);
        }
    });

    
}

