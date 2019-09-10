$(document).ready(function () {
  
    $("#hidden1").val = $("#CustomerId option:selected").text();
    $("#CustomerId").change(function () {
        var ddtext = $("#CustomerId option:selected").text();
        $("#hidden1").val(ddtext);
    });
});