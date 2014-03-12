#pragma strict

var myDegrees = 200;

function Update () {
	transform.Rotate(0,myDegrees* Time.deltaTime,0);
}