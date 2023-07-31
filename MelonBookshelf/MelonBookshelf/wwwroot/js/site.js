// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Run the function on page load to set the initial state of the button
document.onload = setDarkThemeButtonState();


function setDarkThemeButtonState() {
    var button = document.getElementById('darkThemeButton');
    var isDarkThemeCookieExists = document.cookie.indexOf('DarkTheme=Dark') !== -1;
    if (isDarkThemeCookieExists == true) {
        button.classList.add("dark-theme");
    }
    else {
        button.classList.remove("dark-theme");
    }
}

function toggleDarkTheme() {
    var button = document.getElementById('darkThemeButton');
    button.classList.toggle("dark-theme");

    // Check if the button is currently in the dark-theme state and update the cookie accordingly
    //if (button.classList.contains("dark-theme")) {
    //    document.cookie = "DarkTheme=Dark;path=/";
    //} else {
    //    document.cookie = "DarkTheme=;path=/";
    //}

    // Make a POST request to the server to update the DarkTheme cookie

    $.ajax({
        url: "/User/DarkTheme",
        type: "POST",
        success: function (response) {
            alert(response);
        }
    });
}
