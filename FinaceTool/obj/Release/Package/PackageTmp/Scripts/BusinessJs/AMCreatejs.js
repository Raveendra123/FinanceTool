

$(document).ready(function () {

    $("input:text,form").attr("autocomplete", "off");
    //   $("select").addClass("form-control")
    $("#ProjectId").change(function () {
        var projecttext = $("#ProjectId option:selected").text();
        var OpportunityName = $("#OpportunityName").val();


        if (projecttext !== 'Please Select Project Name' && OpportunityName !== '') {
            var x = confirm("This will make Deal Stage as L0, Are you sure you want to Select Project");
            if (x === true) {
                $('#DealStageId').val('4');
            }
            else {
                $('#DealStageId').val('');
            }
        }
        else {
            $('#DealStageId').val('');
        }
    });


    $('#OpportunityName').change(function () {
        var projecttext = $("#ProjectId option:selected").text(); 
        var OpportunityName = $("#OpportunityName").val();
        if (projecttext === 'Please Select Project Name' || OpportunityName !== '') {
            $('#DealStageId').val('');
        }
});
   
   
    $('#DuId').change(function () {

        var DUId = $('#DuId option:selected').val();
        var selectobject = document.getElementById("DBBLDuId");
        if (DUId !== "") {
            for (var i = 1; i < selectobject.length; i++) {
                if (selectobject.options[i].value === DUId)
                    selectobject.remove(i);
            }
        }
    });

    $('#DealStageId').change(function () {
        var DealStagetext = $('#DealStageId option:selected').text();
        var projecttext = $("#ProjectId option:selected").text();
        if (DealStagetext === 'L0' && projecttext === 'Please Select Project Name') {
            $('#DealStageId').val('');
            alert("Please Select Project before selecting L0 Stage");
        }
    });

    $(".datepicker").datepicker(
        {
            dateFormat: "yy/mm/dd",
            changeMonth: true,
            changeYear: true
        });
});
