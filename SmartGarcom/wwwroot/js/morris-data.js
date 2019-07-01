$(function () {

    Morris.Area({
        element: 'morris-area-chart',
        data: [{
            period: '2017 Q1',
            TicketsAbertos: 52,
            TicketsFechados: 52
        }, {
            period: '2017 Q2',
            TicketsAbertos: 70,
            TicketsFechados: 70
        }, {
            period: '2017 Q3',
            TicketsAbertos: 56,
            TicketsFechados: 52
        }, {
            period: '2017 Q4',
            TicketsAbertos: 70,
            TicketsFechados: 70
        }, {
            period: '2018 Q1',
            TicketsAbertos: 70,
            TicketsFechados: 70
        }, {
            period: '2018 Q2',
            TicketsAbertos: 70,
            TicketsFechados: 70
        }, {
            period: '2018 Q3',
            TicketsAbertos: 65,
            TicketsFechados: 60
        }, {
            period: '2018 Q4',
            TicketsAbertos: 80,
            TicketsFechados: 75
        }, {
            period: '2019 Q1',
            TicketsAbertos: 80,
            TicketsFechados: 80
        }, {
            period: '2019 Q2',
            TicketsAbertos: 35,
            TicketsFechados: 30
        }],
        xkey: 'period',
        ykeys: ['TicketsAbertos', 'TicketsFechados'],
        labels: ['Tickets Abertos', 'Tickets Fechados'],
        pointSize: 2,
        hideHover: 'auto',
        resize: true
    });

    Morris.Donut({
        element: 'morris-donut-chart',
        data: [{
            label: "Download Sales",
            value: 12
        }, {
            label: "In-Store Sales",
            value: 30
        }, {
            label: "Mail-Order Sales",
            value: 20
        }],
        resize: true
    });

    Morris.Bar({
        element: 'morris-bar-chart',
        data: [{
            y: '2006',
            a: 100,
            b: 90
        }, {
            y: '2007',
            a: 75,
            b: 65
        }, {
            y: '2008',
            a: 50,
            b: 40
        }, {
            y: '2009',
            a: 75,
            b: 65
        }, {
            y: '2010',
            a: 50,
            b: 40
        }, {
            y: '2011',
            a: 75,
            b: 65
        }, {
            y: '2012',
            a: 100,
            b: 90
        }],
        xkey: 'y',
        ykeys: ['a', 'b'],
        labels: ['Series A', 'Series B'],
        hideHover: 'auto',
        resize: true
    });

});
