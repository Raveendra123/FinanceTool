$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/TaskUserRoleMapping/GetTaskUserRoleMappings",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['TaskUserRoleMappingId', 'Task Name', 'Task Name', 'User Role', 'User Role', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'TaskUserRoleMappingId', index: 'TaskUserRoleMappingId', editable: true },
            { key: false, name: 'TaskName', index: 'TaskName', editable: true },
            {
                key: false, hidden: true, name: 'TaskId', index: 'TaskId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/TaskUserRoleMapping/GetTasks?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'UserRoleName', index: 'UserRoleName', editable: true },
            {
                key: false, hidden: true, name: 'UserRoleId', index: 'UserRoleId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/TaskUserRoleMapping/GetUserRoles?" + new Date(), formatter: 'select',
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
        caption: 'Task User Role Mapping',
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
            url: '/TaskUserRoleMapping/Edit',
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
                $('#tr_TaskName', form).hide();
                $('#tr_TaskId', form).show();
                $('#tr_UserRoleName', form).hide();
                $('#tr_UserRoleId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/TaskUserRoleMapping/Create",
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
                $('#tr_TaskName', form).hide();
                $('#tr_TaskId', form).show();
                $('#tr_UserRoleName', form).hide();
                $('#tr_UserRoleId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/TaskUserRoleMapping/Delete",
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

