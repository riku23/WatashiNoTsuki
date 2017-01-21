﻿using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	// Sound gameobjects
	public GameObject menuMusic;
	public GameObject waveSound;
	public GameObject levelMusic;
	// Class instance
	public static MusicManager mm;

	// Use this for initialization
	void Start () {

	}

	void Awake () {
		if(!mm) {
			mm = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void Update () {
		if (waveSound.GetComponent<AudioSource>().isPlaying == false)
			waveSound.GetComponent<AudioSource>().Play();
		
		if (Application.loadedLevelName == "Menu" || Application.loadedLevelName == "Credits") {
			if (menuMusic.GetComponent<AudioSource> ().isPlaying == false)
				menuMusic.GetComponent<AudioSource> ().Play();
			if (menuMusic.GetComponent<AudioSource> ().isPlaying == true)
				levelMusic.GetComponent<AudioSource> ().Stop();
		} else {
			if (menuMusic.GetComponent<AudioSource> ().isPlaying == true)
				menuMusic.GetComponent<AudioSource> ().Stop();
			if (levelMusic.GetComponent<AudioSource> ().isPlaying == false)
				levelMusic.GetComponent<AudioSource> ().Play();	
		}
	}

}