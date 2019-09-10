$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/CustomerTemplate/GetCustomerTemplate",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['CustomerTemplateId', 'CustomerAccountName', 'CustomerAccountId', 'TemplateName', 'TemplateLevelId', 'ProjectName', 'ProjectId', 'TaskName', 'TaskId', 'TaskType1', 'TaskTypeId', 'UserName', 'UserId', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'CustomerTemplateId', index: 'CustomerTemplateId', editable: true },
            { key: false, name: 'CustomerAccountName', index: 'CustomerAccountName', editable: true },
            {
                key: false, hidden: true, name: 'CustomerAccountId', index: 'CustomerAccountId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetCustomerAccounts?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
             { key: false, name: 'TemplateName', index: 'TemplateName', editable: true },
            {
                key: false, hidden: true, name: 'TemplateLevelId', index: 'TemplateLevelId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetTemplateLevels?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'ProjectName', index: 'ProjectName', editable: true },
            {
                key: false, hidden: true, name: 'ProjectId', index: 'ProjectId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetProjects?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

             { key: false, name: 'TaskName', index: 'TaskName', editable: true },
            {
                key: false, hidden: true, name: 'TaskId', index: 'TaskId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetTasks?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'TaskType1', index: 'TaskType1', editable: true },
            {
                key: false, hidden: true, name: 'TaskTypeId', index: 'TaskTypeId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetTaskTypes?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'UserName', index: 'UserName', editable: true },
            {
                key: false, hidden: true, name: 'UserId', index: 'UserId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/CustomerTemplate/GetUsers?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Customer Template Mapping',
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
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: false },
        {
            // edit options
            zIndex: 100,
            url: '/CustomerTemplate/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).hide();

                $('#tr_TemplateName', form).hide();
                $('#tr_TemplateLevelId', form).hide();

                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).hide();


                $('#tr_TaskName', form).hide();
                $('#tr_TaskId', form).hide();

                $('#tr_TaskType1', form).hide();
                $('#tr_TaskTypeId', form).show();

                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).hide();

            },
        },
        {
            // add options
            zIndex: 100,
            url: "/CustomerTemplate/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form)
            {
                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).show();
                $('#tr_TemplateName', form).hide();
                $('#tr_TemplateLevelId', form).show();
                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).show();
                $('#tr_TaskName', form).hide();
                $('#tr_TaskId', form).show();
                $('#tr_TaskType1', form).hide();
                $('#tr_TaskTypeId', form).show();
                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/CustomerTemplate/Delete",
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

    $('#filterButton').click(function (event) {
        event.preventDefault();
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

