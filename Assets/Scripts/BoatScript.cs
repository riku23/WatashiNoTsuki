using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour {
    public float clamp;

    // Use this for initialization
    void Start () {
	
	}
	
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.forward), clamp);
        //transform.eulerAngles = new Vector3(0,0,Mathf.Clamp(transform.rotation.eulerAngles.z, MinClamp, MaxClamp));
    }
	// Update is called once per frame

}
