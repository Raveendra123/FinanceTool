$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/ResourceCustomerMapping/GetResourceCustomerMappings",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ResourceCustomerMappingId','StakeholderId', 'Customer Account Name', 'Customer Account Name', 'User Name', 'User Name', 'Sap ID', 'Project Name', 'Project Name', 'Customer Name', 'Customer Name', 'IsActive'],
        colModel: [
            { key: false, hidden: true, name: 'StakeholderId', index: 'StakeholderId' },
            { key: true, hidden: true, name: 'ResourceCustomerMappingId', index: 'CustomerAccountCustomerNameMapping', editable: true },
            { key: false, name: 'CustomerAccountName', index: 'CustomerAccountName', editable: true },
            {
                key: false, hidden: true, name: 'CustomerAccountId', index: 'CustomerAccountId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/ResourceCustomerMapping/GetCustomerAccounts?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'UserName', index: 'UserName', editable: true },
            {
                key: false, hidden: true, name: 'UserId', index: 'UserId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/ResourceCustomerMapping/GetUsers?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'SapID', index: 'SapID', editable: true },
            { key: false, name: 'ProjectName', index: 'ProjectName', editable: true },
            {
                key: false, hidden: true, name: 'ProjectId', index: 'ProjectId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/ResourceCustomerMapping/GetProjects?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'CustomerName', index: 'CustomerName', editable: true },
            {
                key: false, hidden: true, name: 'CustomerNameId', index: 'CustomerNameId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, formatter: 'select',
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
        caption: 'Resource Customer Mapping',
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
            url: '/ResourceCustomerMapping/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerAccountId"] == '-1') {
                    return [false, 'Please Enter the Customer Account Code'];
                }
                if (postdata["CustomerNameId"] == '-1') {
                    return [false, 'Please Enter the Customer Name'];
                }
                return [true, ''];
            },
            afterShowForm: function (form) {
                var sel_id = $('#grid').jqGrid('getGridParam', 'selrow');
                var data = $('#grid').getRowData(sel_id);
                var selectDropDownCustomerAccount = $('#tr_CustomerNameId', form).find('td.DataTD').find('select');
                SetCustomerName($(this), selectDropDownCustomerAccount, data.CustomerAccountId);
                selectDropDownCustomerAccount.val(data.StakeholderId);
            },
            onclickSubmit: function (params, postdata) {
                postdata.CustomerNameId = $('#tr_CustomerNameId').find('td.DataTD').find('select').val();
            },
            beforeShowForm: function (form) {
                var selectDropDownCustomerAccount = $('#tr_CustomerAccountId', form).find('td.DataTD').find('select');
                var selectDropDownCustomerName = $('#tr_CustomerNameId', form).find('td.DataTD').find('select');
                selectDropDownCustomerName.empty();
                selectDropDownCustomerName.append($('<option role="option" value="-1">Select One</option>'));
                selectDropDownCustomerAccount.unbind('change');
                selectDropDownCustomerAccount.bind('change', function () {
                    SetCustomerName($(this), selectDropDownCustomerName, selectDropDownCustomerAccount.val());
                });

                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).show();
                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).show();
                $('#tr_SapID', form).hide();
                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
                $('#tr_CustomerName', form).hide();
                $('#tr_CustomerNameId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/ResourceCustomerMapping/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeSubmit: function (postdata, formid) {
                if (postdata["CustomerAccountId"] == '-1') {
                    return [false, 'Please Enter the Customer Account Code'];
                }
                if (postdata["CustomerNameId"] == '-1') {
                    return [false, 'Please Enter the Customer Name'];
                }
                return [true, ''];
            },
            onclickSubmit: function (params, postdata) {
                postdata.CustomerNameId = $('#tr_CustomerNameId').find('td.DataTD').find('select').val();
            },
            beforeShowForm: function (form)
            {
                var selectDropDownCustomerAccount = $('#tr_CustomerAccountId', form).find('td.DataTD').find('select');
                var selectDropDownCustomerName = $('#tr_CustomerNameId', form).find('td.DataTD').find('select');
                selectDropDownCustomerName.empty();
                selectDropDownCustomerName.append($('<option role="option" value="-1">Select One</option>'));
                selectDropDownCustomerAccount.unbind('change');
                selectDropDownCustomerAccount.bind('change', function () {
                    SetCustomerName($(this), selectDropDownCustomerName, selectDropDownCustomerAccount.val());
                });

                $('#tr_CustomerAccountName', form).hide();
                $('#tr_CustomerAccountId', form).show();
                $('#tr_ProjectName', form).hide();
                $('#tr_ProjectId', form).show();
                $('#tr_SapID', form).hide();
                $('#tr_UserName', form).hide();
                $('#tr_UserId', form).show();
                $('#tr_CustomerName', form).hide();
                $('#tr_CustomerNameId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/ResourceCustomerMapping/Delete",
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


    function SetCustomerName($obj, selectDropDownCustomerName, CustomerAccountId) {
        
        selectDropDownCustomerName.empty();

        
        $.ajax({
            url: "/ResourceCustomerMapping/GetCustomerName",
            async:false,
            data: 'CustomerAccountId=' + CustomerAccountId + '',
            success: function (dataOptions) {
                selectDropDownCustomerName.append($(dataOptions));
            },
            cache: false
        });
    }

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

