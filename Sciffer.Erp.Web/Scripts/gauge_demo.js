var opts = {
    lines: 12, // The number of lines to draw
    angle: 0, // The length of each line
    lineWidth: 0.4, // The line thickness
    pointer: {
        length: 0.75, // The radius of the inner circle
        strokeWidth: 0.042, // The rotation offset
        color: '#1D212A' // Fill color
    },
    limitMax: 'false', // If true, the pointer will not go past the end of the gauge
    colorStart: '#1ABC9C', // Colors
    colorStop: '#1ABC9C', // just experiment with them
    strokeColor: '#F0F3F3', // to see which ones work best for you
    generateGradient: true
};
var target = document.getElementById('foo'); // your canvas element
var gauge = new Gauge(target).setOptions(opts); // create sexy gauge!
gauge.maxValue = 100; // set max gauge value
gauge.animationSpeed = 32; // set animation speed (32 is default value)
gauge.set(60); // set actual value
gauge.setTextField(document.getElementById("gauge-text"));

var target = document.getElementById('foo1'); // your canvas element
var gauge1 = new Gauge(target).setOptions(opts); // create sexy gauge!
gauge1.maxValue = 200; // set max gauge value
gauge1.animationSpeed = 30; // set animation speed (32 is default value)
gauge1.set(50); // set actual value
gauge1.setTextField(document.getElementById("gauge-text"));

