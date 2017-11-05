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
	private AudioSource _audioSource;
	
	public float fireRate;

	private float nextFire;
	
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);	
			_audioSource.Play();
		}
	}

	private void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		_rigidbody.velocity = new Vector3(moveHorizontal,0f, moveVertical) * speed;
		
		_rigidbody.position=new Vector3(
			Mathf.Clamp(_rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(_rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		_rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, _rigidbody.velocity.x * -tilt);
	}
}
