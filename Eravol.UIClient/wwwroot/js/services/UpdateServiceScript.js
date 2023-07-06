$(document).ready(function () {
    //Display images after select from computer
    $("#filep").change(function () {
        displayServiceImgFromComputer();
    });

    //Display thumbnail images after select from computer
    $("#filew").change(function () {
        displayImageThumbnailFromComputer();
    });

    //Update service information when user click to Update button
    $('#updateServiceBtn_Js').click(function () {
        updateServiceInfo();
    });

     
});

/**
 * Delete Service Image
 * */
function deleteServiceImage(ImageName) {

    var baseUrl = 'https://localhost:7259/';
    var apiUrl = 'api/Images/';
    var Url = baseUrl + apiUrl + ImageName;
    $.ajax({
        url: Url,
        method: "DELETE",
        success: function (response) {
            console.log(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi khi yêu cầu thất bại
            console.log("Lỗi: " + error);
        }
    }).done(function (response) {
        // Xử lý dữ liệu khi yêu cầu thành công
        var imageElementName = '#div-img_display-' + ImageName;
        var imageElement = $(imageElementName);
        imageElement.remove();
        location.reload();

    }).fail(function () {
        // Xử lý phản hồi lỗi
    });
}

/**
 * Display images after select from computer
 * */
function displayServiceImgFromComputer() {
    var fileInput = document.getElementById('filep');
    var files = fileInput.files;

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        var reader = new FileReader();
        reader.onload = function (e) {
            var imageUrl = e.target.result;

            // Hiển thị ảnh trên frontend
            var html = '<li class="wt-uploadingholder wt-companyimg-uploading">' +
                '<div class="wt-uploadingbox">' +
                '<figure><img src="' + imageUrl + '" alt="img description"></figure>' +
                '<div class="wt-uploadingbar wt-uploading">' +
                '<span class="uploadprogressbar"></span>' +
                '<span>Profile Photo.jpg</span>' +
                '<em>File size: 300 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>' +
                '</div>' +
                '</div>' +
                '</li>';
            // Thêm khối thẻ HTML vào phần tử mục tiêu
            $("#serviceImgContainer").append(html);
            console.log($("#serviceImgContainer"))
        };

        reader.readAsDataURL(file);
    }

    //Update Service Image to Database
    updateImgToDb()
}

/**
 * Display thumbnail images after select from computer
 * */

function displayImageThumbnailFromComputer() {
    var fileInput = document.getElementById('filew');
    var file = fileInput.files[0];

    var reader = new FileReader();
    reader.onload = function (e) {
        var imageUrl = e.target.result;

        // Hiển thị ảnh trên frontend
        var html = '<li class="wt-uploadingholder wt-companyimg-uploading">' +
            '<div class="wt-uploadingbox">' +
            '<figure><img src="' + imageUrl + '" alt="img description"></figure>' +
            '<div class="wt-uploadingbar wt-uploading">' +
            '<span class="uploadprogressbar"></span>' +
            '<span>Profile Photo.jpg</span>' +
            '<em>File size: 300 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>' +
            '</div>' +
            '</div>' +
            '</li>';
        // Thêm khối thẻ HTML vào phần tử mục tiêu
        $("#service-thumbnail-img").append(html);

        //Add new thumbnail
        createServiceThumbnail();
    };

    reader.readAsDataURL(file);
}

/**
 * Update service image to db
 * */
function updateImgToDb(serviceCode) {

    //declare url
    var serviceCode = $("#serviceCode_Js").val();
    var serviceImageUrl = "https://localhost:7259/api/ServiceImages/";

    //Get Service Images
    var serviceImageInput = document.getElementById('filep');
    var serviceImages = serviceImageInput.files;

    //Add Images to formdata
    var formData = new FormData();
    for (var i = 0; i < serviceImages.length; i++) {
        formData.append("serviceImages", serviceImages[i]);
    }

    $.ajax({
        url: serviceImageUrl + serviceCode,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            console.log("image response: " + response);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(xhr);
        }
    });
}

/**
 * Create Service thumbnail by service code
 * */
function createServiceThumbnail() {
    //declare url
    var serviceImageUrl = "https://localhost:7259/api/ServiceImages/thumbnail/";
    var serviceCode = $("#serviceCode_Js").val();
    //Get Service Images
    var serviceImageInput = document.getElementById('filew');
    var serviceImages = serviceImageInput.files;

    //Add Images to formdata
    var formData = new FormData();

    formData.append("thumbnail", serviceImages[0]);

    $.ajax({
        url: serviceImageUrl + serviceCode,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            console.log("image response: " + response);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(xhr);
        }
    });
}

/**
 * Update Service infomation
 * */
function updateServiceInfo() {
    //Get service infomations
    var serviceTitle = $('#serviceTitle_Js').val();
    var serviceCode = $('#serviceCode_Js').val();
    var categoryId = $('#categoryId_Js').val();
    var serviceStatusId = $('#serviceStatusId_Js').val();
    var serviceIntro = $('#serviceIntro_Js').val();
    var serviceDetails = tinymce.get('serviceDetails_Js').getContent();
    var priceType = $("#price-type").val();
    var price = $("#price-amount").val();
    var token = $('#token_Js').val();

    var baseUrl = 'https://localhost:7259/';
    var apiUrl = 'api/Services/';

    $.ajax({
        url: baseUrl + apiUrl,
        type: "PUT",
        data: JSON.stringify({
            serviceCode: serviceCode,
            serviceTitle: serviceTitle,
            serviceIntro: serviceIntro,
            serviceDetails: serviceDetails,
            categoryId: categoryId,
            serviceStatusId: serviceStatusId,
            priceType: priceType,
            price: price
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            alert("Update Service successfully!");
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}