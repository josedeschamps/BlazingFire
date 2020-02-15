using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeMotor : MonoBehaviour
{
	public float shakeTimer;
	public float shakeAmount;
	public float resetTimer = 1f;
	private float resetTime;
	private AudioSource camSFX;



	void Start()
	{

		camSFX = GetComponent<AudioSource>();
	}



	void Update()
	{

		if (shakeTimer >= 0)
		{

			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

			transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

			shakeTimer -= Time.deltaTime;
			resetTime -= Time.deltaTime;


		}

		if (resetTime < 0)
		{


			resetTime = resetTimer;
			ResetPosition();


		}

	}





	public void ShakeCamera(float shakePwr, float shakeDur)
	{


		shakeAmount = shakePwr;
		shakeTimer = shakeDur;
	//	camSFX.Play();
	}

	public void ResetPosition()
	{

		transform.position = new Vector3(0, 0, -10);
	}
}

