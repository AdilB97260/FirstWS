
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function isDecimalKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 45 || charCode > 57))
        return false;

    return true;
}

function CheckDelete() {

    if ($("#dataTables-example tr td [type='checkbox']:checked").length > 0) {
        jConfirm('Are you sure you want to delete?', 'Confirmation', function (r) {
            if (r)
                javascript: __doPostBack('ctl00$main$lbtnDelete', '')
            else
                return false;
        });
        return false;
    }
    else {
        jImportant('Please select atleast one recored to delete.');
        return false;
    }
}

$(function () {

    $("#chkHeader").change(function () {

        if ($(this).attr("checked"))
            $("#dataTables-example tr td [type='checkbox']").attr("checked", "checked");
        else
            $("#dataTables-example tr td [type='checkbox']").prop('checked', this.checked);

    });
    var chk_list = null;
    var sel_chk_list = null;

    $("#dataTables-example tr td [type='checkbox']").change(function () {
        chk_list = $("#dataTables-example tr td [type='checkbox']");
        sel_chk_list = $("#dataTables-example tr td [type='checkbox']:checked");

        if (chk_list.length == sel_chk_list.length) {
            document.getElementById("chkHeader").checked = true;
        } else {
            document.getElementById("chkHeader").checked = false
        }

    });
});


