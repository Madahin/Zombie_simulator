using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{

    //[Range(0.0f, 10.0f)]
    public float Speed = 0.5f;

    [Range(0.0f, 100.0f)]
    public float MouseSpeedFactor = 1.5f;

    public float height = 1.5f;

    private Camera mainCamera;
    public float thrust;
    public GameObject character;
    public GameObject zombiePrefab;
    private CharacterController CharCtrl;

    public Texture heartTexture;

    void Awake()
    {
        //GameManager.Instance.Player = this.gameObject;
        FactionManager.Instance.AddKey(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CharCtrl = this.GetComponent<CharacterController>();
        mainCamera = this.GetComponentInChildren<Camera>();
    }

    void OnGUI()
    {
        for (int i = 0; i < transform.parent.gameObject.GetComponent<Entity>().PV; ++i) {
            GUI.DrawTexture(new Rect(20 + 52 * i, 20, 64, 64), heartTexture);
        }
    }



    void FixedUpdate()
    {/*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = mainCamera.transform.forward * vertical + mainCamera.transform.right * horizontal;
        direction.y = 0.0f;
        direction.Normalize();

        Vector3 speedVector = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        if (speedVector.magnitude <= MaxSpeed)
            rigidBody.AddForce(direction * Speed, ForceMode.Impulse);*/

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
        

        Vector3 positionVec = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            positionVec += mainCamera.transform.right;
            //character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {

            positionVec -= mainCamera.transform.right;
            //character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {

            positionVec += mainCamera.transform.forward;
            //character.transform.position = newPosition;

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            positionVec -= mainCamera.transform.forward;
            //character.transform.position = newPosition;

        }

        if(Input.GetButtonUp("Fire1"))
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(1, 1, 1), transform.forward);
            hits = Array.FindAll(hits, (i) => ((i.transform.gameObject.tag == "Human") || (i.transform.gameObject.tag == "Zombie" && i.transform.gameObject.GetComponent<ZombieFaction>().faction != gameObject.transform.parent.GetComponent<ZombieFaction>().faction)));
            foreach(RaycastHit hit in hits)
            {
                Entity e = hit.transform.gameObject.GetComponent<Entity>();
                e.Hit();
                if (e.PV == 0)
                {
                    Vector3 newpos;
                    NavMeshHit closestHit;
                    if (NavMesh.SamplePosition(hit.transform.position, out closestHit, 500, 1))
                    {
                        newpos = closestHit.position;
                    }
                    else
                    {
                        newpos = hit.transform.position;
                    }
                    GameObject newZombie = Instantiate(zombiePrefab, newpos, Quaternion.identity) as GameObject;
                    newZombie.GetComponent<ZombieFaction>().SetFaction(gameObject.transform.parent.gameObject.GetComponentInChildren<ZombieFaction>().faction);
                    FactionManager.Instance.RemoveEntity(hit.transform.gameObject);
                    FactionManager.Instance.AddEntity(gameObject.transform.parent.gameObject.GetComponentInChildren<ZombieFaction>().faction, newZombie);
                    if (hit.transform.gameObject.GetComponent<ZombieFaction>() != null && hit.transform.gameObject.GetComponentInChildren<ZombieFaction>().IsBoss)
                    {
                        Debug.Log("wololo");
                        FactionManager.Instance.Wololo(hit.transform.gameObject, gameObject.transform.parent.gameObject.GetComponentInChildren<ZombieFaction>().faction);
                    }
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        positionVec.y = 0;
        positionVec.Normalize();
        positionVec *= Speed;
        CharCtrl.Move(positionVec);
        Quaternion r = character.transform.rotation;
        character.transform.rotation = Quaternion.Euler(0, r.eulerAngles.y, 0);
    }
}
