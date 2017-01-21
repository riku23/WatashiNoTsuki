using UnityEngine;
using System.Collections;

public class MoveWaveScript : MonoBehaviour {
	
	// Initial position
	Vector3 initial;
	// Speed
	public float speed = 1f;
	// Target
	Vector3 target, targetNormalized;
	// Has it reached the target?
	bool reached = true;
	// Amplitude of the movement
	public float amplitude = 0.1f;
	// When is the target reached?
	public float yDeltaReached = 1f;
	// Father transform
	Transform father;
	// Support variables
	Vector3 delta;
	float step;

	// Use this for initialization
	void Start () {
		initial = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (reached) {
			target = new Vector3 (initial.x, Random.Range (-1f, 1f) * amplitude, initial.z);
			reached = false;
		} else {
			step = Time.deltaTime * speed;
			targetNormalized = target;
			targetNormalized.y = target.y + this.transform.parent.position.y;
			delta = Vector3.MoveTowards (this.transform.position, targetNormalized, step);
			delta.x = initial.x;
			this.transform.position = delta;

			if (Mathf.Abs(this.transform.position.y - targetNormalized.y) < yDeltaReached)
				reached = true;
		}
	}


}