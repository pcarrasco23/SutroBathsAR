using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arrow
{
    public class SimpleArrowRenderer : ArrowRenderer
    {
        [Space] [SerializeField] float tipWidth = 0.2f;
        [SerializeField] float tipLength = 0.5f;
        [SerializeField] float bodyWidth = 0.1f;
        [SerializeField] MeshFilter tipMeshFilter;
        [SerializeField] MeshFilter bodyMeshFilter;

        readonly List<Vector3> vertices = new();
        readonly List<int> triangles = new();
        readonly List<Vector2> uvs = new();

        Mesh bodyMesh;
        Mesh tipMesh;

        void Awake()
        {
            bodyMesh = new Mesh();
            tipMesh = new Mesh();
        }

        public override void SetPositions(Vector3 startPosition, Vector3 endPosition)
        {
            base.SetPositions(startPosition, endPosition);
            UpdateMeshes();
        }

        void UpdateMeshes()
        {
            var totalLength = 0f;
            for (var i = 0; i < Positions.Count - 1; i++)
            {
                totalLength += Vector3.Distance(Positions[i], Positions[i + 1]);
            }

            var lineLength = totalLength - tipLength;
            var currentLength = 0f;
            var bodyPositions = new List<Vector3> { Positions[0] };
            for (var i = 0; i < Positions.Count - 1; i++)
            {
                var distance = Vector3.Distance(Positions[i], Positions[i + 1]);
                currentLength += distance;
                if (currentLength < lineLength)
                    bodyPositions.Add(Positions[i + 1]);
                else
                {
                    var pos = Vector3.Lerp(Positions[i], Positions[i + 1],
                        1f - (currentLength - lineLength) / distance);

                    bodyPositions.Add(pos);
                    break;
                }
            }

            CreateLineMesh(bodyPositions, bodyWidth, bodyMesh);
            CreateLineMesh(new[] { bodyPositions.Last(), Positions.Last() }, tipWidth, tipMesh);

            bodyMeshFilter.mesh = bodyMesh;
            tipMeshFilter.mesh = tipMesh;

            transform.position = start;
            transform.rotation = Quaternion.LookRotation(end - start, upwards);
        }

        void CreateLineMesh(IReadOnlyList<Vector3> positions, float width, Mesh mesh)
        {
            var widthDirection = Vector3.right;

            vertices.Clear();
            foreach (var position in positions)
            {
                vertices.Add(position + widthDirection * (width / 2f));
                vertices.Add(position - widthDirection * (width / 2f));
            }

            triangles.Clear();
            for (var i = 0; i < vertices.Count - 3; i += 2)
            {
                AddTriangle(i, i + 1, i + 3);
                AddTriangle(i, i + 3, i + 2);
            }

            var totalLength = 0f;
            for (var i = 0; i < positions.Count - 1; i++)
            {
                totalLength += Vector3.Distance(positions[i], positions[i + 1]);
            }

            uvs.Clear();
            var length = 0f;
            for (var i = 0; i < positions.Count - 1; i++)
            {
                uvs.Add(new Vector2(length / totalLength, 1f));
                uvs.Add(new Vector2(length / totalLength, 0f));
                length += Vector3.Distance(positions[i], positions[i + 1]);
            }

            uvs.Add(new Vector2(1f, 1f));
            uvs.Add(new Vector2(1f, 0f));

            mesh.Clear();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        void AddTriangle(int i0, int i1, int i2)
        {
            triangles.Add(i0);
            triangles.Add(i1);
            triangles.Add(i2);
        }
    }
}