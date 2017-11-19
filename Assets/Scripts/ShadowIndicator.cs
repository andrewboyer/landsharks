using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIndicator : MonoBehaviour {

	public GameObject other;
	public Camera cam;
	public float edgeDist; //how far away from the edge of the screen the indicator is
	public GameObject indicator;

	public float camHeight;
	public float camWidth;
	private float boxHeight;
	private float boxWidth;
	private float q1; //angle to the corner of the bounding box in x quadrant
	private float q2;
	private float q3;
	private float q4;

	// Use this for initialization
	void Start () {

		boxHeight = camHeight - 2 * edgeDist;
		boxWidth = camWidth - 2 * edgeDist;

		//in radians
		q1 = Mathf.Atan2(camHeight / 2f, camWidth / 2f);
		q2 = Mathf.PI - q1;
		q3 = q1 + Mathf.PI;
		q4 = 2f * Mathf.PI - q1;

		Debug.Log(new Vector2(boxWidth, boxHeight));
		Debug.Log (new Vector4 (q1, q2, q3, q4));
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 playerPos = cam.transform.position;
		Vector2 otherPos = other.transform.position;
		Vector2 camPos = cam.gameObject.transform.position;
		Vector2 diff = otherPos - camPos;
		float l = camPos.x - camWidth / 2f;
		float r = camPos.x + camWidth / 2f;
		float t = camPos.y + camHeight / 2f;
		float b = camPos.y - camHeight / 2f;

		if (l <= otherPos.x && otherPos.x <= r && b <= otherPos.y && otherPos.y <= t) //can the other player be seen?
			indicator.SetActive (false);
		
		else {

			indicator.SetActive (true);
			float x = playerPos.x;
			float y = playerPos.y;
			float angle = Mathf.Atan (diff.y / diff.x);
			if (otherPos.x < playerPos.x)
				angle += Mathf.PI;
			if (angle < 0)
				angle += (2f * Mathf.PI);
			Debug.Log (angle);
			indicator.transform.eulerAngles = Vector3.forward * (angle * Mathf.Rad2Deg - 90f);

			float px = playerPos.x;
			float py = playerPos.y;
			float ox = otherPos.x;
			float oy = otherPos.y;
			float m = (oy - py) / (ox - px);

			if (angle <= q1 || angle >= q4) { //indicator on right edge

				x = camPos.x + boxWidth / 2f;
				y = (m * x) - (m * px) + py;

			} else if (q2 >= angle && angle >= q1) { //indicator on top edge

				y = camPos.y + boxHeight / 2f;
				x = ((y - py) / m) + px;

			} else if (q3 >= angle && angle >= q2) { //indicator on left edge

				x = camPos.x - boxWidth / 2f;
				y = (m * x) - (m * px) + py;

			} else if (q4 >= angle && angle >= q3) { //indicator on bottom edge

				y = camPos.y - boxHeight / 2f;
				x = ((y - py) / m) + px;

			} else {
				
				Debug.Log ("INVALID INDICATOR ANGLE: " + angle.ToString ());

			}

			indicator.transform.position = new Vector2 (x, y);

		}
		
	}
}
