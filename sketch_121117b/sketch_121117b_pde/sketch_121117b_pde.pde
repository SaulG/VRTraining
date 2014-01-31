import oscP5.*;
import netP5.*;
import processing.serial.*;
import java.awt.*;

OscP5 oscP5;

NetAddress myBroadcastLocation; 

Panel panel1 = new Panel();

TextField addressTextField = new TextField("/test", 16);
TextField dataTextField = new TextField("3", 16);
  
void setup() {
  //size of the window
  size(400,140); 
  //initialize the oscP5 and the broadcast data to port 12000
  //this is where unity will listen to
  oscP5 = new OscP5(this, 12000);
  //broadcastLocation to liston to on port 8000
  myBroadcastLocation = new NetAddress("127.0.0.1",8000);
  
  //to add in the window a panel
  add(panel1);
  //to add in the window textfields
  add(addressTextField);
  add(dataTextField);
  
  //settings of the panel
  panel1.setLayout(new BorderLayout()); 
  panel1.setBounds(150,100,50,50); 
   
} 


//This function is to check if the click in the window is inside in a certain range
//of pixels where is the send button
boolean overButton(int x, int y, int width, int height) 
{
  if (mouseX >= x && mouseX <= x+width && 
      mouseY >= y && mouseY <= y+height) {
    return true;
  } else {
    return false;
  }
}

void draw() {
  //black background
  background(0,0,0);
  
  //fill the rect with gray color
  fill(33, 33,33);
  rect(140, 40, 115, 35);
  
  //fill the texts with white color
  fill(255, 255,255);
  //set text "Send" in a specific location and dimension
  text("Send",150, 50, 200, 70); 
  //set text "Sent" and take the value of dataTextField 
  //in a specific location and dimension
  text("Sent: " + dataTextField.getText(),100, 100, 200, 70); 
}

//This function is trigger when the mouse or track pad are clicked
void mousePressed() {
   if( overButton(140, 40, 115, 35)) {
     sendEvent(addressTextField.getText(),dataTextField.getText());
   }
}


//Send to a specific address the data using Osc protocol
void sendEvent(String address, String data) {
  //set the address
  OscMessage myOscMessage = new OscMessage(address); 
  //set the data 
  myOscMessage.add(data);
  //send the package
  oscP5.send(myOscMessage, myBroadcastLocation);
}

