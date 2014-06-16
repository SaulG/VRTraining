using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Editor : MonoBehaviour {
	private const string FILE_NAME = "MyFile.txt";

	// Use this for initialization
	void Start () {
    	if (File.Exists(FILE_NAME)){
            Console.WriteLine("{0} already exists.", FILE_NAME);
            return;
        }
        StreamWriter sr = File.CreateText(FILE_NAME);
        sr.WriteLine ("This is my file.");
        sr.WriteLine ("I can write ints {0} or floats {1}, and so on.", 
            1, 4.2);
        sr.Close();
	}
	
	// Update is called once per frame
	void Update () {
		print("Hola mundo");
	}
}

