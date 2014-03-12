//This an script that checks the type of variable
//in case you declare a variable without the type
#pragma strict

//starting with a lower case word and the second with
// high case will show in the inspector "My Speed"
var mySpeed : float;

var someString : String = "This is a test";

internal var someSetting : boolean = true;

var someObject : GameObject;

function Update () {
	transform.Translate(0, 0.5* Time.deltaTime, 0);
}