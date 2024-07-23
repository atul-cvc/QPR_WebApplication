// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//put code here to execute on page ready
$(function () {
    //Datatable
    $('#example1').DataTable();

    //Datepicker
    $('.datepicker').datepicker(
        {
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
var cntCall = 0;
$(function () {
    var morethanthree = parseInt($("#proseSanctTotalThreetoSix").val()) + parseInt($("#proseSanctTotallessSix").val());
    $("#name_of_officer").val(morethanthree);

    $(".add-row").click(function () {
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
            markup = markup + "<td style='width:15%'><b><input path='' class='form-control' name='ageWisePendingDto[" + i + "].prosePendingNameDesig' /></b></td>";
            markup = markup + "<td style='width:13%'><b><input path='' class='form-control datepicker' type='date' name='ageWisePendingDto[" + i + "].prosePendingDateRecommend' /></b></td>";
            markup = markup + "<td style='width:13%'><b><input path='' class='form-control datepicker'  type='date' name='ageWisePendingDto[" + i + "].prosePendingDateReceipt' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='ageWisePendingDto[" + i + "].prosePendingSanctionPC' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='ageWisePendingDto[" + i + "].prosePendingStatusRequest' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='ageWisePendingDto[" + i + "].prosePendingNameAuthority' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<input type='hidden' name='ageWisePendingDto[" + i + "].pend_id' ><a href='javascript:void(0)'  onclick='return removeDisplayFunction(this,0);' class='delete-row'><button class='btn btn-outline-danger'>Delete</button></td></tr>";

            $("#officerNumber tbody").append(markup);


        }
        resetSerialNumber();
    });

});

function resetSerialNumber() {
    var sno = 1;
    $("#officerNumber tbody tr").each(function (i) {
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
