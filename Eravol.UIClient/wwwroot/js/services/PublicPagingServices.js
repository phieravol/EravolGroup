$(document).ready(function () {

    //Load all category
    getPublicCategoryBySearchTerm();

    //Load all service status
    getServiceStatuses();

    //Add event listener onchange to Category Search
    $('#categorySearch_Js').on('change', function () {
        //Get all category by search tearm
        getPublicCategoryBySearchTerm();
    });

    //listen when user search service then call ajax
    $('#serviceSearch_Js').on('change', function () {
        //Get all category by search tearm
        handleFilter();
    });

    //listen when user change price bar then call ajax
    $('#wt-productrangeslider').on('change', function () {
        //Get all category by search tearm
        handleFilter();
    });

    //Add event listener when radio button changed
    $("input[name='serviceType']").change(function () {
        handleFilter();
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
                    `<span class="wt-checkbox" onclick="handleFilter()">
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
                    `<span class="wt-checkbox" onclick="handleFilter()">
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
function handleFilter() {
    //Get list of category filter
    categories = getCategoryFilters();
    //Get list of service statuses filter
    serviceStatuses = getServiceStatusFilters();

    //display category filter lable
    displayCategoryLable(categories);
    //display service statuses filter label
    displayServiceStatusesLabel(serviceStatuses);

    //Send ajax request
    sendPublicServicePagingReques(categories, serviceStatuses);
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
function sendPublicServicePagingReques(categories, serviceStatuses) {
    //Get search term
    var serviceSearchTerm = $("#serviceSearch_Js").val();
    //Get price type and value of service
    var minPrice = 0;
    var maxPrice = 999999;
    var priceType = $('input[name="serviceType"]:checked').val();
    var priceValue = $('#wt-consultationfeeamount').val();

    var numbers = priceValue.match(/\d+/g);
    if (numbers.length === 2) {
        minPrice = parseInt(numbers[0]); // Chuyển số thứ nhất thành kiểu số nguyên
        maxPrice = parseInt(numbers[1]); // Chuyển số thứ hai thành kiểu số nguyên
    }

    var categoryIds = categories.map(function (category) {
        return category.categoryId;
    });

    var serviceStatusIds = serviceStatuses.map(function (status) {
        return status.serviceStatusId;
    });

    var baseUrl = 'https://localhost:7259/';
    var apiUrl = 'api/ServicesPublic';

    apiUrl += `?PriceType=${priceType}&MinPrice=${minPrice}&MaxPrice=${maxPrice}`

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
            console.log(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi khi yêu cầu thất bại
            console.log("Lỗi: " + error);
        }
    });
}