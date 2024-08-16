// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//DO NOT PUT CODE IN HERE OTHER THAN DATATABLE AND DATEPICKER
$(function () {

    //Datatable
    $('#example1').DataTable();

    //Datepicker
    $('.datepicker').datepicker({
        //    buttonText: "Select date",
        //    showOn: "Button",
        dateFormat: "dd/mm/yy",
        shortYearCutoff: 1,
        changeYear: true,
        changeMonth: true,
        //showWeek: false,
        showOtherMonths: true,
        selectOtherMonths: false,
        minDate: '-100Y',
        maxDate: '+100Y',
    });
});



function isNumberKey(evt) {

    evt = evt || window.event; // Handle IE compatibility
    var charCode = (typeof evt.which === "undefined") ? evt.keyCode : evt.which;
    if (charCode >= 48 && charCode <= 57) {
        return true;
    }
    return false;
}

var cnt = 1;
var cnt1 = 1;
var cntCall = 0;
var cntCall1 = 0;
var addauthority_cnt = 1;
var addauthority_cntCall = 0;
$(function () {

    $(".add-row-dept-proc").click(function () {
        var name_of_officer = $("#name_of_officer").val();
        if (name_of_officer)
            var totRows = parseInt($("#totRows").val());
        var morethanthree = parseInt($("#proseSanctTotalThreetoSix").val()) + parseInt($("#proseSanctTotallessSix").val());
        $("#name_of_officer").val(morethanthree);
        //debugger;
        if (totRows == 0) {
            $("#totRows").val(name_of_officer);
        }
        else if (totRows < name_of_officer) {
            $("#totRows").val(name_of_officer);
            name_of_officer = parseInt(name_of_officer) - parseInt(totRows)
        } else {
            alert("Total number of officer can not be less than already selected number of officers");
            return;
        }

        cntCall++;
        var markup = "";
        for (var i = 0; i < parseInt(name_of_officer); i++) {

            markup = "<tr class='text-center'><td style='width:1%'>" + (cnt++) + "</td>";
            markup = markup + "<td style='width:15%'><b><input path='' class='form-control'  type='text' name='ageWisePendingDto[" + i + "].prosePendingCBIFIRNo' /></b></td>";
            markup = markup + "<td style='width:15%'><b><input path='' class='form-control datepicker' type='date' name='ageWisePendingDto[" + i + "].prosePendingNameDesig' /></b></td>";
            markup = markup + "<td style='width:13%'><b><input path='' class='form-control datepicker' type='date' name='ageWisePendingDto[" + i + "].prosePendingDateRecommend' /></b></td>";
            markup = markup + "<td style='width:13%'><b><input path='' class='form-control datepicker'  type='date' name='ageWisePendingDto[" + i + "].prosePendingDateReceipt' /></b></td>";
            //markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='ageWisePendingDto[" + i + "].prosePendingSanctionPC' /></b></td>";
            //markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='ageWisePendingDto[" + i + "].prosePendingStatusRequest' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='ageWisePendingDto[" + i + "].prosePendingNameAuthority' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<input type='hidden' name='ageWisePendingDto[" + i + "].pend_id' ><a href='javascript:void(0)'  onclick='return removeDisplayFunction(this,0);' class='delete-row'><button class='btn btn-outline-danger'>Delete</button></td></tr>";

            $("#officerNumber tbody").append(markup);
        }
        resetSerialNumber();
    });

    $(".add-row-status-pendency").click(function () {
        //debugger;
        cnt = parseInt('0');
        //alert(cnt);
        var name_of_officer = $("#name_of_officer").val();
        var totRows = parseInt($("#totRows").val());

        if (totRows == 0) {
            $("#totRows").val(name_of_officer);
        }
        else if (totRows < name_of_officer) {
            $("#totRows").val(name_of_officer);
            name_of_officer = parseInt(name_of_officer) - parseInt(totRows)
        } else {
            alert("Total number of officer can not be less than already selected number of officers");
            return;
        }

        cntCall++;
        var markup = "";
        //alert(name_of_officer);
        for (var i = 0; i < parseInt(name_of_officer); i++) {

            markup = "<tr class='text-center'><td style='width:1%'>" + (cnt + i) + "</td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  name='fiComplaintDto[" + (cnt) + "].createdDate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  name='fiComplaintDto[" + (cnt) + "].fileNo' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path=''  class='form-control datepicker' type='date' name='fiComplaintDto[" + (cnt) + "].complaindate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='fiComplaintDto[" + (cnt) + "].boirackdate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='fiComplaintDto[" + (cnt) + "].decision' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='fiComplaintDto[" + (cnt) + "].boDecision' value='FI'/></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control datepicker'  type='date' name='fiComplaintDto[" + (cnt) + "].boDecisionDate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  type='text' name='fiComplaintDto[" + (cnt) + "].allegationDeatils' /><input path='' style='padding: 2px; width: 10vw'  type='hidden' name='fiComplaintDto[" + (cnt) + "].status' value='0' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<a href='javascript:void(0)'  onclick='return removeDisplayFunctionStatusPendency(this,0);' class='delete-row'><button class='btn btn-outline-danger'>Delete</button></td></tr>";
            $("#officerNumber-status-pendency tbody").append(markup);



        }
        resetSerialNumberStatusPendency();
    });

    $(".add-row1").click(function () {
        //debugger;
        cnt1 = parseInt('2') - 1;
        //alert(cnt);
        var name_of_officer = $("#name_of_officer1").val();
        var totRows = parseInt($("#totRows1").val());

        if (totRows == 0) {
            $("#totRows1").val(name_of_officer);
        }
        else if (totRows < name_of_officer) {
            $("#totRows1").val(name_of_officer);
            name_of_officer = parseInt(name_of_officer) - parseInt(totRows)
        } else {
            alert("Total number of officer can not be less than already selected number of officers");
            return;
        }

        cntCall1++;
        var markup = "";
        //alert(name_of_officer);
        for (var i = 0; i < parseInt(name_of_officer); i++) {

            markup = "<tr><td style='width:1%'>" + (cnt1++) + "</td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='CaComplaintDto[" + (cnt1) + "].createdDate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='CaComplaintDto[" + (cnt1) + "].fileNo' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control datepicker'  type='date' name='CaComplaintDto[" + (cnt1) + "].complaindate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='CaComplaintDto[" + (cnt1) + "].boirackdate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='CaComplaintDto[" + (cnt1) + "].decision' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='CaComplaintDto[" + (cnt1) + "].boDecision' value='CA'/></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control datepicker'  type='date' name='CaComplaintDto[" + (cnt1) + "].boDecisionDate' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  type='text' name='CaComplaintDto[" + (cnt1) + "].allegationDeatils' /><input path='' style='padding: 2px; width: 10vw'  type='hidden' name='CaComplaintDto[" + (cnt1) + "].status' value='0' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<a href='javascript:void(0)'  onclick='return removeDisplayFunction(this,0);' class='delete-row btn btn-outline-danger'>Delete</a></td></tr>";
            $("#officerNumber1 tbody").append(markup);



        }
        resetSerialNumber1();
    });

    //$('input[type="number"]').on('blur', function () {
    //    //debugger;
    //    var value = $(this).val();
    //    //alert('value : ' + value)
    //    if (value < 0) {
    //        //$(this).val('0');
    //        //alert('Please check the value')
    //    }
    //    //else {
    //    //    $(this).val(Number(value).toString());
    //    //}
    //});

    //$('form input').keydown(function (e) {
    //    if (e.keyCode == 13) {
    //        e.preventDefault();
    //        return false;
    //    }
    //});
    $('form input').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    });

    $('input[type="number"]').on('keyup', function () {
        //debugger;
        var value = $(this).val();
        $(this).val(Number(value).toString());
    });
});

function resetSerialNumber() {
    var sno = 1;
    $("#officerNumber tbody tr").each(function (i) {
        // find the first td in the row
        $(this).find("td:first").text(sno++);

    });
}

function resetSerialNumber1() {
    var sno = 1;
    $("#officerNumber1 tbody tr").each(function (i) {
        // find the first td in the row
        $(this).find("td:first").text(sno++);

    });
}

function resetSerialNumberStatusPendency() {
    var sno = 1;
    $("#officerNumber-status-pendency tbody tr").each(function (i) {
        // find the first td in the row
        $(this).find("td:first").text(sno++);

    });
}

function removeDisplayFunction(btndel, pendId) {

    if (pendId != 0) {
        $.ajax({
            type: "POST",
            url: "/pro11/user/cvofficer/deleteRowAgeWise",
            data: {
                "pendId": pendId
            },
            success: function (data) {
                alert(data);
                console.log("SUCCESS: ", data);
            },
            error: function (e) {
                console.log("ERROR: ", e);

            },
            done: function (e) {
                console.log("DONE");
            }
        });
    }

    if (typeof (btndel) == "object") {
        $(btndel).closest("tr").remove();
        var totRows = parseInt($("#totRows").val());
        $("#totRows").val(totRows - 1);
        resetSerialNumber();
    } else {
        return false;
    }
}
function removeDisplayFunctionStatusPendency(btndel, pendId) {

    if (pendId != 0) {
        $.ajax({
            type: "POST",
            url: "/pro11/user/cvofficer/deleteRowAgeWise",
            data: {
                "pendId": pendId
            },
            success: function (data) {
                alert(data);
                console.log("SUCCESS: ", data);
            },
            error: function (e) {
                console.log("ERROR: ", e);

            },
            done: function (e) {
                console.log("DONE");
            }
        });
    }

    if (typeof (btndel) == "object") {
        $(btndel).closest("tr").remove();
        var totRows = parseInt($("#totRows").val());
        $("#totRows").val(totRows - 1);
        resetSerialNumberStatusPendency();
    } else {
        return false;
    }
}

function removeDisplayFunctionMajor(btndel, pendId) {

    if (pendId != 0) {
        $.ajax({
            type: "POST",
            url: "/pro11/user/cvofficer/deleteRowAppellateAuthority",
            data: {
                "pendId": pendId
            },
            success: function (data) {
                alert(data);
                console.log("SUCCESS: ", data);
            },
            error: function (e) {
                console.log("ERROR: ", e);

            },
            done: function (e) {
                console.log("DONE");
            }
        });
    }

    if (typeof (btndel) == "object") {
        $(btndel).closest("tr").remove();
        var authorityRows = parseInt($("#authorityRows").val());
        $("#authorityRows").val(authorityRows - 1);
        resetSerialNumberMajor();
    } else {
        return false;
    }
}

function resetSerialNumberMajor() {
    var sno = 1;
    $("#officerauthority tbody tr").each(function (i) {

        //find the first td in the row
        $(this).find("td:first").text(sno++);

    });
}

function pageReloadWithoutQueryString() {
    window.location.href = window.location.origin + window.location.pathname;
}

function validateFields() {
    // Get all input elements of type number
    const inputs = document.querySelectorAll('form input[type="number"]');
    // Check if any input has a value less than 0
    //debugger;
    for (let input of inputs) {
        if (parseFloat(input.value) < 0) {

            Swal.fire({
                // title: 'Are you sure?',
                text: "All fields must be greater than or equal to 0",
                icon: 'warning',
                // showCancelButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Okay',
            })

            return false;
        }
    }
    //debugger;
    return true;
}

function confirmFormSubmission(formName) {
    //debugger;
    event.preventDefault(); // Prevent the form from submitting immediately
    if (validateFields()) {
        Swal.fire({
            title: 'Are you sure?',
            text: "Data will be saved automatically. Do you want to continue?",
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                // User confirmed, submit the form
                document.getElementById(formName).submit();
            }
        });
    }
}

function confirmDel(url) {
    //alert(url);
    event.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        text: "Record will be deleted. Do you want to continue?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            // Redirect to the URL if user confirmed
            window.location.href = url;
        }
    });
}