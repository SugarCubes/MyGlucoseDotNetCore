$(document).ready(function () {

    // From: https://stackoverflow.com/a/25359264
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results === null) {
            return null;
        }
        return decodeURI(results[1]) || 0;
    }; // urlParam

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
        $url = "/API/ChartApi/GetUserMealChart?UserName=" + $.urlParam("UserName");
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

                if (response.mealEntries !== null && response.mealEntries.length > 0) {

                    var mealArray = [["Date", "Total Carbs"]];
                    $.each(response.mealEntries, function () {
                        var mealItem = [this.updatedAt, this.totalCarbs];
                        mealArray.push(mealItem);
                    });

                    var gData = google.visualization.arrayToDataTable(mealArray);
                    var data = new google.visualization.DataTable();
                    //gData.addColumn('date', 'Day');
                    //gData.addColumn('number', 'Total Carbs');

                    var options = {
                        chart: {
                            title: 'Meals Over Time'//,
                            //subtitle: 'in millions of dollars (USD)'
                        },
                        width: $('#linechart_material').width(),
                        height: 500
                    };

                    var chart = new google.charts.Line(document.getElementById('linechart_material'));

                    if (mealArray.length > 0)
                        chart.draw(gData, google.charts.Line.convertOptions(options));

                } // if
                else
                    $('#linechart_material').html('<div class="chart_inner">There are no meal entries for this user.</div>');

            },
            error: function (response) {
                console.log(response);
                //$link.removeClass('overlay');
                $('#linechart_material').text('There was an error retrieving the entries.');

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