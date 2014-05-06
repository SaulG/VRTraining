/*
Envio de mensajes de deteccion de orientacion y pasos

*/

import hypermedia.net.*;
import SimpleOpenNI.*;

SimpleOpenNI context;

//ultimos valores de las coordenadas de rodilla derecha e izquierda
PVector ultimoRodillaDerecha;
PVector ultimoRodillaIzquierda;

//umbral para determinar movimiento aceptable entre coordenadas
public static final int UMBRAL = 3;

UDP udp; 


void setup(){
  udp = new UDP( this, 6000 );
  udp.log( true );     // <-- printout the connection activity
  udp.listen( true );
  //dimensiones de la ventana a crearse
  size(640, 480);
  
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
  
  //color de fondo de la ventana
  background(200, 0, 0);
  textSize(30); 
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
    String message  = "asas";  // the message to send
    String ip       = "127.0.0.1";  // the remote IP address
    int port        = 1600;    // the destination port
    
    // formats the message for Pd
    message = message+";\n";
    // send the message
    udp.send( message, ip, port );
  }
