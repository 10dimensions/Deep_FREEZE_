using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

/// <summary>
/// This class is for teleport control
/// </summary>
public class sciFiController : MonoBehaviour
{

    #region parameters
    public GameObject pfx;

    public GameObject pyramid;
    public GameObject tajMahal;

    [SerializeField] private VRInteractiveItem m_intrcMap, m_intrcTajmahal, m_intrcPyramid;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (PCPlayerControl.m_mapClicked == true)
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
    }
    #endregion

    #region Button_Callbacks
    public void playerMove()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    public void mapClick()
    {
        pfx.SetActive(true);

        tajMahal.SetActive(true);
        pyramid.SetActive(true);
        PCPlayerControl.m_mapClicked = true;
    }

    void ClickOnTaj()
    {
        SceneManager.LoadScene(3);
    }
    void ClickOnPyr()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Pyramid scene clicked");
    }
    void ClickOnMap()
    {
        mapClick();
    }
#endregion
}

