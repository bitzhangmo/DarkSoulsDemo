using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// Variable
	[Header("***** Key settings *****")]
	public string keyUp = "w";
	public string keyDown = "s";
	public string keyLeft = "a";
	public string keyRight = "d";

	public string keyA = "j";
	public string keyB;
	public string keyC;
	public string keyD;

	public string keyJRight;
	public string keyJLeft;
	public string keyJUp;
	public string keyJDown;


	[Header("***** Output signals *****")]
	public float Dup;
	public float Dright;
	public float Dmag;
	public Vector3 Dvec;

	public float Jup;
	public float Jright;

	// pressing signal
	public bool run;

	// trigger one signal
	public bool jump;
	private bool lastJump;
	public bool attack;
	private bool lastAttack;

	// double trigger signal
	[Header("***** Others *****")]
	public bool inputEnabled = true;

	private float targetDup;
	private float targetDright;
	private float velocityDup;
	private float velocityDright;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Jup = (Input.GetKey(keyJUp)?1.0f:0) - (Input.GetKey(keyJDown)?1.0f:0);
		Jright = (Input.GetKey(keyJRight)?1.0f:0) - (Input.GetKey(keyJLeft)?1.0f:0);

		targetDup = (Input.GetKey(keyUp)? 1.0f:0) - (Input.GetKey(keyDown)?1.0f : 0);
		targetDright = (Input.GetKey(keyRight)? 1.0f:0) - (Input.GetKey(keyLeft)?1.0f : 0);

		if(inputEnabled == false)
		{
			targetDup = 0;
			targetDright = 0;
		}

		Dup = Mathf.SmoothDamp(Dup , targetDup , ref velocityDup , 0.1f);
		Dright = Mathf.SmoothDamp(Dright , targetDright , ref velocityDright, 0.1f);
		
		Vector2 tempDAxis = SquareToCircle(new Vector2(Dright,Dup));
		float Dright2 = tempDAxis.x;
		float Dup2 = tempDAxis.y;

		Dmag = Mathf.Sqrt((Dup2*Dup2)+(Dright2*Dright2));
		Dvec = Dright2 * transform.right + Dup2 * transform.forward;
	
		run = Input.GetKey(keyA);

		bool newJump = Input.GetKey(keyB);
		//jump = tempJump;
		if(newJump != lastJump && newJump == true)
		{
			jump = true;
			//print("jump trigger");
		}
		else jump = false;
		lastJump = newJump;

		
		bool newAttack = Input.GetKey(keyC);
		//jump = tempJump;
		if(newAttack != lastAttack && newAttack == true)
		{
			attack = true;
			//print("jump trigger");
		}
		else attack = false;
		lastAttack = newAttack;
	}

	private Vector2 SquareToCircle(Vector2 input)
	{
		Vector2 output = Vector2.zero;
		output.x = input.x * Mathf.Sqrt(1-(input.y*input.y)/2.0f);
		output.y = input.y * Mathf.Sqrt(1-(input.x*input.x)/2.0f);
		return output;
	}
}
