#!/usr/bin/env python
import RPi.GPIO as GPIO
from RPLCD import CharLCD, cleared, cursor
from CharacterDefinitions import FourLinesCharacterDefinition

class LcdDisplayHardware:
	
	def __init__(self, width, height):
		GPIO.setwarnings(False)
		self.lcd = None
		self.width = width
		self.height = height
		

	def __initHardware(self):
		if (self.lcd is None):
			self.lcd = CharLCD(pin_rs=26, pin_e=24, pins_data=[22, 18, 16, 12], numbering_mode=GPIO.BOARD, cols=self.width, rows=self.height, dotsize=8)	

	def defineCharacter(self, characterNumber, characterDefinition):
		self.__initHardware()
		self.lcd.create_char(characterNumber, characterDefinition)

	def drawLine(self, lineNumber, line):
		self.__initHardware()
		self.lcd.cursor_pos = (lineNumber, 0)
		self.lcd.write_string(line)
	
	def drawString(self, lineNumber, columnNumber, line):
		self.__initHardware()
		self.lcd.cursor_pos = (lineNumber, columnNumber)
		self.lcd.write_string(line)

	def turnDisplay(self, state):
		LED_ON = 32
		GPIO.setup(LED_ON, GPIO.OUT)
		GPIO.output(LED_ON, state)

	def setupCharacters(self, characterDefinition):
		self.__initHardware()
		for characterNumber in range(0, len(characterDefinition.CustomCharacters)):
			self.defineCharacter(characterNumber, characterDefinition.CustomCharacters[characterNumber])

class RpiLcdDisplay:
	
	def __init__(self, width, height):
		self.width = width
		self.height = height

		self.outputBuffer = []
		for y in range(0,height):
			self.outputBuffer.append(list(" " * width))

		self.drawPositionX = 0
		self.drawPositionY = 1

		self.hardware = LcdDisplayHardware(width, height)

	def turnDisplay(self, state):
		self.hardware.turnDisplay(state)

	def bufferCharacter(self, y, x, character):
		self.outputBuffer[y][x] = character

	def bufferString(self, y, x, string):
		for i in range(0, len(string)):
			self.outputBuffer[y][x + i] = string[i]

	def bufferCharacterArray(self, y, x, characterArray):
		w = len(characterArray[0])
		h = len(characterArray)
		
		for line in range (0, h):
			for column in range (0, w):
				self.outputBuffer[line + y][column + x] = unichr(characterArray[line][column])

	def drawBuffer(self):
		for y in range(0, self.height):
			self.hardware.drawLine(y, self.outputBuffer[y])
