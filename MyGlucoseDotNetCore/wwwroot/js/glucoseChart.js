$(document).ready(function () {

    google.charts.load('current', { 'packages': ['line'] });
    google.charts.setOnLoadCallback(drawChart);

    $(document).on('change', ".fromDate", function (incomingDate) {
        //$url += "&fromDate=" + $(".fromDate").val();
        google.charts.load('current', { 'packages': ['line'] });
        google.charts.setOnLoadCallback(drawChart);
    });

    $(document).on('change', ".toDate", function (incomingDate) {
        //alert($(".toDate").val());
        //$url += "&toDate=" + $(".toDate").val();
        google.charts.load('current', { 'packages': ['line'] });
        google.charts.setOnLoadCallback(drawChart);
    });

    function drawChart() {
        $url = "/API/ChartApi/GetUserGlucoseChart?UserName=" + $.urlParam("UserName");
        if ($(".fromDate").val() !== null && $(".fromDate").val() !== "")
            $url += "&fromDate=" + $(".fromDate").val();
        if ($(".toDate").val() !== null && $(".toDate").val() !== "")
            $url += "&toDate=" + $(".toDate").val();
        //if ($.urlParam("UserName") === null)
        //    $url = "/API/ChartApi/GetGlucoseChart";
        //else
        //    $url = "/API/ChartApi/GetUserGlucoseChart?UserName=" + $.urlParam("UserName");

        console.log("Sending request: [href: " + $url + "]" + ";");

        $.ajax({
            url: $url,
            method: 'get',
            beforeSend: function () {
                //$link.addClass('overlay');
            },
            success: function (response) {
                console.log(response);

                if (response.glucoseEntries !== null && response.glucoseEntries.length > 0) {

                    var glucoseArray = [["Date", "Reading"]];
                    $.each(response.glucoseEntries, function () {
                        var glucoseItem = [this.updatedAt, this.measurement];
                        glucoseArray.push(glucoseItem);
                    });

                    var gData = google.visualization.arrayToDataTable(glucoseArray);
                    var data = new google.visualization.DataTable();
                    gData.addColumn('date', 'Day');
                    gData.addColumn('number', 'Glucose Reading');

                    var options = {
                        chart: {
                            title: 'Glucose Readings Over Time'//,
                            //subtitle: 'in millions of dollars (USD)'
                        },
                        width: 900,
                        height: 500
                    };
                    /////This is the google API implementation
                    var chart = new google.charts.Line(document.getElementById('linechart_material'));


                    if (glucoseArray.length > 0)
                        chart.draw(gData, google.charts.Line.convertOptions(options));

                } // if
                else
                    $('#linechart_material').text('There are no glucose entries for this user.');

            },
            error: function (response) {
                console.log(response);
                $('#linechart_material').text('There was an error retrieving the entries.');
                //$link.removeClass('overlay');
            }
        });

    }; // drawChart


    // From: https://stackoverflow.com/a/25359264
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        return decodeURI(results[1]) || 0;
    }; // urlParam

}); // document onLoad
