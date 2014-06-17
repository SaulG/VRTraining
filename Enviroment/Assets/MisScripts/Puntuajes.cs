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

	public GUIStyle cajaEstilo;
	public Texture2D texturaCaja;

	private int referenciaHeight;
	private int referenciaWidth;

	public void Start(){
		float tiempoJugador = Time.time - gameState.Instance.obtenerTiempoInicial();
		mensajeTituloPuntuajes = "Tu tiempo fue de:";
		mensajeCuerpoPuntuajes = System.String.Format("{0} segundos.",tiempoJugador);
		mensajeLevantaMano = "* Levanta mano derecha para continuar.";
		cajaEstilo =  new GUIStyle();
		texturaCaja = new Texture2D(128, 128);
		for (int y = 0; y < texturaCaja.height; ++y)
        {
            for (int x = 0; x < texturaCaja.width; ++x)
            {
                Color color = new Color(0, 0, 0);
                texturaCaja.SetPixel(x, y, color);
            }
        }  
		texturaCaja.Apply();
		cajaEstilo.normal.background = texturaCaja;
	}

	public override void OnVRGUI (){
		referenciaHeight = Screen.height/ 4;
		referenciaWidth = Screen.width/8;
		GUI.Box (new Rect (referenciaWidth, referenciaHeight, 350, 150), "", cajaEstilo);
		GUI.Label (new Rect (referenciaWidth+30, referenciaHeight+0, 160, 20), mensajeTituloPuntuajes, estiloMensajeTituloPuntuajes);
		GUI.Label (new Rect (referenciaWidth+120, referenciaHeight+60, 340, 100), mensajeCuerpoPuntuajes, estiloMensajeCuerpoPuntuajes);
		GUI.Label (new Rect (referenciaWidth+30, referenciaHeight+120, 340, 100), mensajeLevantaMano, estiloMensajeLevantaMano);

		if(gameState.Instance.obtenerLevantaMano()){
			cambiarEstado();
		}
	}
	
	private void cambiarEstado(){
		gameState.Instance.asignaNivel("Comienza");
        Application.LoadLevel("comienza");
	}
}
