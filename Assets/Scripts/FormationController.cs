using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float speed;
	public float padding;

	private int direction = 1;
	private float boundaryLeftEdge, boundaryRightEdge;

	// Use this for initialization
	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		boundaryLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		boundaryRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}

	// Update is called once per frame
	void Update() {
		// check if we need to flip direction
		float formationLeftEdge = transform.position.x - 0.5f * width;
		float formationRightEdge = transform.position.x + 0.5f * width;
		if (formationRightEdge > boundaryRightEdge || formationLeftEdge < boundaryLeftEdge) {
			direction *= -1;
		}

		// move the position
		transform.position += Vector3.right * direction * speed * Time.deltaTime;
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
}
