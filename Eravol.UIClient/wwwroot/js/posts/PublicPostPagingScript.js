$(window).on("load", function () {

    //Load all category
    getPublicCategoryBySearchTerm();

    //Load all skill
    getSkillRequireBySearchTerm();

    //Load all skill Require when user search
    $('#skillSearch_Js').on('change', function () {
        getSkillRequireBySearchTerm();
    });

    //listen when user search service then call ajax
    $('#postSearch_Js').on('change', function () {
        //Get all category by search tearm
        handleFilter(1);
    });

    //listen when user change price bar then call ajax
    $('#wt-productrangeslider').on('change', function () {
        //Get all category by search tearm
        handleFilter(1);
    });
});

/**
 * Get all public category by search term
 * */
function getPublicCategoryBySearchTerm() {
    var categorySearch = $('#categorySearch_Js').val();
    var baseUrl = 'https://localhost:7259/';
    var relativeUrl = 'api/PublicCategories/';
    var url = baseUrl + relativeUrl + categorySearch;
    //Send ajax request to get category list
    $.ajax({
        url: url,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            //console.log(response);
            var categories = response;
            var categoryContainer = $("#mCSB_1_container");
            categoryContainer.empty();
            categories["$values"].forEach(function (category) {
                var html =
                    `<span class="wt-checkbox" onclick="handleFilter(${1})">
						<input id=category-`+ category.categoryId + ` type="checkbox" name="category" value=" ` + category.categoryId + `">
						<label for="category-`+ category.categoryId + `"> ` + category.categoryName + `</label>
					</span>`;
                $("#mCSB_1_container").append(html);
            });
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });
}

/**
 * Get Skill require by search term
 * */
function getSkillRequireBySearchTerm() {
    var skillSearch = $('#skillSearch_Js').val();
    var baseUrl = 'https://localhost:7259/';
    var relativeUrl = 'api/PostSkillRequire/SearchTerm?SearchTerm=';
    var url = baseUrl + relativeUrl + skillSearch;
    //Send ajax request to get category list
    $.ajax({
        url: url,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            //console.log(response);
            var skills = response;
            var categoryContainer = $("#mCSB_2_container");
            categoryContainer.empty();
            skills["$values"].forEach(function (skill) {
                
                var html =
                    `<span class="wt-checkbox" onclick="handleFilter(${1})">
						<input id="skill-${skill.skillId}" type="checkbox" name="skill" value="${skill.skillId}">
						<label for="skill-${skill.skillId}">${skill.skillName}</label>
					</span>`;
                $("#mCSB_2_container").append(html);
            });
        },
        error: function (xhr, status, error) {
            console.log(status);
            console.log(xhr);
            console.log(error);
        }
    });

}

/**
 * Handle filter
 * */
function handleFilter(currentPage) {

    categories = getCategoryFilters();  //Get list of category filter
    
    postSkills = getPostSkillsFilters();   //Get list of service statuses filter

    displayCategoryLable(categories);   //display category filter lable

    displayPostSkillsLabel(postSkills);   //display service statuses filter label

    sendPublicPostPagingReques(categories, postSkills, currentPage); //Send ajax to paging services
}


/**
 * Get Categories filters
 * */
function getCategoryFilters() {
    var categoryCheckboxes = $('input[type="checkbox"]:checked[name="category"]');
    var categories = [];

    categoryCheckboxes.each(function () {
        var categoryId = $(this).val();
        var categoryName = $(this).next('label').text();
        var category = {
            categoryId: categoryId,
            categoryName: categoryName
        };

        categories.push(category);
    });

    return categories;
}

/**
 * Display category filter lable
 * */
function displayCategoryLable(categories) {
    $("#category-filtertag").empty();
    var clearCategoryFilters = `<li class="wt-filtertagclear" id="category-filtertag">
									<a href="javascrip:void(0)"><i class="fa fa-times"></i> <span>Categories filter</span></a>
								</li>`;
    $("#category-filtertag").append(clearCategoryFilters);
    categories.forEach(function (category) {
        var html = `<li class="alert alert-dismissable fade in">
				        <a href="javascrip:void(0)"><i class="fa fa-times close" data-dismiss="alert" aria-label="close"></i> <span>`+ category.categoryName + `</span></a>
			        </li>`;
        $("#category-filtertag").append(html);
    });
}

/**
 * Get post skills filter
 * */
function getPostSkillsFilters() {
    var skillCheckboxes = $('input[type="checkbox"]:checked[name="skill"]');
    var skills = [];

    skillCheckboxes.each(function () {
        var skillId = $(this).val();
        var skillName = $(this).next('label').text();
        var skill = {
            skillId: skillId,
            skillName: skillName
        };

        skills.push(skill);
    });

    return skills;
}

/**
 * Display post skill lable
 * */
function displayPostSkillsLabel(skills) {
    $("#skill-filtertag").empty();
    var clearSkillFilters = `<li class="wt-filtertagclear" id="skill-filtertag">
									<a href="javascrip:void(0)"><i class="fa fa-times"></i> <span>Skill filter</span></a>
								</li>`;
    $("#skill-filtertag").append(clearSkillFilters);
    skills.forEach(function (skill) {
        var html = `<li class="alert alert-dismissable fade in">
				        <a href="javascrip:void(0)"><i class="fa fa-times close" data-dismiss="alert" aria-label="close"></i> <span>`+ skill.skillName + `</span></a>
			        </li>`;
        $("#skill-filtertag").append(html);
    });
}


/**
 * Send ajax request to PublicServiceController
 * */
function sendPublicPostPagingReques(categories, postSkill, currentPage) {
    //Get search term
    var postSearchTerm = $("#postSearch_Js").val();
    //Get price type and value of service
    var priceValue = $('#wt-consultationfeeamount').val();
    var minPrice = 0;
    var maxPrice = 999999;
    
    if (priceValue != null) {
        var numbers = priceValue.match(/\d+/g);
        if (numbers.length === 2) {
            minPrice = parseInt(numbers[0]); // Chuyển số thứ nhất thành kiểu số nguyên
            maxPrice = parseInt(numbers[1]); // Chuyển số thứ hai thành kiểu số nguyên
        }
    }
    if (categories != null) {
        var categoryIds = categories.map(function (category) {
            return category.categoryId;
        });
    }

    if (postSkill != null) {
        var skillIds = postSkill.map(function (skill) {
            return skill.skillId;
        });
    }

    var baseUrl = 'https://localhost:7259/';
    var apiUrl = 'api/PostsPublic/FilterPost';

    apiUrl += `?MinPrice=${minPrice}&MaxPrice=${maxPrice}&CurrentPage=${currentPage}`

    if (postSearchTerm != null) {
        apiUrl += `&SearchTerm=${postSearchTerm}`;
    }

    if (categories != null && categories.length != 0) {
        categoryIds.forEach(function (categoryId) {
            apiUrl += `&categoryFilters=${categoryId}`;
        });
    }

    if (skillIds != null && skillIds.length != 0) {
        skillIds.forEach(function (skillId) {
            apiUrl += `&skillFilters=${skillId}`;
        });
    }
    var Url = baseUrl + apiUrl;
    $.ajax({
        url: Url,
        method: "GET",
        success: function (response) {
            displayPostResponse(response);   //Display service list items
            displayPagingIndexesItems(response);    //Display pageIndex list items
            displayTotalResultItems(response);  //Display total result item
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi khi yêu cầu thất bại
            console.log("error: " + error);
            console.log("status: " + status);
            console.log("xhr: " + xhr);
        }
    });
}


/**
 * Display Post response
 * */
function displayPostResponse(response) {
    var postsList = response.items;
    $("#posts-container").empty();

    postsList["$values"].forEach(function (post) {
        console.log(post);
        const postedDate = new Date(post.postedDate);
        const formattedDate = `${(postedDate.getMonth() + 1).toString().padStart(2, '0')}/${postedDate.getDate().toString().padStart(2, '0')}/${postedDate.getFullYear()}`;
        const expireDate = new Date(post.expirationDate);
        
        const now = new Date();
        console.log(expireDate);
        console.log(now);
        var timeDifference = expireDate - now;
        
        if (timeDifference > 1) {
            const hoursDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
            timeDifference = `${hoursDifference} days`;
        }
        else if (timeDifference > 0 && expireDate < 1) {
            timeDifference = "One day left"
        }
        else{
            timeDifference = 'Expired';
        }
        var skillString = "";
        $.each(post.skillRequire["$values"], function (index, skill) {
            // Tạo phần tử <a> chứa skillRequire
            skillString += `<a href="javascript:void(0);">${skill.skillName}</a>`;
        });

        var html =
            `<div class="wt-userlistinghold wt-featured wt-userlistingholdvtwo">
				<span class="wt-featuredtag wt-featuredtagcolor2"><img src="/images/featured.png" alt="img description" data-tipso="Plus Member" class="template-content tipso_style"></span>
				<div class="wt-userlistingcontent">
					<div class="wt-contenthead">
						<div class="wt-title">
							<a href="usersingle.html"><i class="fa fa-check-circle"></i> ${post.fullName}</a>
							<h2>${post.postTitle}</h2>
						</div>
						<div class="wt-description">
							<p>${post.sortDesc}</p>
						</div>
						<div class="wt-tag wt-widgettag">
							${skillString}
						</div>
					</div>
					<div class="wt-viewjobholder">
						<ul>
							<li><span><i class="fa fa-dollar-sign wt-viewjobdollar"></i>${post.budget} $</span></li>
							<li><span><em><img src="/images/flag/img-05.png" alt="img description"></em>${post.country}</span></li>
							<li><span><i class="far fa-folder wt-viewjobfolder"></i>Posted Date: ${formattedDate}</span></li>
							<li><span><i class="far fa-clock wt-viewjobclock"></i>Remain: ${timeDifference}</span></li>
							<li><span><i class="fa fa-tag wt-viewjobtag"></i> ${post.categoryName}</span></li>
							<li><a href="javascript:void(0);" class="wt-clicklike"><i class="fa fa-heart"></i> Click to Save</a></li>
							<li class="wt-btnarea"><a href="userlisting.html" class="wt-btn">View Job</a></li>
						</ul>
					</div>
				</div>
			</div>`;

        $("#posts-container").append(html);
    });

}

/**
 * Display pageIndex response
 * */
function displayPagingIndexesItems(response) {
    var pageIndexes = ``;

    for (var i = 0; i < response.totalPages; i++) {
        pageIndexes += `<li> <a onclick="handleFilter(${i + 1})">${i + 1}</a></li>`;
    }
    var pagingHtml = `
                    <li class="wt-prevpage"><a href="javascrip:void(0);"><i class="lnr lnr-chevron-left"></i></a></li>
					    ${pageIndexes}
					<li class="wt-nextpage"><a href="javascrip:void(0);"><i class="lnr lnr-chevron-right"></i></a></li>`;
    $("#nav-paging_index").empty();
    $("#nav-paging_index").append(pagingHtml);
}

/**
 * Display total result item
 * */
function displayTotalResultItems(response) {
    $("#span-total_result").empty();
    var totalResultHtml = `<span>${response.items["$values"].length} results found</span>`;
    $("#span-total_result").append(totalResultHtml);
}