$(document).ready(function () {

    //Display images after select from computer
    $("#filep").change(function () {
        insertProfileImages();
    });

    //Display thumbnail images after select from computer
    $("#filew").change(function () {
        insertAvatarImages();
    });

    //Update user profile
    $("#btn-profile_update").click(function () {
        updateUserProfile();
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

/**
 * Update user profile button
 * */
function updateUserProfile() {
    var token = $("#token_Js").val();
    var firstname = $('#input-firstname_Js').val();
    var lastname = $('#input-lastname_Js').val();
    var phonenumber = $('#input-phone_Js').val();
    var birthday = $('#input-birthdat_Js').val();
    var tagline = $('#input-tagline_Js').val();
    var description = $('#text-desc_Js').val();
    var country = $('#select-country_Js').val();
    var address = $('#input-address_Js').val();

    var Url = 'https://localhost:7259/api/UserProfile/ProfileInformation';
    $.ajax({
        url: Url,
        type: "PUT",
        data: JSON.stringify({
            firstName: firstname,
            lastName: lastname,
            tagline: tagline,
            description: description,
            phoneNumber: phonenumber,
            country: country,
            address: address,
            birthday: birthday
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-success d-flex align-items-center" role="alert">
                                <i class="fa-solid fa-circle-check fa-lg"></i>
                                <div class="m-1">
                                    Update Profile success
                                </div>
                            </div>
                        </div>`;
            $('#wt-main').append(html);
            
            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}

