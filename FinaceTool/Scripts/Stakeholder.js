$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/Stakeholder/GetStakeholder",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['StakeholderId', 'Customer Account', 'Customer Account', 'Customer Name','Customer Email', 'Role', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'StakeholderId', index: 'StakeholderId', editable: true },
            { key: false, name: 'CustomerAccountName', index: 'CustomerAccountName', editable: true },
            {
                key: false, hidden: true, name: 'CustomerAccountId', index: 'CustomerAccountId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/Stakeholder/GetCustomerAccounts?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'CustomerName', index: 'CustomerName', editable: true },
            { key: false, name: 'CustomerEmail', index: 'CustomerEmail', editable: true },
            { key: false, name: 'Role', index: 'Role', editable: true },
            { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Delivery Unit List',
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
            url: '/Stakeholder/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerName"] == '') {
                    return [false, 'Please Enter the Customer Name.'];
                }
                if (postdata["CustomerEmail"] != '') {
                    if (!ValidateEmail(postdata["CustomerEmail"])) {
                        return [false, 'Please Enter the Email in valid format'];
                    }
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/Stakeholder/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerName"] == '') {
                    return [false, 'Please Enter the Customer Name.'];
                }
                if (postdata["CustomerEmail"] != '') {
                    if (!ValidateEmail(postdata["CustomerEmail"])) {
                        return [false, 'Please Enter the Email in valid format'];
                    }
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
                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/Stakeholder/Delete",
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


function ValidateEmail(email) {

    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

    return expr.test(email);

};