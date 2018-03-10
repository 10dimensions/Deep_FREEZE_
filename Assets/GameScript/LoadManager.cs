using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public Button clicker;
	public ParticleSystem pfx;

	public GameObject pyramid;
	public GameObject tajMahal;

	private string recordedObjectName;


	// Use this for initialization

	void awake()
	{
		pfx.Stop();	
		pyramid.SetActive(false);
		pyramid.SetActive(false);

		clicker.onClick.AddListener(sndRecord);
		
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sndRecord()
	{

		hologramLoad();

	}

	public void hologramLoad()
	{	
		Debug.Log("map clicked");

		//pfx.Play();

		/*
		if(recordedObjectName=="Pyramid")
		{
			pyramid.SetActive(true);
			tajMahal.SetActive(false);
		}

		else if(recordedObjectName=="tajMahal")
		{
			pyramid.SetActive(false);
			tajMahal.SetActive(true);
		}
		*/

		pyramid.SetActive(true);
		tajMahal.SetActive(true);
	}

	public void sceneLoad(string sceneName)
	{
		SceneManager.LoadScene(sceneName);

	}
}
