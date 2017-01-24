var app = require('http').createServer(handler)
var io = require('socket.io')(app);
var fs = require('fs');
var ip = require('ip');

app.listen(4567);

function handler (req, res) {
  fs.readFile(__dirname + '/index.html',
  function (err, data) {
    if (err) {
      res.writeHead(500);
      return res.end('Error loading index.html');
    }

    res.writeHead(200);
    res.end(data);
  });
}
console.log("Server running on : "+ip.address().toString());
var unitysocket =null;
io.on('connection', function (socket) {
  socket.emit('connectedtoserver',{ipAdress : ip.address().toString()+":4567"});
	// if the unity interface just connected
	socket.on('unityconnected', function (data) {
    console.log("Unity interface now pluged in.");
		unitysocket = this;
  });
	// if an html client connected
	socket.on('htmlclientconnected', function (data) {
    console.log(socket.id +" just connected !");
		socket.on('playermoved', function (data) {
			if (unitysocket!=null)
			unitysocket.emit("playermoved",data);
			console.log(socket.id +" moved "+data.delta);
		});

		socket.on('playerclicked', function (data) {
			if (unitysocket!=null)
			unitysocket.emit("playerclicked");
			console.log(socket.id +" clicked !");
		});
  });
});
