using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;



public class UDPClient : MonoBehaviour {
	//thread object
	private Thread receiverThread;
	//udpclient object
	private UdpClient client;
	
	//port to listen
	public static int port = 1600;
	//ip address to listen
	public static string ipAddress = "127.0.0.1";

	//the last message received
	private string lastMessageReceived;

	public Boolean camina_usuario;

	public Boolean derecha_usuario;

	public Boolean izquierda_usuario;
	
	//this function is infinity loop
	void Update(){
		if (lastMessageReceived != null) {
				string[] datosCSV = lastMessageReceived.Split(","[0]);
				if (datosCSV [1] == "0" && (datosCSV [0] == "1" || datosCSV [2] == "1")) {
						if (datosCSV [0] == "1") {
								derecha_usuario = true;
								izquierda_usuario = false;
						}
						if (datosCSV [2] == "1") {
								izquierda_usuario = true;
								derecha_usuario = false;
						}
				} else {
						izquierda_usuario = false;
						derecha_usuario = false;
				}
				if (datosCSV [4] == "1") {
						camina_usuario = true;
				} else {
						camina_usuario = false;
				}
		}
		Debug.Log(lastMessageReceived);
	}

	void Start(){
		init();
	}
	//Initialize the thread to run in background
	void init(){
		//debug
		print("Running");
		//initialize the thread and set the function updateReceivedData
		//As a task for the thread
		receiverThread = new Thread(new ThreadStart(updateReceivedData));
		//Enable background running
		receiverThread.IsBackground = true;
		//Start the thread
		receiverThread.Start();
		//debug
		print ("Already start thread");
	}
	
	//Stop the thread
	void stopListenning(){
		if (receiverThread != null){
			receiverThread.Abort();
		}
	}
	
	//Run the UDP client
	private void updateReceivedData(){
		//Debug
		print("Starting updating data");
		//Initialize UDPClient with the port parsed
		client = new UdpClient(port);
		//Just declaring the byte array
		byte [] data;
		//infinite loop
		while(true){
			try {
				//initilizing the end point object which contains the ipaddress and port to listen
				IPEndPoint ipEndPointObj = new IPEndPoint(IPAddress.Parse(ipAddress), port);
				//Received the data from the ipendpoint object
				data = client.Receive(ref(ipEndPointObj));
				//Getting the data from the bytes array
				lastMessageReceived =  System.Text.Encoding.ASCII.GetString(data, 0, data.Length);
				//To handle errors
			} catch (Exception e){
				//debug
				print(e.ToString());
			}
		}
	}
	
	
}