/* ====== Index ======

1. RECNT ORDERS
2. USER ACTIVITY
3. ANALYTICS COUNTRY
4. PAGE VIEWS
5. ACTIVITY USER

====== End ======*/
$(function () {
  "use strict";

  /*======== 1. RECNT ORDERS ========*/
  if ($("#recent-orders")) {
    var start = moment().subtract(29, "days");
    // console.log(start.format("ll"));
    var end = moment();
    // console.log(start.format("ll"));
    var cb = function (start, end) {
      $("#recent-orders .date-range-report span").html(
        start.format("ll") + " - " + end.format("ll")
      );
    };

    

    $("#recent-orders .date-range-report").daterangepicker(
      {

        months: [
          "فروردین",
          "اردیبهشت",
          "خرداد",
          "تیر",
          "مرداد",
          "شهریور",
          "مهر",
          "آبان",
          "آذر",
          "دی",
          "بهمن",
          "اسفند",
        ],
        
        startDate: start.format("YYYY/M/D"),
        endDate: end.format("YYYY/M/D"),
        opens: 'left',
       
        ranges: {
          امروز: [moment(), moment()],
          دیروز: [
            moment().subtract(1, "days"),
            moment().subtract(1, "days")
          ],
          "هفته گذشته": [moment().subtract(6, "days"), moment()],
          "ماه گذشته": [moment().subtract(29, "days"), moment()],
          "این ماه": [moment().startOf("month"), moment().endOf("month")],
          "آخرین ماه": [
            moment()
              .subtract(1, "month")
              .startOf("month"),
            moment()
              .subtract(1, "month")
              .endOf("month")
          ]
        }
      ,
        locale: {
          format: "YYYY/M/D",
          separator: " - ",
          applyLabel: "اعمال",
          cancelLabel: "انصراف",
          fromLabel: "از",
          toLabel: "تا",
          customRangeLabel: "سفارشی",
          weekLabel: "هف",
          daysOfWeek: ["ی", "د", "س", "چ", "پ", "ج", "ش"],
          monthNames: [
            "دی",
            "بهمن",
            "اسفند",
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            
          ],
          firstDay: 6,
        },},
      cb
    );
    cb(start, end);
  }

  /*======== 2. USER ACTIVITY ========*/
  if ($("#user-activity")) {
    var start = moment().subtract(1, "days");
    var end = moment().subtract(1, "days");
    var cb = function (start, end) {
      $("#user-activity .date-range-report span").html(
        start.format("ll") + " - " + end.format("ll")
      );
    };

    $("#user-activity .date-range-report").daterangepicker(
      {

         locale: {
          format: "YYYY/M/D",
          separator: " - ",
          applyLabel: "اعمال",
          cancelLabel: "انصراف",
          fromLabel: "از",
          toLabel: "تا",
          customRangeLabel: "سفارشی",
          weekLabel: "هف",
          daysOfWeek: ["ی", "د", "س", "چ", "پ", "ج", "ش"],
          monthNames: [
            "دی",
            "بهمن",
            "اسفند",
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",

          ],
          firstDay: 6,
        },
        
        startDate: start,
        endDate: end,
        opens: 'left',
       
        ranges: {
          امروز: [moment(), moment()],
          دیروز: [
            moment().subtract(1, "days"),
            moment().subtract(1, "days")
          ],
          "هفته گذشته": [moment().subtract(6, "days"), moment()],
          "ماه گذشته": [moment().subtract(29, "days"), moment()],
          "این ماه": [moment().startOf("month"), moment().endOf("month")],
          "آخرین ماه": [
            moment()
              .subtract(1, "month")
              .startOf("month"),
            moment()
              .subtract(1, "month")
              .endOf("month")
          ]
        }
      },
      cb
    );
    cb(start, end);
  }

  /*======== 3. ANALYTICS COUNTRY ========*/
  if ($("#analytics-country")) {
    var start = moment();
    var end = moment();
    var cb = function (start, end) {
      $("#analytics-country .date-range-report span").html(
        start.format("ll") + " - " + end.format("ll")
      );
    };

    $("#analytics-country .date-range-report").daterangepicker(
      {

         locale: {
          format: "YYYY/M/D",
          separator: " - ",
          applyLabel: "اعمال",
          cancelLabel: "انصراف",
          fromLabel: "از",
          toLabel: "تا",
          customRangeLabel: "سفارشی",
          weekLabel: "هف",
          daysOfWeek: ["ی", "د", "س", "چ", "پ", "ج", "ش"],
          monthNames: [
            "دی",
            "بهمن",
            "اسفند",
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",

          ],
          firstDay: 6,
        },
        
        startDate: start,
        endDate: end,
        opens: 'left',
       
        ranges: {
          امروز: [moment(), moment()],
          دیروز: [
            moment().subtract(1, "days"),
            moment().subtract(1, "days")
          ],
          "هفته گذشته": [moment().subtract(6, "days"), moment()],
          "ماه گذشته": [moment().subtract(29, "days"), moment()],
          "این ماه": [moment().startOf("month"), moment().endOf("month")],
          "آخرین ماه": [
            moment()
              .subtract(1, "month")
              .startOf("month"),
            moment()
              .subtract(1, "month")
              .endOf("month")
          ]
        }
      },
      cb
    );
    cb(start, end);
  }

  /*======== 4. PAGE VIEWS ========*/
  if ($("#page-views")) {
    var start = moment();
    var end = moment();
    var cb = function (start, end) {
      $("#page-views .date-range-report span").html(
        start.format("ll") + " - " + end.format("ll")
      );
    };

    $("#page-views .date-range-report").daterangepicker(
      {

         locale: {
          format: "YYYY/M/D",
          separator: " - ",
          applyLabel: "اعمال",
          cancelLabel: "انصراف",
          fromLabel: "از",
          toLabel: "تا",
          customRangeLabel: "سفارشی",
          weekLabel: "هف",
          daysOfWeek: ["ی", "د", "س", "چ", "پ", "ج", "ش"],
          monthNames: [
            "دی",
            "بهمن",
            "اسفند",
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",

          ],
          firstDay: 6,
        },
        
        startDate: start,
        endDate: end,
        opens: 'left',
       
        ranges: {
          امروز: [moment(), moment()],
          دیروز: [
            moment().subtract(1, "days"),
            moment().subtract(1, "days")
          ],
          "هفته گذشته": [moment().subtract(6, "days"), moment()],
          "ماه گذشته": [moment().subtract(29, "days"), moment()],
          "این ماه": [moment().startOf("month"), moment().endOf("month")],
          "آخرین ماه": [
            moment()
              .subtract(1, "month")
              .startOf("month"),
            moment()
              .subtract(1, "month")
              .endOf("month")
          ]
        }
      },
      cb
    );
    cb(start, end);
  }
  /*======== 5. ACTIVITY USER ========*/
  if ($("#activity-user")) {
    var start = moment();
    var end = moment();
    var cb = function (start, end) {
      $("#activity-user .date-range-report span").html(
        start.format("ll") + " - " + end.format("ll")
      );
    };

    $("#activity-user .date-range-report").daterangepicker(
      {

         locale: {
          format: "YYYY/M/D",
          separator: " - ",
          applyLabel: "اعمال",
          cancelLabel: "انصراف",
          fromLabel: "از",
          toLabel: "تا",
          customRangeLabel: "سفارشی",
          weekLabel: "هف",
          daysOfWeek: ["ی", "د", "س", "چ", "پ", "ج", "ش"],
          monthNames: [
            "دی",
            "بهمن",
            "اسفند",
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",

          ],
          firstDay: 6,
        },
        
        startDate: start,
        endDate: end,
        opens: 'left',
       
        ranges: {
          امروز: [moment(), moment()],
          دیروز: [
            moment().subtract(1, "days"),
            moment().subtract(1, "days")
          ],
          "هفته گذشته": [moment().subtract(6, "days"), moment()],
          "ماه گذشته": [moment().subtract(29, "days"), moment()],
          "این ماه": [moment().startOf("month"), moment().endOf("month")],
          "آخرین ماه": [
            moment()
              .subtract(1, "month")
              .startOf("month"),
            moment()
              .subtract(1, "month")
              .endOf("month")
          ]
        }
      },
      cb
    );
    cb(start, end);
  }
});
