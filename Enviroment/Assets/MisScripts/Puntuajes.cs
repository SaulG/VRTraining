using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Puntuajes : VRGUI {

	private static string CAMINO = "/Recursos";
	private static string NOMBRE_ARCHIVO = "puntuajes.txt";


	public GUIStyle estiloMensajeTituloPuntuajes;
	public GUIStyle estiloMensajeTituloCuerpoPuntuajes;
	public GUIStyle estiloMensajeCuerpoPuntuajes;


	private string mensajeTituloPuntuajes;
	private string mensajeTituloCuerpoPuntuajes;
	private string mensajeCuerpoPuntuajes;

	public void onStart(){
		mensajeTituloPuntuajes = "Tu puntuaje fue:";
		mensajeTituloCuerpoPuntuajes = "Tabla de Puntuajes";
	}

	public void escribirResultado(string usuario, string tiempo){
		if(!File.Exists ((CAMINO + NOMBRE_ARCHIVO))){
			using (StreamWriter sw = File.CreateText((CAMINO + NOMBRE_ARCHIVO))){
				sw.WriteLine(usuario+","+tiempo);
			}
		}
	}

	public Dictionary<string, string> obtenerResultados(){
		Dictionary<string, string> puntuajes = new Dictionary<string, string>();
		using(StreamReader sr = File.OpenText((CAMINO + NOMBRE_ARCHIVO))){
			string s = "";
			string [] puntuajeUsuario;
			while((s = sr.ReadLine()) != null){
				puntuajeUsuario = s.Split(","[0]);
				puntuajes.Add(puntuajeUsuario[0].ToString(), puntuajeUsuario[1].ToString());
			}
		}
		return puntuajes;
	}

	public override void OnVRGUI (){
		GUI.Box (new Rect (0, 0, 350, 100), "");
		GUI.Label (new Rect (30, 0, 160, 20), mensajeTituloPuntuajes, estiloMensajeTituloPuntuajes);
		GUI.Label (new Rect (20, 30, 340, 100), mensajeTituloCuerpoPuntuajes, estiloMensajeTituloCuerpoPuntuajes);
		GUI.Label (new Rect (20, 60, 350, 50), mensajeCuerpoPuntuajes, estiloMensajeCuerpoPuntuajes);
		if(gameState.Instance.obtenerLevantaMano()){
			gameState.Instance.asignaNivel("Comienza");
            Application.LoadLevel("comienza");
		}
	}
}
