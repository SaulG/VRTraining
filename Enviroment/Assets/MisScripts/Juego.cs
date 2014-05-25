using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Juego : VRGUI {

	private static bool DEBUGEANDO = true;

	private static float ENFRIAMIENTO_MENU = 2;

	private float ultimoCambioEstado;

	private List<string> colores;
	private int coloresVisitados;

	private Vector3 posicionInicial;

	private GameObject jugador;
	
	public bool muestraInstrucciones;
	private bool dentroArea;
	
	public GUIStyle cuerpoEstilo;
	public GUIStyle tituloEstilo;
	public GUIStyle amarilloEstilo;
	public GUIStyle rojoEstilo;
	public GUIStyle azulEstilo;
	public GUIStyle verdeEstilo;
	public GUIStyle coloresVisitadosEstilo;
	
	public string mensajeInstruccionesTitulo;
	public string mensajeInstruccionesCuerpo;
	public string mensajeColoresVisitados;
	
	public void Start(){
		mensajeInstruccionesTitulo = "Instrucciones:"; 
		mensajeInstruccionesCuerpo = "Visitar cada una de las areas correspondientes según su color y orden en el que son presentados:";
		colores = new List<string>{"rojo", "amarillo", "verde", "azul"};
		coloresVisitados = 0;
		ultimoCambioEstado = ENFRIAMIENTO_MENU;
		jugador = GameObject.Find ("OVRPlayerController");
		posicionInicial = jugador.transform.position;
		colores = randomizeList ();
		muestraInstrucciones = true;
		dentroArea = false;

	}

	public void Update(){
		if (Input.GetKeyDown ("y"))
			moverJugadorPosicionInicial ();
		if (gameState.Instance.obtenerLevantaMano())
			changeInstructionsState();
		ultimoCambioEstado += Time.deltaTime;
		verificaPosicionUsuario();
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

	private void verificaColor(string colorRecibido){
		if(colores[coloresVisitados] == colorRecibido){
			coloresVisitados += 1;
		} else {
			moverJugadorPosicionInicial();
		}
	}

	public void moverJugadorPosicionInicial(){
		GameObject jugador = GameObject.Find ("OVRPlayerController");
		jugador.transform.position = posicionInicial;
	}

	public override void OnVRGUI () {
		if (coloresVisitados != colores.Count){
			if (muestraInstrucciones) {
				GUI.Box (new Rect (0, 0, 350, 100), "");
				GUI.Label (new Rect (30, 0, 160, 20), mensajeInstruccionesTitulo, tituloEstilo);
				GUI.Label (new Rect (20, 30, 340, 100), mensajeInstruccionesCuerpo, cuerpoEstilo);
				GUI.Label (new Rect (20, 60, 350, 50), "1.-" + colores [0], obtenerEstilo (colores [0]));
				GUI.Label (new Rect (20, 80, 350, 50), "2.-" + colores [1], obtenerEstilo (colores [1]));
				GUI.Label (new Rect (100, 60, 350, 50), "3.-" + colores [2], obtenerEstilo (colores [2]));
				GUI.Label (new Rect (100, 80, 350, 50), "4.-" + colores [3], obtenerEstilo (colores [3]));
			} else {
				mensajeColoresVisitados = obtenMensajeColoresVisitados();
				GUI.Label(new Rect (20, 30, 340, 100), mensajeColoresVisitados, coloresVisitadosEstilo);
			}
		}else{
			gameState.Instance.asignaNivel("Puntuajes");
            Application.LoadLevel("puntuajes");
		}
	}

	public void changeInstructionsState(){
		if(ultimoCambioEstado >= ENFRIAMIENTO_MENU){
			muestraInstrucciones = !muestraInstrucciones;
			ultimoCambioEstado = 0;
		}
	}


	public string obtenMensajeColoresVisitados(){
		return "Faltan "+( 4 - coloresVisitados)+" colores por visitar.";
	}

	private GUIStyle obtenerEstilo(string color){
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

	void verificaPosicionUsuario(){
		print("Verficiando");
		Vector3 posicionActual = jugador.transform.position;
		bool cumpleUno = false;
		print(System.String.Format("Posicion x:{0}, y:{1}, z: {2}", posicionActual.x, posicionActual.y, posicionActual.z));
		if( ( ( posicionActual.x > 68.0 ) && ( posicionActual.x < 98.0 )  ) && ( ( posicionActual.z > 2.0 ) && ( posicionActual.z < 32.0 )  ) ){
		 	if(!(dentroArea)){
				dentroArea = true;
				verificaColor("azul");
			}
			cumpleUno = true;
		}

		if( ( ( posicionActual.x > 68.0 ) && ( posicionActual.x < 98.0 )  ) && ( ( posicionActual.z > 69.0 ) && ( posicionActual.z < 98.0 )  ) ){
			if(!(dentroArea)){
				dentroArea = true;
				verificaColor("amarillo");
			}
			cumpleUno = true;
	
		}

		if( ( ( posicionActual.x > 1.0 ) && ( posicionActual.x < 30.0 )  ) && ( ( posicionActual.z > 1.0 ) && ( posicionActual.z < 30.0 )  ) ){
			if(!(dentroArea)){
				dentroArea = true;
				verificaColor("verde");
			}
			cumpleUno = true;

		}

		if( ( ( posicionActual.x > 1.0 ) && ( posicionActual.x < 28.0 )  ) && ( ( posicionActual.z > 69.0 ) && ( posicionActual.z < 98.0)  ) ){
			if(!(dentroArea)){
				dentroArea = true;
				verificaColor("rojo");
			}
			cumpleUno = true;
		}
		if(!(cumpleUno)){
			dentroArea = false;
		}
	}
}
