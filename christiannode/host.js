var Http = require('http');
var Jade = require('jade');
var Express = require('express');
var Open = require('open');

var hostname = '0.0.0.0';
var port = 1337;

var app = Express();
Http.createServer(app).listen(port, hostname);
Open('http://localhost:' + port + "/");

function prepareHtmlResponse(jade, json) {
	var fn = Jade.compileFile(jade);
	return fn(json);
}

app.get("/time", function(req, res) {
	console.log("/time");
	res.writeHead(200, { 'Content-Type': 'text/html' });
	res.end(new Date().getTime().toString());
});

app.get("/", function(req, res) {
	console.log("/");
	res.writeHead(200, { 'Content-Type': 'text/html' });
	res.end(prepareHtmlResponse('./home.jade'));
});




