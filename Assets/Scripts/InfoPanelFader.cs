using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelFader : MonoBehaviour
{
	[SerializeField] float Distance = 8.0f;
	[SerializeField] GameObject pinpoint;
	[SerializeField] Text SnackBarTextField;
	[SerializeField] [Multiline] string text;
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
			SnackBarTextField.text = text;
		}
		else
		{
			transform.localScale = new Vector3(0, 0, 0);
			pinpoint.SetActive(true);
		}
	}
}
