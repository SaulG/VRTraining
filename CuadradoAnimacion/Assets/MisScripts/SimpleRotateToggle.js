#pragma strict

internal var rotate : boolean = false;

function Update() {
	Debug.Log(Time.deltaTime);
	if(rotate){
		transform.Rotate(0, 20 * Time.deltaTime, 0);
	}
}

function OnMouseDown () {
	rotate = rotate? false : true;
}