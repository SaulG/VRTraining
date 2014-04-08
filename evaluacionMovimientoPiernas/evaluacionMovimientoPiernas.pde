import SimpleOpenNI.*;

SimpleOpenNI context;


void setup(){
  //dimensiones de la ventana a crearse
  size(640, 480);
  
  //inicializa objeto que obtiene informacion del kinect
  context = new SimpleOpenNI(this);
  
  //verifica conexion a kinect
  if(context.isInit() == false){
    println("No se puede inicializar el programa, verifique conexion con el Kinect.");
    exit();
    return;
  }  
  
  //habilita la generacion del mapa de profundidad
  context.enableDepth();
  
  //habilita la generacion de las uniones del esqueleto
  context.enableUser();
  
  //color de fondo de la ventana
  background(200, 0, 0);
 
}


void draw() {
   //actualiza la informacion de la camara del kinect
  context.update();
 
  //guarda la imagen del ususario en una imagen
  image(context.userImage(), 0, 0);
 
  //dibuja el esqueleto si esta disponible 
  //dada una lista de usuarios detectados
  int[] listaUsuarios = context.getUsers();
  for(int i = 0; i < listaUsuarios.length; i++){
     
    if(context.isTrackingSkeleton(listaUsuarios[i])){
       //dibuja el esqueleto del usuario detectado
       dibujaEsqueleto(listaUsuarios[i]);
     }        
     
  }
}

//dibuja las uniones del esqueleto
void dibujaEsqueleto(int usuarioId){
  //vectores para las rodillas
  PVector rodillaDerecha = new PVector();
  PVector rodillaIzquierda = new PVector();
  
  //obtener la posicion de las uniones de los esqueletos
  context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_RIGHT_KNEE, rodillaDerecha);
  context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_LEFT_KNEE, rodillaIzquierda);
  
  //imprime el vector de las rodillas
  println("Rodilla derecha: "+rodillaDerecha);
  println("Rodilla izquierda: "+rodillaIzquierda);
  
  
  //vectores de las rodillas en 2D
  PVector rodillaDerecha2D = new PVector();
  PVector rodillaIzquierda2D = new PVector();
  
  //obtiene la proyeccion en 2D de las uniones
  context.convertRealWorldToProjective(rodillaDerecha, rodillaDerecha2D);
  context.convertRealWorldToProjective(rodillaIzquierda, rodillaIzquierda2D);
  
  //imprime los vectores en 2D
  println("Rodilla derecha 2D: "+rodillaDerecha2D);
  println("Rodilla izquierda 2D: "+rodillaIzquierda2D);
  
  

  context.drawLimb(usuarioId, SimpleOpenNI.SKEL_RIGHT_KNEE, SimpleOpenNI.SKEL_RIGHT_FOOT);
  context.drawLimb(usuarioId, SimpleOpenNI.SKEL_LEFT_KNEE, SimpleOpenNI.SKEL_LEFT_FOOT);

}


//***************************
//* Eventos de SimpleOpenNI *
//***************************

void onNewUser(SimpleOpenNI nuevoUsuarioContext, int usuarioId){
  println("Nuevo usuario ha sido detectado con id: "+usuarioId);
  println("Comenzando a realizar seguimiento del esqueleto");
  
  //comenzando el seguimiento del esqueleto 
  nuevoUsuarioContext.startTrackingSkeleton(usuarioId);
}

void onLostUser(SimpleOpenNI nuevoUsuarioContext, int usuarioId){
  println("Se perdio el seguimiento del usuario con id: "+usuarioId);
}

void onVisibleUser(SimpleOpenNI nuevoUsuarioContext, int usuarioId){
  //  Mucho spam en el log, es bueno para debugear si el usuario sigue ahi
  //  println("El usuario sigue ahi id: "+usuarioId);
}

//Buena manera de crear mis funciones personalizadas
//para cambiar configuracion mientras se usa este programa
void keyPressed(){
}
