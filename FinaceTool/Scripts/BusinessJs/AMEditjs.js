$(document).ready(function () {
    $("input:text,form").attr("autocomplete", "off");
    $("#CustomerId").change(function () {
        var customertext = $("#CustomerId option:selected").text();
        $("#hdncustomername").val(customertext);
    });

    $("#AMId").change(function () {
        var AMText = $("#AMId option:selected").text();
        $("#hdnAMName").val(AMText);
    });

    $("#LobId").change(function () {
        var lobtext = $("#LobId option:selected").text();
        $("#hdnLOBName").val(lobtext);
    });

    $("#DuId").change(function () {
        var dutext = $("#DuId option:selected").text();
        $("#DBBLDuId").remove.text(dutext);
        $("#hdnDUName").val(dutext);
    });

    $("#DBBLDuId").change(function () {
        var DBBLduText = $("#DBBLDuId option:selected").text();
        $("#hdnDBBLDU").val(DBBLduText);
    });

    $("#ProjectIdForEdit").change(function () {
        var ptext = $("#ProjectIdForEdit option:selected").text();
        $("#hdnProjectName").val(ptext);
        var OpportunityName = $('#OpportunityName').val();
        if (OpportunityName !== 'L0' && ptext !== 'Please Select Project Name') {
            $('#DealStageId').val('4');
        }
        else {
            $('#DealStageId').val('');
        }


        var ProjectId = $('#ProjectIdForEdit option:seletced').val();
        var OpportunityId = $('#OpportunityID').val();
        var Url = "@Url.Content(~/AMData/GetData)";
        $.ajax({
            url: Url,
            dataType: 'json',
            data: { opportunityId: OpportunityId, projectId: ProjectId },
            success: function (data) {
                $("#stateID").empty();
                $("#stateID").append("<option value='0'>--Select State--</option>");
                $.each(data, function (index, optiondata) {
                    $("#stateID").append("<option value='" + optiondata.ID + "'>" + optiondata.StateName + "</option>");
                });
            }
        });

        $("#ServiceLineId").change(function () {
            var servicelinetext = $("#ServiceLineId option:selected").text();
            $("#hdnServiceLine").val(servicelinetext);
        });

        $("#ProductGroupId").change(function () {
            var productgrouptext = $("#ProductGroupId option:selected").text();
            $("#hdnProductGroup").val(productgrouptext);
        });

        $("#SowStatusId").change(function () {
            var sowtext = $("#SowStatusId option:selected").text();
            $("#hdnSowStatus").val(sowtext);
        });

        $("#DealStageId").change(function () {
            var dealstagetext = $("#DealStageId option:selected").text();
            $("#hdnDealStage").val(dealstagetext);
            var projecttext = $("#ProjectIdForEdit option:selected").text();
            if (dealstagetext === 'L0' && projecttext === 'Please Select Project Name') {
                $('#DealStageId').val('');
                alert("Please Select Project before selecting Opportunity won stage");
            }

        });

        $(".datepicker").datepicker(
            {
                dateFormat: "yy/mm/dd",
                changeMonth: true,
                changeYear: true
            });
    });
});