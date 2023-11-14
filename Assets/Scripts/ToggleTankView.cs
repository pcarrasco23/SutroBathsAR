using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTankView : MonoBehaviour
{
	[SerializeField] private Toggle _toggle;
	[SerializeField] private GameObject _mainBuilding;
	[SerializeField] private GameObject _tanks;

	// Start is called before the first frame update
	void Start()
	{
		_toggle.onValueChanged.AddListener((b) =>
		{
			if (b)
			{
				_mainBuilding.SetActive(false);
				_tanks.SetActive(true);
			}
			else
			{
				_mainBuilding.SetActive(true);
				_tanks.SetActive(false);
			}
		});
	}
}
