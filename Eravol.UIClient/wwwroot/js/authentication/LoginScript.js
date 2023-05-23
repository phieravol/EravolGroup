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
      url: "https://localhost:7259/api/Auth/authenticate",
      type: "POST",
      data: JSON.stringify(inputData),
      contentType: "application/json",
      // If send ajax request successfully
      success: function (response) {
        const loginStatus = response.loginStatus;
        const loginResult = response.loginResult;

        if (loginStatus==false) {
          var passwordCheckmark = $(".login-checkmark-password");
          passwordCheckmark.css("display", "block");
          passwordCheckmark.css("color", "red");
          passwordCheckmark.text(response.loginResult);
        } else {
            console.log("login successfull");
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