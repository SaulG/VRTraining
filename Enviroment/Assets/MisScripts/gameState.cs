using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class gameState : MonoBehaviour {
	
	// Declare properties
	private static gameState instance;
	private Usuario usuarioDatos;
	private string nivelActivo;


	
	
	
	
	// ---------------------------------------------------------------------------------------------------
	// gamestate()
	// --------------------------------------------------------------------------------------------------- 
	// Creates an instance of gamestate as a gameobject if an instance does not exist
	// ---------------------------------------------------------------------------------------------------
	public static gameState Instance{
		get{
			if(instance == null)
			{
				instance = new GameObject("gameState").AddComponent<gameState> ();
			}
			
			return instance;
		}
	}	


	
	// Sets the instance to null when the application quits
	public void OnApplicationQuit(){
		instance = null;
		usuarioDatos.stopListenning ();
		usuarioDatos = null;
	}
	// ---------------------------------------------------------------------------------------------------
	
	
	// ---------------------------------------------------------------------------------------------------
	// esperarDeteccionUsuario()
	// --------------------------------------------------------------------------------------------------- 
	// Crea un estado donde se juega el juego.
	// ---------------------------------------------------------------------------------------------------
	public void esperarDeteccionUsuario(){
		usuarioDatos= new Usuario();

		Application.LoadLevel ("esperaUsuario");

	}


	
	// ---------------------------------------------------------------------------------------------------
	// getLevel()
	// --------------------------------------------------------------------------------------------------- 
	// Returns the currently active level
	// ---------------------------------------------------------------------------------------------------
	public string obtenerNivel(){
		return nivelActivo;
	}
	
	
	// ---------------------------------------------------------------------------------------------------
	// setLevel()
	// --------------------------------------------------------------------------------------------------- 
	// Sets the currently active level to a new value
	// ---------------------------------------------------------------------------------------------------
	public void asignaNivel(string nuevoNivel){
		// Set activeLevel to newLevel
		nivelActivo = nuevoNivel;
	}


	public bool obtenerCamina(){
		return usuarioDatos.obtenerCamina();
	}

	public string obtenerOrientacion(){
		return usuarioDatos.obtenerOrientacion();
	}

	public bool obtenerUsuarioEsDetectado(){
		return true;//usuarioDatos.obtenerUsuarioDetectado();
	}

	public bool obtenerLevantaMano(){
		return true;//usuarioDatos.obtenerLevantaMano();
	}

}