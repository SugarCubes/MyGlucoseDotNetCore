$(document).ready(function () {

    // From: https://stackoverflow.com/a/25359264
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        return decodeURI(results[1]) || 0;
    }; // urlParam

    // Load the chart when the document loads:
    google.charts.load('current', { 'packages': ['line'] });
    google.charts.setOnLoadCallback(drawChart);

    // Listen for date changes:
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

        $url = "/API/ChartApi/GetUserStepChart?UserName=" + $.urlParam("UserName");

        if ($(".fromDate").val() !== null && $(".fromDate").val() !== "")
            $url += "&fromDate=" + $(".fromDate").val();
        if ($(".toDate").val() !== null && $(".toDate").val() !== "")
            $url += "&toDate=" + $(".toDate").val();

        console.log("Sending request: [href: " + $url + "]" + ";");

        $.ajax({
            url: $url,
            method: 'get',
            beforeSend: function () {
                //$link.addClass('overlay');
            },
            success: function (response) {
                console.log(response);

                if (response.stepEntries !== null && response.stepEntries.length > 0) {

                    var stepArray = [["Date", "Daily Steps"]];

                    $.each(response.stepEntries, function () {
                        var stepItem = [this.updatedAt, this.steps];
                        stepArray.push(stepItem);
                    });

                    var gData = google.visualization.arrayToDataTable(stepArray);
                    var data = new google.visualization.DataTable();
                    //gData.addColumn('date', 'Day');
                    //gData.addColumn('number', 'Steps');

                    var options = {
                        chart: {
                            title: 'Steps Over Time'//,
                            //subtitle: 'in millions of dollars (USD)'
                        },
                        width: $('#linechart_material').width(),
                        height: 500
                    };
                    /////This is the google API implementation
                    var chart = new google.charts.Line(document.getElementById('linechart_material'));


                    if (stepArray.length > 0)
                        chart.draw(gData, google.charts.Line.convertOptions(options));

                } // if
                else
                    $('#linechart_material').html('<div class="chart_inner">There are no daily step entries for this user.</div>');

                //let $div = $('#feat_' + response.featureId);
                //$div.remove();                              // Delete the row entirely
            },
            error: function (response) {
                console.log(response);
                $('#linechart_material').text('There was an error retrieving the entries.');
                //$link.removeClass('overlay');
            }
        });

    } // drawChart

}); // document onLoad
