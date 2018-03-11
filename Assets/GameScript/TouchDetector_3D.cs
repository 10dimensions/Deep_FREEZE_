using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRStandardAssets.Utils;

/// <summary>
/// This is for Pyramids detecting
/// </summary>
public class TouchDetector_3D : MonoBehaviour {
    

    public VRInteractiveItem m_vrIntrctv;

    private void OnEnable()
    {
        m_vrIntrctv.OnOver += M_VrIntrctv_OnOver;
        m_vrIntrctv.OnOut += HandleOut;
    }
    private void OnDisable()
    {
        m_vrIntrctv.OnOver -= M_VrIntrctv_OnOver;
        m_vrIntrctv.OnOut -= HandleOut;
    }

    void M_VrIntrctv_OnOver()
    {
        PCPlayerControl.m_instance.OnPyramidObj(this.transform);
        PCPlayerControl.txt.text = this.gameObject.name;
    }
    void HandleOut()
    {
        PCPlayerControl.m_instance.OnPyramidObj(null);
        PCPlayerControl.txt.text = "exited: "+this.gameObject.name;
    }
}
