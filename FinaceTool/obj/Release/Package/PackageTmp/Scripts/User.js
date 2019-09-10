$(document).ready(function () {
    
});

$(function () {
    $("#grid").jqGrid({
        url: "/User/GetUsers",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['UserId', 'Sap ID', 'User Name', 'Password', 'Email', 'User Role', 'UserRole', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'UserId', index: 'UserId', editable: true },
            { key: false, name: 'SapID', index: 'SapID', editable: true },
            { key: false, name: 'UserName', index: 'UserName', editable: true },
            { key: false, name: 'Password', index: 'Password', editable: true, hidden: true},
            { key: false, name: 'Email', index: 'Email', editable: true },
            { key: false, name: 'UserRoleName', index: 'UserRoleName', editable: true },
            {
                key: false, hidden: true, name: 'UserRoleId', index: 'UserRoleId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/User/GetMasterUserRoles?"+new Date(), formatter: 'select',
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
        caption: 'User List',
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
            url: '/User/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["SapID"] == '') {
                    return [false, 'Please Enter the SAP ID'];
                }
                if (postdata["UserName"] == '') {
                    return [false, 'Please Enter the User Name'];
                }
                if (postdata["Password"] == '') {
                    return [false, 'Please Enter the Password'];
                }
                if (postdata["Email"] == '') {
                    return [false, 'Please Enter the Email'];
                }

                if (!ValidateEmail(postdata["Email"])) {
                    return [false, 'Please Enter the Email in valid format'];
                }

                if (postdata["UserRole"] == '') {
                    return [false, 'Please Enter the UserRole'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) { $('#tr_Password', form).hide(); $('#tr_UserRoleName', form).hide(); $('#tr_UserRoleId', form).show(); },
        },
        {
            // add options
            zIndex: 100,
            url: "/User/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["SapID"] == '') {
                    return [false, 'Please Enter the SAP ID'];
                }
                if (postdata["UserName"] == '') {
                    return [false, 'Please Enter the User Name'];
                }
                if (postdata["Email"] == '') {
                    return [false, 'Please Enter the Email'];
                }
                if (!ValidateEmail(postdata["Email"])) {
                    return [false, 'Please Enter the Email in valid format'];
                }
                if (postdata["UserRole"] == '') {
                    return [false, 'Please Enter the UserRole'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_Password', form).hide();
                $('#tr_UserRoleName', form).hide(); $('#tr_UserRoleId', form).show();
                //document.getElementsByName("Password")[0].type = "password";
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/User/Delete",
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