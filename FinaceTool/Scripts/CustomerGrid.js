$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/CustomerAccount/GetCustomers",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['CustomerAccountId', 'Customer Account Code', 'Customer Account Name', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'CustomerAccountId', index: 'CustomerAccountId', editable: true },
            { key: false, name: 'CustomerAccountCode', index: 'CustomerAccountCode', editable: true },
            { key: false, name: 'CustomerAccountName', index: 'CustomerAccountName', editable: true },
            { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Customer Account List',
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
        loadError: function () {
           alert("Interal Server Error while loading the page. Please contact system administrator.")
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: false },
        {
            // edit options
            zIndex: 100,
            url: '/CustomerAccount/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerAccountCode"] == '') {
                    return [false, 'Please Enter the Customer Account Code'];
                }
                if (postdata["CustomerAccountName"] == '') {
                    return [false, 'Please Enter the Customer Account Name'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/CustomerAccount/Create",
            mtype: 'Post',
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerAccountCode"] == '') {
                    return [false, 'Please Enter the Customer Account Code'];
                }
                if (postdata["CustomerAccountName"] == '') {
                    return [false, 'Please Enter the Customer Account Name'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/CustomerAccount/Delete",
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

