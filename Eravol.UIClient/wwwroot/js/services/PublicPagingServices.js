$(window).on("load", function () {

    //Load all category
    getPublicCategoryBySearchTerm();

    //Load all service status
    getServiceStatuses();

    //Load all services
    sendPublicServicePagingReques(null, null, 1);

    //Add event listener onchange to Category Search
    $('#categorySearch_Js').on('change', function () {
        //Get all category by search tearm
        getPublicCategoryBySearchTerm();
    });

    //listen when user search service then call ajax
    $('#serviceSearch_Js').on('change', function () {
        //Get all category by search tearm
        handleFilter(1);
    });

    //listen when user change price bar then call ajax
    $('#wt-productrangeslider').on('change', function () {
        //Get all category by search tearm
        handleFilter(1);
    });

    //Add event listener when radio button changed
    $("input[name='serviceType']").change(function () {
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
						<input id=category-`+ category.categoryId + ` type="checkbox" name="category" value=" `+ category.categoryId + `">
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
 * Get all service status
 * */
function getServiceStatuses() {
    var baseUrl = 'https://localhost:7259/';
    var relativeUrl = 'api/ServiceStatuses';
    var url = baseUrl + relativeUrl;

    $.ajax({
        url: url,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            var statuses = response;
            
            statuses["$values"].forEach(function (status) {
                var html =
                    `<span class="wt-checkbox" onclick="handleFilter(${1})">
						<input id="status-`+ status.serviceStatusId + `" type="checkbox" name="serviceStatus" value="` + status.serviceStatusId +`">
						<label for="status-`+ status.serviceStatusId + `">`+ status.serviceStatusName +`</label>
					</span>`;
                $("#service-status_Js").append(html);
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
    //Get list of category filter
    categories = getCategoryFilters();
    //Get list of service statuses filter
    serviceStatuses = getServiceStatusFilters();

    //display category filter lable
    displayCategoryLable(categories);
    //display service statuses filter label
    displayServiceStatusesLabel(serviceStatuses);

    //Send ajax request
    sendPublicServicePagingReques(categories, serviceStatuses, currentPage);
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
 * Get service statuses filter
 * */
function getServiceStatusFilters() {
    var statusCheckboxes = $('input[type="checkbox"]:checked[name="serviceStatus"]');
    var serviceStatuses = [];
    statusCheckboxes.each(function () {
        var serviceStatusId = $(this).val();

        var serviceStatusName = $(this).next('label').text();
        var serviceStatus = {
            serviceStatusId: serviceStatusId,
            serviceStatusName: serviceStatusName
        };
        serviceStatuses.push(serviceStatus);

    });

    
    return serviceStatuses;
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
				        <a href="javascrip:void(0)"><i class="fa fa-times close" data-dismiss="alert" aria-label="close"></i> <span>`+ category.categoryName +`</span></a>
			        </li>`;
        $("#category-filtertag").append(html);
    });
}

/**
 * Display all service status filter label
 * */
function displayServiceStatusesLabel(serviceStatuses) {
    $("#status-filtertag").empty();
    var clearStatusElement = `<li class="wt-filtertagclear" id="clear-statusTag">
									<a href="javascrip:void(0)"><i class="fa fa-times"></i> <span>Service Status filter</span></a>
							  </li>`;
    $("#status-filtertag").append(clearStatusElement);

    serviceStatuses.forEach(function (status) {
        var html =
            `<li class="alert alert-dismissable fade in">
				<a href="javascrip:void(0)"><i class="fa fa-times close" data-dismiss="alert" aria-label="close"></i> <span>`+ status.serviceStatusName + `</span></a>
			</li>`;
        $("#status-filtertag").append(html);
    });
}

/**
 * Send ajax request to PublicServiceController
 * */
function sendPublicServicePagingReques(categories, serviceStatuses, currentPage) {
    //Get search term
    var serviceSearchTerm = $("#serviceSearch_Js").val();
    //Get price type and value of service
    var minPrice = 0;
    var maxPrice = 999999;
    var priceType = $('input[name="serviceType"]:checked').val();
    var priceValue = $('#wt-consultationfeeamount').val();

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

    if (serviceStatuses != null) {
        var serviceStatusIds = serviceStatuses.map(function (status) {
            return status.serviceStatusId;
        });
    }
    
    var baseUrl = 'https://localhost:7259/';
    var apiUrl = 'api/ServicesPublic';

    apiUrl += `?PriceType=${priceType}&MinPrice=${minPrice}&MaxPrice=${maxPrice}&currentPage=${currentPage}`

    if (serviceSearchTerm != null) {
        apiUrl += `&SearchTerm=${serviceSearchTerm}`;
    }

    if (categories != null && categories.length != 0) {
        categoryIds.forEach(function (categoryId) {
            apiUrl += `&categoryFilters=${categoryId}`;
        });
    }

    if (serviceStatusIds != null && serviceStatusIds.length != 0) {
        serviceStatusIds.forEach(function (statusId) {
            apiUrl += `&serviceStatusFilters=${statusId}`;
        });
    }

    var Url = baseUrl + apiUrl;
    $.ajax({
        url: Url,
        method: "GET",
        success: function (response) {
            //display service response
            displayServiceResponse(response);
            //Display pageIndex response
            displayPagingIndexesItems(response);
            //Display total result item
            displayTotalResultItems(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi khi yêu cầu thất bại
            console.log("Lỗi: " + error);
        }
    });
}


/**
 * Display services response
 * */
function displayServiceResponse(response) {
    var servicesList = response.items;
    $("#services-container").empty();

    servicesList["$values"].forEach(function (service) {
        var serviceType = "";
        if (service.priceType == "anyType") {
            serviceType = "Any Price Type";
        } else if (service.priceType == "hourly") {
            serviceType = "Hourly Price";
        } else if (service.priceType == "fixed") {
            serviceType = "Fix Price";
        }

        var html =
            `<div class="wt-userlistinghold wt-featured wt-userlistingholdvtwo">
						<span class="wt-featuredtag"><img src="/images/featured.png" alt="img description" data-tipso="Plus Member" class="template-content tipso_style"></span>
						<div class="wt-userlistingcontent">
							<div class="wt-contenthead">
								<div class="wt-title">
									<a href="usersingle.html"><i class="fa fa-check-circle"></i> ${service.freelancerName}</a>
									<h2>${service.serviceTitle}</h2>
								</div>
								<div class="wt-description">
									<p>${service.serviceIntro}</p>
								</div>

							</div>
							<div class="wt-viewjobholder">
								<ul>
									<li><span><i class="fa fa-dollar-sign wt-viewjobdollar"></i>Price: ${service.price}$ </span></li>
											<li><span><em><img style="width:16px" src="https://localhost:7259/api/Images/${service.categoryImage}" alt="img description"></em>${service.categoryName}</span></li>
											<li><span><i class="far fa-folder wt-viewjobfolder"></i>Type: ${serviceType}</span></li>
									<li><span><i class="far fa-clock wt-viewjobclock"></i>Status: ${service.serviceStatusName}</span ></li >
									<li><span><i class="fa fa-tag wt-viewjobtag"></i>Job ID: ${service.serviceCode}</span></li>
									<li><a href="javascript:void(0);" class="wt-clicklike wt-clicksave"><i class="fa fa-heart"></i> Save</a></li>
									<li class="wt-btnarea"><a href="userlisting.html" class="wt-btn">View Job</a></li>
								</ul>
							</div>
						</div>
					</div>`;

        $("#services-container").append(html);

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