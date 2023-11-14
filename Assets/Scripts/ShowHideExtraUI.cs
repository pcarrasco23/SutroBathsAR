using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideExtraUI : MonoBehaviour
{
	[SerializeField] float Distance = 8.0f;
	[SerializeField] private GameObject uiObject;

	// Update is called once per frame
	void Update()
	{
		float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
		if (dist < Distance)
		{
			uiObject.SetActive(true);
		}
		else
		{
			uiObject.SetActive(false);
		}
	}
}
