$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/Project/GetProjects",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ProjectId', 'Project Code', 'Project Name', 'Project Type', 'ProjectType', 'Delivery Unit', 'Delivery Unit',  'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'ProjectId', index: 'ProjectId', editable: true },
            { key: false, name: 'ProjectCode', index: 'ProjectCode', editable: true },
            { key: false, name: 'ProjectName', index: 'ProjectName', editable: true },
            { key: false, name: 'ProjectTypeName', index: 'ProjectTypeName', editable: true },
            {
                key: false, hidden: true, name: 'ProjectTypeId', index: 'ProjectTypeId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/Project/GetProjectTypes?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'DeliveryUnitName', index: 'DeliveryUnitName', editable: true },
            {
                key: false, hidden: true, name: 'DeliveryUnitId', index: 'DeliveryUnitId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/Project/GetDeliveryUnits?" + new Date(), formatter: 'select',
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
        caption: 'Project List',
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
            url: '/Project/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["ProjectCode"] == '') {
                    return [false, 'Please Enter the Project Code'];
                }
                if (postdata["ProjectName"] == '') {
                    return [false, 'Please Enter the Project Name'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_ProjectTypeName', form).hide();
                $('#tr_ProjectTypeId', form).show();
                $('#tr_DeliveryUnitName', form).hide();
                $('#tr_DeliveryUnitId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/Project/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["ProjectCode"] == '') {
                    return [false, 'Please Enter the Project Code'];
                }
                if (postdata["ProjectName"] == '') {
                    return [false, 'Please Enter the Project Name'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form)
            {
                $('#tr_ProjectTypeName', form).hide();
                $('#tr_ProjectTypeId', form).show();
                $('#tr_DeliveryUnitName', form).hide();
                $('#tr_DeliveryUnitId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width:275,
            url: "/Project/Delete",
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

