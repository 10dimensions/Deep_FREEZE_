using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sciFiController : MonoBehaviour {

	LoadManager loadManager=new LoadManager();
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
		
		
		if (Input.GetMouseButtonDown(0))
     	{
         Debug.Log("Mouse is down");
         
         RaycastHit hitInfo = new RaycastHit();
         bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
         if (hit) 
         {
             Debug.Log("Hit " + hitInfo.transform.gameObject.name);
             if (hitInfo.transform.gameObject.tag == "Map")
             {
                 Debug.Log ("It's working!");
				loadManager.sndRecord();

             } else {
                 Debug.Log ("nopz");
             }
         } else {
             Debug.Log("No hit");
         }
         Debug.Log("Mouse is down");
     } 


    }
}

