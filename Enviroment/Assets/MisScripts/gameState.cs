using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class gameState : MonoBehaviour {
	
	// Declare properties
	private static gameState instance;
	private Usuario usuarioDatos;
	private string nivelActivo;
	private float tiempoInicial;


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
		usuarioDatos.stopListenning ();
		usuarioDatos = null;
		instance = null;
	}

	// ---------------------------------------------------------------------------------------------------
	
	
	// ---------------------------------------------------------------------------------------------------
	// esperarDeteccionUsuario()
	// --------------------------------------------------------------------------------------------------- 
	// Crea un estado donde se juega el juego.
	// ---------------------------------------------------------------------------------------------------
	public void esperarDeteccionUsuario(){
		usuarioDatos = new Usuario();
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
		return usuarioDatos.obtenerUsuarioDetectado();
	}

	public bool obtenerLevantaMano(){
		return usuarioDatos.obtenerLevantaMano();
	}

	public void asignaTiempoInicial(){
		tiempoInicial = Time.time;
	}

	public float obtenerTiempoInicial(){
		return tiempoInicial;
	}

	public void asignaTiempoLateUpdate (DateTime fecha){
		usuarioDatos.asignaTiempoLateUpdate (fecha);
	}

	public DateTime obtenerTiempoLateUpdate(){
		return usuarioDatos.obtenerTiempoLateUpdate();
	}

	public void asignaBanderaLateUpdate(bool bandera){
		usuarioDatos.asignaBanderaLateUpdate (bandera);
	}

	public bool obtenerBanderaLateUpdate(){
		return usuarioDatos.obtenerBanderaLateUpdate ();
	}

	/*
	var stopwatch:System.Diagnostics.Stopwatch  = new System.Diagnostics.Stopwatch();
	stopwatch.Start();
	 
	// your function here..
	 
	stopwatch.Stop();
	Debug.Log("Timer: " + stopwatch.Elapsed);
	//Debug.Log("Timer: " + stopwatch.ElapsedMilliseconds); // this one gives you the time in ms
	stopwatch.Reset();
	*/
}