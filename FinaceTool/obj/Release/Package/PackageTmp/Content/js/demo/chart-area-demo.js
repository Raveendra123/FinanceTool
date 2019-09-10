// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Area Chart Example
var ctx = document.getElementById("myAreaChart");
var Quarternames = $('#session').val();
//console.log('labels: '+ Quarternames);
var lablesArr = [];
lablesArr = Quarternames.split(',');
//console.log('arr: '+ lablesArr[0]);

var forecastvalues = $('#session1').val();
var forecastvaluesArr = [];
forecastvaluesArr = forecastvalues.split(',');
var maxValueInArray = Math.max.apply(Math, forecastvaluesArr);
var maxvalue=Math.ceil(maxValueInArray / 1000) * 1000;
//console.log('arr: ' + forecastvaluesArr[0] + 'arr: ' + forecastvaluesArr[1] + 'arr: ' + forecastvaluesArr[2] + 'arr: ' + forecastvaluesArr[3]);
var myLineChart = new Chart(ctx, {
  type: 'line',
  data: {
      //labels: ["Mar 1", "Mar 2", "Mar 3", "Mar 4", "Mar 5", "Mar 6", "Mar 7", "Mar 8", "Mar 9", "Mar 10", "Mar 11", "Mar 12", "Mar 13"],
      

      labels: lablesArr, 
    //  labels: d,
    datasets: [{
      label: "Sessions",
      lineTension: 0.3,
      backgroundColor: "rgba(2,117,216,0.2)",
      borderColor: "rgba(2,117,216,1)",
      pointRadius: 5,
      pointBackgroundColor: "rgba(2,117,216,1)",
      pointBorderColor: "rgba(255,255,255,0.8)",
      pointHoverRadius: 5,
      pointHoverBackgroundColor: "rgba(2,117,216,1)",
      pointHitRadius: 50,
      pointBorderWidth: 2,
        //data: [10000, 30162, 26263, 18394, 18287, 28682, 31274, 33259, 25849, 24159, 32651, 31984, 38451],
        data: forecastvaluesArr,
       // data: f,
    }],
  },
  options: {
    scales: {
      xAxes: [{
        time: {
          unit: 'date'
        },
        gridLines: {
          display: false
        },
        ticks: {
          maxTicksLimit: 4
        }
      }],
      yAxes: [{
        ticks: {
          min: 0,
            max: maxvalue,
          maxTicksLimit: 5
        },
        gridLines: {
          color: "rgba(0, 0, 0, .125)",
        }
      }],
    },
    legend: {
      display: false
    }
  }
});

