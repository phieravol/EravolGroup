$(document).ready(function () {

    const serviceUrl = "https://localhost:7259/api/Services";
    const serviceImageUrl = "https://localhost:7259/api/ServiceImages?serviceId="

    $("#isGenerateCode_Js").change(function () {
        var selectedValue = $(this).val(); // Get selected value
        //set states for service code input element
        setStateServiceCodeElement(selectedValue);
    });

    //Display images after select from computer
    $("#filep").change(function () {
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
    });

    //Display thumbnail images after select from computer
    $("#filew").change(function () {
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
                console.log($("#service-thumbnail-img"))
            };

            reader.readAsDataURL(file);
       
    });

    // Add event listener when user click create services
    $("#createServiceBtn_Js").click(function () {

        //Get Service informations
        var serviceTitle = $("#serviceTitle_Js").val();
        var IsGenerateCode = $("#isGenerateCode_Js").val();
        var serviceCode = $("#serviceCode_Js").val();
        var categoryId = $("#categoryId_Js").val();
        var serviceStatusId = $("#serviceStatusId_Js").val();
        var serviceIntro = $("#serviceIntro_Js").val();
        var serviceDetails = $("#serviceDetails_Js").val();
        var priceType = $("#price-type").val();
        var priceAmount = $("#price-amount").val();

        

        //Get Service Images
        var serviceImageInput = document.getElementById('filep');
        var serviceImages = serviceImageInput.files;

        //Add Images to formdata
        var formData = new FormData();
        for (var i = 0; i < serviceImages.length; i++) {
            formData.append("images", serviceImages[i]);
        }

        var serviceData = {
            "serviceCode": serviceCode,
            "serviceTitle": serviceTitle,
            "serviceIntro": serviceIntro,
            "serviceDetails": serviceDetails,
            "isGenerateCode": IsGenerateCode,
            "categoryId": categoryId,
            "serviceStatusId": serviceStatusId
        };

        //Send Ajax Request to create new service
        createServiceByFormData(serviceData);
    });

});


/**
 * Set Service Code Status
 * */
function setStateServiceCodeElement(selectedValue) {
    var ServiceCodeElement = $("#serviceCode_Js");
    if (selectedValue == "true") {
        ServiceCodeElement.prop('disabled', true);
    } else {
        ServiceCodeElement.prop('disabled', false);
    }
}

/**
 * Create new service by form data
 * */
function createServiceByFormData(formData) {
    const serviceUrl = "https://localhost:7259/api/Services";
    var token = $("#token_Js").val();

    //Get Service informations
    var serviceTitle = $("#serviceTitle_Js").val();
    var IsGenerateCode = $("#isGenerateCode_Js").val() == 'true' ? true : false;
    var serviceCode = $("#serviceCode_Js").val();
    var categoryId = $("#categoryId_Js").val();
    var serviceStatusId = $("#serviceStatusId_Js").val();
    var serviceIntro = $("#serviceIntro_Js").val();
    var serviceDetails = tinymce.get('serviceDetails_Js').getContent();
    var priceType = $("#price-type").val();
    var price = $("#price-amount").val();


    $.ajax({
        url: serviceUrl,
        type: "POST",
        data: JSON.stringify({
            serviceCode: serviceCode,
            serviceTitle: serviceTitle,
            serviceIntro: serviceIntro,
            serviceDetails: serviceDetails,
            isGenerateCode: IsGenerateCode,
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

            //create service Thumbnail
            createServiceThumbnail(response.serviceCode)

            //create service images
            createServiceImages(response.serviceCode);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}


/**
 * Create Service by service code
 * */
function createServiceImages(serviceCode) {

    //declare url
    var serviceImageUrl = "https://localhost:7259/api/ServiceImages/";

    //Get Service Images
    var serviceImageInput = document.getElementById('filep');
    var serviceImages = serviceImageInput.files;

    //Add Images to formdata
    var formData = new FormData();
    for (var i = 0; i < serviceImages.length; i++) {
        formData.append("serviceImages", serviceImages[i]);
    }

    console.log(serviceImageUrl + serviceCode);
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
function createServiceThumbnail(serviceCode) {
    //declare url
    var serviceImageUrl = "https://localhost:7259/api/ServiceImages/thumbnail/";
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
