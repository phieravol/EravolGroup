$(document).ready(function () {

    var loginStatus = sessionStorage.getItem('loginStatus');
    var fullname = sessionStorage.getItem('fullname');
    var username = '@'+sessionStorage.getItem('username');

    if (loginStatus == "true") {
        var loginAreaElement = $(".wt-loginarea");
        var loggedElement = $(".wt-userlogedin");

        loginAreaElement.css("display", "none");
        loggedElement.css("display", "block");

        var loggedFullname = $(".logged-fullname");
        var loggedUsername = $(".logged-username");

        loggedFullname.text(fullname);
        loggedUsername.text(username);
    }
});


/**
 * hadle event submit login form
 * */
$("#js-btn-login").click(function () {
    
    let username = $(".js-login__username").val();
    let password = $(".js-login__password").val();

    if (isValidFormat(username, password)) {
        handleLoginFormData(username, password);
    } 
});


/**
 * check is form field format is valid or not
 */
function isValidFormat(username, password) {

    var result = true;
    var usernameCheckmark = $(".login-checkmark-username");
    var passwordCheckmark = $(".login-checkmark-password");

    if (username=="" || username==null || !username) {
        usernameCheckmark.css("display", "block");
        usernameCheckmark.css("color", "red");
        usernameCheckmark.text("username is empty!");
        result = false;
    } else {
        usernameCheckmark.css("display", "none");
    }

    if (password=="" || password==null || !password) {
        passwordCheckmark.css("display", "block");
        passwordCheckmark.css("color", "red");
        passwordCheckmark.text("password is empty!");
        result = false;
    } else {
        passwordCheckmark.css("display", "none");
    }
    
    return result;
}

function handleLoginFormData(username, password) {
    var inputData = {
        userName: username,
        password: password,
    };

    $.ajax({
        url: "https://localhost:7053/user/login?userName="+username+"&password="+password,
        type: "GET",
        // data: JSON.stringify(inputData),
        contentType: "application/json",
        // If send ajax request successfully
        success: function (response) {
            const loginStatus = response.loginStatus;
            const loginResult = response.loginResult;
            console.log(response);
            console.log(loginStatus);

            var loginAreaElement = $(".wt-loginarea");
            var loggedElement = $(".wt-userlogedin");

            if (loginStatus == 'false') {
                var passwordCheckmark = $(".login-checkmark-password");
                passwordCheckmark.css("display", "block");
                passwordCheckmark.css("color", "red");
                passwordCheckmark.text(response.loginResult);

                loginAreaElement.css("display", "block");
                loggedElement.css("display", "none");
            } else {
                
                console.log("login successfull");

                sessionStorage.setItem('loginStatus', loginStatus);
                sessionStorage.setItem('fullname', response.fullname);
                sessionStorage.setItem('email', response.email);
                sessionStorage.setItem('username', response.username);
                sessionStorage.setItem('phoneNumber', response.phoneNumber);
                sessionStorage.setItem('roles', response.roles);
                location.reload()
            }
        },
        //if send ajax request failed
        error: function (xhr, status, error) {
            console.log("Request failed");
            console.log(error);
            console.log(xhr);
        },
    });
}