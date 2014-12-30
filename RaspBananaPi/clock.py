import RPi.GPIO as GPIO
from RPLCD import CharLCD, cleared, cursor
from datetime import datetime
import time

LED_ON = 11

lcd = CharLCD(pin_rs=15, pin_e=13, pins_data=[22, 18, 16, 12],
              numbering_mode=GPIO.BOARD,
              cols=20, rows=4, dotsize=8)

smiley = (
     0b00000,
     0b01010,
     0b01010,
     0b00000,
     0b10001,
     0b10001,
     0b01110,
     0b00000,
)

a0 = (
  0b00000,
  0b00000,
  0b00000,
  0b00000,
  0b00011,
  0b01111,
  0b01111,
  0b11111
)

a1 = (
  0b00000,
  0b00000,
  0b00000,
  0b00000,
  0b11000,
  0b11110,
  0b11110,
  0b11111
)

a2 = (
  0b00000,
  0b00000,
  0b00000,
  0b00000,
  0b11111,
  0b11111,
  0b11111,
  0b11111
)

a3 = (
  0b11111,
  0b01111,
  0b01111,
  0b00011,
  0b00000,
  0b00000,
  0b00000,
  0b00000
)

a4 = (
  0b11111,
  0b11110,
  0b11110,
  0b11000,
  0b00000,
  0b00000,
  0b00000,
  0b00000
)

a5 = (
  0b11111,
  0b11111,
  0b11111,
  0b11111,
  0b00000,
  0b00000,
  0b00000,
  0b00000
)

lcd.create_char(0, a0)
lcd.create_char(1, a1)
lcd.create_char(2, a2)
lcd.create_char(3, a3)
lcd.create_char(4, a4)
lcd.create_char(5, a5)


d0 = (
  (0, 2, 1),
  (0b11111111,0b11111110,0b11111111),
  (0b11111111,0b11111110,0b11111111),
  (3,5,4)
);

d1 = (
  (2, 1, 0b11111110),
  (0b11111110, 0b11111111, 0b11111110),
  (0b11111110, 0b11111111, 0b11111110),
  (5, 5, 5)
);

d2 = (
  (0, 2, 1),
  (0b11111110, 0b11111110, 0b11111111),
  (0, 5, 4),
  (5, 5, 5)
);

d3 = (
  (0, 2, 1),
  (0b11111110, 2, 0b11111111),
  (0b11111110, 0b11111110, 0b11111111),
  (3, 5, 4)
);

d4 = (
  (2, 0b11111110,  0b11111110),
  (0b11111111, 0b11111110, 0b11111111),
  (5, 5, 0b11111111),
  (0b11111110, 0b11111110, 5)
);

d5 = (
  (2, 2, 2),
  (0b11111111,2,1),
  (0b11111110, 0b11111110, 0b11111111),
  (5, 5, 4)
);

d6 = (
  (0, 2, 1),
  (0b11111111, 2, 1),
  (0b11111111, 0b11111110, 0b11111111),
  (3, 5, 4)
);

d7 = (
  (2, 2, 2),
  (0b11111110, 0, 4),
  (0, 4, 0b11111110),
  (5, 0b11111110, 0b11111110)
);

d8 = (
  (0, 2, 1),
  (3, 1, 4),
  (0, 3, 1),
  (3, 5, 4)
);

d9 = (
  (0, 2, 1),
  (0b11111111, 0b11111110, 0b11111111),
  (3, 5, 0b11111111),
  (3, 5, 4)
);

dd = (
  [0b11111110],
  [5],
  [2],
  [0b11111110]
);

ds = (
  [0b1111111110],
  [0b1111111110],
  [0b1111111110],
  [0b1111111110],
  [0b1111111110],
  [0b1111111110],
  [0b1111111110],
  [0b1111111110]
);

bigDigits = [d0, d1, d2, d3, d4, d5, d6, d7, d8, d9]

dayOfWeek = [ 
  ['M','o'],
  ['T','u'],
  ['W','e'],
  ['T','h'],
  ['F','r'],
  ['S','a'],
  ['S','u']
]

output = (
  list("                    "),
  list("                    "),
  list("                    "),
  list("                    ")
)

def putBigChar(output, c, offset):
  w = len(c[0])
  for y in range (0, 4):
    for x in range (0, w):
      output[y][x + offset] = unichr(c[y][x])

def putBigNumber(output, number, offset):
  putBigChar(output, bigDigits[(number / 10)], offset)
  putBigChar(output, bigDigits[(number % 10)], offset + 4)

def displayOutput(output):
  for x in range(0,4):
    lcd.cursor_pos = (x,0)
    lcd.write_string("".join(output[x])) 

def displayCurrentTime(output):
  t = datetime.today()

  putBigNumber(output, t.hour,  0)
  putBigNumber(output, t.minute, 10)

  o = ord('0')
  output[3][18] = chr(o + t.second / 10)
  output[3][19] = chr(o + t.second % 10)
  output[1][18] = chr(o + t.day / 10)
  output[1][19] = chr(o + t.day % 10)
  d = t.weekday()
  output[0][18] = dayOfWeek[d][0]
  output[0][19] = dayOfWeek[d][1]
  displayOutput(output)
  if t.second % 2 == 0:
    putBigChar(output, dd, 8)
  else:
    putBigChar(output, ds, 8)


GPIO.setup(LED_ON, GPIO.OUT)
GPIO.output(LED_ON, True)

while True:
  displayCurrentTime(output)
  time.sleep(1)

