var selectedServices = [];

$(document).ready(function () {
    //Delete service information when user click to Delete button
    $('#btn-selected_delete').click(function () {
        deleteSelectedServices(selectedServices);
    });


});

/**
 * Handle checkbox
 * */
function handleCheckbox(checkbox) {
    selectedServices = getCheckedValues();

    //check status of checkbox
    var isChecked = checkbox.checked;

    if (isChecked) {
        selectedServices.push(checkbox.value);
    }
    setButtonStatus(selectedServices);
}

/**
 * Delete Service
 * */
function deleteServiceInfo(serviceCode) {
    
}


/**
 * Delete selected service
 * */
function deleteSelectedService() {

}


/**
 * Get all checked value of checkbox
 * */
function getCheckedValues() {
    var checkboxes = document.querySelectorAll('input[name="checkedService"]:checked');
    var values = [];

    checkboxes.forEach(function (checkbox) {
        values.push(checkbox.value);
    });
    return values;
}

/**
 * set delete selected button states
 * */
function setButtonStatus(selectedServices) {
    var button = $('#btn-selected_delete');
    if (selectedServices != null && selectedServices.length != 0) {
        document.getElementById('btn-selected_delete').setAttribute("style", "display: inline;");
    } else {
        document.getElementById('btn-selected_delete').setAttribute("style", "display: none;");
    }
}

/**
 * delete selected services
 * */
function deleteSelectedServices(selectedServices) {
    if (selectedServices == null || selectedServices.length == 0) {
        alert("You must select least 1 service to delete!");
    } else {
        var baseUrl = "https://localhost:7259/";
        var relativeUrl = "api/Services";
        var Url = baseUrl + relativeUrl;

        $.ajax({
            url: Url,
            type: 'DELETE',
            data: JSON.stringify(selectedServices),
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}