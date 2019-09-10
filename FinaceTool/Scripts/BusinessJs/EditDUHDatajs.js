
$(document).ready(function () {

    var projectname = $("#ProjectID option:selected").text();
    if (projectname == "Please Select Project") {

        $('#createprojectlink').show();
        $('#ProjectID').hide();
    }
    else {

        $('#createprojectlink').hide();
    }

    $('#OpportunityID').change(function (evt) {

        var OpportunityName = $("#OpportunityID option:selected").text();
        $("#hdnOpportunityName").val(OpportunityName);

    });


    $('#ProjectID').change(function (evt) {
        var ProjName = $("#ProjectID option:selected").text();
       
        $("#hdnProjectName").val(ProjName);


        $.ajax({
            type: 'POST',
            url: 'DUHData/GetBindingdetails',
            contentType: 'application/json; charset=utf-8',
            data: { proid: projectId, locale: 'en-US' },
            dataType: 'json',
            success: AjaxSucceeded,
            error: AjaxFailed
        });
    });

    function AjaxCall(url, data, type) {
        return $.ajax({
            url: url,
            type: type ? type : 'GET',
            data: data,
            contentType: 'application/json'
        });
    }
});

function myJavaScriptFunction() {

    var msg = $("#OpportunityID option:selected").text();
   
    $('#OpportunityName1').val(msg);
    $("#showModal").html(msg);
    $("#myModal").modal();
};

$("#btnSave").click(function () {
    alert("hi");
    debugger;
    var ProjectName = document.getElementById("ProjectName1").value;
    var ProjectCode = document.getElementById("ProjectCode").value;
    var OpportunityName = $("#OpportunityID option:selected").val();
    var SowStatus = $("#SowStatus1 option:selected").val();
    var poavailable = $("#poavailable1 option:selected").val();
    var POBalance = document.getElementById("POBalance1").value;
    var Note1 = document.getElementById("Note1").value;
    $.ajax({
        url: '@Url.Content("~/DUHData/CreateProject/")',
        type: 'POST',
        dataType: 'application/json',
        data: { 'ProjectName': ProjectName, 'ProjectCode': ProjectCode, 'OpportunityName': OpportunityName, 'SowStatus': SowStatus, 'poavailable': poavailable, 'POBalance': POBalance, 'Note1': Note1 },
        success: function (result) {
            console.log("success");
            if (result != undefined) {
                // $("divModalBody").html(result);
                console.log(result);
                window.location.reload();
            }
        },
        error: function (xhr) {

            var data = xhr.responseText;

            alert(data.substring(13, 40));


            window.location.reload();
        }
    });

});
