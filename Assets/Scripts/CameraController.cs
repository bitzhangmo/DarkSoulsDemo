using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public PlayerInput pi;
	public float horizontalSpeed = 20.0f;
	public float verticalSpeed = 20.0f;
	private GameObject playerHandle;
	private GameObject cameraHandle;
	private GameObject model;
	private GameObject camera;
	private float tempEulerx;
	private Vector3 cameraDampVelocity;

	public float cameraDampValue = 0.5f;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		cameraHandle = transform.parent.gameObject;
		playerHandle = cameraHandle.transform.parent.gameObject;
		tempEulerx = 20;
		model = playerHandle.GetComponent<ActorController>().model;
		camera = Camera.main.gameObject;
	}

	private void FixedUpdate()
	{
		Vector3 tempModelEuler = model.transform.eulerAngles;
		playerHandle.transform.Rotate(Vector3.up,pi.Jright *  horizontalSpeed * Time.fixedDeltaTime);
		tempEulerx -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
		cameraHandle.transform.localEulerAngles = new Vector3(Mathf.Clamp(tempEulerx,-40,30),0,0);
		model.transform.eulerAngles = tempModelEuler;

		camera.transform.position = Vector3.SmoothDamp(camera.transform.position,transform.position,ref cameraDampVelocity,cameraDampValue);
		//camera.transform.position = Vector3.Lerp(camera.transform.position,transform.position,0.2f);
		camera.transform.eulerAngles = transform.eulerAngles;
	}

}
