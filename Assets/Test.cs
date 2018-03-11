using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class Test : MonoBehaviour {

    public Text txt;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private VRInput m_Input;
    private void OnEnable()
    {
        //m_InteractiveItem.OnOver += HandleOver;
        //m_InteractiveItem.OnOut += HandleOut;
        //m_InteractiveItem.OnClick += HandleClick;
        //m_InteractiveItem.OnDoubleClick += HandleDoubleClick;

        m_Input.OnUp += OnUp;
        m_Input.OnDown += OnDown;
        m_Input.OnClick += OnClick;
        //m_Input.OnSwipe += Swipe;
        m_Input.OnCancel += Cancel;
        m_Input.OnDoubleClick += doubleClick;
    }
    private void Start()
    {
        OVRTouchpad.Create();
       // OVRTouchpad.TouchHandler += HandleTouchHandler;
        txt.text = OVRInput.GetActiveController().ToString();
    }
    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
        {
           // txt.text = "SingleTap!";

        }
        else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Up)
        {
            txt.text = "Up!";

        }
        else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Left)
        {
            txt.text = "Left!";

        }
        else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right)
        {
            txt.text = "Right!";

        }
        else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Down)
        {
            txt.text = "Down!";

        }
    }
    private void OnDisable()
    {
        //m_InteractiveItem.OnOver -= HandleOver;
        //m_InteractiveItem.OnOut -= HandleOut;
        //m_InteractiveItem.OnClick -= HandleClick;
        //m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;

        m_Input.OnUp -= OnUp;
        m_Input.OnDown -= OnDown;
        m_Input.OnClick -= OnClick;
        //m_Input.OnSwipe -= Swipe;
        m_Input.OnCancel -= Cancel;
        m_Input.OnDoubleClick -= doubleClick;
    }
    void OnUp()
    {txt.text = "OnUp Input Over";CancelInvoke(); Invoke("stop", 2);}
    void OnDown()
    { txt.text = "OnDown Input Over";CancelInvoke(); Invoke("stop", 2);}
    void OnClick()
    { txt.text = "OnCLick Input Over";CancelInvoke(); Invoke("stop", 2);}

    void Swipe(VRInput.SwipeDirection swipeDirection)
    { txt.text = "Swipe Input Over: " + swipeDirection + Time.realtimeSinceStartup;
        CancelInvoke(); Invoke("stop",2);}
    void Cancel()
    { txt.text = "Cancel Input Over";}
    void doubleClick()
    { txt.text = "doubleClick Input Over";CancelInvoke(); Invoke("stop", 2);}
    //Handle the Over event
    private void HandleOver()
    {
        Debug.Log("Show over state");
        txt.text = "Handle Over";
    }
    void stop()
    {
        txt.text = "nothing";
    }

    //Handle the Out event
    private void HandleOut()
    {
        Debug.Log("Show out state");
        txt.text = "Handle Out";
    }


    //Handle the Click event
    private void HandleClick()
    {
        Debug.Log("Show click state");
        txt.text = "Handle Click";
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        Debug.Log("Show double click");
        txt.text = "Handle DClick";
    }
}
