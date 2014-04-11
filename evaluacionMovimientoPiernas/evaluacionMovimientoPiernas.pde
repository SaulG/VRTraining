import SimpleOpenNI.*;

SimpleOpenNI context;

//ultimos valores de las coordenadas de rodilla derecha e izquierda
PVector ultimoRodillaDerecha;
PVector ultimoRodillaIzquierda;

//umbral para determinar movimiento aceptable entre coordenadas
public static final int UMBRAL = 3;

void setup(){
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
}

//dibuja las uniones del esqueleto
void dibujaEsqueleto(int usuarioId){
  //vectores para las rodillas
  PVector rodillaDerecha = new PVector();
  PVector rodillaIzquierda = new PVector();
  
  //Tuve que ver el codigo fuente para entender algunas
  //funciones debido a que no esta completamente bien documentado
  //https://code.google.com/p/simple-openni/source/browse/trunk/SimpleOpenNI-2.0/src/p5_src/SimpleOpenNI.java?r=440
  
  //obtener la posicion de las uniones de los esqueletos
  context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_RIGHT_KNEE, rodillaDerecha);
  context.getJointPositionSkeleton(usuarioId, SimpleOpenNI.SKEL_LEFT_KNEE, rodillaIzquierda);
  
  //imprime el vector de las rodillas
  println("Rodilla derecha: "+rodillaDerecha);
  println("Rodilla izquierda: "+rodillaIzquierda);
  
  
  //vectores de las rodillas en 2D
  PVector rodillaDerecha2D = new PVector();
  PVector rodillaIzquierda2D = new PVector();
  boolean movRodDerecha;
  boolean movRodIzquierda;
  
  //obtiene la proyeccion en 2D de las uniones
  context.convertRealWorldToProjective(rodillaDerecha, rodillaDerecha2D);
  context.convertRealWorldToProjective(rodillaIzquierda, rodillaIzquierda2D);
  
  //imprime los vectores en 2D
  println("Rodilla derecha 2D: "+rodillaDerecha2D);
  println("Rodilla izquierda 2D: "+rodillaIzquierda2D);
  
 
  //verifica que no sean nulos los ultimos valores obtenidos  
  if( (ultimoRodillaDerecha != null) && (ultimoRodillaIzquierda != null) ){
    
     /*
      Verifica que el ultimo valor x de la rodilla mas UMBRAL sea menor igual al valor actual de la rodilla derecha
      o que el ultimo valor x de la rodilla menos el UMBRAL sea mayor igual al valor actual de la rodilla izquierda
      
      Ejemplo:
      
      Cuando hay movimiento en x 
      
      ultimo valor x = 516
      actual valor x = 521
      umbral = 3
                          (519 <= 521) -> TRUE
                          (513 >= 521) -> FALSE
      
       Cuando hay movimiento en x

       ultimo valor x = 516
  `    actual valor x = 510
       umbral = 3
                          (519 <= 510) -> FALSE
                          (513 >= 510) -> TRUE
               
      Cuando no hay movimiento en x   
      
      ultimo valor x = 518
      actual valor x = 518
      umbral = 3                    
      
                          (521 <= 518) -> FALSE
                          (515 >= 520) -> FALSE
   
    Lo mismo pasa con la coordenada y.            
    */
  
    if ((((ultimoRodillaDerecha.x + UMBRAL) <= rodillaDerecha2D.x) || 
        ( (ultimoRodillaDerecha.x - UMBRAL) >= rodillaDerecha2D.x)) && 
        (((ultimoRodillaDerecha.y + UMBRAL) <= rodillaDerecha2D.y) || 
        ( (ultimoRodillaDerecha.y - UMBRAL) >= rodillaDerecha2D.y))){
                  //imprime camina
                  //println("************************CAMINA*****************************");
                  text("SE DETECTO PASO", 10, 150);
            }else{
              text("NO SE DETECTO NADA", 10, 80);
            }
  }
  
  //guarda los ultimos valores obtenidos de las rodillas
  ultimoRodillaDerecha = rodillaDerecha2D;
  ultimoRodillaIzquierda = rodillaIzquierda2D;
  
  //dibuja una linea con la union de la rodilla y el pie en ambos lados
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
