#pragma strict

function Start () {

}

function Update () {
	//si el jugador presiona "t" llama la funcion ToggleTrigger
	if(Input.GetKeyDown("t")) ToggleTrigger();
}

function ToggleTrigger() {
	if (collider.isTrigger){
		collider.isTrigger = false;
		Debug.Log("Trigger is false");
	}else{
		collider.isTrigger = true;
		Debug.Log("Trigger is true");
	}
}