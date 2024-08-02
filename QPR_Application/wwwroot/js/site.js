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

    $(".add-row-advice_cvc").click(function () {
        cnt = parseInt('0');
        //alert(cnt);
        var name_of_officer = $("#name_of_officer_advice_cvc").val();
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
        for (var i = 0; i < parseInt(name_of_officer); i++) {

            markup = "<tr class='text-center'><td style='width:1%'>" + (cnt + i) + "</td>";
            markup = markup + "<td style='width:14%'><b><select class='form-control text-center' name='cvcAdviceDto[" + (cnt + i) + "].stageType' id='cvcAdviceOption[" + (cnt + i) + "]' onchange='loadOptions(" + (cnt + i) + ")'><option value='0'>Select</option><option value='First Stage'>First Stage</option><option value='Second Stage'>Second Stage</option></select></b></td>";
            markup = markup + "<td style='width:20%'><select class='form-control text-center' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_typecvcadvice' id='cvcAdviceType[" + (cnt + i) + "]'><option value='0'>Select</option><option value='Prosecution'>Prosecution</option><option value='Major PP'>Major PP</option><option value='Minor PP'>Minor PP</option><option value='Administrative Action'>Administrative Action</option><option value='Exoneration'>Exoneration</option></select></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_cvcfilenumber' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_dept_ref_number' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_name_designation' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_actiontaken_da' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' name='cvcAdviceDto[" + (cnt + i) + "].deviation_cvc_advice_firststage_actiontaken_aa' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<input type='hidden'  name='cvcAdviceDto[" + (cnt + i) + "].pend_id' ><a href='javascript:void(0)'  onclick='return removeDisplayFunction(this,0);' ><button class='btn btn-outline-danger'>Delete</button></a></td></tr>";
            $("#officerNumber tbody").append(markup);

        }
        resetSerialNumber();
    });

    $(".addauthority").click(function () {
        //debugger;

        addauthority_cnt = parseInt('0');
        var authorityMajor = $("#authorityMajor").val();
        var authorityRows = parseInt($("#authorityRows").val());

        if (authorityRows == 0) {
            $("#authorityRows").val(authorityMajor);
        }
        else if (authorityRows < authorityMajor) {
            $("#authorityRows").val(authorityMajor);
            authorityMajor = parseInt(authorityMajor) - parseInt(authorityRows)
        } else {
            alert("Total number of officer can not be less than already selected number of officers");
            return;
        }

        addauthority_cntCall++;
        var markup = "";
        for (var i = 0; i < parseInt(authorityMajor); i++) {
            markup = "<tr class='text-center'><td style='width:1%'>" + (addauthority_cnt + i) + "</td>";
            markup = markup + "<td style='width:14%'><b><select class='form-control' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].stageType' id='cvcAdviceOption1[" + (addauthority_cnt + i) + "]' onchange='loadOptions1(" + (addauthority_cnt + i) + ")'><option value='0'>Select</option><option value='First Stage'>First Stage</option><option value='Second Stage'>Second Stage</option></select></b></td>";
            markup = markup + "<td style='width:8%'><select class='form-control' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_typecvcadvice' id='cvcAdviceType1[" + (addauthority_cnt + i) + "]'><option value='0'>Select</option><option value='Prosecution'>Prosecution</option><option value='Major PP'>Major PP</option><option value='Minor PP'>Minor PP</option><option value='Administrative Action'>Administrative Action</option><option value='Exoneration'>Exoneration</option></select></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  type='text' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_cvcfilenumber' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  type='text' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_dept_ref_number' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_name_designation' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control'  type='text' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_actiontaken_da' /></b></td>";
            markup = markup + "<td style='width:14%'><b><input path='' class='form-control' type='text' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].appellate_deviation_firststage_actiontaken_aa' /></b></td>";
            markup = markup + "<td style='width:1%'>" + "<input type='hidden' name='appellateAuthorityDto[" + (addauthority_cnt + i) + "].pend_id' ><a href='javascript:void(0)'  onclick='return removeDisplayFunctionMajor(this,0);' class='delete-row btn btn-outline-danger'>Delete</a></td></tr>";
            $("#officerauthority tbody").append(markup);

        }
        resetSerialNumberMajor();
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