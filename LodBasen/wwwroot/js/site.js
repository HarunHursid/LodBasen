﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// SIDEBAR TOGGLE

var sidebarOpen = false;
var sidebar = document.getElementById("sidebar");

function openSidebar() {
    if (!sidebarOpen) {
        sidebar.classList.add("sidebar-responsive");
        sidebarOpen = true;
    }
}

function closeSidebar() {
    if (sidebarOpen) {
        sidebar.classList.remove("sidebar-responsive");
        sidebarOpen = false;
    }
}

// ------------ CHARTS ------------
// Bar Chart

var barChartOptions = {
    series: [
        {
            data: [10, 8, 6],
        },
    ],
    chart: {
        type: "bar",
        height: 350,
        toolbar: {
            show: false,
        },
    },
    colors: ["#246dec", "#cc3c43", "#367952"],
    plotOptions: {
        bar: {
            distributed: true,
            borderRadius: 4,
            horizontal: false,
            columnWidth: "40%",
        },
    },
    dataLabels: {
        enabled: false,
    },
    legend: {
        show: false,
    },
    xaxis: {
        categories: ["William", "Karina", "Lotte"],
    },
    yaxis: {
        title: {
            text: "Antal Solgte",
        },
    },
};

var barChart = new ApexCharts(
    document.querySelector("#bar-chart"),
    barChartOptions
);
barChart.render();

// Area Chart

var AreaChartOptions = {
    series: [
        {
            name: "Purchase Orders",
            data: [31, 40, 28, 51, 42, 109, 100],
        },
        {
            name: "Sales Orders",
            data: [11, 32, 45, 32, 34, 52, 41],
        },
    ],
    chart: {
        height: 350,
        type: "area",
        toolbar: {
            show: false,
        },
    },
    colors: ["#4f35a1", "#246dec"],
    dataLabels: {
        enabled: false,
    },
    stroke: {
        curve: "smooth",
    },
    labels: ["Jan", "Feb", "Mar", "Apr", "Maj", "Jun", "Jul"],
    markers: {
        size: 0,
    },
    yaxis: [
        {
            title: {
                text: "Purchase Orders",
            },
        },
        {
            opposite: true,
            title: {
                text: "Sales Orders",
            },
        },
    ],
    tooltip: {
        shared: true,
        intersect: false,
    },
};

var areaChart = new ApexCharts(
    document.querySelector("#area-chart"),
    AreaChartOptions
);
areaChart.render();
