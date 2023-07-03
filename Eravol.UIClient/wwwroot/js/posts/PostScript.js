$(document).ready(function () {

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

    //Init selected skill list
    selectedSkills = [];

    //add event listener to handle after user click add skill button
    $("#btn-skill_add").click(function () {
        //read skill id and name
        var skillSelectedId = $("#select-skill_display").val();
        var skillName = $("#select-skill_display").children("option:selected").text();
        var skill = {
            skillId: skillSelectedId,
            skillName: skillName
        };

        selectedSkills.push(skill);
        displaySkillList(selectedSkills);
    });

    $("#submit-container").click(function () {
        handleSubmitPosts(selectedSkills)
    });


});

/**
* Add skill to skill list when user click on add skill button
* */
function displaySkillList(selectedSkills) {
    $("#ul-skill_list").empty();
    selectedSkills.forEach(function (skill) {
        var html = `<li>
                            <div class="wt-dragdroptool">
                                <a href="javascript:void(0)" class="lnr lnr-menu"></a>
                            </div>
                            <input type="hidden" id="hidden-id_${skill.skillId}" value="${skill.skillId}" />
                            <span class="skill-dynamic-html">${skill.skillName}</span>
                            <div class="wt-rightarea">
                                <button onclick="removeSkillById(${skill.skillId})" class="wt-deleteinfo"><i class="lnr lnr-trash"></i></button>
                            </div>
                        </li>`;
        $("#ul-skill_list").append(html);
    });
}


/**
* Remove skill by skill Id
 * */
function removeSkillById(id) {
    console.log(id);
    selectedSkills = $.grep(selectedSkills, function (skill) {
        return skill.skillId != id;
    });
    displaySkillList(selectedSkills);
}


function handleSubmitPosts(selectedSkills) {
    createServiceByFormData(selectedSkills);

}
function createServiceByFormData(selectedSkills) {
    const postUrl = "https://localhost:7259/api/Posts";
    var token = $("#token_Js").val();
    console.log("token: " + token);

    //Get Service informations
    var postTitle = $("#postTitle_Js").val();
    var expirationDate = $("#expirationDate_JS").val();
    var categoryId = $("#categoryId_Js").val();
    var budget = $("#budget_Js").val();
    var sortDesc = $("#sortDesc_Js").val();
    var postDetails = tinymce.get('wt-ckeditor').getContent();
    
    var postStatusId = 2;

    $.ajax({
        url: postUrl,
        type: "POST",
        data: JSON.stringify({
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
            var postId = response;
            var createSkillRequires = [];
            selectedSkills.forEach(function (skill) {
                console.log(skill.skillId); // in ra giá trị từng phần tử trong mảng
                var skill = {
                    postId: postId,
                    skillId: skill.skillId
                };
                console.log(skill);
                createSkillRequires.push(skill);
            });
            createPostSkillRequired(createSkillRequires);
            location.href = "https://localhost:7053/Posts/Clients";
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
    
}
function createPostSkillRequired(createSkillRequires) {
    const postSkillUrl = "https://localhost:7259/api/PostSkillRequire";
    $.ajax({
        url: postSkillUrl,
        type: "POST",
        data: JSON.stringify(createSkillRequires),
        contentType: 'application/json',
        success: function (response) {
            console.log(response);

        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}

