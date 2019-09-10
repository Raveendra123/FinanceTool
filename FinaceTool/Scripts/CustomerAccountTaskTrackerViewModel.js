$(document).ready(function () {
    
    if ((UserRoleId == 9)||(UserRoleId == 3))
    {
        document.getElementById("CopyForDiv").disabled = false;
        document.getElementById("copyEntries").disabled = false;
    }
    else
    {
        document.getElementById("CopyForDiv").disabled = true;
        document.getElementById("copyEntries").disabled = true;
    }
    

    $('#copyEntries').bind('click', function () {

        var CustomerAccountIdValue = $('#CustomerAccountId').val();
        var TemplatePeriodMonthValue = $('#TemplatePeriodMonth').val();
        var TemplatePeriodYearValue = $('#TemplatePeriodYear').val();
        var ProjectIdValue = $('#ProjectId').val();
        var copyMonth = $('#Month').val();
        var copyYear = $('#Year').val();

        if (newValidation(CustomerAccountIdValue, TemplatePeriodMonthValue, TemplatePeriodYearValue, ProjectIdValue) == false) {
            return false;
        }

        if (copyMonth == -1) {
            alert("Please select Month ");
            return false;
        }
        if (copyYear == -1) {
            alert("Please select Year ");
            return false;
        }
        $.ajax({
            url: "/CustomerAccountTaskTracker/CopyEntries",
            data: "CustomerAccountId=" + CustomerAccountIdValue + "&TemplatePeriodMonth=" + TemplatePeriodMonthValue + "&TemplatePeriodYear=" + TemplatePeriodYearValue + "&ProjectId=" + ProjectIdValue + "&copyMonth=" + copyMonth + "&copyYear=" + copyYear,
            success: function (dataOptions) {
                alert(dataOptions);
            },
            cache: false
        });
       
    });

    $('#filterButton').bind('click', function () {
        var CustomerAccountIdValue = $('#CustomerAccountId').val();
        var TemplatePeriodMonthValue = $('#TemplatePeriodMonth').val();
        var TemplatePeriodYearValue = $('#TemplatePeriodYear').val();
        var ProjectIdValue = $('#ProjectId').val();

        if (newValidation(CustomerAccountIdValue, TemplatePeriodMonthValue, TemplatePeriodYearValue, ProjectIdValue) == false) {
            return false;
        }
        $('#grid').setGridParam({ url: "/CustomerAccountTaskTracker/GetCustomerAccountTaskTrackers?CustomerAccountId=" + CustomerAccountIdValue + "&TemplatePeriodMonth=" + TemplatePeriodMonthValue + "&TemplatePeriodYear=" + TemplatePeriodYearValue + "&ProjectId=" + ProjectIdValue + "" }).trigger('reloadGrid');
    });
});

function newValidation(CustomerAccountIdValue, TemplatePeriodMonthValue, TemplatePeriodYearValue, ProjectIdValue) {

    if (CustomerAccountIdValue == '') {
        alert("Please select Customer Account");
        return false;
    }

    if (TemplatePeriodMonthValue == '') {
        alert("Please select month.");
        return false;
    }

    if (TemplatePeriodYearValue == '') {
        alert("Please select year.");
        return false;
    }
}

$(function () {
    $("#grid").jqGrid({
        datatype: 'json',
        mtype: 'Get',
        colNames: ['CustomerAccountTaskTrackerId', 'Task Name', 'Task Name', 'Task Type', 'Task Type', 'User Name', 'User Name','Sap ID', 'Client Stakeholder', 'Client Stakeholder', 'Due Date', 'Task Status', 'Completed Date'],
        colModel: [
            { key: true, hidden: true, name: 'CustomerAccountTaskTrackerId', index: 'CustomerAccountTaskTrackerId', editable: true },

            { key: false, name: 'TaskName', index: 'TaskName', editable: true, width: 350 },
            {
                key: false, hidden: true, name: 'TaskId', index: 'TaskId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerAccountTaskTracker/GetTasks?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'TaskTypeName', index: 'TaskTypeName', editable: true },
            {
                key: false, hidden: true, name: 'TaskTypeId', index: 'TaskTypeId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerAccountTaskTracker/GetTaskTypes?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            
            { key: false, name: 'UserName', index: 'UserName', editable: true },
            {
                key: false, hidden: true, name: 'UserId', index: 'UserId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false,  formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'SapID', index: 'SapID', editable: true },
            { key: false, name: 'CustomerName', index: 'CustomerName', editable: true,width:200 },
            {
                key: false, hidden: true, name: 'CustomerNameId', index: 'CustomerNameId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerAccountTaskTracker/GetCustomerAccounts?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            {
                name: 'DueDate', height: 100, index: 'DueDate', align: 'right', sortable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'm/d/Y' }, editable: true, editoptions: {
                    readonly: 'readonly', dataInit: function (el)
                    {
                        $(el).datepicker({ dateFormat: 'mm/dd/yy' }).datepicker('setDate', el.value);
                    }
                }
            },

            {
                key: false, hidden: false, name: 'TaskStatusName', index: 'TaskStatusName', editable: true, edittype: "select",
                editoptions: {
                    
                    aysnc: false, dataUrl: "/CustomerAccountTaskTracker/GetTaskStatus?" + new Date(), formatter: 'select',

                    dataEvents: [
{ type: 'change', fn: function (e) { myfunction($(this)); } },

                    ],

                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            {
                name: 'CompletedDate', height: 100, index: 'CompletedDate', align: 'right', sortable: false, formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'm/d/Y' }, editable: true, editoptions: {
                    readonly: 'readonly', dataInit: function (el)
                    {
                        $(el).datepicker({ dateFormat: 'mm/dd/yy' }).datepicker('setDate', el.value);
                    }
                }
            }],
            //{ key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Tracking Activities',
        loadonce: false,
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false,
        editurl: '/CustomerAccountTaskTracker/Edit'
    }).navGrid('#pager', { edit: false, add: true, del: true, search: false, refresh: false },
        {
        },
        {
            // add options
            zIndex: 100,
            url: "/CustomerAccountTaskTracker/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                var CustomerAccountIdValue = $('#CustomerAccountId').val();
                var TemplatePeriodMonthValue = $('#TemplatePeriodMonth').val();
                var TemplatePeriodYearValue = $('#TemplatePeriodYear').val();
                var ProjectIdValue = $('#ProjectId').val();

                if (CustomerAccountIdValue == '') {
                    return [false, 'Please select Customer Account'];
                }

                if (TemplatePeriodMonthValue == '') {
                    return [false, 'Please select month.'];
                }
                if (TemplatePeriodYearValue == '') {
                    return [false, 'Please select year.'];
                }
                if ($('#tr_TaskId').find('td.DataTD').find('select').val() == -1) {
                    return [false, 'Please select Task Name.'];
                }

                if ($('#tr_UserId').find('td.DataTD').find('select').val() == -1) {
                    return [false, 'Please select User.'];
                }
                if ($('#tr_CustomerNameId').find('td.DataTD').find('select').val() == -1) {
                    return [false, 'Please select Client Stakeholder.'];
                }

                return [true, ''];
            },
            onclickSubmit: function (params, postdata) {
                postdata.CustomerAccountId = $('#CustomerAccountId').val();
                postdata.MonthId = $('#TemplatePeriodMonth').val();
                postdata.YearId = $('#TemplatePeriodYear').val();
                postdata.ProjectId = $('#ProjectId').val();
                postdata.UserId = $('#tr_UserId').find('td.DataTD').find('select').val();
                postdata.CustomerNameId = $('#tr_CustomerNameId').find('td.DataTD').find('select').val();
                postdata.TaskStatusId = $('#tr_TaskStatusName').find('td.DataTD').find('select').val();

            },
            afterShowForm: function (form) {
                var idSelector = $.jgrid.jqID(this.p.id);

                if ($('#CustomerAccountId').val() == '')
                {
                    $.jgrid.hideModal("#editmod" + idSelector, { gbox: "#gbox_" + idSelector });
                    alert("Please select the Customer Account.");
                    return false;
                }
                if ($('#TemplatePeriodMonth').val() == '') {
                    $.jgrid.hideModal("#editmod" + idSelector, { gbox: "#gbox_" + idSelector });
                    alert("Please select the Template Period Month.");
                    return false;
                }
                if ($('#TemplatePeriodYear').val() == '') {
                    $.jgrid.hideModal("#editmod" + idSelector, { gbox: "#gbox_" + idSelector });
                    alert("Please select the Template Period Year.");
                    return false;
                }
                //setTimeout(function () {
                //    var selectTaskStatusId = $('#tr_TaskStatusName', form).find('td.DataTD').find('select');
                //    selectTaskStatusId.val(2);
                //}, 50);

                    var selectTaskStatusId = $('#tr_TaskStatusName', form).find('td.DataTD').find('select');
                    selectTaskStatusId.val(2);
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                
                var selectTaskId = $('#tr_TaskId', form).find('td.DataTD').find('select');
                
                var selectDropDownUser = $('#tr_UserId', form).find('td.DataTD').find('select');
                var selectDropDownClient = $('#tr_CustomerNameId', form).find('td.DataTD').find('select');
                
                
                selectDropDownUser.empty();
                selectDropDownClient.empty();

                selectDropDownUser.append($('<option role="option" value="-1">Select One</option>'));
                selectDropDownClient.append($('<option role="option" value="-1">Select One</option>'));

                selectTaskId.unbind('change');
                selectTaskId.bind('change', function () {
                    SetUser($(this), selectDropDownUser, selectTaskId.val(), selectDropDownClient);
                });

                
                selectDropDownUser.unbind('change');
                selectDropDownUser.bind('change', function () {
                    SetClientStakeHolderName($(this), selectDropDownClient, $('#CustomerAccountId').val(), selectDropDownUser.val(), $('#ProjectId').val());
                });


                
                $('#tr_SapID', form).hide();
                $('#tr_TaskName', form).hide();
                $('#tr_TaskId', form).show();

                $('#tr_TaskTypeName', form).hide();
                $('#tr_TaskTypeId', form).show();

                $('#tr_ClientStateHolderName', form).show();
                $('#tr_ClientStateHolderId', form).hide();

                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
                $('#tr_CustomerName', form).hide();
                $('#tr_CustomerNameId', form).show();
                $('#tr_DueDate', form).hide();
                $('#tr_TaskStatusName', form).show();
                $('#tr_CompletedDate', form).hide();
                
                //setTimeout(function () {
                //    var selectTaskStatusId = $('#tr_TaskStatusName', form).find('td.DataTD').find('select');
                //    selectTaskStatusId.val(2);
                //}, 50);
                //event.preventDefault();
                filterGrid();
                
            }
        },

        {
            // delete options
            zIndex: 100,
            width:275,
            url: "/CustomerAccountTaskTracker/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this record?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });

    
    $('#grid').jqGrid('navGrid', '#pager',
               {
                   edit: false,
                   add: false,
                   del: false,
                   search: false
               }
        );
    $("#grid").jqGrid('inlineNav', '#pager',
     {
         edit: true,
         editicon: "ui-icon-pencil",
         add: false,
         addicon: "ui-icon-plus",
         save: true,
         saveicon: "ui-icon-disk",
         cancel: true,
         cancelicon: "ui-icon-cancel",
         editParams: {
             keys: false,
             oneditfunc: null,
             successfunc: function (val) {
                 if (val.responseText != "") {
                     var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
                     var value = $('#grid').jqGrid('getCell', sel_id, '_id');
                     $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                     alert(val.responseText);
                 }
             },
             url: null,
             
             extraparam: {

                 args1: function () {
                     var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
                     return findObject(sel_id, "TaskStatusName").val();
                 },
                 args2: function () {
                     var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
                     return findObject(sel_id, "SapID").val();
                 },
                 args3: function () {
                     var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
                     return findObject(sel_id, "DueDate").val();
                 },
                 args4: function () {
        var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
        return findObject(sel_id, "CompletedDate").val();
    }
             },
             
             aftersavefunc: null,
             oneditfunc: function (row_id) {
                 var userNameValue = findObject(row_id, "UserName").val();

                     findObject(row_id, "TaskStatusName").hide();
                     findObject(row_id, "TaskStatusId").hide();

                     findObject(row_id, "TaskName").prop("disabled", true);
                     findObject(row_id, "TaskTypeName").prop("disabled", true);
                     findObject(row_id, "UserName").prop("disabled", true);
                     findObject(row_id, "SapID").prop("disabled", true);
                     findObject(row_id, "CustomerName").prop("disabled", true);
                     findObject(row_id, "IsActive").prop("disabled", true);
                     
                     //alert(row_id);

                     //findObject(row_id, "TaskStatusName").unbind('change');
                     //findObject(row_id, "TaskStatusName").bind('change', function () {
                     //    callFun($(this), row_id);
                     //});

                     
                     //selectTaskStatusName.bind('change', function () {
                         
                         
                     //});
             },
             afterrestorefunc: null,
             restoreAfterError: true,
             mtype: "POST"
         },
         addParams: {
             useDefValues: true,
             addRowParams: {
                 keys: true,
                 extraparam: {},
                 // oneditfunc: function () { alert(); },
                 successfunc: function (val) {
                     if (val.responseText != "") {
                         alert(val.responseText);
                         $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                     }
                 }
             }
         }
     }
);

    
    function myfunction(selectDropBox)
    {
        var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
            findObject(sel_id, "CompletedDate").val("");
    }
   
    function myValidate1(row_id) {
        $("#grid_ilcancel").click();
        $("#grid_iledit").removeClass('ui-state-disabled');
        //$("#del_" + thisId).removeClass('ui-state-disabled');

        //$("#grid_iledit").addClass('ui-state-disabled');
        //$("#grid_ilcancel").addClass('ui-state-disabled');
        //$("#grid_ilsave").addClass('ui-state-disabled');
        alert("You don;t have rights");
            return [false, ""];
    }
    function findObject(rowId, fieldName) {
        return $("#" + rowId + "_" + fieldName);
    }
    function SetUser($obj, selectDropDownUser, TaskId, selectDropDownClient) {
        selectDropDownUser.empty();
        //$.get('/CustomerAccountTaskTracker/GetUsers', 'TaskId=' + TaskId + '', function (data) {
        //    selectDropDownUser.append($(data));
        //});

        $.ajax({
            url: "/CustomerAccountTaskTracker/GetUsers",
            data: 'TaskId=' + TaskId + '&CustomerAccountId=' + $('#CustomerAccountId').val() + '&ProjectId=' + $('#ProjectId').val(),
            success: function (dataOptions) {
                selectDropDownUser.append($(dataOptions));
            },
            cache: false
        });
        selectDropDownClient.empty();
        selectDropDownClient.append($('<option role="option" value="-1">Select One</option>'));
    }


    function SetClientStakeHolderName($obj, selectDropDownClient, CustomerAccountId,UserId,ProjectId) {
        selectDropDownClient.empty();
        
        $.ajax({
            url: "/CustomerAccountTaskTracker/GetClientStakeHolders",
            data: 'CustomerAccountId=' + CustomerAccountId + '&UserId='+UserId+'&ProjectId='+ProjectId+'',
            success: function (dataOptions) {
                selectDropDownClient.append($(dataOptions));
            },
            cache: false
        });

    }
    $('#add_grid').hide();
    $('#del_grid').hide();
    $('#grid_iledit').hide();
    $('#grid_ilsave').hide();
    $('#grid_ilcancel').hide();

    $('#filterButton').click(function (event) {
        if ((UserRoleId == '9') || (UserRoleId == '3')){
            $('#add_grid').show();
            $('#del_grid').show();
            $('#grid_iledit').show();
            $('#grid_ilsave').show();
            $('#grid_ilcancel').show();
        }
        else {
            $('#add_grid').hide();
            $('#del_grid').hide();
            $('#grid_iledit').show();
            $('#grid_ilsave').show();
            $('#grid_ilcancel').show();
        }
        //event.preventDefault();
        filterGrid();
    });

});

function filterGrid() {
    var postDataValues = $("#grid").jqGrid('getGridParam', 'postData');
    $('.filterItem').each(function (index, item) {
        postDataValues[$(item).attr('id')] = $(item).val();
    });
    $("#grid").jqGrid().setGridParam({ postData: postDataValues, page: 1 }).trigger('reloadGrid');
}

