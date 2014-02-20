import SimpleOpenNI.*;
SimpleOpenNI  kinect;

void setup()
{
  //size of the window
  size(640, 480);
  //object kinect
  kinect = new SimpleOpenNI(this);
  //enable the depth feature of the kinect
  kinect.enableDepth();  
}

void draw()
{
  //reload data from kinect
  kinect.update();

  //save image from the depth image of the kinect
  PImage depthImage = kinect.depthImage();
 
  //set the image in the window
  image(depthImage, 0, 0);
}

void mousePressed(){
  //get unidimensional array of all the millimeters values where
  //each value is the distance between the kinect and the object 
  //in front of the camera and its represented as the size of the image 640 * 480
  int[] depthValues = kinect.depthMap();
  //get the index of the unidimensional array
  int clickPosition = mouseX + (mouseY * 640);
  //get the millimeters value 
  int mm = depthValues[clickPosition];
  //print out the millimeters value from the pixel selected
  //this value is what the kinect receives using IR camera
  println("mm: " + mm/10);
}
