// Note this message is not caught, because client had not registered an event listener yet
self.postMessage('Initializing...');

// Worker listens for messages
self.addEventListener('message', function(e) {
	var data = e.data;
	
	switch (data) {
	case 'start':
		self.postMessage('START');
	break;
	
	case 'stop':
		self.postMessage('STOP');
		self.close();
	break;
	
	default:
		self.postMessage(data);
	};

}, false);