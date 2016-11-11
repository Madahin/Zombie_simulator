using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerControler : MonoBehaviour
{

    //[Range(0.0f, 10.0f)]
    public float MaxSpeed;
    public float Speed;

    [Range(0.0f, 100.0f)]
    public float MouseSpeedFactor = 1.5f;

    public float height = 1.5f;

    private Camera mainCamera;
    private Rigidbody rigidBody;
    public float thrust;
    public GameObject character;
    //private CharacterController CharCtrl;

    void Awake()
    {
        //GameManager.Instance.Player = this.gameObject;
    }

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //CharCtrl = this.GetComponent<CharacterController>();
        mainCamera = this.GetComponentInChildren<Camera>();
        rigidBody = this.GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = mainCamera.transform.forward * vertical + mainCamera.transform.right * horizontal;
        direction.y = 0.0f;
        direction.Normalize();

        Vector3 speedVector = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        if (speedVector.magnitude <= MaxSpeed)
            rigidBody.AddForce(direction * Speed, ForceMode.Impulse);

        //rigidBody.velocity = direction * moveFactor + Vector3.up * yVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        /***********************************************************/
        /*               DICKBUTT  SHIT                            */
        /***********************************************************/

        //if (Input.GetKey(KeyCode.Escape))
            //GameManager.Instance.LoadLevel("Menu");



        /*gameObject.transform.Translate(Input.GetAxis("Horizontal") * MoveSpeedFactor,
                                       0f,
                                       Input.GetAxis("Vertical") * MoveSpeedFactor);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    height,
                                                    gameObject.transform.position.z);*/

        mainCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * MouseSpeedFactor * Time.deltaTime, 0f, 0f);
        //mainCamera.transform.Rotate(0f, Input.GetAxis("Mouse X") * MouseSpeedFactor * Time.deltaTime, 0f);

        character.transform.Rotate(0f, Input.GetAxis("Mouse X") * MouseSpeedFactor * Time.deltaTime, 0f);

        float gimbalLock = mainCamera.transform.rotation.eulerAngles.x;

        if (gimbalLock >= 269 && gimbalLock < 275)
        {
            gimbalLock = 275f;
        }

        if (gimbalLock >= 87 && gimbalLock < 91)
        {
            gimbalLock = 88f;
        }

        mainCamera.transform.rotation = Quaternion.Euler(gimbalLock,
                                                         mainCamera.transform.rotation.eulerAngles.y,
                                                         0f);

        float position = 0;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {

            Vector3 newPosition = character.transform.position;
            newPosition += mainCamera.transform.right;
            newPosition.y = position;
            character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {

            Vector3 newPosition = character.transform.position;
            newPosition -= mainCamera.transform.right;
            newPosition.y = position;
            character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {

            Vector3 newPosition = character.transform.position;
            newPosition += mainCamera.transform.forward;
            newPosition.y = position;
            character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Vector3 newPosition = character.transform.position;
            newPosition -= mainCamera.transform.forward;
            newPosition.y = position;
            character.transform.position = newPosition;

        }

        Quaternion r = character.transform.rotation;
        character.transform.rotation = Quaternion.Euler(0, r.eulerAngles.y, 0);
    }
}
