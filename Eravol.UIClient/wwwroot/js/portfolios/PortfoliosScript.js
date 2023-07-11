﻿$(document).ready(function () {
    //Create portfolio into database
    $("#btn-portfolio_add").click(function () {
        addUserPortfolio();
    });

    //Display portfolio when image input have change
    $('#filet').change(function () {
        displayPortfolioImage();
    });
});

/**
 * Create User Portfolio into database
 * */
function addUserPortfolio() {
    var token = $("#token_Js").val();
    var Url = 'https://localhost:7259/api/Portfolios';

    var portfolioTitle = $('#portfolio-title_add').val();
    var portfolioUrl = $('#portfolio-url_add').val();
    var portfolioDesc = $('#portfolio-Desc_add').val();

    var portfolioImageInput = document.getElementById('filet');
    var file = portfolioImageInput.files[0];
    
    var formData = new FormData();
    formData.append("PortfolioTitle", portfolioTitle);
    formData.append("PortfolioUrl", portfolioUrl);
    formData.append("PortfolioDescription", portfolioDesc);
    formData.append("PortfolioImage", file);

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
            var html = `<li>
                            <div class="wt-accordioninnertitle">
                                <div class="wt-projecttitle collapsed" data-toggle="collapse" data-target="#innertitleaone-${response.portfolioId}">
                                    <figure><img src="https://localhost:7259/api/Images/${response.portfolioImageName}" alt="img description"></figure>
                                    <h3>${response.portfolioTitle}<span>${response.portfolioUrl}</span></h3>
                                </div>
                                <div class="wt-rightarea">
                                    <a href="javascript:void(0);" class="wt-addinfo wt-skillsaddinfo" data-toggle="collapse" data-target="#innertitleaone-${response.portfolioId}"><i class="lnr lnr-pencil"></i></a>
                                    <a href="javascript:void(0);" class="wt-deleteinfo"><i class="lnr lnr-trash"></i></a>
                                </div>
                            </div>
                            <div class="wt-collapseexp collapse" id="innertitleaone-${response.portfolioId}" aria-labelledby="accordioninnertitle" data-parent="#accordion">
                                <form class="wt-formtheme wt-userform wt-formprojectinfo">
                                    <fieldset>
                                        <div class="form-group form-group-half">
                                            <input type="text" name="Project Title" class="form-control" placeholder="Project Title">
                                        </div>
                                        <div class="form-group form-group-half">
                                            <input type="text" name="Project URL" class="form-control" placeholder="Project URL">
                                        </div>
                                        <div class="form-group form-group-label wt-infouploading">
                                            <div class="wt-labelgroup">
                                                <label for="filen">
                                                    <span class="wt-btn">Select Files</span>
                                                    <input type="file" name="file" id="filen">
                                                </label>
                                                <span>Drop files here to upload</span>
                                                <em class="wt-fileuploading">Uploading<i class="fa fa-spinner fa-spin"></i></em>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <ul class="wt-attachfile">
                                                <li class="wt-uploading">
                                                    <span>Logo.jpg</span>
                                                    <em>File size: 300 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>
                                                </li>
                                                <li>
                                                    <span>Wireframe Document.doc</span>
                                                    <em>File size: 512 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>
                                                </li>
                                                <li>
                                                    <span>Requirments.pdf</span>
                                                    <em>File size: 110 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>
                                                </li>
                                                <li>
                                                    <span>Company Intro.docx</span>
                                                    <em>File size: 224 kb<a href="javascript:void(0);" class="lnr lnr-cross"></a></em>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group wt-btnarea">
                                            <a href="javascript:void(0);" class="wt-btn">Save</a>
                                        </div>
                                    </fieldset>
                                </form>
                            </div>
                        </li>`;
            $("#portfolio-display").append(html);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(error);
        }
    });
}

/**
 * Delete portfolio by Id
 * */
function deleteUserPortfolio(portfolioId) {
    var token = $("#token_Js").val();
    var Url = `https://localhost:7259/api/Portfolios/${portfolioId}`;
    $.ajax({
        url: Url,
        type: "DELETE",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            var portfolioElement = $(`#portfolio_delete-${portfolioId}`);
            portfolioElement.remove();
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.error(error);
        }
    });
}


/**
 * Display portfolio image
 * */
function displayPortfolioImage() {
    var portfolioImageInput = document.getElementById('filet');
    var file = portfolioImageInput.files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        var imageUrl = e.target.result;

        // Hiển thị ảnh trên frontend
        var html = `<div class="wt-uploadingbox" id="img-portfolio-upload">
                       <div class="wt-designimg">
                           <label for="demoz"><img src="${imageUrl}" alt="img description"></label>
                       </div>
                   </div>
                   `;
        // Thêm khối thẻ HTML vào phần tử mục tiêu
        $("#img-portfolio-upload").remove();
        $("#portfolio-img_display").append(html);
        console.log($("#portfolio-img_display"))
    };

    reader.readAsDataURL(file);
}