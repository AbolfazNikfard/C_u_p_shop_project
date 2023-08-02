
var activity = document.getElementById("activity");
if (activity !== null)
{
    var activityData = [
        {
            first: [0, 65, 52, 115, 98, 165, 125],
            second: [45, 38, 100, 87, 152, 187, 85]
        },
        {
            first: [0, 65, 77, 33, 49, 100, 100],
            second: [88, 33, 20, 44, 111, 140, 77]
        },
        {
            first: [0, 40, 77, 55, 33, 116, 50],
            second: [55, 32, 20, 55, 111, 134, 66]
        },
        {
            first: [0, 44, 22, 77, 33, 151, 99],
            second: [60, 32, 120, 55, 19, 134, 88]
        }
    ];

    var config = {
        // The type of chart we want to create
        type: "line",
        // The data for our dataset
        data: {
            labels:@Html.Raw(XLabels), //[
                //"4 فروردین",
                //"5 فروردین",
                //"6 فروردین",
                //"7 فروردین",
                //"8 فروردین",
                //"9 فروردین",
                //"10 فروردین"
            //],
            datasets: [
                {
                    label: "فعال",
                    backgroundColor: "transparent",
                    borderColor: "rgba(82, 136, 255, .8)",
                    data: activityData[0].first,
                    lineTension: 0,
                    pointRadius: 5,
                    pointBackgroundColor: "rgba(255,255,255,1)",
                    pointHoverBackgroundColor: "rgba(255,255,255,1)",
                    pointBorderWidth: 2,
                    pointHoverRadius: 7,
                    pointHoverBorderWidth: 1
                },
                {
                    label: "غیر فعال",
                    backgroundColor: "transparent",
                    borderColor: "rgba(255, 199, 15, .8)",
                    data: activityData[0].second,
                    lineTension: 0,
                    borderDash: [10, 5],
                    borderWidth: 1,
                    pointRadius: 5,
                    pointBackgroundColor: "rgba(255,255,255,1)",
                    pointHoverBackgroundColor: "rgba(255,255,255,1)",
                    pointBorderWidth: 2,
                    pointHoverRadius: 7,
                    pointHoverBorderWidth: 1
                }
            ]
        },
        // Configuration options go here
        options: {
            responsive: true,
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            scales: {
                xAxes: [
                    {
                        gridLines: {
                            display: false,
                        },
                        ticks: {
                            fontColor: "#8a909d", // this here
                        },
                    }
                ],
                yAxes: [
                    {
                        gridLines: {
                            fontColor: "#8a909d",
                            fontFamily: "iranyekan, sans-serif",
                            display: true,
                            color: "#eee",
                            zeroLineColor: "#eee"
                        },
                        ticks: {
                            // callback: function(tick, index, array) {
                            //   return (index % 2) ? "" : tick;
                            // }
                            stepSize: 50,
                            fontColor: "#8a909d",
                            fontFamily: "iranyekan, sans-serif"
                        }
                    }
                ]
            },
            tooltips: {
                mode: "index",
                intersect: false,
                titleFontColor: "#888",
                bodyFontColor: "#555",
                titleFontSize: 12,
                bodyFontSize: 15,
                backgroundColor: "rgba(256,256,256,0.95)",
                displayColors: true,
                xPadding: 10,
                yPadding: 7,
                borderColor: "rgba(220, 220, 220, 0.9)",
                borderWidth: 2,
                caretSize: 6,
                caretPadding: 5
            }
        }
    };

    var ctx = document.getElementById("activity").getContext("2d");
    var myLine = new Chart(ctx, config);

    var items = document.querySelectorAll("#user-activity .nav-tabs .nav-item");
    items.forEach(function (item, index) {
        item.addEventListener("click", function () {
            config.data.datasets[0].data = activityData[index].first;
            config.data.datasets[1].data = activityData[index].second;
            myLine.update();
        });
    });
}
