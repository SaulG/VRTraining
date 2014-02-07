//Defining ip address
private var UDPHost : String = "127.0.0.1";
//port to listen
private var listenerPort : int = 8000;
//the port to broadcast
private var broadcastPort : int = 57131;
//osc object
private var oscHandler : Osc;

//Path or Address like a label that Osc handles as communication protocol
private var address : String = "";
//Reserved to use for the data received
private var data : String = "";
//Reference for the GUIText object defined in the Unity's IDE
public var output_txt : GUIText;


public function Start ()
{	
	//plugin reference UDPPacketIO.cs
	var udp : UDPPacketIO = GetComponent("UDPPacketIO");
	//initializing using local address, port to send packets and port o receive packets
	udp.init(UDPHost, broadcastPort, listenerPort);
	//plugin reference of Osc.cs
	oscHandler = GetComponent("Osc");
	//initilizing with udp object with methods to handle UDP protocol
	oscHandler.init(udp);
	//Setting a path and function while is running OSC		
	oscHandler.SetAddressHandler("/test", updateText);
		
}
//debug stuff
Debug.Log("Running");

function Update () {
	//Change text of output_txt
	output_txt.text = "Address: " + address + " Data: " + data;	
}	

public function updateText(oscMessage : OscMessage) : void
{	
	address = Osc.OscMessageToString(oscMessage);
	data = oscMessage.Values[0];
} 
