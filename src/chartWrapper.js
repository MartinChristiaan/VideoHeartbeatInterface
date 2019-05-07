module.exports = {
    // parseJson : string -> string
    CreateChart: function (xvalues,yvalues,labels,ctx) {
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
        for (let i = 0; i < labels.length; i++) {
            d = {
                label : labels[i],
                data : yvalues[i],
                backgroundColor : 'rgba(0, 0, 0, 0)',
                borderColor : chartColors[i],
                pointRadius: 0,
            }
            datasets = datasets.concat(d)          
        }

    var myChart = new Chart(ctx, {
        type: 'line',
        

        data:{
            labels : xvalues,
            datasets : datasets
        },
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
                    // type: 'time',
                    // time: {
                    //     unit: 'second'
                    // }, 

                    gridLines: {
                        color: "rgba(0, 0, 0, 0)",
                    },
                    scaleLabel: {
                        display: true,
                        labelString: "Time (s)"
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

    AppendTimeSeries:function(chart,values,time)
    {
        for (let i = 0; i < values.length; i++) {
            chart.data.datasets[i].data.push(values[i]);            
        }        
        chart.data.labels.push(time)
        if(chart.data.labels.length > 400)
        {
            chart.data.labels.shift()
            for (let i = 0; i < values.length; i++) {
                chart.data.datasets[i].data.shift();            
            }   
        }


        chart.update()
    },
    

    UpdateData:function(ctx,x,datasetsBasic)
    {
        var chart = ctx.data('graph');
        for (let i = 0; i < chart.data.datasets.length; i++) {
            chart.data.datasets[i].data = datasetsBasic[i].data;            
        }
        chart.data.labels =x
        chart.update()
    }
   
};
