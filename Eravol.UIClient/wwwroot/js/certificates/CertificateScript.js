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

/**
 * Display certificate image
 * */
function showUpdateCertImage(certificateId) {
    var certificateElementInput = document.getElementById(`filec-${certificateId}`);
    var file = certificateElementInput.files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        var imageUrl = e.target.result;
        var fileName = file.name;
        var fileSizeInBytes = file.size;
        var fileSizeInKB = Math.round(fileSizeInBytes / 1024);
        // Hiển thị ảnh trên frontend
        var html = `<div id="certupdate-img_${certificateId}">
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
        $(`#certupdate-img_${certificateId}`).remove();
        $(`#containder-imgcert_${certificateId}`).append(html);
        
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

            const date = new Date(response.certificateDate);
            const formattedDate = date.toISOString().split("T")[0];

            const month = (date.getMonth() + 1).toString().padStart(2, "0");
            const day = date.getDate().toString().padStart(2, "0");
            const year = date.getFullYear();

            const displayDate = `${month}/${day}/${year}`;

            var html = `<li id="cert-item_${response.certificateId}">
                            <div class="wt-accordioninnertitle">
                                <div class="wt-projecttitle collapsed" data-toggle="collapse" data-target="#innertitlecwone-${response.certificateId}">
                                    <figure><img src="https://localhost:7259/api/Images/${response.certificateImageName}" alt="img description"></figure>
                                    <h3>${response.certificateTitle}<samp>${displayDate}</samp></h3>
                                </div>
                                <div class="wt-rightarea">
                                    <a href="javascript:void(0);" class="wt-addinfo wt-skillsaddinfo" data-toggle="collapse" data-target="#innertitlecwone-${response.certificateId}"><i class="lnr lnr-pencil"></i></a>
                                    <a href="javascript:void(0);" onclick="deleteCertificate(${response.certificateId})" class="wt-deleteinfo"><i class="lnr lnr-trash"></i></a>
                                </div>
                            </div>
                            <div class="wt-collapseexp collapse" id="innertitlecwone-${response.certificateId}" aria-labelledby="accordioninnertitle1" data-parent="#accordion">
                                <form class="wt-formtheme wt-userform wt-formprojectinfo">
                                    <fieldset>
                                        <div class="form-group form-group-half">
                                            <input type="text" name="cert-title_update" class="form-control" value="${response.certificateTitle}" placeholder="Certificate Title">
                                        </div>
                                        <div class="form-group form-group-half">
                                            <input type="date" name="cert-date_update" value="${formattedDate}" class="form-control" placeholder="Certificate Date">
                                        </div>
                                        <div class="form-group form-group-label wt-infouploading">
                                            <div class="wt-labelgroup">
                                                <label for="filec-${response.certificateId}">
                                                    <span class="wt-btn">Select Files</span>
                                                    <input type="file" name="file" onchange="showUpdateCertImage(${response.certificateId})" id="filec-${response.certificateId}">
                                                </label>
                                                <span>Drop files here to upload</span>
                                                <em class="wt-fileuploading">Uploading<i class="fa fa-spinner fa-spin"></i></em>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <ul class="wt-attachfile" id="containder-imgcert_${response.certificateId}">
                                                                
                                            </ul>
                                        </div>
                                        <div class="form-group wt-btnarea">
                                            <button type="button" onclick="updateCertificate(${response.certificateId})" class="wt-btn border-0">Save</button>
                                        </div>
                                    </fieldset>
                                </form>
                            </div>
                        </li>`;
            $('#cert-container').append(html);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(error);
            var message = "Create certificate Failed!";
            displayNotifilcation(false, message);
        }
    });
}

/**
 * Update certificate
 * */
function updateCertificate(certificateId) {
    var token = $("#token_Js").val();
    var certTitle = $(`#certtitle-update_${certificateId}`).val();
    var certDate = $(`#certdate-update_${certificateId}`).val();
    var certificateElementInput = document.getElementById(`filec-${certificateId}`);
    var file = certificateElementInput.files[0];

    var formData = new FormData();
    formData.append("certificateId", certificateId);
    formData.append("certificateTitle", certTitle);
    formData.append("certificateDate", certDate);
    formData.append("certificateImageName", file.name);
    formData.append("certificateImageSize", file.size);
    formData.append("certificateImage", file);

    var Url = `https://localhost:7259/api/Certificates`;
    $.ajax({
        url: Url,
        type: "PUT",
        data: formData,
        processData: false,
        contentType: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            var message = "Update certificate successfull!";
            displayNotifilcation(true, message);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(error);
            var message = "Update certificate Failed!";
            displayNotifilcation(false, message);
        }
    });
}

function deleteCertificate(certificateId) {
    var token = $("#token_Js").val();
    var Url = `https://localhost:7259/api/Certificates/${certificateId}`;
    $.ajax({
        url: Url,
        type: "DELETE",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            var message = "Delete certificate successfull!";
            displayNotifilcation(true, message);

            $(`#cert-item_${certificateId}`).remove();
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(xhr);
            console.error(status);
            console.error(error);
            var message = "Delete certificate Failed!";
            displayNotifilcation(false, message);
        }
    });
}