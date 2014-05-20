using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : VRGUI {
	private List<string> colores = new List<string>{"rojo", "amarillo", "verde", "azul"};
	private Vector3 posicionInicial;
	private int indexColoresVisitados = 0;

	public GUIStyle cuerpoEstilo;
	public GUIStyle tituloEstilo;
	

	public void Start(){
		GameObject jugador = GameObject.Find ("OVRPlayerController");
		posicionInicial = jugador.transform.position;
		Comienza ();
	}

	public void Update(){
		if (Input.GetKeyDown ("y"))
						moverJugadorPosicionInicial ();
	}

	public void Comienza(){
		List<string> colores_aux = new List<string>();
		int nmAleatorio;
		while(colores.Count == 0){
			nmAleatorio = (int)Random.Range(0,(colores.Count-1));
			colores_aux.Add(colores[nmAleatorio]);
			colores.RemoveAt(nmAleatorio);
		}
		Debug.Log("Cantidad de colores:"+colores_aux.Count);
	}

	public void verificaColor(string colorRecibido){
		if(colores[indexColoresVisitados] == colorRecibido){
			indexColoresVisitados += 1;
		} else {
			moverJugadorPosicionInicial();
		}
	}

	public void moverJugadorPosicionInicial(){
		GameObject jugador = GameObject.Find ("OVRPlayerController");
		jugador.transform.position = posicionInicial;
	}

	public override void OnVRGUI () {
		GUI.Box(new Rect (0,0,350,100), "");
		GUI.Label(new Rect (30,0,160,20), "Instrucciones:", tituloEstilo);
		GUI.Label(new Rect (10,30,350,100), "Tienes que ir a obtener estos colores.", cuerpoEstilo);
	}
	
}
