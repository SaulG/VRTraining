var sendToObject : GameObject;

function OnTriggerEvent (object : Collider){
	//call the function on the object
	sendToObject.SendMessage("ToggleTrigger");
}