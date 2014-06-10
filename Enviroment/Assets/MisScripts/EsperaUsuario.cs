using UnityEngine;
using System.Collections;

public class EsperaUsuario : VRGUI {

	private string mensajeDeteccionUsuario;
	private string mensajeLevantaMano;

	public GUIStyle estiloMensajes;

	public void Start(){
		mensajeDeteccionUsuario = "Detectando usuario...";
		mensajeLevantaMano = "Levanta tu mano derecha para comenzar";
	}

	public override void OnVRGUI () {
		if (!(gameState.Instance.obtenerUsuarioEsDetectado()) ) {
			GUI.Label (new Rect (180, 150, (Screen.width/2), (Screen.height/2)), mensajeDeteccionUsuario, estiloMensajes);
		}else if (!(gameState.Instance.obtenerLevantaMano()) ) {
			GUI.Label (new Rect (90, 150, (Screen.width/2), (Screen.height/2)), mensajeLevantaMano, estiloMensajes);
		} else {
			cambiaEstado();		
		}
	}

	private void cambiaEstado(){
		print("Comienzando estado de cuarto");
		gameState.Instance.asignaNivel("Cuarto");
		gameState.Instance.asignaTiempoInicial();
        Application.LoadLevel("cuarto");
	}
}
