$(document).ready(function () {
    $("#btn-skill_add").click(function () {

        addUserSkill();
    });

    $("#btn-skill_update").click(function () {

        console.log('ok');
    });


});

/**
 * Add user skill to database
 * */
function addUserSkill() {
    var token = $("#token_Js").val();

    var skillId = $('#select-skill_Js').val();
    var skillName = $('#select-skill_Js').find('option:selected').text();
    var skillRate = $('#rateSkill_Js').val();

    const Url = 'https://localhost:7259/api/PublicUserSkills';

    $.ajax({
        url: Url,
        type: "POST",
        data: JSON.stringify({
            skillId: skillId,
            skillName: skillName,
            score: skillRate
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-success d-flex align-items-center" role="alert">
                                <i class="fa-solid fa-circle-check fa-lg"></i>
                                <div class="m-1">
                                    Add Skill success into your skill list.
                                </div>
                            </div>
                        </div>`;
            $('#wt-main').append(html);
            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-danger d-flex align-items-center" role="alert">
                            <i class="fa-solid fa-circle-xmark fa-xl"></i>
                                <div class="m-1">
                                    This skill is Exist in your skill list
                                </div>
                             </div>
                        </div>`;
            $('#wt-main').append(html);

            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
        }
    });
}

/**
 * Handle update skill score
 * */
function updateUserSkill(input, userSkillId) {
    var token = $("#token_Js").val();
    var skillRate = input.value;

    const Url = 'https://localhost:7259/api/PublicUserSkills';

    $.ajax({
        url: Url,
        type: "PUT",
        data: JSON.stringify({
            userSkillId: userSkillId,
            score: skillRate
        }),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-success d-flex align-items-center" role="alert">
                                <i class="fa-solid fa-circle-check fa-lg"></i>
                                <div class="m-1">
                                    Update Skill successed!
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
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-danger d-flex align-items-center" role="alert">
                            <i class="fa-solid fa-circle-xmark fa-xl"></i>
                                <div class="m-1">
                                    Update skill failed!
                                </div>
                             </div>
                        </div>`;
            $('#wt-main').append(html);

            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
        }
    });
}

/**
 * Delete UserSkill by UserSkillId
 * */
function handelDeleteUserSkill(userSkillId) {
    var token = $("#token_Js").val();
    const Url = `https://localhost:7259/api/PublicUserSkills/${userSkillId}`;
    $.ajax({
        url: Url,
        type: "DELETE",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-success d-flex align-items-center" role="alert">
                                <i class="fa-solid fa-circle-check fa-lg"></i>
                                <div class="m-1">
                                    Delete Skill successed!
                                </div>
                            </div>
                        </div>`;
            $('#wt-main').append(html);
            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
            var html = `<div id="notification-items" class="position-fixed" style="z-index:2;top: 20px;right: 20px;">
                            <div class="alert alert-danger d-flex align-items-center" role="alert">
                            <i class="fa-solid fa-circle-xmark fa-xl"></i>
                                <div class="m-1">
                                    Delete skill failed!
                                </div>
                             </div>
                        </div>`;
            $('#wt-main').append(html);

            setTimeout(function () {
                $('#notification-items').fadeIn().delay(1000).fadeOut();
            }, 1000);
        }
    });
}