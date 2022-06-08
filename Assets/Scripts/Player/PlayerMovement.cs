using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	private Animator anim;
	private Camera cam;
	private CharacterController controller;

	private Vector3 desiredMoveDirection;
	private Vector3 moveVector;

	public Vector2 moveAxis;
	private float verticalVel;

	[Header("Settings")]
	[SerializeField] float movementSpeed;
	[SerializeField] float runmovementSpeed;
	[SerializeField] float walkmovementSpeed;
	[SerializeField] float crouchmovementSpeed;
	[SerializeField] float rotationSpeed = 0.1f;
	[SerializeField] float fallSpeed = .2f;
	public float acceleration = 1;

	[Header("Booleans")]
	[SerializeField] bool blockRotationPlayer;
	private bool isGrounded;
	public static bool crawl;
	public static bool isSprinting = false;
	private bool isSprintingKeyPressed = false;
	public static bool isHidden;

	void Start()
	{
		anim = this.GetComponent<Animator>();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController>();
	}

	void Update()
	{
		InputMagnitude();

		isGrounded = controller.isGrounded;

		if (isGrounded)
			verticalVel -= 0;
		else
			verticalVel -= 1;

		moveVector = new Vector3(0, verticalVel * fallSpeed * Time.deltaTime, 0);
		controller.Move(moveVector);

		if (!isSprinting && !crawl)
		{
			SprintEndCheck();
		}

	}

	void PlayerMoveAndRotation()
	{
		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		desiredMoveDirection = forward * moveAxis.y + right * moveAxis.x;

		if (blockRotationPlayer == false)
		{
			//Camera
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed * acceleration);
			controller.Move(desiredMoveDirection * Time.deltaTime * (movementSpeed * acceleration));
		}
		else
		{
			//Strafe
			controller.Move((transform.forward * moveAxis.y + transform.right * moveAxis.y) * Time.deltaTime * (movementSpeed * acceleration));
		}
	}

	public void LookAt(Vector3 pos)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), rotationSpeed);
	}

	public void RotateToCamera(Transform t)
	{
		var forward = cam.transform.forward;

		desiredMoveDirection = forward;
		Quaternion lookAtRotation = Quaternion.LookRotation(desiredMoveDirection);
		Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		t.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, rotationSpeed);
	}


	void CheckCrouch()
	{ 
		crawl = !crawl;

		if (crawl)
		{
			anim.SetBool("Crawl", true);
			StartCoroutine(CrouchAnimCoroutine(1.5f));
			movementSpeed = crouchmovementSpeed;
		}
		else
		{
			anim.SetBool("Crawl", false);
			StartCoroutine(StandAnimCoroutine(1.05f));
			movementSpeed = walkmovementSpeed;
		}


		IEnumerator CrouchAnimCoroutine(float duration)
		{
			this.acceleration = 0;
			this.enabled = false;
			anim.Play("Kneeling Down");
			anim.Play("Injured Kneeling Down");
			yield return new WaitForSeconds(duration);
			this.acceleration = 1;
			this.enabled = true;

		}

		IEnumerator StandAnimCoroutine(float duration)
		{
			this.acceleration = 0;
			this.enabled = false;
			anim.Play("Standing");
			anim.Play("Injured Standing");
			yield return new WaitForSeconds(duration);
			this.acceleration = 1;
			this.enabled = true;

		}
	}


	void SprintStartCheck()
	{
		isSprinting = true;

		if (crawl)
		{
			anim.SetBool("Crawl", false);
			StartCoroutine(StandAnimCoroutine(1.05f));
		}
		anim.SetBool("Sprint", true);
		movementSpeed = runmovementSpeed;

		IEnumerator StandAnimCoroutine(float duration)
		{
			this.acceleration = 0;
			this.enabled = false;
			anim.Play("Standing");
			anim.Play("Injured Standing");
			yield return new WaitForSeconds(duration);
			this.acceleration = 1;
			this.enabled = true;

		}
	}

	void SprintEndCheck()
	{
		if (crawl)
		{
			anim.SetBool("Crawl", true);
			StartCoroutine(CrouchAnimCoroutine(1.25f));
		}
		anim.SetBool("Sprint", false);
		movementSpeed = walkmovementSpeed;

		IEnumerator CrouchAnimCoroutine(float duration)
		{
			this.acceleration = 0;
			this.enabled = false;
			anim.Play("Kneeling Down");
			anim.Play("Injured Kneeling Down");
			yield return new WaitForSeconds(duration);
			this.acceleration = 1;
			this.enabled = true;

		}

	}

	public void IsHidden()
	{
		isHidden = true;
	}

	public void isNotHidden()
	{
		isHidden = false;
	}

	void InputMagnitude()
	{
		//Calculate the Input Magnitude
		float inputMagnitude = new Vector2(moveAxis.x, moveAxis.y).sqrMagnitude;

		//Physically move player
		if (inputMagnitude > 0.1f)
		{
			anim.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f, Time.deltaTime);
			PlayerMoveAndRotation();
		}
		else
		{
			anim.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f, Time.deltaTime);
		}

		if (moveAxis.x == 0f && moveAxis.y == 0f && inputMagnitude < .1f && isSprintingKeyPressed && !crawl)
		{
			isSprinting = false;
		}
		else if(isSprintingKeyPressed && !crawl)
		{
			SprintStartCheck();
		}

		if (isSprintingKeyPressed && crawl && anim.GetBool("Sprint") == true)
		{
			crawl = false;
		}

	}

	#region Input

	public void OnMove(InputValue value)
	{
		moveAxis.x = value.Get<Vector2>().x;
		moveAxis.y = value.Get<Vector2>().y;
	}

	public void OnCrouch()
	{
		CheckCrouch();
	}

	public void OnSprintStart()
	{
		isSprintingKeyPressed = true;
		SprintStartCheck();			
	}

	public void OnSprintEnd()
	{
		isSprintingKeyPressed = false;
		SprintEndCheck();
	}

	#endregion

	private void OnDisable()
	{
		anim.SetFloat("InputMagnitude", 0);
	}
}
