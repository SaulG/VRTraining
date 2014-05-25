using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;



public class UDPClient : MonoBehaviour {

	private static int puerto = 1600;
	private static string direccionIp = "127.0.0.1";

	private Thread hiloUdp;
	private UdpClient clienteUdp;
	private string mensaje;

	private string orientacion;
	private bool camina;
	private bool levantaMano;
	private bool usuarioDetectado;

	public UDPClient(){

		this.orientacion = "neutral";
		this.camina = false;
		this.levantaMano = false;
		this.mensaje = "";
		init();
	}


	public void actualizaDatosUsuario(){

		if (mensaje != null) {

			string[] datosCSV = mensaje.Split(","[0]);

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


			if (datosCSV [5] == "1") {
				asignaCamina(true);
			} else {
				asignaCamina(false);
			}


			if (datosCSV[6] == "1"){
				asignaLevantaMano(true);
			}else{
				asignaLevantaMano(false);
			}


		}

		Debug.Log(mensaje);
	}

	//Initialize the thread to run in background
	public void init(){
		//initialize the thread and set the function updateReceivedData
		//As a task for the thread
		hiloUdp = new Thread(new ThreadStart(updateReceivedData));
		//Enable background running
		hiloUdp.IsBackground = true;
		//Start the thread
		hiloUdp.Start();
		//debug
		print ("Comenzo el hilo");
	}
	
	//Stop the thread
	public void stopListenning(){
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
				
				actualizaDatosUsuario();
				//To handle errors
			} catch (Exception e){
				//debug
				print(e.ToString());
			}
		}
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
		return this.camina;
	}
	public string obtenerOrientacion(){
		return orientacion;
	}

}