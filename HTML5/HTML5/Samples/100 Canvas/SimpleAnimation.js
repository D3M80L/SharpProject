var canvasWidth  = 800;
var canvasHeight = 600;

$('#CanvasGame').attr('width', canvasWidth);
$('#CanvasGame').attr('height', canvasHeight);

var canvas2dContext = $('#CanvasGame')[0].getContext('2d');

var ball = new Image();
ball.src = "ball.png";

var ballRadius = ball.width / 2;
var ballDiameter = ball.width;
var ballPosX = (canvasWidth / 2) - ballRadius;
var ballPosY = (canvasHeight / 2) - ballRadius;
var ballRotation = 100;

var vX = 1;
var vY = 1;
var step = 5;

function changePosition() {
	ballPosX = ballPosX + vX * step;
	ballPosY = ballPosY + vY * step;
	ballRotation = ballRotation + 5;
	
	if (ballPosX <= 0) {
		ballPosX = 0;
		vX *= -1;
	} else if (ballPosX >= canvasWidth - ballDiameter) {
		ballPosX = canvasWidth - ballDiameter;
		vX *= -1;
	}
	
	if (ballPosY <= 0) {
		ballPosY = 0;
		vY *= -1;
	} else if (ballPosY >= canvasHeight - ballDiameter) {
		ballPosY = canvasHeight - ballDiameter;
		vY *= -1;
	}
}

function draw() {
	canvas2dContext.clearRect(0, 0, canvasWidth, canvasHeight);
	
	canvas2dContext.save();
	canvas2dContext.translate(ballPosX + ballRadius, ballPosY + ballRadius);
	canvas2dContext.rotate(ballRotation*Math.PI/180);
	canvas2dContext.drawImage(ball, -ballRadius, -ballRadius);
	canvas2dContext.restore();
}

setInterval(function() {
	changePosition();
	draw();
}, 1000 / 25);
