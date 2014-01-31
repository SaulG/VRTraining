import processing.serial.*;

Serial port; 
float val;
int x;
float easing = 0.05;
float easedVal;

void setup(){
 size(440, 440);
 frameRate(30);
 smooth();
 //String arduinoPort = Serial.list()[0];
 port = new Serial(this, "/dev/tty.usbmodemfa131", 9600);
 background(0);
}

void draw() {
  println(port.read());
  if ( port.available() > 0) {         // If data is available,
    val = port.read();                 // read it and store it in val
    val = map(val, 0, 255, 0, height); // Convert the values
  }
  float targetVal = val;
easedVal += (targetVal - easedVal) * easing;
     stroke(0);
     line(x, 0, x, height);
     stroke(255);
     line(x+1, 0, x+1, height);
     line(x, 220, x, val);
     line(x, 440, x, easedVal + 220);  // Averaged value
     x++;
     if (x > width) {
x = 0; 
}
delay(500);
}
