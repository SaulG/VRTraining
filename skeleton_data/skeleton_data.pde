//importing the library SimpleOpenNI
import SimpleOpenNI.*;

//Create kinect object
SimpleOpenNI  kinect;

//settings of the sketch
void setup() {

//initialice the kinect values
  kinect = new SimpleOpenNI(this);

//enable the depth camera
  kinect.enableDepth(); 

//Enable tracking the skeleton user
 kinect.enableUser(SimpleOpenNI.SKEL_PROFILE_ALL);

//set the size of the window 
  size(640, 480);
}


void draw() {
  
  //get current information from the kinect
  kinect.update();
  
  /*
  get the depth data image from the kinect
  and set it in the window using 0,0 coordinates
  */
  image(kinect.depthImage(), 0, 0);

  /*
  Initialice a IntVector to store the list of     users to be track.
  */
  IntVector userList = new IntVector();
 
  //set the users tracked in the user's list
  kinect.getUsers(userList);

  //verify if at least one user is tracked
  if (userList.size() > 0) {

  //if is tracked then get the first user
    int userId = userList.get(0);

  /*
    verify ifthe kinect is tracking the skeleton     of the specific user tracked
    */
    if ( kinect.isTrackingSkeleton(userId)) {
      
      //then get and draw the skeleton data
      drawSkeleton(userId);
    }
  }
}

//function to get and draw the skeleton data
void drawSkeleton(int userId) {
  //set black color to the line to draw
  stroke(0);
  //set the weight of the line to draw
  strokeWeight(5);
 
  /*
  drawLimb() takes as parameters the user id 
  from the list of users to get the skeleton id
  draw line "connection" between the head point
  detected and the neck point detected
  */
  
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_HEAD, SimpleOpenNI.SKEL_NECK);
  
  /*
  the same but now connecting neck to the left
  shoulder
  */
  
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_NECK, SimpleOpenNI.SKEL_LEFT_SHOULDER);
  
  //and so on ...
  
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_SHOULDER, SimpleOpenNI.SKEL_LEFT_ELBOW);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_ELBOW, SimpleOpenNI.SKEL_LEFT_HAND);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_NECK, SimpleOpenNI.SKEL_RIGHT_SHOULDER);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_SHOULDER, SimpleOpenNI.SKEL_RIGHT_ELBOW);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_ELBOW, SimpleOpenNI.SKEL_RIGHT_HAND);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_SHOULDER, SimpleOpenNI.SKEL_TORSO);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_SHOULDER, SimpleOpenNI.SKEL_TORSO);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_TORSO, SimpleOpenNI.SKEL_LEFT_HIP);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_HIP, SimpleOpenNI.SKEL_LEFT_KNEE);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_LEFT_KNEE, SimpleOpenNI.SKEL_LEFT_FOOT);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_TORSO, SimpleOpenNI.SKEL_RIGHT_HIP);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_HIP, SimpleOpenNI.SKEL_RIGHT_KNEE);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_KNEE, SimpleOpenNI.SKEL_RIGHT_FOOT);
  kinect.drawLimb(userId, SimpleOpenNI.SKEL_RIGHT_HIP, SimpleOpenNI.SKEL_LEFT_HIP);

  //disable the lines drawing
  noStroke();
  
  //fill the figures that will be draw with red color
  
  fill(255,0,0);
  
  /*
  draw a circle in each joint
  using as parameter the user id and 
  the constant to get the coordinates to
  be draw
  */
  drawJoint(userId, SimpleOpenNI.SKEL_HEAD);
  drawJoint(userId, SimpleOpenNI.SKEL_NECK);
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_SHOULDER);
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_ELBOW);
  drawJoint(userId, SimpleOpenNI.SKEL_NECK);
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_SHOULDER);
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_ELBOW);
  drawJoint(userId, SimpleOpenNI.SKEL_TORSO);
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_HIP);  
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_KNEE);
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_HIP);  
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_FOOT);
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_KNEE);
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_HIP);  
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_FOOT);
  drawJoint(userId, SimpleOpenNI.SKEL_RIGHT_HAND);
  drawJoint(userId, SimpleOpenNI.SKEL_LEFT_HAND);
}

//draw joints
void drawJoint(int userId, int jointID) {
  //create vector object
  PVector joint = new PVector();
/*
if confidence is 1 the data from the point tracked is full trusted but if the value is 0 that means that the algorithm that the library is using is guessing the position of that point. Uses as parameter the user id, the constant of the joint to track and the vector to save the coordinates of the join
*/
float confidence = kinect.getJointPositionSkeleton(userId, jointID, joint);
  //if is not trusted, don't bother to draw it
  if(confidence < 0.5){
    return;
  }
  //create a vector variable
  PVector convertedJoint = new PVector();
  /*
  get the current location of the joint track 
  in the depth image using convertedJoint to save it
  */
  kinect.convertRealWorldToProjective(joint, convertedJoint);
  
  /*
  draw a ellipse there with the coordinates and height and width
  */
  ellipse(convertedJoint.x, convertedJoint.y, 5, 5);
}

// user-tracking callbacks!
void onNewUser(int userId) {
  println("start pose detection");
  kinect.startPoseDetection("Psi", userId);
}

void onEndCalibration(int userId, boolean successful) {
  if (successful) { 
    println("  User calibrated !!!");
    kinect.startTrackingSkeleton(userId);
  } 
  else { 
    println("  Failed to calibrate user !!!");
    kinect.startPoseDetection("Psi", userId);
  }
}

void onStartPose(String pose, int userId) {
  println("Started pose for user");
  kinect.stopPoseDetection(userId); 
  kinect.requestCalibrationSkeleton(userId, true);
}
