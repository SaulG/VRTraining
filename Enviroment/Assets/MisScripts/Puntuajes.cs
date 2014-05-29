using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Puntuajes : VRGUI {

	public GUIStyle estiloMensajeTituloPuntuajes;
	public GUIStyle estiloMensajeCuerpoPuntuajes;
	public GUIStyle estiloMensajeLevantaMano;


	private string mensajeTituloPuntuajes;
	private string mensajeCuerpoPuntuajes;
	private string mensajeLevantaMano;

	public void Start(){
		float tiempoJugador = Time.time - gameState.Instance.obtenerTiempoInicial();
		mensajeTituloPuntuajes = "Tu puntuaje fue:";
		mensajeCuerpoPuntuajes = System.String.Format("{0} segundos.",tiempoJugador);
		mensajeLevantaMano = "* Levanta mano derecha para continuar.";
	}

	public override void OnVRGUI (){
		GUI.Box (new Rect (0, 0, 350, 100), "");
		GUI.Label (new Rect (30, 0, 160, 20), mensajeTituloPuntuajes, estiloMensajeTituloPuntuajes);
		GUI.Label (new Rect (40, 30, 340, 100), mensajeCuerpoPuntuajes, estiloMensajeCuerpoPuntuajes);
		GUI.Label (new Rect (30, 50, 340, 100), mensajeLevantaMano, estiloMensajeLevantaMano);

		if(gameState.Instance.obtenerLevantaMano()){
			cambiarEstado();
		}
	}

	private void cambiarEstado(){
		gameState.Instance.asignaNivel("Comienza");
        Application.LoadLevel("comienza");
	}
}
