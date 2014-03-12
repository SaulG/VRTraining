#pragma strict

var turn = 300;
var flag : boolean = true;
function Update () {
	if(Input.GetKeyDown(KeyCode.Space)){
		flag = flag? false : true;
	}
	if (flag){
			transform.Rotate(turn *Time.deltaTime, 0, 0);
		}
}