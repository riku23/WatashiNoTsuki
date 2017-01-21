using UnityEngine;
using System.Collections;

public class BlockControllerScript : MonoBehaviour {

	// Moon
	public GameObject moon;
	// Array containing the waves
	Transform[] seaBlocks;
	// Normalization factor
	public float normalization;
	// Horizontal limit distance
	public float hLimitDistance;
	// Temp vector
	Vector2 temp2;
	// Current distance
	float xDistance;
	// Displacement & maximum displacement
	float displacement;
	float maxDisplacement;
	// Number of childrens
	public int cCount;
	// Gaussian parameters
	public float xpctGauss = 0;
	public float vrncGauss = 5;
	public float normalizeGaussian = 10;

	// Use this for initialization
	void Start () {
		temp2 = new Vector2();

		// Get the childrens
		cCount = this.transform.childCount;
		seaBlocks = new Transform[cCount];
		for (int i = 0; i < cCount; i++) {
			seaBlocks[i] = this.transform.GetChild(i);	 
		}
	}
	
	// Update is called once per frame
	void Update () {
		maxDisplacement = 0f;

		// First loop to move the blocks
		foreach (Transform current in seaBlocks) {
			// Acquire temp values
			temp2 = current.position;
			xDistance = Mathf.Abs(moon.transform.position.x - temp2.x);

			// If it is in the wave area update the heigth
			if (xDistance < hLimitDistance) {
				if (moon.transform.position.y > 0) {
					displacement = normalization * moon.transform.position.y * getGaussianValue(xpctGauss, vrncGauss, xDistance * normalizeGaussian);
					temp2.y = displacement;
				} else {
					displacement = normalization * moon.transform.position.y * getGaussianValue(xpctGauss, vrncGauss, xDistance * normalizeGaussian);
					temp2.y = displacement;				
				}		
			} else {
				temp2 = current.position;
				temp2.y = 0f;
			}
			current.transform.position = temp2;

			// Update 
			if (Mathf.Abs(displacement) > maxDisplacement)
				maxDisplacement = Mathf.Abs(displacement);
		}

		// Second loop to normalize the displacement
		foreach (Transform current in seaBlocks) {
			// Acquire temp values
			temp2 = current.position;
			xDistance = Mathf.Abs(moon.transform.position.x - temp2.x);

			// If it is in the wave area update the heigth
			if (xDistance < hLimitDistance) {
				if (moon.transform.position.y > 0) {
					// temp2.y += maxDisplacement;
				} else {
					// temp2.y -= maxDisplacement;
				}
				current.transform.position = temp2;
			}
		}
	}

	// Generates a Gaussian (suggested values [0, 5])
	float getGaussianValue(float xpct, float vrnc, float x) {
		float c = Mathf.Sqrt (vrnc);
		float a = 1 / (c * Mathf.Sqrt (2 * Mathf.PI));
		float b = xpct;

		return a * Mathf.Exp (-1 * (x - b) * (x - b) / 2 * vrnc);
	}

}

//	displacement = xDistance / normalization * moon.transform.position.y * -1f * getGaussianValue(xpctGauss, vrncGauss, xDistance * normalizeGaussian);
//	Default parameters: [1, 100, 0, 0, 1, 0.4]