using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public Transform shotSpawn;
	public GameObject shot;
	private AudioSource _audioSource;

	public float fireRate;
	public float fireDelay;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", fireDelay, fireRate);
	}

	private void Fire(){
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		_audioSource.Play ();
	}

}
