using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderScript : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private TextMeshProUGUI _sliderText;
	[SerializeField] private GameObject _modernEntrance;
	[SerializeField] private GameObject _artdecoEntrance;

	// Start is called before the first frame update
	void Start()
    {
		_slider.onValueChanged.AddListener((v) =>
		{
			_sliderText.text = v.ToString("0");

			if (v > 1949)
			{
				_modernEntrance.SetActive(true);
				_artdecoEntrance.SetActive(false);
			}
			else if (v > 1933)
			{
				_modernEntrance.SetActive(false);
				_artdecoEntrance.SetActive(true);
			}
			else
			{
				_modernEntrance.SetActive(false);
				_artdecoEntrance.SetActive(false);
			}
		});
	}
}
