using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float tumble = 5.0f;

    void Start ()
    {
        GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
    }
	
    void Update ()
    {
    }
}
