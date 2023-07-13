$(document).ready(function () {
    //change certificate add image container
    $("#filea").change(function () {
        const addElementName = '#cert-img_create';
        const replaceElementName = '#cert-img-replace';
        displayCertificateImg(addElementName, replaceElementName);
    });

    $("#btn-certificate_create").click(function () {
        createCertificate();
    });

});

/**
 * Display Certificate image
 * */
function displayCertificateImg(addElementName, replaceElementName) {
    var certificateElementInput = document.getElementById('filea');
    var file = certificateElementInput.files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        var imageUrl = e.target.result;
        var fileName = file.name;
        var fileSizeInBytes = file.size;
        var fileSizeInKB = Math.round(fileSizeInBytes / 1024);
        // Hiển thị ảnh trên frontend
        var html = `<div id="cert-img-replace">
                        <div class="wt-uploadingbox" id="img-portfolio-upload">
                            <div class="wt-designimg">
                                <label for="demoz"><img src="${imageUrl}" alt="img description"></label>
                            </div>
                        </div>
                        <li class="wt-uploading">
                            <span>${fileName}</span>
                            <em>File size: ${fileSizeInKB} kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>
                        </li>
                    </div>`;
        // Thêm khối thẻ HTML vào phần tử mục tiêu
        $(replaceElementName).remove();
        $(addElementName).append(html);
    };
    reader.readAsDataURL(file);
}

function createCertificate() {
    var token = $("#token_Js").val();
    var certTitle = $("#cert-create_title").val();
    var certDate = $("#cert-create_date").val();
    var certificateElementInput = document.getElementById('filea');
    var file = certificateElementInput.files[0];

    var formData = new FormData();
    formData.append("certificateTitle", certTitle);
    formData.append("certificateDate", certDate);
    formData.append("certificateImageName", file.name);
    formData.append("certificateImageSize", file.size);
    formData.append("certificateImage", file);

    var Url = `https://localhost:7259/api/Certificates`;
    $.ajax({
        url: Url,
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            var message = "Create certificate successfull!";
            displayNotifilcation(true, message);

            var imgElement = $(`#portfolio-showimage-${portfolioId}`);
            imgElement.prop('src', `https://localhost:7259/api/Images/${response.portfolioImageName}`);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(error);
            var message = "Create certificate Failed!";
            displayNotifilcation(false, message);
        }
    });
}