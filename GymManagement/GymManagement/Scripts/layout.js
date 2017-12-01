$(document).ready(function () {
    var url = $(location).attr('href');
    if (url.indexOf("Home/Index") >= 0)
    {
        $("#body-content").removeClass("container body-content wrapper");
    }
    else {
        $("#body-content").addClass("container body-content wrapper");
    }
});