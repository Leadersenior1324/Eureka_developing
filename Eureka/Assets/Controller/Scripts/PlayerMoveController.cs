using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public SimpleTouchController leftController;
	public SimpleTouchController rightController;
	public Transform headTrans;
	public float speedMovements = 5f;
	public float speedProgressiveLook = 3000f;

	// PRIVATE
	private Rigidbody _rigidbody;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		rightController.TouchEvent += RightController_TouchEvent;
	}

	void RightController_TouchEvent (Vector2 value)
	{ 
		UpdateAim(value);
	}

	void Update()
	{
		// move
		_rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * speedMovements) +
			(transform.right * leftController.GetTouchPosition.x * speedMovements) );
	}

	void UpdateAim(Vector2 value)
	{
		if(headTrans != null)
		{
			Quaternion rot = Quaternion.Euler(0f,
				transform.localEulerAngles.y - value.x * -speedProgressiveLook,
				0f);

			_rigidbody.MoveRotation(rot);

			rot = Quaternion.Euler(headTrans.localEulerAngles.x - value.y * speedProgressiveLook,
				0f,
				0f);
			headTrans.localRotation = rot;

		}
		else
		{
			float xAxis = transform.localEulerAngles.x - value.y * speedProgressiveLook;

			if (xAxis > 50 && xAxis < 250)
			{
				xAxis = 50;
			}
			if (xAxis < 300 && xAxis > 270)
			{
				xAxis = 300;
			}
			Quaternion rot = Quaternion.Euler(xAxis,
				transform.localEulerAngles.y + value.x * speedProgressiveLook,
				0f);
			_rigidbody.MoveRotation(rot);
		}
	}

	void OnDestroy()
	{
		rightController.TouchEvent -= RightController_TouchEvent;
	}

}
