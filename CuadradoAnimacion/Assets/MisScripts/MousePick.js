#pragma strict

private var pickNum : int = 0;

function OnMouseDown () {
	pickNum += 1;
	Debug.Log("<color=green>Evento encontrado:</color> Este objeto ha sido seleccionado "+pickNum);
}