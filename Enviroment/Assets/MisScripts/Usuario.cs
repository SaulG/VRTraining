using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;



public class Usuario : MonoBehaviour {

	private static int puerto = 1600;
	private static string direccionIp = "127.0.0.1";
	private static string formato = "MM dd yyy HH:mm:ss.fff";
	private Thread hiloUdp;
	private UdpClient clienteUdp;
	private string mensaje;
	private string nombre_archivo;
	private string orientacion;
	private bool camina;
	private bool levantaMano;
	private bool usuarioDetectado;

	private DateTime obtieneDatos;
	private DateTime enviaDatos;
	private DateTime recibeDatosTiempo;
	private DateTime actualizaInformacion;

	private StreamWriter sw;

	public Usuario(){
		this.orientacion = "neutral";
		this.camina = false;
		this.levantaMano = false;
		this.mensaje = "";
		init();
	}


	private void actualizaDatosUsuario(){
		/*
						0   ,                1,                   2,               3,    4 ,     5,   6,  7
			usuarioDetectado, orientacion_izq, orientacion_neutral, orientacion_der, camina, mano, fecha obtiene datos, fecha envia datos
		*/
		if (obtenerMensaje() != null) {

			string[] datosCSV = obtenerMensaje().Split(","[0]);

			asignaUsuarioDetectado((datosCSV[0] == "1")? true : false);

			if (datosCSV [2] == "0" && (datosCSV [1] == "1" || datosCSV [3] == "1")) {

				if (datosCSV [1] == "1") {
					asignaOrientacion("derecha");
				}

				if (datosCSV [3] == "1") {
					asignaOrientacion("izquierda");
				}
			} else {
				asignaOrientacion("neutral");
			}


			if (datosCSV [4] == "1") {
				asignaCamina(true);
			} else {
				asignaCamina(false);
			}


			if (datosCSV[5] == "1"){
				asignaLevantaMano(true);
			}else{
				asignaLevantaMano(false);
			}
			
			obtieneDatos = DateTime.ParseExact(datosCSV[6], formato, null);
			enviaDatos = DateTime.ParseExact(datosCSV[7], formato, null);
			actualizaInformacion = DateTime.Now;
		}

		Debug.Log(mensaje);
	}

	//Initialize the thread to run in background
	private void init(){
		//initialize the thread and set the function updateReceivedData
		//As a task for the thread
		hiloUdp = new Thread(new ThreadStart(updateReceivedData));
		//Enable background running
		hiloUdp.IsBackground = true;
		//Start the thread
		hiloUdp.Start();
		//debug
		print ("Comenzo el hilo");
		DateTime hoy = DateTime.Now;
		string hoy_str = hoy.ToString ("dd-MMM-HH-mm-ss");
		nombre_archivo = "latencia-"+hoy_str+".txt";
		sw = new StreamWriter(nombre_archivo);
	}
	
	//Stop the thread
	public void stopListenning(){
		sw.Close();
		sw.Dispose ();
		if (hiloUdp != null){
			hiloUdp.Abort();
			hiloUdp = null;
		}
	}
	
	//Run the UDP client
	private void updateReceivedData(){
		//Debug
		print("Comenzando a actualizar la informacion");
		//Initialize UDPClient with the port parsed
		clienteUdp = new UdpClient(puerto);
		//Just declaring the byte array
		byte [] datos;
		//infinite loop
		while(true){
			try {
				//initilizing the end point object which contains the ipaddress and port to listen
				IPEndPoint ipEndPointObj = new IPEndPoint(IPAddress.Parse(direccionIp), puerto);
				//Received the data from the ipendpoint object
				datos = clienteUdp.Receive(ref(ipEndPointObj));
				//Getting the data from the bytes array
				mensaje = System.Text.Encoding.ASCII.GetString(datos, 0, datos.Length);
				print(mensaje);
				recibeDatosTiempo = DateTime.Now;
				actualizaDatosUsuario();
				seActualizaInformacion();
				//To handle errors
			} catch (Exception e){
				//debug
				print(e.ToString());
			}
		}
	}

	public void seActualizaInformacion(){
		//Debug.Log ("Se actualiza la informacion");
		sw.WriteLine(String.Format ("{0},{1},{2},{3}",obtieneDatos.ToString(formato), enviaDatos.ToString(formato), recibeDatosTiempo.ToString(formato), actualizaInformacion.ToString(formato)));
	}

	private void asignaCamina(bool camina){
		this.camina = camina;
	}

	private void asignaOrientacion(string orientacion){
		this.orientacion = orientacion;
	}

	private void asignaLevantaMano(bool levantaMano){
		this.levantaMano = levantaMano;
	}

	private void asignaMensaje(string mensaje){
		this.mensaje = mensaje;
	}

	public void asignaUsuarioDetectado(bool usuarioDetectado){
		this.usuarioDetectado = usuarioDetectado;
	}

	public bool obtenerLevantaMano(){
		return levantaMano;
	}

	public bool obtenerUsuarioDetectado(){
		return usuarioDetectado;
	}

	public bool obtenerCamina(){
		return camina;
	}
	public string obtenerOrientacion(){
		return orientacion;
	}

	public string obtenerMensaje(){
		return mensaje;
	}
}