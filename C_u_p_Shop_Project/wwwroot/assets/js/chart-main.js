/**
    Item Name: Ekka - Ecommerce HTML Template.
    Author: ashishmaraviya
    Version: 1.0
    Copyright 2021-2022
	Author URI: https://themeforest.net/user/ashishmaraviya
**/
(function($) {
    "use strict";

    var ctx = document.getElementById("growthChart").getContext('2d');

    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["فروردین 21", "اردیبهشت 21", "خرداد 21", "تیر 21", "مرداد 21", "شهریور 21", "مهر 21", "آبان 21", "آذر 12", "دی 21"],
            datasets: [{
                label: 'آپلود', // Name the series
                data: [2, 50, 45, 65, 63, 56, 50, 35, 28, 45], // Specify the data values array
                fill: false,
                borderColor: '#2196f3', // Add custom color border (Line)
                backgroundColor: '#2196f3', // Add custom color background (Points and Fill)
                borderWidth: 1 // Specify bar border width
            },
                      {
                label: 'درآمد',
                data: [20, 58, 32, 78, 56, 89, 87, 96, 92, 100],
                fill: false,
                borderColor: '#ff6191',
                backgroundColor: '#ff6191',
                borderWidth: 1
            },
            {
                label: 'حراج',
                data: [20, 25, 10, 35, 45, 32, 78, 56, 89, 87],
                fill: false,
                borderColor: '#33317d',
                backgroundColor: '#33317d',
                borderWidth: 1
            },
            {
                label: 'بازگشتی',
                data: [2, 7, 4, 10, 5, 3, 2, 8, 3, 4],
                fill: false,
                borderColor: '#f79165',
                backgroundColor: '#f79165',
                borderWidth: 1
            }]
        },
        options: {
          responsive: true, // Instruct chart js to respond nicely.
          maintainAspectRatio: false, // Add to prevent default behaviour of full-width/height 
        }
    });

})(jQuery);