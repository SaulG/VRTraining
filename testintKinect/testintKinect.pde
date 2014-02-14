import SimpleOpenNI.*;


SimpleOpenNI  context;

void setup()
{
  //window proportion
  size(640, 480);
  //contain all related with the kinect's data
  context = new SimpleOpenNI(this);
  //Check if the camera is connected
  if (context.isInit() == false)
  {
    println("Can't init SimpleOpenNI, maybe the camera is not connected!"); 
    exit();
    return;
  }

  // mirror is by default enabled
  context.setMirror(true);

  // enable depthMap generation 
  context.enableDepth();
}

void draw()
{
  // update the cam
  context.update();
  // background color
  background(200, 0, 0);

  // draw depthImageMap
  image(context.depthImage(), 0, 0);
}

