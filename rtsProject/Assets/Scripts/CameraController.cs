using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

    public float panSpeed;
    
    private Transform myTrans;

    private float horizontal;
    private float vertical;

    private Vector3 movementDir;
    
    // Use this for initialization
	void Start () 
    {
        myTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movementDir = new Vector3((horizontal - vertical), 0, (horizontal + vertical));

        myTrans.position += movementDir.normalized * panSpeed * Time.deltaTime;
	}
}
