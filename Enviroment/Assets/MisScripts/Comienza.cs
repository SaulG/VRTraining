﻿using UnityEngine;
using System.Collections;

public class Comienza : MonoBehaviour {

	// Our Startscreen GUI
	void OnGUI ()
	{
		if (GUI.Button (new Rect (30, 30, 150, 30), "Comienza Juego"))
		{
			cambiaEstado();
		}
	}
	
	private void cambiaEstado(){
		print("Esperar deteccion usuario");
		DontDestroyOnLoad(gameState.Instance);
		gameState.Instance.esperarDeteccionUsuario();       
	}
}
