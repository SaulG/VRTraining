//libreria de simpleopenni
import SimpleOpenNI.*;

SimpleOpenNI  kinect;

//preferencias antes de iniciar
void setup()
{
  //proporcion de la ventana
  size(640, 480);
  
  //contiene todo lo relacionado con los datos del kinect
  kinect = new SimpleOpenNI(this);
  
  //verifica que la camara este conectada
  if (kinect.isInit() == false)
  {
    //en caso de no estar conectada salir del programa
    println("Es posible que la camara no este conectada."); 
    exit();
    return;
  }
  
  //habilita el mapa de profundidad
  kinect.enableDepth();  
}

void draw()
{
  //actualiza datos del kinect
  kinect.update();

  //guardar la imagen del mapa de profundidad 
  PImage depthImage = kinect.depthImage();
 
  //colocar la imagen de profundidad en x: 0  y: 0
  image(depthImage, 0, 0);
}

//al presionar el raton
void mousePressed(){
  //vector de enteros con valor de la distancia de cada pixel
  //obtenido por el sensor infrarrojo
  
  //el tamanio del vector es de 307,200 pixeles
  int[] depthValues = kinect.depthMap();
  
  //obtener el index del vector en la imagen
  int clickPosition = mouseX + (mouseY * 640);
  //obtiene el valor de los milimetros
  int mm = ((depthValues[clickPosition])/10);
  //imprimie la cantidad de milimetros
  println("mm: " + mm);
}
