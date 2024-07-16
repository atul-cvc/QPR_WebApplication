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
