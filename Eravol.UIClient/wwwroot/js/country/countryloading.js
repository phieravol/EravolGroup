//var myHeaders = new Headers();
//myHeaders.append("apikey", "wMzuswxpV6XxGCdO6fAY1Tv0EWM6IA3c");
//let API_KEY = "baffa73fe1211638acc096f0f5d1ce20"
//let apiURL = "https://api.countrylayer.com/v2/all?access_key=baffa73fe1211638acc096f0f5d1ce20";

//var requestOptions = {
//    method: 'GET',
//    redirect: 'follow',
//    headers: myHeaders
//};
///**
// * Call API to get data from 3rd party
// * */
//fetch("https://api.apilayer.com/number_verification/countries", requestOptions)
//    .then(response => response.json())
//    .then(data => {
//        displayCountryList(data);
//    })
//    .catch(error => console.log('error', error));

///**
// * Display all country list
// * */
//function displayCountryList(data) {
//    let countries = data;
//    var select = $(".select__listcountry");
//    Object.keys(countries).forEach(key => {
//        let option = document.createElement("option");
//        option.text = countries[key]["country_name"] + " (" + countries[key]["dialling_code"] +")";
//        option.value = key;
//        select.append(option);
//    });
//}

