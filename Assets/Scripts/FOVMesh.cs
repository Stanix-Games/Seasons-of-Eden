using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FOWMesh : MonoBehaviour {
    [SerializeField] private Material material;
    [SerializeField] private float shadowLength = 10.0f;

    private FieldOfView fov;
    private readonly List<Vector3> rays = new List<Vector3>();

    // Start is called before the first frame update
    void Start ()
    {
        fov = GetComponentInParent<FieldOfView>();
    }

    private void Update()
    {
        // TODO: Optimize allocations.
        // TODO: Result for concave shadows might not be optimal

        var colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(20f, 20f), 0f, fov.obstacleMask);
        var lookDir = fov.MakeDirectionFromAngle(0).normalized;

        foreach (var collider in colliders) {
            var points = Physics2DUtils.GetPointsCached(collider);
            CalculateAndBatchShadow(collider.transform, points);
        }

        // "Fog of war"
        var halfFOV = fov.viewAngle * 0.5f;
        var fromAngle = halfFOV;
        var toAngle = 360 - halfFOV;
        const int totalSteps = 3;
        for (var step = totalSteps; step >= 0; step--)
        {
            var angle = fromAngle + (toAngle - fromAngle) * step / totalSteps;
            rays.Add(transform.position);
            rays.Add(transform.position + (Vector3)fov.MakeDirectionFromAngle(angle).normalized * fov.viewRadius);
        }
        ResetRays();
    }

    private void CalculateAndBatchShadow(Transform pointsTransform, Vector2[] points)
    {
        var lastPoint = pointsTransform.TransformPoint(points[points.Length - 1]);
        lastPoint.z = transform.position.z;
        var totalSections = 0;
        var secondLoop = false;
        var hadReverseDirection = false;
        var isShadowSection = false;

        for (var i = 0; i < points.Length; i++)
        {
            var currentPoint = pointsTransform.TransformPoint(points[i]);
            currentPoint.z = transform.position.z;
            var normal = Vector2.Perpendicular(currentPoint - lastPoint);
            var deltaFromPoint = currentPoint - transform.position;

            // var middle = (lastPoint + currentPoint) * 0.5f;
            // Debug.DrawLine(middle, middle + (Vector3) normal, Color.green);
            // Debug.DrawLine(transform.position, transform.position + deltaFromPoint);

            // Check if normal looks to same direction as we are
            if (Vector2.Dot(deltaFromPoint, normal) > 0)
            {
                if (hadReverseDirection)
                {
                    if (!isShadowSection)
                    {
                        isShadowSection = true;
                        var deltaFromLast = lastPoint - transform.position;
                        AddRay(rays, lastPoint, lastPoint + deltaFromLast * shadowLength);
                    }
                    AddRay(rays, currentPoint, currentPoint + deltaFromPoint * shadowLength);
                }
            } else
            {
                isShadowSection = false;
                if (rays.Count > 0)
                {
                    totalSections++;
                    ResetRays();
                }
                // Section from first iteration has ended
                if (secondLoop)
                {
                    break;
                }
                hadReverseDirection = true;
            }
            
            // Check if current section loops back to first point
            if ((isShadowSection || totalSections == 0) && i == points.Length - 1)
            {
                // If we are inside of object then there wouldn't be any "starting" point of section as all edges should cast shadow
                if (secondLoop)
                {
                    if (isShadowSection)
                    {
                        break;
                    }
                    isShadowSection = true;
                    hadReverseDirection = true;
                }
                secondLoop = true;
                i = -1;
            }

            lastPoint = currentPoint;
        }
        // Final draw in case there something left in array
        ResetRays();
    }

    private void ResetRays()
    {
        if (rays.Count > 0)
        {
            UpdateMeshAndBatch(rays);
            rays.Clear();
        }
    }

    private void AddRay(List<Vector3> rays, Vector3 from, Vector3 to)
    {
        // Debug.DrawLine(from, to, predefColors[rays.Count / 2]);
        rays.Add(from);
        rays.Add(to);
    }

    private void UpdateMeshAndBatch(List<Vector3> rays)
    {
        // For every points ( Ignoring last one ) there is 1 triangle
        var triangles = rays.Count - 2;

        var indicies = new int[triangles * 3];
        var verts = new Vector3[rays.Count];
        var normals = new Vector3[rays.Count];

        // Toggles order of triangles to make sure that they are always rendered in same clock-direction
        var order = false;
        for (var i = 0; i < triangles; i += 1)
        {
            if (order)
            {
                indicies[i * 3 + 2] = i + 0;
                indicies[i * 3 + 1] = i + 1;
                indicies[i * 3 + 0] = i + 2;
            } else
            {
                indicies[i * 3 + 0] = i + 0;
                indicies[i * 3 + 1] = i + 1;
                indicies[i * 3 + 2] = i + 2;
            }
            order = !order;
        }

        for (var i = 0; i < rays.Count; i++)
        {
            verts[i] = rays[i];
            normals[i] = new Vector3(0, 0, 1);
        }

        var mesh = new Mesh
        {
            vertices = verts,
            triangles = indicies,
            normals = normals
        };

        Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
    }
}