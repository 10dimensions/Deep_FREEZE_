using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

public partial class PCPlayerControl{

    #region parameters
    enum CurrentMotion
    {
        None,
        Run,
        VerticalJump,
        ProjectileJump
    }
    [SerializeField] private Rigidbody m_rigidbody;
    private CurrentMotion currentMotion = CurrentMotion.None;
    IEnumerator m_verticalJumpCoroutine;
    public static PCPlayerControl m_instance;
    private VRInput m_Input;
    public  Text mtxt;
    public static Text txt{
        get{
            return m_instance.mtxt;
        }
    }
    public float m_maxHeight;
    public Vector3 m_clampValueMax, m_clampValueMin;
    public Transform m_mainCam, m_mainCamParent;
    public Vector3 initialDis;
    bool m_playerStatic = true;
    Transform m_transform;

    Vector3 m_initialPlayerPos;
    Vector3 m_initialPlayerRot;
    Vector3 m_initialCameraPos;
    Vector3 m_initialCameraRot;


                     // How far the line renderer will reach if a target isn't hit.
        [SerializeField] private float m_Damping = 0.5f;                                // The damping with which this gameobject follows the camera.
        [SerializeField] private Reticle m_Reticle;                                     // This is what the gun arm should be aiming at.
        private const float k_DampingCoef = -20f;  
   // public Scene AutoRunStartScene;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        m_instance = this;
        m_Input = VRInput.m_instance;
    }
    private void Start()
    {
        m_transform = this.transform;
        m_clampValueMin = new Vector3(-450,0,-600);
        m_clampValueMax = new Vector3(350, 0, 600);
        initialDis = m_mainCam.position - m_transform.position;
        m_rigidbody.isKinematic = true;

        OVRTouchpad.Create();
        OVRTouchpad.TouchHandler += HandleTouchHandler;

        m_initialCameraPos = m_mainCam.position;
        m_initialCameraRot = m_mainCam.eulerAngles;

        m_initialPlayerPos = m_transform.position;
        m_initialPlayerRot = m_transform.eulerAngles;
    }
    private void OnEnable()
    {
        m_Input.OnClick += OnClick;
        m_Input.OnDown += OnDown;


        m_Input.OnUp += OnUp;
        m_Input.OnCancel += Cancel;
        m_Input.OnDoubleClick += doubleClick;
    }
    private void OnDisable()
    {
        m_Input.OnClick -= OnClick;
        m_Input.OnDown -= OnDown;
    }
    void OnLevelWasLoaded()
    {
        //TODO change build-index
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            m_playerStatic = true;
            m_transform.position = m_initialPlayerPos;
            m_transform.eulerAngles = m_initialPlayerRot;
            m_mainCam.position = m_initialCameraPos;
            m_mainCam.eulerAngles = m_initialPlayerRot;
        }
        else if(SceneManager.GetActiveScene().buildIndex==2)
        {
            m_playerStatic = false;
            m_rigidbody.isKinematic = false;
            StartAutoRun();
        }
        else
        {
            m_playerStatic = false;
        }
    }
    private void Update()
    {
        if (m_playerStatic)
            return;
        
        MyRotUpdate();
        if(currentMotion == CurrentMotion.Run)
        {
            if(ReachedCorner())
            {
                FacePlayerOpposite();
                return;
            }
            this.transform.Translate(Vector3.forward*0.1f);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            DoVerticalJump();
        }
    }
    private void LateUpdate()
    {
        
    }
    private void MyRotUpdate()
    {
        m_mainCamParent.position = m_transform.position;
        m_transform.eulerAngles = m_mainCam.eulerAngles;

    }
    #endregion


    #region Jumping_handler
    IEnumerator VerticalJump()
    {
        int frame = 60;
        m_rigidbody.isKinematic = true;
        while(frame-- >0)
        {
            this.transform.Translate(new Vector3(0,1f,0));
            yield return 0;
        }
        txt.text = "Jumping completed";
        m_rigidbody.isKinematic = false;
        yield return 0;
    }
    IEnumerator ProjectileJump(Transform a_srcTrns, Vector3 a_dest)
    {
        
        m_rigidbody.isKinematic = true;

        currentMotion = CurrentMotion.ProjectileJump;


        float heightLimit = a_srcTrns.position.y + 5;
        float l_Dis = Vector3.Distance(a_srcTrns.position,a_dest);
        int frames = 125;

        Vector3 l_DisVector = a_dest - a_srcTrns.position;
        l_DisVector = new Vector3(l_DisVector.x, heightLimit, l_DisVector.z);

        Vector3 l_disPerFrame = l_DisVector / frames;
        float l_heightPerFrame = (heightLimit-a_srcTrns.position.y) / (frames * 0.5f);

        int frameCount = 0;
        while(frameCount++ < frames*0.5f)
        {
            a_srcTrns.position += new Vector3(l_disPerFrame.x, l_heightPerFrame, l_disPerFrame.z);
            yield return null;
        }
        while (frameCount-- >0)
        {
            a_srcTrns.position += new Vector3(l_disPerFrame.x, (-1)*l_heightPerFrame, l_disPerFrame.z);
            yield return null;
        }
        m_rigidbody.isKinematic = false;
        yield return 0;
    }
    void DoVerticalJump()
    {
        if (this.transform.position.y >= m_maxHeight)
            return;
        
        if (m_verticalJumpCoroutine != null)
            StopCoroutine(m_verticalJumpCoroutine);

        m_verticalJumpCoroutine = VerticalJump();
        StartCoroutine(m_verticalJumpCoroutine);
    }
#endregion

    void OnClick()
    {
        txt.text = "should Jump";
        DoVerticalJump();
    }
    void OnDown()
    {
        txt.text = "On Down";
    }
   void StartAutoRun()
    {
        currentMotion = CurrentMotion.Run;
    }

    bool ReachedCorner()
    {
        return m_transform.position.x < m_clampValueMin.x || m_transform.position.x > m_clampValueMax.x || 
                          m_transform.position.z < m_clampValueMin.z || m_transform.position.z > m_clampValueMax.z;

    }
    void FacePlayerOpposite()
    {
        m_mainCam.Rotate(new Vector3(0,180,0));
        MyRotUpdate();
        this.transform.Translate(Vector3.forward * 0.1f);
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

    void OnUp()
    { txt.text = "OnUp Input Over"; CancelInvoke(); Invoke("stop", 2); }

    void Cancel()
    { txt.text = "Cancel Input Over"; }
    void doubleClick()
    { txt.text = "doubleClick Input Over"; CancelInvoke(); Invoke("stop", 2); }
    //Handle the Over event
    private void HandleOver()
    {
        Debug.Log("Show over state");
        txt.text = "Handle Over";
    }
}

public partial class PCPlayerControl : MonoBehaviour {


	//public Transform playerCamera;
	//private Vector3 playerMovement = Vector3.zero;

	//public float forwardSpeed;
	//public float sideSpeed;
	//public float jumpModifier;

	//public float pitch;
	//public float yaw;

	//public float mouseSensitivity;
	//public float minYaw;
	//public float maxYaw;

	//private int yInvert = -1;
	//private float yInput, xInput;
	//private float currentPitch = 0;

	//void Start ()
	//{
 //       StartCoroutine(Jump(this.transform, this.transform.position+new Vector3(15,0,11)));
 //       Debug.Log(this.transform.forward);
	//}

    //private void OnEnable()
    //{

    //    //m_Input.OnClick += OnClick;
    //    //m_Input.OnDoubleClick += doubleClick;
    //}


	//void FixedUpdate ()

	//{
	//	//movePlayer();
	//	//movePlayerMouse();
	//	//transform.localPosition+=new Vector3(0,-0.5f,0);

		

	//}

	//void movePlayer()
	//{

	//	//playerMovement.Set(Input.GetAxis("Horizontal") * sideSpeed, 0.0f, Input.GetAxis("Vertical") * forwardSpeed);
        
	//	//// Clamped to forwardSpeed so moving in diagonal isn't faster
	//	////playerController.Move(transform.TransformDirection(Vector3.ClampMagnitude(playerMovement, forwardSpeed)));


	//	////if(Input.GetButton("Space"))
	//	//if(Input.GetKeyDown("space"))
	//	//{
	//	//	transform.localPosition+=new Vector3(0,2,0);

	//	//}

	//}



	//void movePlayerMouse()

	//{

	//	yInput = Input.GetAxisRaw("Mouse Y") * pitch * mouseSensitivity * yInvert;
	//	Debug.Log("Mouse yInput: "+ yInput);


	//	xInput = Input.GetAxisRaw("Mouse X") * yaw * mouseSensitivity;



	//	// Player rotates left and right (and camera with)

	//	transform.Rotate(Vector3.up * xInput);



	//	// Only camera rotates vertically

	//	currentPitch = Mathf.Clamp(currentPitch + yInput, minYaw, maxYaw);

	//	playerCamera.transform.localEulerAngles = Vector3.right * currentPitch;

	//}

}
