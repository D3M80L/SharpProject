"use strict";
var bouncingBallModule = (function() {
	var canvasWidth,
		canvasHeight,
		canvas2dContext,
		ballDiameter,
		ballRadius,
		ballPosX,
		ballPosY,
		ball,
		ballRotation,
		vX,
		vY,
		step,
		maxDistanceX,
		maxDistanceY;
	
	var init = function() {
		canvasWidth = 800;
		canvasHeight = 600;
		
		$('#CanvasGame').attr('width', canvasWidth);
		$('#CanvasGame').attr('height', canvasHeight);
		canvas2dContext = $('#CanvasGame')[0].getContext('2d');
		
		ball = new Image();
		ball.onload = ballLoaded;
		ball.src = "ball.png";
	};
	
	var ballLoaded = function() {
		ballDiameter = this.width;
		ballRadius = ballDiameter / 2;
		ballPosX = (canvasWidth / 2) - ballRadius;
		ballPosY = (canvasHeight / 2) - ballRadius;
		ballRotation = 100;
		vX = 1;
		vY = 1;
		step = 5;
		maxDistanceX = canvasWidth - ballRadius;
		maxDistanceY = canvasHeight - ballRadius;
		startAnimation();
	};
	
	var changePosition = function() {
		ballPosX = ballPosX + vX * step;
		ballPosY = ballPosY + vY * step;
		ballRotation = ballRotation + 5;
		
		if (ballPosX <= ballRadius) {
			ballPosX = ballRadius;
			vX *= -1;
		} else if (ballPosX >= maxDistanceX) {
			ballPosX = maxDistanceX;
			vX *= -1;
		}
		
		if (ballPosY <= ballRadius) {
			ballPosY = ballRadius;
			vY *= -1;
		} else if (ballPosY >= maxDistanceY) {
			ballPosY = maxDistanceY;
			vY *= -1;
		}
	};
	
	var draw = function draw() {
		canvas2dContext.clearRect(0, 0, canvasWidth, canvasHeight);
		
		canvas2dContext.save();
		canvas2dContext.translate(ballPosX, ballPosY);
		canvas2dContext.rotate(ballRotation*Math.PI/180);
		canvas2dContext.drawImage(ball, -ballRadius, -ballRadius);
		canvas2dContext.restore();
		console.log(ballRadius);
	};
	
	var startAnimation = function() {
		setInterval(function() {
			changePosition();
			draw();
		}, 1000 / 25);
	};
	
	return {
		run: function() {
			init();
		}
	};

})();

bouncingBallModule.run();