$(document).ready(function () {
    //Init skill require
    var skillsRequire = [];
    skillsRequire = getSkillRequire();

    $("#searchSkillInput").keyup(function () {
        var searchKeyword = $(this).val().toLowerCase();

        $("#select-skill_display option").each(function () {
            var optionValue = $(this).text().toLowerCase();
            if (optionValue.indexOf(searchKeyword) !== -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    //add event listener to handle after user click add skill button
    $("#select-skill_display").on('change', function () {
        //read skill id and name
        var skillSelectedId = $(this).val();
        var skillName = $(this).children("option:selected").text();
        var skill = {
            skillId: skillSelectedId,
            skillName: skillName
        };

        skillsRequire.push(skill);
        console.log(skillsRequire);
        insertPostSkillRequire(skill);
        displaySkillList(skillsRequire);
    });

    //Add event listener to handle after user click on update button
    $("#update-post-button").on('click', function () {
        handleSubmitUpdatePost();
    });
});

/**
* Add skill to skill list when user click on add skill button
* */
function displaySkillList(selectedSkills) {
    $("#ul-skill_list").empty();
    selectedSkills.forEach(function (skill) {
        var html = `<li class="li-skill_list">
                                                <div class="wt-dragdroptool">
                                                    <a href="javascript:void(0)" class="lnr lnr-menu"></a>
                                                </div>
                                                <input type="hidden" value="${skill.skillId}" />
                                                <span class="skill-dynamic-html">${skill.skillName}</span>
                                                <div class="wt-rightarea" onclick="removeSkillById(${skill.skillId}, '${skill.skillName}')">
                                                    <button class="wt-deleteinfo"><i class="lnr lnr-trash"></i></button>
                                                </div>
                                            </li>`;
        $("#ul-skill_list").append(html);
    });
}

/**
 * Insert skill require to database
 * */
function insertPostSkillRequire(skill) {
    const postUrl = "https://localhost:7259/api/PostSkillRequire/SpecifiedPost";
    var token = $("#token_Js").val();
    var postId = $("#postId_JS").val();
    var postSkillRequest = {
        postId: postId,
        skillId: skill.skillId
    };
    $.ajax({
        url: postUrl,
        type: "POST",
        data: JSON.stringify(postSkillRequest),
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            console.log("Add Post Skill Ok");
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}

/**
 * Get all skill require of current post
 * */
function getSkillRequire() {
    skillsRequire = [];
    $('.li-skill_list').each(function () {
        var skillId = $(this).find('input[type="hidden"]').val();
        var skillName = $(this).find('.skill-dynamic-html').text();

        var skill = {
            skillId: skillId,
            skillName: skillName
        };

        skillsRequire.push(skill);

    });
    return skillsRequire;
}

/**
 * remove skill id
 * */
function removeSkillById(skillId, skillName) {
    skillsRequire = getSkillRequire();
    var token = $("#token_Js").val();
    var postId = $("#postId_JS").val();
    var postSkillUrl = `https://localhost:7259/api/PostSkillRequire?skillRequireId=${skillId}&postId=${postId}`;
    $.ajax({
        url: postSkillUrl,
        type: "Delete",
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        success: function (response) {
            skillsRequire = $.grep(skillsRequire, function (skill) {
                return skill.skillId != skillId;
            });
            displaySkillList(skillsRequire);
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}

/**
 * Handle submit update post form
 * */
function handleSubmitUpdatePost() {
    
    //Get Post informations
    var postId = $("#postId_JS").val();
    var postTitle = $("#postTitle_Js").val();
    var expirationDate = $("#expirationDate_JS").val();
    var categoryId = $("#categoryId_Js").val();
    var budget = $("#budget_Js").val();
    var sortDesc = $("#sortDesc_Js").val();
    var postDetails = tinymce.get('wt-ckeditor').getContent();
    var postStatusId = 2;
    //Init URL and author token
    const postUrl = "https://localhost:7259/api/Posts/" + postId;
    var token = $("#token_Js").val();
    $.ajax({
        url: postUrl,
        type: "PUT",
        data: JSON.stringify({
            postId: postId,
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
            location.href = "https://localhost:7053/Posts/Clients";
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}