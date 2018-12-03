$(document).ready(function () {

    // From: https://stackoverflow.com/a/25359264
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        return decodeURI(results[1]) || 0;
    } // urlParam

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
        $url = "/API/ChartApi/GetUserExerciseChart?UserName=" + $.urlParam("UserName");
        if ($(".fromDate").val() != null && $(".fromDate").val() != "")
            $url += "&fromDate=" + $(".fromDate").val();
        if ($(".toDate").val() != null && $(".toDate").val() != "")
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

                var exerciseArray = [["Date", "Minutes"]];
                $.each(response.exerciseEntries, function () {
                    var exerciseItem = [this.updatedAt, this.minutes];
                    exerciseArray.push(exerciseItem);
                });

                var gData = google.visualization.arrayToDataTable(exerciseArray);
                var data = new google.visualization.DataTable();
                gData.addColumn('date', 'Date');
                gData.addColumn('number', 'Minutes');

                var options = {
                    chart: {
                        title: 'Exercise Over Time'//,
                        //subtitle: 'in millions of dollars (USD)'
                    },
                    width: 900,
                    height: 500
                };

                var chart = new google.charts.Line(document.getElementById('linechart_material'));

                if (exerciseArray.length > 0)
                    chart.draw(gData, google.charts.Line.convertOptions(options));

                $url = null;
            },
            error: function (response) {
                console.log(response);
                //$link.removeClass('overlay');
            }
        });

    } // drawChart

}); // document onLoad