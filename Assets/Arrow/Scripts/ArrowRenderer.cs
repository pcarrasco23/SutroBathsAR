using System.Collections.Generic;
using UnityEngine;

namespace Arrow
{
    public abstract class ArrowRenderer : MonoBehaviour
    {
        [SerializeField] float height = 0.5f;
        [SerializeField] float segmentLength = 0.5f;

        [Space] [SerializeField] protected Vector3 start;
        [SerializeField] protected Vector3 end;
        [SerializeField] protected Vector3 upwards = Vector3.up;

        protected readonly List<Vector3> Positions = new();
        protected readonly List<Quaternion> Rotations = new();
        protected readonly List<float> Alphas = new();

        protected virtual float Offset => 0f;
        protected virtual float FadeDistance => 0f;

        public virtual void SetPositions(Vector3 startPosition, Vector3 endPosition)
        {
            start = startPosition;
            end = endPosition;
            Update();
        }

        protected virtual void Update()
        {
            UpdateArrowData();
        }

        void UpdateArrowData()
        {
            var distance = Vector3.Distance(start, end);
            var radius = height / 2f + distance * distance / (8f * height);
            var diff = radius - height;
            var arcAngleRad = 2f * Mathf.Acos(diff / radius);
            var arcLength = arcAngleRad * radius;
            var arcAngle = arcAngleRad * Mathf.Rad2Deg;
            var segmentAngle = segmentLength / radius * Mathf.Rad2Deg;

            var center = new Vector3(0, -diff, distance / 2f);
            var left = Vector3.zero;
            var right = new Vector3(0, 0, distance);

            var segmentsCount = (int)(arcLength / segmentLength) + 1;

            var offset = segmentAngle * Mathf.Repeat(Offset, 1);
            var firstSegmentPos = Quaternion.Euler(offset, 0f, 0f) * (left - center) + center;

            var fadeStartDistance = AngleToDistance(segmentAngle / 2f);

            Positions.Clear();
            Rotations.Clear();
            Alphas.Clear();

            for (var i = 0; i < segmentsCount; i++)
            {
                var currentAngle = segmentAngle * i;
                var pos = Quaternion.Euler(currentAngle, 0f, 0f) * (firstSegmentPos - center) + center;

                var distance0 = AngleToDistance(currentAngle + offset);
                var distance1 = AngleToDistance(arcAngle - currentAngle - offset) - FadeDistance;

                Positions.Add(pos);
                Rotations.Add(Quaternion.FromToRotation(Vector3.up, pos - center));
                Alphas.Add(GetAlpha(distance0, distance1, fadeStartDistance));
            }

            Positions.Add(right);
            Rotations.Add(Quaternion.FromToRotation(Vector3.up, right - center));
            Alphas.Add(GetAlpha(arcLength, 0f, fadeStartDistance));

            float AngleToDistance(float angle0) => angle0 * radius / Mathf.Rad2Deg;

            float GetAlpha(float distance0, float distance1, float distanceMax) =>
                Mathf.Clamp01(Mathf.Clamp01(distance0 / distanceMax) + Mathf.Clamp01(distance1 / distanceMax) - 1f);
        }
    }
}