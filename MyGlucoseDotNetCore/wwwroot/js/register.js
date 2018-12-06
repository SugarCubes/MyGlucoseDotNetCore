$(document).ready(function () {

    $('#doctorList').hide();
    //$('#Role').
    //$(document).on('checked', ".Doctor", function (value) {
    $('input[type=radio][name=Role]').change(function () {
        console.log("Object Value: " + this.value);
        if (this.value === "Patient") {
            $('#doctorList').show();
            $('#degreeAbreviation').hide();
        } else if (this.value === "Doctor") {
            $('#doctorList').hide();
            $('#degreeAbreviation').show();
        }
    });

});