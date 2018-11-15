$(document).ready(function () {

    google.charts.load('current', { 'packages': ['line'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {

        if ($.urlParam("UserName") === null)
            $url = "/API/ChartApi/GetGlucoseChart";
        else
            $url = "/API/ChartApi/GetUserGlucoseChart?UserName=" + $.urlParam("UserName");

        console.log("Sending request: [href: " + $url + "]" + ";");

        $.ajax({
            url: $url,
            method: 'get',
            beforeSend: function () {
                //$link.addClass('overlay');
            },
            success: function (response) {
                console.log(response);

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

                var chart = new google.charts.Line(document.getElementById('linechart_material'));

                if (glucoseArray.length > 0)
                    chart.draw(gData, google.charts.Line.convertOptions(options));

                //let $div = $('#feat_' + response.featureId);
                //$div.remove();                              // Delete the row entirely
            },
            error: function (response) {
                console.log(response);
                //$link.removeClass('overlay');
            }
        });

    } // drawChart


    // From: https://stackoverflow.com/a/25359264
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        return decodeURI(results[1]) || 0;
    } // urlParam

}); // document onLoad