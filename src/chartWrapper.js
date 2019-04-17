module.exports = {
    // parseJson : string -> string
    CreateChart: function (myId,dataconfig) {
        var chartColors = [
             'rgb(255, 99, 132)',
             'rgb(255, 159, 64)',
             'rgb(255, 205, 86)',
             'rgb(75, 192, 192)',
             'rgb(54, 162, 235)',
             'rgb(153, 102, 255)',
             'rgb(201, 203, 207)'
        ]
    let datasets = []
        for (let i = 0; i < dataconfig.length; i++) {
            d = {
                label : dataconfig[i].label,
                backgroundColor : 'rgba(0, 0, 0, 0)',
                borderColor : chartColors[i],
                pointRadius: 0,
            }
            datasets = datasets.concat(d)          
        }
    console.log(datasets)
    var ctx = document.getElementById(myId);
    var myChart = new Chart(ctx, {
        type: 'line',
        

        data:{
            labels : [],
            datasets : datasets
        }
        
        ,

        options: {
            responsive: true,
            maintainAspectRatio: false,
            
            animation: {
                duration: 0 // general animation time
            },
            hover: {
                animationDuration: 0 // duration of animations when hovering an item
            },
            responsiveAnimationDuration: 0, // animation duration after a resize
            elements: {
                line: {
                    tension: 0 // disables bezier curves
                }
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        color: "rgba(0, 0, 0, 0)",
                    }
                }],
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                    gridLines: {
                        color: "rgba(0, 0, 0, 0)",
                    }  
                }]
            }
        }
    });
 
    return myChart
    },
    UpdateData:function(chart,x,datasetsBasic)
    {
        for (let i = 0; i < chart.data.datasets.length; i++) {
            chart.data.datasets[i].data = datasetsBasic[i].data;            
        }
        chart.data.labels =x
        chart.update()
    }
   
};
