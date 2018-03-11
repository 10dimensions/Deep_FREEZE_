using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class sciFiController : MonoBehaviour {

	public GameObject pfx;

	public GameObject pyramid;
	public GameObject tajMahal;

    [SerializeField] private VRInteractiveItem m_intrcMap, m_intrcTajmahal, m_intrcPyramid;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("MAP_OPEN",0)== 1)
        {
            mapClick();
        }
    }
    private void OnEnable()
    {
        m_intrcMap.OnOver += ClickOnMap;
        m_intrcTajmahal.OnOver += ClickOnTaj;
        m_intrcPyramid.OnOver += ClickOnPyr;
    }
    private void OnDisable()
    {
        m_intrcMap.OnOver -= ClickOnMap;
        m_intrcTajmahal.OnOver -= ClickOnTaj;
        m_intrcPyramid.OnOver -= ClickOnPyr;
    }
    void Update()
    {
       playerMove();		
		
		//if (Input.GetMouseButtonDown(0))
     	//{
        //     controllerClick();
        //} 
    }


    public void playerMove(){
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }


    //public void controllerClick()
    //{
    //    RaycastHit hitInfo = new RaycastHit();
    //     bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
    //     if (hit) 
    //     {

    //         if (hitInfo.transform.gameObject.tag == "Map")
    //         {
    //             Debug.Log ("It's working!");
    //            mapClick();
    //         } 
    //         else if(hitInfo.transform.gameObject.tag == "pyramid")
    //         {
    //        Debug.Log("pyramid clicked");
    //        SceneManager.LoadScene("TajMahal");
            
    //        }
    //        else if(hitInfo.transform.gameObject.tag == "taj"){
             
    //        }
    //    }
    //}


    public void mapClick()
    {   
        pfx.SetActive(true);

        tajMahal.SetActive(true);
        pyramid.SetActive(true);
        PlayerPrefs.SetInt("MAP_OPEN",1);
    }

    void ClickOnTaj()
    {
        SceneManager.LoadScene("TajMahal");
    }
    void ClickOnPyr()
    {
        Debug.Log("Pyramid scene clicked");
    }
    void ClickOnMap()
    {
        mapClick();
    }
}

