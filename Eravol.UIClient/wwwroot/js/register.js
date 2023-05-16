(function ($) {
    'use strict';
    /*==================================================================
        [ Daterangepicker ]*/
    try {
        $('.js-datepicker').daterangepicker({
            "singleDatePicker": true,
            "showDropdowns": true,
            "autoUpdateInput": false,
            locale: {
                format: 'DD/MM/YYYY'
            },
        });
    
        var myCalendar = $('.js-datepicker');
        var isClick = 0;
    
        $(window).on('click',function(){
            isClick = 0;
        });
    
        $(myCalendar).on('apply.daterangepicker',function(ev, picker){
            isClick = 0;
            $(this).val(picker.startDate.format('DD/MM/YYYY'));
    
        });
    
        $('.js-btn-calendar').on('click',function(e){
            e.stopPropagation();
    
            if(isClick === 1) isClick = 0;
            else if(isClick === 0) isClick = 1;
    
            if (isClick === 1) {
                myCalendar.focus();
            }
        });
    
        $(myCalendar).on('click',function(e){
            e.stopPropagation();
            isClick = 1;
        });
    
        $('.daterangepicker').on('click',function(e){
            e.stopPropagation();
        });
    
    
    } catch(er) {console.log(er);}
    /*[ Select 2 Config ]
        ===========================================================*/
    
    try {
        var selectSimple = $('.js-select-simple');
    
        selectSimple.each(function () {
            var that = $(this);
            var selectBox = that.find('select');
            var selectDropdown = that.find('.select-dropdown');
            selectBox.select2({
                dropdownParent: selectDropdown
            });
        });
    
    } catch (err) {
        console.log(err);
    }
    

})(jQuery);

/**
 * hadle event submit register form
 * */
$("#button__submit").click(function () {
    //check confirm password is match or not
    if (!checkConfirmEmail()) {
        return;
    } else {
        //Call register API
        sendRequestFormData();
    }

});


/**
 * Check confirm password is match or not
 * */
function checkConfirmEmail() {
    let password = $(".pass__enter").val();
    let confirmPassword = $(".pass__confirm").val();
    let confirmElement = $(".confirm-error-message");

    if (password !== confirmPassword) {
        confirmElement.text("Your confirm password is not match, please try again!");
        confirmElement.css("display", "block");
        return false;
    }
    confirmElement.css("display", "none");
    return true;
}
/**
 * send request by ajax
 * */
function sendRequestFormData() {
    let username = $(".input-username").val();
    let email = $(".input-email").val();
    let firstname = $(".input-firstname").val();
    let lastname = $(".input-lastname").val();
    let password = $(".pass__enter").val();
    let birthday = $(".input-birthday").val();
    let role = $("input[name='role']:checked").val();
    let countryCode = $(".select__listcountry").val();
    let phoneNumber = $(".input-phone").val();

    //convert birthday into format yyyy-MM-dd
    var parts = birthday.split('/');
    var day = parts[0];
    var month = parts[1];
    var year = parts[2];
    var birthdayFormated = new Date(year, month, day);
    console.log(birthdayFormated);

    var inputData = {
        userName: username,
        email: email,
        password: password,
        firstName: firstname,
        lastName: lastname,
        country: countryCode,
        birthday: birthdayFormated,
        phoneNumber: phoneNumber,
        role: role
    };

    $.ajax({
        url: "https://localhost:7259/api/Auth/register",
        type: "POST",
        data: JSON.stringify(inputData),
        contentType: "application/json",
        success: function (response) {
            console.log("Request successful");
            window.location.href = "/";
            console.log(response);
        },
        error: function (xhr, status, error) {
            console.log("Request failed");
            console.log(error);
            console.log(xhr);
        }
    });
}