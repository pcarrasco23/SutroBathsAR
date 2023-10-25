using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelFader : MonoBehaviour
{
	[SerializeField] float Distance = 8.0f;
	[SerializeField] GameObject pinpoint;
	private Vector3 originalScale;

	private void Start()
	{
		originalScale = transform.localScale;
		transform.localScale = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
    {
		float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
		if (dist < Distance)
		{
			transform.localScale = originalScale;
			pinpoint.SetActive(false);
		}
		else
		{
			transform.localScale = new Vector3(0, 0, 0);
			pinpoint.SetActive(true);
		}
	}
}
