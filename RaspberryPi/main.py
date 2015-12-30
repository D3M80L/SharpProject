#!/usr/bin/env python
from RpiLcdDisplay import RpiLcdDisplay, FourLinesCharacterDefinition
from CharacterDefinitions import FourLinesCharacterDefinition
from datetime import datetime
from PressureMeter import PressureMeter

class RpiCronChecker:
	def __init__(self):
		self.characterDefinition = FourLinesCharacterDefinition()
		self.rpiDisplay = RpiLcdDisplay(20, 4)
		self.pressureMeter = PressureMeter()

	def run(self):
		self.pressureMeter.read()
		self.drawTime()
		self.rpiDisplay.hardware.setupCharacters(self.characterDefinition)
		self.rpiDisplay.drawBuffer()
		self.rpiDisplay.turnDisplay(self.computeDisplayState())

	def drawTime(self):
		currentTime = datetime.today()
		largeDigits = self.characterDefinition.LargeDigits
		outputBuffer = self.rpiDisplay.bufferCharacterArray

		hourTens = currentTime.hour / 10
		if hourTens > 0:
			outputBuffer(0, 0, largeDigits[hourTens])
		outputBuffer(0, 4, largeDigits[currentTime.hour % 10])

		outputBuffer(0, 8, self.characterDefinition.TimeSeparator)

		outputBuffer(0, 10, largeDigits[currentTime.minute / 10])
		outputBuffer(0, 14, largeDigits[currentTime.minute % 10])

		dayOfWeek = [
			('Mo'),
			('Tu'),
			('We'),
			('Th'),
			('Fr'),
			('Sa'),
			('Su')
		]

		self.rpiDisplay.bufferString(0, 18, dayOfWeek[currentTime.weekday()])
		self.rpiDisplay.bufferString(1, 18, str(currentTime.day).rjust(2, ' '))

		temperature = int(round(self.pressureMeter.temperature))

		self.rpiDisplay.bufferString(3, 17, str(temperature).rjust(3, ' '))

	def computeDisplayState(self):
		currentTime = datetime.today()

		if (currentTime.hour >= 6 and currentTime.hour <= 22):
			return True

		if (currentTime.minute % 15 == 0):
			return True

		if ((currentTime.month == 12 and currentTime.day == 31 and currentTime.hour >= 22) or (currentTime.month == 1 and currentTime.day == 1 and currentTime.hour <= 6)) :
			return True
		

		return False

checker = RpiCronChecker()
checker.run()

