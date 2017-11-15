using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;

	void Start () 
	{
		startPosition = transform.position;
	}
    
	void Update ()
	{
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
	/*
	[SerializeField]
        private float rollSpeed;

        private Material m;

        private void Awake()
        {
            m = GetComponent<Renderer>().material;
        }

  void Update ()
  {
            float y = m.mainTextureOffset.y;
            y += rollSpeed * Time.deltaTime;
            y = Mathf.Repeat(y, 1);
            Vector2 offset = new Vector2(m.mainTextureOffset.x, y);
            m.mainTextureOffset = offset;
  }*/
}
