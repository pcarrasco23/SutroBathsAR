using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arrow
{
    public class AnimatedArrowRenderer : ArrowRenderer
    {
        [Space] [SerializeField] float fadeDistance = 0.35f;
        [SerializeField] float speed = 1f;
        [SerializeField] GameObject tipPrefab;
        [SerializeField] GameObject segmentPrefab;

        Transform arrow;

        readonly List<Transform> segments = new();
        readonly List<MeshRenderer> renderers = new();

        protected override float Offset => Time.time * speed;
        protected override float FadeDistance => fadeDistance;

        protected override void Update()
        {
            base.Update();
            UpdateSegments();
        }

        void UpdateSegments()
        {
            Debug.DrawLine(start, end, Color.yellow);

            CheckSegments(Positions.Count - 1);

            for (var i = 0; i < Positions.Count - 1; i++)
            {
                segments[i].localPosition = Positions[i];
                segments[i].localRotation = Rotations[i];

                var meshRenderer = renderers[i];

                if (!meshRenderer)
                    continue;

                var material = meshRenderer.material;

                var currentColor = material.color;
                currentColor.a = Alphas[i];
                material.color = currentColor;
            }

            if (!arrow)
                arrow = Instantiate(tipPrefab, transform).transform;

            arrow.localPosition = Positions.Last();
            arrow.localRotation = Rotations.Last();

            transform.position = start;
            transform.rotation = Quaternion.LookRotation(end - start, upwards);
        }

        void CheckSegments(int segmentsCount)
        {
            while (segments.Count < segmentsCount)
            {
                var segment = Instantiate(segmentPrefab, transform).transform;
                segments.Add(segment);
                renderers.Add(segment.GetComponent<MeshRenderer>());
            }

            for (var i = 0; i < segments.Count; i++)
            {
                var segment = segments[i].gameObject;
                if (segment.activeSelf != i < segmentsCount)
                    segment.SetActive(i < segmentsCount);
            }
        }
    }
}