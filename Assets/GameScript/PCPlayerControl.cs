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
    public Text txt;
    public float m_maxHeight;
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
        
    }
    private void OnEnable()
    {
        m_Input.OnClick += OnClick;
        m_Input.OnDown += OnDown;
    }
    private void OnDisable()
    {
        m_Input.OnClick -= OnClick;
        m_Input.OnDown -= OnDown;
    }
    void OnLevelWasLoaded()
    {
        //TODO change build-index
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            StartAutoRun();
        }
    }
    private void Update()
    {
        if(currentMotion == CurrentMotion.Run)
        {
            this.transform.Translate(Vector3.forward*0.1f);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            DoVerticalJump();
        }
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
