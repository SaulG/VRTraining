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
	//the current message received
	private string currentMessage;
	//the last message received
	private string lastMessageReceived;

	public Boolean camina_usuario;

	//this function is infinity loop
	void Update(){
		if (lastMessageReceived == "camina") {
			camina_usuario = true;
			Debug.Log (lastMessageReceived);
		} else {
			camina_usuario = false;
			Debug.Log (lastMessageReceived);
		}
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