// graph.js

function albumsSales(data, id) {

    var width = 400,
        height = 400,
        radius = Math.min(width, height) / 2;

    var color = d3.scale.ordinal()
        .range(["#a05d56", "#6b486b", "#ff8c00", "#98abc5", "#8a89a6", "#d0743c", "#7b6888", "#7b6888", "#7b6888", "#7b6888", "#7b6888", "#7b6888"]);

    var arc = d3.svg.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var pie = d3.layout.pie()
        .sort(null)
        .value(function (data) { console.log("----------------------->" + len); return len; });

    var svg = d3.select("#" + id).append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
    var sum = 0;
    var len = 0;
    data.forEach(function (d) {
        len++;
        console.log(d);
        console.log(d.Sales.length);
        for (var i = 0; i < d.Sales.length; i++) {
            sum = sum + d.Sales[i];
            console.log(d.Sales[i]);
        }
        data.Sales = sum

        console.log(d);
        sum = 0;
        

    });

    var g = svg.selectAll(".arc")
        .data(pie(data))
        .enter().append("g")
        .attr("class", "arc");

    g.append("path")
        .attr("data.Names", arc)
        .style("fill", function (data) { return color(data.Names); });

    g.append("text")
        .attr("transform", data.forEach(function (d) {  return "translate(" + arc.centroid(data.Sales) + ")"; }))
        .attr("dy", ".35em")
        .style("text-anchor", "middle")
        .text(function (data) { return data.Names; });
}

function createPopularAlbumsGraph(data) {

    var margin = { top: 20, right: 20, bottom: 250, left: 40 },
        width = 600 - margin.left - margin.right,
        height = 600 - margin.top - margin.bottom;

    // set the ranges
    var x = d3.scale.ordinal().rangeRoundBands([0, width], .05);
    var y = d3.scale.linear().range([height, 0]);

    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom");

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(10);

    // add the SVG element
    var svg = d3.select("#popular-destinations-graph").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

    // load the data
    data.forEach(function (d) {
        d.Title = d.Title;
        d.NumberOfComment = +d.NumberOfComment;
    });

    // scale the range of the data
    x.domain(data.map(function (d) { return d.Title; }));
    y.domain([0, d3.max(data, function (d) { return d.NumberOfComment; })]);

    // add axis
    svg.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis)
        .selectAll("text")
        .style("text-anchor", "end")
        .attr("dx", "-.8em")
        .attr("dy", "-.55em")
        .attr("transform", "rotate(-90)");

    svg.append("g")
        .attr("class", "y axis")
        .call(yAxis)
        .append("text")
        .attr("transform", "rotate(-90)")
        .attr("y", 5)
        .attr("dy", ".71em")
        .style("text-anchor", "end")
        .text("NumberOfComment");

    // Add bar chart
    svg.selectAll("bar")
        .data(data)
        .enter().append("rect")
        .attr("class", "bar")
        .attr("x", function (d) { return x(d.Title); })
        .attr("width", x.rangeBand())
        .attr("y", function (d) { return y(d.NumberOfComment); })
        .attr("height", function (d) { return height - y(d.NumberOfComment); });
}


function albumSales(data) {
    (function (d3) {
        'use strict';
        var dataset = [
            { label: 'abulia', count: 10 },
            { label: 'bsdf', count: 15 },
            { label: 'csf', count: 50 },
            { label: 'dsfg', count: 20 },
        ];
        var width = 360;
        var height = 360;
        var radius = Math.min(width, height) / 2;

        var color = d3.scale.ordinal()
            .range(["#a05d56", "#6b486b", "#ff8c00", "#98abc5", "#8a89a6", "#d0743c", "#7b6888", "#7b6588", "#7b6828", "#7b6188", "#7b6858", "#7b6884"]);

        var svg = d3.select('#album_salses_graph')
            .append('svg')
            .attr('width', width)
            .attr('height', height)
            .append('g')
            .attr('transform', 'translate(' + (width / 2) + ',' + (height / 2) + ')');

        var arc = d3.
            svg.arc()
            .innerRadius(0)
            .outerRadius(radius);

        var pie = d3.layout.pie()
            .value(function (d) { console.log("d.Sales=" + d.Sales); return d.count; })
            .sort(null);

        var path = svg.selectAll('path')
            .data(pie(dataset))
            .enter()
            .append('path')
            .attr('d', arc)
            .attr('fill', function (d) {
                return color(d.data.label);
            });
    })(window.d3);
}
