using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public GameObject laser;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		float padding = GetComponent<Renderer>().bounds.size.x / 2.0f;
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			GameObject laserIns = Instantiate(laser, transform.position, Quaternion.identity);
			laserIns.transform.Translate(new Vector3(0f, 0.4f));
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-speed * Time.deltaTime, 0f);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += new Vector3(speed * Time.deltaTime, 0f);
		}

		// restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
