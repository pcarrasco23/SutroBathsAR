using UnityEngine;
using UnityEngine.UI;

public class AltitudeSliderScript : MonoBehaviour
{
	[SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
		_slider.onValueChanged.AddListener((v) =>
		{
			float x = this.transform.position.x;
			float z = this.transform.position.z;

			this.transform.position = new Vector3(x, v, z);
		});
    }
}
