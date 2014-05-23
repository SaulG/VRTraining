using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : VRGUI {

	public static bool DEBUGEANDO = true;

	public static float COOLDOWN_MENU = 2;

	private List<string> colores = new List<string>{"rojo", "amarillo", "verde", "azul"};
	
	private Vector3 posicionInicial;

	private int indexColoresVisitados;

	private float ultimoCambioEstado;


	
	public GUIStyle cuerpoEstilo;
	public GUIStyle tituloEstilo;
	public GUIStyle amarilloEstilo;
	public GUIStyle rojoEstilo;
	public GUIStyle azulEstilo;
	public GUIStyle verdeEstilo;
	
	public string instrucciones_titulo = "Instrucciones:"; 
	public string instrucciones_cuerpo = "Visitar cada una de las areas correspondientes según su color y orden en el que son presentados:";

	public bool state_gui = true;

	public void Start(){
		indexColoresVisitados = 0;
		ultimoCambioEstado = COOLDOWN_MENU;
		GameObject jugador = GameObject.Find ("OVRPlayerController");
		posicionInicial = jugador.transform.position;
		colores = randomizeList ();
	}

	public void Update(){
		if (Input.GetKeyDown ("y"))
						moverJugadorPosicionInicial ();
		ultimoCambioEstado += Time.deltaTime;
	}
	
	public List<string> randomizeList(){
		int n = colores.Count;
		while (n > 1){
			int r = ((int)Random.Range(0,n)) % n;
			n --;
			string aux = colores[r];
			colores[r] = colores[n];
			colores[n] = aux;
		}
		if (DEBUGEANDO) {
			Debug.Log ("Cantidad de colores: " + colores.Count);
			Debug.Log ("Color: "+colores[0]);
			Debug.Log ("Color: "+colores[1]);		
			Debug.Log ("Color: "+colores[2]);		
			Debug.Log ("Color: "+colores[3]);		
		}
		return colores;
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
		if (state_gui) {
						GUI.Box (new Rect (0, 0, 350, 100), "");
						GUI.Label (new Rect (30, 0, 160, 20), instrucciones_titulo, tituloEstilo);
						GUI.Label (new Rect (20, 30, 340, 100), instrucciones_cuerpo, cuerpoEstilo);
						GUI.Label (new Rect (20, 60, 350, 50), "1.-" + colores [0], obtenerEstilo (colores [0]));
						GUI.Label (new Rect (20, 80, 350, 50), "2.-" + colores [1], obtenerEstilo (colores [1]));
						GUI.Label (new Rect (100, 60, 350, 50), "3.-" + colores [2], obtenerEstilo (colores [2]));
						GUI.Label (new Rect (100, 80, 350, 50), "4.-" + colores [3], obtenerEstilo (colores [3]));
				}
	}

	public void changeInstructionsState(){
		if(ultimoCambioEstado >= COOLDOWN_MENU){
			state_gui = !state_gui;
			if (DEBUGEANDO) {
				Debug.Log ("CAMBIO DE ESTADO: " + state_gui);
			}
			ultimoCambioEstado = 0;
		}
		if (DEBUGEANDO) {
			Debug.Log ("DELTATIME: " + ultimoCambioEstado);
		}

	}




	public GUIStyle obtenerEstilo(string color){
		GUIStyle estilo = cuerpoEstilo;
		switch(color){
		case "azul":
			estilo = azulEstilo;
			break;
		case "amarillo":
			estilo = amarilloEstilo;
			break;
		case "rojo":
			estilo = rojoEstilo;
			break;
		case "verde":
			estilo = verdeEstilo;
			break;
	    }
		return estilo;
	}
}
