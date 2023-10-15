using UnityEngine;
using UnityEngine.UI;

namespace Arrow.Demo
{
    public class FollowMouseDemo : MonoBehaviour
    {
        [SerializeField] float distanceFromScreen = 5f;
        [SerializeField] Toggle[] toggles;
        [SerializeField] ArrowRenderer[] arrows;

        Camera mainCamera;
        ArrowRenderer currentArrow;

        void Awake()
        {
            mainCamera = Camera.main;
            SetCurrentArrow(arrows[0]);

            for (var i = 0; i < toggles.Length && i < arrows.Length; i++)
            {
                var arrow = arrows[i];
                toggles[i].onValueChanged.AddListener(OnToggleValueChanged);

                void OnToggleValueChanged(bool value)
                {
                    if (value)
                        SetCurrentArrow(arrow);
                }
            }
        }

        void Update()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = distanceFromScreen;

            var worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            currentArrow.SetPositions(transform.position, worldMousePosition);
        }

        void SetCurrentArrow(ArrowRenderer arrow)
        {
            currentArrow = arrow;

            foreach (var arrow0 in arrows)
            {
                arrow0.gameObject.SetActive(arrow0 == currentArrow);
            }
        }
    }
}