using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchAreaButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int poinerId;
	private bool canFire;

	private void Awake()
	{
		touched = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!touched)
		{
			touched = true;
			poinerId = eventData.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.pointerId == poinerId)
		{
			canFire = false;
			touched = false;
		}
	}

	public bool CanFire()
	{
		return canFire;
	}
}
