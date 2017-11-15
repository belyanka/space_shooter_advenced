using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	public int score;

	private void Start()
	{
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj!=null)
		{
			gameController = gameControllerObj.GetComponent<GameController>();
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}
		if (explosion!=null)
		{
			Instantiate(explosion, transform.position, transform.rotation);	
		}

		if (other.CompareTag("Player"))
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		gameController.AddScore(score);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
