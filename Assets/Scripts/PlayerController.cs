using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{

	private Rigidbody _rigidbody;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public Transform shotSpawn;
	public GameObject shot;
	public SimpleTouchPad touchPad;
	public TouchAreaButton areaButton;
	
	private AudioSource _audioSource;
	private Quaternion calibrationQuaternion;
	private Matrix4x4 calibrationMatrix;
	
	public float fireRate;

	private float nextFire;
	
	private void Start()
	{
		CalibrateAccelerometer();
		_rigidbody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (areaButton.CanFire() && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);	
			_audioSource.Play();
		}
	}

	private void FixedUpdate()
	{
//		float moveHorizontal = Input.GetAxis("Horizontal");
//		float moveVertical = Input.GetAxis("Vertical");
		
		//_rigidbody.velocity = new Vector3(moveHorizontal,0f, moveVertical) * speed;

//		Vector3 acceleration = FixAcceleration(Input.acceleration);
//		_rigidbody.velocity = new Vector3(acceleration.x,0f, acceleration.y) * speed;

		Vector2 direction = touchPad.GetDirection();
		_rigidbody.velocity = new Vector3(direction.x,0f, direction.y) * speed;
		_rigidbody.position=new Vector3(
			Mathf.Clamp(_rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(_rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		_rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, _rigidbody.velocity.x * -tilt);
	}
	
	//Used to calibrate the Iput.acceleration input
	void CalibrateAccelerometer () {
//		Vector3 accelerationSnapshot = Input.acceleration;
//		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
//		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0, 0, -1), accelerationSnapshot);
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
		calibrationMatrix = matrix.inverse;
	}
    
	//Get the 'calibrated' value from the Input
	Vector3 FixAcceleration (Vector3 acceleration) {
//		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
//		return fixedAcceleration;
		Vector3 fixAcceeleration = calibrationMatrix.MultiplyVector(acceleration);
		return fixAcceeleration;
	}
}
