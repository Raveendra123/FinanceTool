$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/ProjectUserMapping/GetProjectUserMappings",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ProjectUserMappingId', 'Project Name', 'Project Name', 'User Name', 'User Name', 'Sap ID', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'ProjectUserMappingId', index: 'ProjectUserMappingId', editable: true },
            { key: false, name: 'ProjectName', index: 'ProjectName', editable: true },
            {
                key: false, hidden: true, name: 'ProjectId', index: 'ProjectId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/ProjectUserMapping/GetProjects?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'UserName', index: 'UserName', editable: true },
            {
                key: false, hidden: true, name: 'UserId', index: 'UserId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/ProjectUserMapping/GetUsers?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'SapID', index: 'SapID', editable: true },
            { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Project User Mapping',
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
            url: '/ProjectUserMapping/Edit',
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
                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).show();
                $('#tr_SapID', form).hide();
                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/ProjectUserMapping/Create",
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
                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).show();
                $('#tr_SapID', form).hide();
                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/ProjectUserMapping/Delete",
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

