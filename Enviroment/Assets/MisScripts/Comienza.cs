using UnityEngine;
using System.Collections;

public class Comienza : MonoBehaviour {

	// Our Startscreen GUI
	void OnGUI ()
	{
		if (GUI.Button (new Rect ((Screen.width/3), (Screen.height/3), (Screen.width/4), (Screen.height/5)), "Comienza Juego"))
		{
			cambiarEstado();
		}
	}
	
	private void cambiarEstado(){
		print("Esperar deteccion usuario");
		DontDestroyOnLoad(gameState.Instance);
		gameState.Instance.esperarDeteccionUsuario();       
	}
}
