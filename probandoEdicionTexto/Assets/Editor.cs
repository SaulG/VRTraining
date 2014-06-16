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

	}
	
	// Update is called once per frame
	void Update () { 
		TextWriter sr = new StreamWriter(FILE_NAME, true);
		sr.WriteLine ("This is my file.");
		sr.WriteLine ("I can write ints {0} or floats {1}, and so on.",1, 4.2);
		sr.Close();
		print("Hola mundo");
	}
}

