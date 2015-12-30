import Adafruit_BMP.BMP085 as BMP085

class PressureMeter:

	def __init__(self):
		self.sensor = BMP085.BMP085(busnum=1, mode=BMP085.BMP085_STANDARD)
		self.temperature = float('NaN')
		self.pressue = float('NaN')

	def read(self):
		temperature = self.sensor.read_temperature()
		pressue = self.sensor.read_pressure()
		
		self.temperature = temperature
		self.pressue = pressue
