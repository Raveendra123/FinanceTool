$(document).ready(function () {

});

$(function () {
    $("#grid").jqGrid({
        url: "/Task/GetTasks",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['TaskId', 'Task Code', 'Task Name', 'Task Type', 'Task Type', 'Task Frequency', 'Task Frequency','Task Color', 'IsActive'],
        colModel: [
            { key: true, hidden: true, name: 'TaskId', index: 'TaskId', editable: true },
            { key: false, name: 'TaskCode', index: 'TaskCode', editable: true, width: 300 },
            { key: false, name: 'TaskName', index: 'TaskName', editable: true, width: 400    },
            { key: false, name: 'TaskTypeName', index: 'TaskTypeName', editable: true },
            {
                key: false, hidden: true, name: 'TaskTypeId', index: 'TaskTypeId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/Task/GetTaskTypes?"+new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },

            { key: false, name: 'Frequency', index: 'Frequency', editable: true },
            {
                key: false, hidden: true, name: 'TaskFrequencyId', index: 'TaskFrequencyId', editable: true, edittype: "select",
                editoptions: {
                    aysnc: false, dataUrl: "/Task/GetFrequncyUnits?" + new Date(), formatter: 'select',
                    buildSelect: function (response) {
                        return response;
                    }
                },
            },
            { key: false, name: 'TaskColor', index: 'TaskColor', editable: true },
            { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'select', editoptions: { value: { 'true': 'true', 'false': 'false' } } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [5, 10],
        height: '100%',
        viewrecords: true,
        caption: 'Task List',
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
            url: '/Task/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            width: 500,
            recreateForm: true,
            beforeSubmit: function (postdata, formid) {
                if (postdata["TaskCode"] == '') {
                    return [false, 'Please Enter the Task Code'];
                }
                if (postdata["TaskName"] == '') {
                    return [false, 'Please Enter the Task Name'];
                }
                if (postdata["TaskColor"] == '') {
                    return [false, 'Please Enter the Task Color'];
                }
                return [true, ''];
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            },
            beforeShowForm: function (form) {
                $('#tr_TaskTypeName', form).hide();
                $('#tr_TaskTypeId', form).show();
                $('#tr_Frequency', form).hide();
                $('#tr_TaskFrequencyId', form).show();
            },
        },
        {
            // add options
            zIndex: 100,
            url: "/Task/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            width: 500,
            beforeSubmit: function (postdata, formid) {
                if (postdata["TaskCode"] == '') {
                    return [false, 'Please Enter the Task Code'];
                }
                if (postdata["TaskName"] == '') {
                    return [false, 'Please Enter the Task Name'];
                }
                if (postdata["TaskColor"] == '') {
                    return [false, 'Please Enter the Task Color'];
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
                $('#tr_TaskTypeName', form).hide();
                $('#tr_TaskTypeId', form).show();
                $('#tr_Frequency', form).hide();
                $('#tr_TaskFrequencyId', form).show();
            },
        },
        {
            // delete options
            zIndex: 100,
            width: 275,
            url: "/Task/Delete",
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

