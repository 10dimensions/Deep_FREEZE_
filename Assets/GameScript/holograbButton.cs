using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class holograbButton : MonoBehaviour {

	
	void OnMouseDown()
     {	
		 PlayerPrefs.SetString("objClicked", this.gameObject.name);
         SceneManager.LoadScene("TajMahal");    
     }
 }  


