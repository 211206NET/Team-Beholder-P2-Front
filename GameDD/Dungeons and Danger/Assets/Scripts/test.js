//Lower Level nitty gritty of node.js
/*
//console.log("Hello World!");
//require returns an object we can do things with
var http = require("http");

var countN = 0;

//Listener is a function that gets called whenever somebody tries to access your server
var server = http.createServer( 
    //Read request, return response
    function ( req, res ){
        //sgsgsrg
        res.statusCode = 200;
        res.setHeader("content-Type", "test/plain");//Unity probably doesn't care about content header
        countN ++;
        res.end("Hello web browser, you are visitor: " + countN.toString());
    }
    //Or lambda style:
    //( req, res ) => {
    //}
    
);

server.listen(8000);

*/


