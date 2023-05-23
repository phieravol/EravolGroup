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
        console.log(result);
    } else {
        usernameCheckmark.css("display", "none");
    }

    if (password=="" || password==null || !password) {
        passwordCheckmark.css("display", "block");
        passwordCheckmark.css("color", "red");
        passwordCheckmark.text("password is empty!");
        result = false;
        console.log(result);
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
      url: "https://localhost:7259/api/Auth/authenticate",
      type: "POST",
      data: JSON.stringify(inputData),
      contentType: "application/json",
      success: function (response) {
        // window.location.href = "/";
        console.log(response);
        const loginStatus = response.loginStatus;
        console.log(response.loginStatus);
        if (loginStatus==false) {
          var passwordCheckmark = $(".login-checkmark-password");
          console.log(passwordCheckmark);
          passwordCheckmark.css("display", "block");
          passwordCheckmark.css("color", "red");
          passwordCheckmark.text(response.loginResult);
        }
        console.log("login successfull");
      },
      error: function (xhr, status, error) {
        console.log("Request failed");
        console.log(error);
        console.log(xhr);
      },
    });
}