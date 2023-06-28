/**
 * hadle event submit login form
 * */
//$("#js-btn-login").click(function () {
    
//    let username = $(".js-login__username").val();
//    let password = $(".js-login__password").val();

//    if (isValidFormat(username, password)) {
//        handleLoginFormData(username, password);
//    } 
//});


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
    var url = "https://localhost:7053/user/login?userName=" + username + "&password=" + password;
    console.log(url);
    $.ajax({
        url: url,
        type: "GET",
        // data: JSON.stringify(inputData),
        contentType: "application/json",
        // If send ajax request successfully
        success: function (response) {

            
            console.log(response);

            //var loginAreaElement = $(".wt-loginarea");
            //var loggedElement = $(".wt-userlogedin");

            //if (loginStatus == 'false') {
            //    var passwordCheckmark = $(".login-checkmark-password");
            //    passwordCheckmark.css("display", "block");
            //    passwordCheckmark.css("color", "red");
            //    passwordCheckmark.text(response.loginResult);

            //    loginAreaElement.css("display", "block");
            //    loggedElement.css("display", "none");
            //} else {
            //    sessionStorage.setItem('loginStatus', loginStatus);
            //    sessionStorage.setItem('fullname', response.fullname);
            //    sessionStorage.setItem('email', response.email);
            //    sessionStorage.setItem('username', response.username);
            //    sessionStorage.setItem('phoneNumber', response.phoneNumber);
            //    sessionStorage.setItem('roles', response.roles);
            //    //location.reload()

            switch (response.roles) {
                case "Admin":
                    window.location.href = "https://localhost:7053/dashboard/insight/admin";
                    break;
                case "Freelancer":
                    window.location.href = "https://localhost:7053/dashboard/insight/clients";
                    break;
                case "Client":
                    window.location.href = "https://localhost:7053/dashboard/insight/freelancers";
                    break;
                default:
                    location.reload();
                    break;
            }

        },
        //if send ajax request failed
        error: function (xhr, status, error) {
            console.log("Request failed");
            console.log(error);
            console.log(xhr);
            console.log(status);
        },
    });
}