using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour {

	public static AudioClip apology, choice, clickPrompt, egypt, taj, welcomeBack, welcome;
	public static AudioSource audiosrc;

	// Use this for initialization
	void Start () {
		apology=Resources.Load<AudioClip>("apology");
		choice=Resources.Load<AudioClip>("choice");
		clickPrompt=Resources.Load<AudioClip>("clickPrompt");
		egypt=Resources.Load<AudioClip>("egypt");
		taj=Resources.Load<AudioClip>("taj");
		welcomeBack=Resources.Load<AudioClip>("welcomeBack");
		welcome=Resources.Load<AudioClip>("welcome");

		audiosrc=GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound(string clip)
	{
		switch(clip)
		{
			case "apology":
				audiosrc.PlayOneShot(apology);
				break;
			case "choice":
				audiosrc.PlayOneShot(choice);
				break;
			case "clickPrompt":
				audiosrc.PlayOneShot(clickPrompt);
				break;
			case "egypt":
				audiosrc.PlayOneShot(egypt);
				break;
			case "taj":
				audiosrc.PlayOneShot(taj);
				break;
			case "welcomeBack":
				audiosrc.PlayOneShot(welcomeBack);
				break;
			case "welcome":
				audiosrc.PlayOneShot(welcome);
				break;
			
		}
	}

}
