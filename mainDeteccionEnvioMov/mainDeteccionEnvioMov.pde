/*
Envio de mensajes de deteccion de orientacion y pasos

*/

import hypermedia.net.*;
import SimpleOpenNI.*;

public SimpleOpenNI context;

//ultimos valores de las coordenadas de rodilla derecha e izquierda
public PVector ultimoRodillaDerecha;
public PVector ultimoRodillaIzquierda;


//umbral para determinar movimiento aceptable entre coordenadas
public static final int UMBRAL = 1;
public static final float CONFIDENCIA = 0.85;

// the remote IP address
public static final String ip = "127.0.0.1";

// the destination port
public static final int port = 1600;  
     
public UDP udp; 
public String camina_msj;
public String orientacion_msj;
public String mano_msj;
public String usuarioDetectado_msj;

void setup(){
  udp = new UDP( this, 6000 );
  udp.log( true );     // <-- printout the connection activity
  udp.listen( true );
  //inicializa objeto que obtiene informacion del kinect
  context = new SimpleOpenNI(this);
  
  //initializa los objetos vectores
  ultimoRodillaDerecha = new PVector();
  ultimoRodillaIzquierda = new PVector();

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
         orientacion_msj = orientacionTorso(listaUsuarios[i]);
         camina_msj = deteccionCamina(listaUsuarios[i]);
         mano_msj = deteccionMano(listaUsuarios[i]);
         udp.send(usuarioDetectado_msj+','+orientacion_msj+','+camina_msj+','+mano_msj,ip, port);
      }        
       
    }
   
 }
  

//obtiene la orientacion del torse
String orientacionTorso(int usuarioId){
 PVector position = new PVector();
 String mensaje = "0,0,0,0";
 
 context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_TORSO, position);

 PMatrix3D orientation = new PMatrix3D();
 float confidence = context.getJointOrientationSkeleton(usuarioId, SimpleOpenNI.SKEL_TORSO, orientation);
 if(confidence > CONFIDENCIA){  
   float grados = degrees(atan2( sqrt( (orientation.m21 * orientation.m21) + (orientation.m22 * orientation.m22) ), (orientation.m20 * -1) )); 
    
   if(grados < 70.0){
       mensaje = String.format("0,0,1,%.2f", grados);
   }else if(grados > 110.0){
      mensaje = String.format("1,0,0,%.2f", grados);
   }else{
      mensaje = String.format("0,1,0,%.2f", grados);
   }
 }
 return mensaje;
}

String deteccionMano(int usuarioId){
  PVector cuello = new PVector();
  PVector manoDerecha = new PVector();
  String mensaje = "0";
  float confidence_cuello = 0;
  float confidence_mano = 0;
  confidence_cuello = context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_NECK, cuello);
  confidence_mano = context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_LEFT_HAND, manoDerecha);
  if((confidence_cuello > CONFIDENCIA) && (confidence_mano > CONFIDENCIA)){
     //vectores de las rodillas en 2D
    PVector cuello2D = new PVector();
    PVector manoDerecha2D = new PVector();
 
    //obtiene la proyeccion en 2D de las uniones
    context.convertRealWorldToProjective(manoDerecha, manoDerecha2D);
    context.convertRealWorldToProjective(cuello, cuello2D);
      if(cuello2D.y >= manoDerecha2D.y){
         //imprime camina
         mensaje = "1";
      }
  }
  return mensaje;
}


//detecta si camina
String deteccionCamina(int usuarioId){
  //vectores para las rodillas
  PVector rodillaDerecha = new PVector();
  PVector rodillaIzquierda = new PVector();
  String mensaje = "0";
  float confidence_rD = 0;
  float confidence_rI = 0;
  
  //Tuve que ver el codigo fuente para entender algunas
  //funciones debido a que no esta completamente bien documentado
  //https://code.google.com/p/simple-openni/source/browse/trunk/SimpleOpenNI-2.0/src/p5_src/SimpleOpenNI.java?r=440
  
  //obtener la posicion de las uniones de los esqueletos
  confidence_rD = context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_RIGHT_KNEE, rodillaDerecha);
  confidence_rI = context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_LEFT_KNEE, rodillaIzquierda);
  if((confidence_rD > CONFIDENCIA) || (confidence_rI > CONFIDENCIA)){
  
    //vectores de las rodillas en 2D
    PVector rodillaDerecha2D = new PVector();
    PVector rodillaIzquierda2D = new PVector();
    boolean movRodDerecha;
    boolean movRodIzquierda;
    
    //obtiene la proyeccion en 2D de las uniones
    context.convertRealWorldToProjective(rodillaDerecha, rodillaDerecha2D);
    context.convertRealWorldToProjective(rodillaIzquierda, rodillaIzquierda2D);
    
   
    //verifica que no sean nulos los ultimos valores obtenidos  
    if( (ultimoRodillaDerecha != null) && (ultimoRodillaIzquierda != null) ){
      
      if (((rodillaDerecha2D.y + UMBRAL) < rodillaIzquierda2D.y) || ((rodillaIzquierda2D.y + UMBRAL) < rodillaDerecha2D.y)){
                    //imprime camina
                    mensaje = "1";
        }
    }
    //guarda los ultimos valores obtenidos de las rodillas
    ultimoRodillaDerecha = rodillaDerecha2D;
    ultimoRodillaIzquierda = rodillaIzquierda2D;
    
    //dibuja una linea con la union de la rodilla y el pie en ambos lados
    //context.drawLimb(usuarioId, SimpleOpenNI.SKEL_RIGHT_KNEE, SimpleOpenNI.SKEL_RIGHT_FOOT);
    //context.drawLimb(usuarioId, SimpleOpenNI.SKEL_LEFT_KNEE, SimpleOpenNI.SKEL_LEFT_FOOT);
    }
    return mensaje;
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
  usuarioDetectado_msj = "0";
}

void onVisibleUser(SimpleOpenNI nuevoUsuarioContext, int usuarioId){
  //  Mucho spam en el log, es bueno para debugear si el usuario sigue ahi
  //  println("El usuario sigue ahi id: "+usuarioId);
  usuarioDetectado_msj = "1";
}


