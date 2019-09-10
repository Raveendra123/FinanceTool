$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/DeliveryUnit/GetDeliveryUnit",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['DeliveryUnitId', 'Delivery Unit Name', 'Super Delivery Unit', 'Super Delivery Unit', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'DeliveryUnitId', index: 'DeliveryUnitId', editable: true },
            { key: false, name: 'DeliveryUnitName', index: 'DeliveryUnitName', editable: true },
            { key: false, name: 'SuperDeliveryUnitName', index: 'SuperDeliveryUnitName', editable: true },
            {
                key: false, hidden: true, name: 'SuperDeliveryUnitId', index: 'SuperDeliveryUnitId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/DeliveryUnit/GetSuperDeliveryUnits?" + new Date(), formatter: 'select',
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
            url: '/DeliveryUnit/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["DeliveryUnitName"] == '') {
                    return [false, 'Please Enter the Delivery Unit Name'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_SuperDeliveryUnitName', form).hide();
                $('#tr_SuperDeliveryUnitId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/DeliveryUnit/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["DeliveryUnitName"] == '') {
                    return [false, 'Please Enter the Delivery Unit Name'];
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
                $('#tr_SuperDeliveryUnitName', form).hide();
                $('#tr_SuperDeliveryUnitId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/DeliveryUnit/Delete",
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

