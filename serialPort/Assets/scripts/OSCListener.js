private var UDPHost : String = "127.0.0.1";
private var listenerPort : int = 8000;
private var broadcastPort : int = 57131;
private var oscHandler : Osc;

private var address : String = "";
private var data : String = "";
public var output_txt : GUIText;


public function Start ()
{	
	var udp : UDPPacketIO = GetComponent("UDPPacketIO");
	udp.init(UDPHost, broadcastPort, listenerPort);
	oscHandler = GetComponent("Osc");
	oscHandler.init(udp);
			
	oscHandler.SetAddressHandler("/test", updateText);
		
}
Debug.Log("Running");

function Update () {
	output_txt.text = "Address: " + address + " Data: " + data;	
}	

public function updateText(oscMessage : OscMessage) : void
{	
	address = Osc.OscMessageToString(oscMessage);
	data = oscMessage.Values[0];
} 