$(document).ready(function () {
    var weekOrders = [0, 0, 0, 0, 0, 0, 0];
    $.ajax({ url: "/api/OrderApi" }).done(function (results) {

        var sumBurgers = 0;
        var sumDrinks = 0;
        var sum = 0;
        for (var i = 0; i < results.length; i++) {
            sum = sum + results[i].TotalPrice;


            for (var j = 0; j < results[i].OrderBurgers.length; j++) {
                sumBurgers += results[i].OrderBurgers[j].Quantity;
            }


            for (var y = 0; y < results[i].OrderDrinks.length; y++) {
                sumDrinks += results[i].OrderDrinks[y].Quantity;

            }



            var dateArray = results[i].OrderDate.split("T");
            var date = new Date(dateArray[0]);
            switch (date.getDay()) {
                case 0:
                    weekOrders[0] += 1;
                    break;
                case 1:
                    weekOrders[1] += 1;
                    break;
                case 2:
                    weekOrders[2] += 1;
                    break;
                case 3:
                    weekOrders[3] += 1;
                    break;
                case 4:
                    weekOrders[4] += 1;
                    break;
                case 5:
                    weekOrders[5] += 1;
                    break;
                case 6:
                    weekOrders[6] += 1;
                    break;

                default:
                    break;
            }


        }
        sum = (sum * 100) / 100;
        $("#RevenuesAnnual").text(sum.toFixed(2));
        $("#CompletedOrders").text(results.length);
        $("#BurgersSold").text(sumBurgers);
        $("#DrinksSold").text(sumDrinks);
    }).then(function () {



        let myChart = document.getElementById('myChart').getContext('2d');
        Chart.defaults.global.defaultFontFamily = 'Lato';
        Chart.defaults.global.defaultFontSize = 18;
        Chart.defaults.global.defaultFontColor = '#777';
        let massPopChart = new Chart(myChart, {
            type: 'bar',//bar,horizontalBar,pie,line,doughnut,radar,polarArea
            data: {
                labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
                datasets: [{
                    label: 'Orders',
                    data: [
                        weekOrders[1],
                        weekOrders[2],
                        weekOrders[3],
                        weekOrders[4],
                        weekOrders[5],
                        weekOrders[6],
                        weekOrders[0]
                    ],
                    //backgroundColor:'green'
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(54, 162, 235, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(75, 192, 192, 0.6)',
                        'rgba(153, 102, 255, 0.6)',
                        'rgba(255, 159, 64, 0.6)',
                        'rgba(255, 99, 132, 0.6)'
                    ],
                    borderWidth: 1,
                    borderColor: '#777',
                    hoverBorderWidth: 3,
                    hoverBorderColor: '#000'
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Orders by days of the week',
                    fontSize: 25
                },
                legend: {
                    display: true,
                    position: 'right',
                    labels: {
                        fontColor: '#000'
                    }
                },
                layout: {
                    padding: {
                        left: 50,
                        right: 50,
                        bottom: 100,
                        top: 100
                    }
                },
                tooltips: {
                    enabled: true
                }
            }
        });


    });











});