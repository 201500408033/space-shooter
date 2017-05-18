using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float tilt = 4.0f;
    public Boundary boundary;

    public float fireRate = 0.5f;
    public GameObject shot;
    public GameObject shot1;
    public GameObject shot2;
    public Transform shotSpawn;
    private GameController gameController;
    public int pcscore;
    private float nextFire = 0.0f;
    public bool Flagboom;

    // Use this for initialization
    void Start ()
    {
    	GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
    	gameController = gameControllerObject.GetComponent<GameController> ();
    	Flagboom=false;
    	pcscore=gameController.GetScore();
    }

    // Update is called once per frame
    void Update ()
    {
    	pcscore=gameController.GetScore();
        if (Input.GetButton ("Fire1") && Time.time > nextFire) {
        	if(pcscore<10){
        		nextFire = Time.time + fireRate;
            	Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
            	GetComponent<AudioSource> ().Play ();
        	}else{
        		nextFire = Time.time + fireRate;
            	Instantiate (shot1, shotSpawn.position, shotSpawn.rotation);
            	GetComponent<AudioSource> ().Play ();
        	}
        	if(pcscore>10){
        		Flagboom=true;
        	}
            
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        Rigidbody rb = GetComponent<Rigidbody> ();
        if (rb != null) {
            rb.velocity = movement * speed;
            rb.position = new Vector3
            (
                Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
            );
            rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }
    void OnGUI(){
    	if(Flagboom){
    		int screenWidth=Screen.width;
	    	int screenHeight=Screen.height;
	    	if(GUI.Button(new Rect(screenWidth-200,50,150,20),"Boom")){
	    		nextFire = Time.time + fireRate;
            	Instantiate (shot2, new Vector3(0,0,0), shotSpawn.rotation);
            	GetComponent<AudioSource> ().Play ();
            	pcscore=pcscore-10;
	    	}
    	}
    }
}
