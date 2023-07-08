$(document).ready(function () {
    $("#btn-experience_add").click(function () {
        addUserExperience();
    });

});

/**
 * Add a new User Experience
 * */
function addUserExperience() {
    var title = $('#input-title_add').val();
    var position = $('#input-position_add').val();
    var startingDate = $('#input-starting_add').val();
    var endingDate = $('#input-ending_add').val() == "" ? null : $('#input-ending_add').val();
    var description = $('#input-description_add').val();

    var token = $("#token_Js").val();
    var Url = 'https://localhost:7259/api/Experiences';

    $.ajax({
        url: Url,
        type: "POST",
        data: JSON.stringify({
            companyTitle: title,
            position: position,
            jobDescription: description,
            startingDate: startingDate,
            endingDate: endingDate
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            var message = "Create your job exerience successed!";
            displayNotifilcation(true, message);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
            var message = "Create job exerience failed!";
            displayNotifilcation(false, message);
        }
    });
}

/**
 * Update User Experience
 * */
function updateUserExperience(element, experienceId) {
    var companyTitle = $(element).closest("li").find("#exp-title_update").val();
    var startingDate = $(element).closest("li").find("#exp-starting_update").val();
    var endingDate = $(element).closest("li").find("#exp-ending_update").val();
    var position = $(element).closest("li").find("#exp-position_update").val();
    var description = $(element).closest("li").find("#exp-description_update").val();

    var token = $("#token_Js").val();
    const Url = 'https://localhost:7259/api/Experiences';
    $.ajax({
        url: Url,
        type: "PUT",
        data: JSON.stringify({
            experienceId: experienceId,
            companyTitle: companyTitle,
            position: position,
            jobDescription: description,
            startingDate: startingDate,
            endingDate: endingDate
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            var message = "Update job exerience successed!";
            displayNotifilcation(true, message);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
            var message = "Update job exerience failed!";
            displayNotifilcation(false, message);
        }
    });
}

/**
 * Delete user experience
 * */
function deleteUserExperience(experienceId) {
    var token = $("#token_Js").val();
    const Url = `https://localhost:7259/api/Experiences/${experienceId}`;
    $.ajax({
        url: Url,
        type: "DELETE",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            var message = "Delete job exerience successed!";
            displayNotifilcation(true, message);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
            var message = "Delete job exerience failed!";
            displayNotifilcation(false, message);
        }
    });
}

/**
 * Display notifilcation
 * */
function displayNotifilcation(status, message) {
    var html = '';
    if (status == true) {
        html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-success d-flex align-items-center" role="alert">
                                <i class="fa-solid fa-circle-check fa-lg"></i>
                                <div class="m-1">
                                    ${message}.
                                </div>
                            </div>
                        </div>`;
    }
    else {
        html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-danger d-flex align-items-center" role="alert">
                            <i class="fa-solid fa-circle-xmark fa-xl"></i>
                                <div class="m-1">
                                    ${message}
                                </div>
                             </div>
                        </div>`;
    }

    $('#wt-main').append(html);
    setTimeout(function () {
        $('#notification-items').fadeIn().delay(1000).fadeOut();
    }, 1000);
}