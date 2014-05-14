using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;



public class UDPClient : MonoBehaviour {
	//thread object
	Thread receiverThread;
	//udpclient object
	UdpClient client;
	
	//port to listen
	public int port = 1600;
	//ip address to listen
	public string ipAddress = "127.0.0.1";
	//text field to set the port
	public string portTxtField;
	//the current message received
	public string currentMessage;
	//the last message received
	public string lastMessageReceived;

	public Boolean camina_usuario;
	
	//this function is infinity loop
	void Update(){
		//to avoid duplicate message received and just
		//change the text object in case the message is different
		//if (!lastMessageReceived.Equals(currentMessage)) {
		//
		//if (lastMessageReceived == "asap") {
		//	camina_usuario = true;
		//	Debug.Log (lastMessageReceived.ToString ());

		//} else {
	//		camina_usuario = false;
	//	}
		//output_txt.text = lastMessageReceived;
		//Debug.Log(output_txt.text);
		currentMessage = lastMessageReceived;
		//}
	}

	void Start(){
		init();
	}
	/*
	//Function to setting up the GUI
	void OnGUI(){
		//Starting the string with default message
		//lastMessageReceived = "No message received yet.";
		//set the default message in the object text
		//output_txt.text = lastMessageReceived;
		//set the default message in the current message
		//currentMessage = lastMessageReceived;
		
		//Set the GUI horizontal to add the components
		GUILayout.BeginHorizontal();
		//add a label with Ip address as static text
		GUILayout.Label("Ip address: ");
		//Add a text field with the ipAddress
		ipAddress = GUILayout.TextField(ipAddress);
		//Add a label with port as static text
		GUILayout.Label("Port: ");
		//add a text field with the port
		portTxtField = GUILayout.TextField(port.ToString());
		
		//In case of click Start Listening button 
		if(GUILayout.Button("Start listening")){
			//Print out the ip address and current port setted
			print("It's about to connect to this ip Address:"+ipAddress+" and port: "+portTxtField);
			//call init function

		}
		//In case of click disconnect
		if(GUILayout.Button("Disconnect")){
			//kill the thread added
			stopListenning();
		}
	}
	*/
	
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
		//Parse the port from the text field of port
		port = int.Parse (portTxtField);
		//Initialize UDPClient with the port parsed
		client = new UdpClient(port);
		//Just declaring the byte array
		byte [] data;
		//infinite loop
		while(true){
			try {
				//debug
				print("Updating data");
				//initilizing the end point object which contains the ipaddress and port to listen
				IPEndPoint ipEndPointObj = new IPEndPoint(IPAddress.Parse(ipAddress), port);
				//debug
				print("After IPEndPoint");
				//Received the data from the ipendpoint object
				data = client.Receive(ref(ipEndPointObj));
				//debug
				print("Taking message");
				//Getting the data from the bytes array
				lastMessageReceived =  System.Text.Encoding.ASCII.GetString(data, 0, data.Length);
				//debug
				//print(lastMessageReceived);
				//To handle errors
			} catch (Exception e){
				//debug
				print(e.ToString());
			}
		}
	}
	
	
}