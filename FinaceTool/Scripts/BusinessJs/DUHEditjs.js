$(document).ready(function () {
    $("input:text,form").attr("autocomplete", "off");
    if ($('#poavilable').text() !== 'Yes') {
        $('#pobalancetd').hide();
    }
    else {
        $('#pobalancetd').show();
    }

    //ProjectID

});