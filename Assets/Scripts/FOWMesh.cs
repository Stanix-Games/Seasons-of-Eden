using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWMesh : MonoBehaviour {
    [SerializeField] private float meshResolution = 2;
    [SerializeField] private Material material;

    private FieldOfView fow;
    private readonly List<Vector3> rays = new List<Vector3>();

    // Start is called before the first frame update
    void Start ()
    {
        fow = GetComponentInParent<FieldOfView> ();
    }

    private void Update()
    {
        // TODO: Optimize allocations. We could create one statically-sized list based on viewAngle + meshResolution
        // TODO: We could raytrace all points that are in range of collider for better perfomance and visual effects
        // But that might work pretty badly for hight-vertex count models, such as houses and such. Actuall speed benefits should be tested before that

        int halfSteps = Mathf.RoundToInt(fow.viewAngle * meshResolution * 0.5f);
        float stepAngle = fow.viewAngle / (halfSteps * 2f);

        for (int i = -halfSteps; i <= halfSteps; i++)
        {
            float angle = -i * stepAngle;
            Vector3 dir = fow.DirFromAngle(angle);

            // Casting from far away up to now
            Vector3 raycastStart = transform.position + dir.normalized * fow.viewRadius;
            Vector3 raycastEnd = transform.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(raycastStart, -dir, fow.viewRadius, fow.obstacleMask);

            if (hits.Length == 0)
            {
                if (rays.Count > 0)
                {
                    UpdateBatchAndDraw(rays);
                    rays.Clear();
                }
                continue;
            }

            // Result of raycast is always sorted from closest ones to start
            // This finds last point of closest object
            for (int j = hits.Length - 1; j >= 0; j--)
            {
                if (hits[j].collider != hits[hits.Length - 1].collider)
                {
                    break;
                }
                raycastEnd = hits[j].point;
            }

            rays.Add(raycastStart);
            rays.Add(raycastEnd);
        }

        if (rays.Count > 0)
        {
            UpdateBatchAndDraw(rays);
            rays.Clear();
        }

        // "Fog of war"
        float fromAngle = halfSteps * stepAngle;
        float toAngle = -halfSteps * stepAngle + 360;
        const int totalSteps = 2;
        for (int step = 0; step <= totalSteps; step++)
        {
            float angle = fromAngle + (toAngle - fromAngle) * step / totalSteps;
            rays.Add(transform.position);
            rays.Add(transform.position + (Vector3)fow.DirFromAngle(angle).normalized * fow.viewRadius);
        }
        UpdateBatchAndDraw(rays);
        rays.Clear();
    }

    private void UpdateBatchAndDraw(List<Vector3> rays)
    {
        // For every points ( Ignoring last one ) there is 1 triangle
        int triangles = rays.Count - 2;
        int[] indicies = new int[triangles * 3];
        Vector3[] verts = new Vector3[rays.Count];

        // TODO: We could create our own shader ( In theory, at least ) that always assumes normals = UP
        // This will free up 1 alloc
        Vector3[] normals = new Vector3[rays.Count];

        bool order = true;
        for (int i = 0; i < triangles; i += 1)
        {
            if (order)
            {
                indicies[(i * 3) + 2] = i + 0;
                indicies[(i * 3) + 1] = i + 1;
                indicies[(i * 3) + 0] = i + 2;
            } else
            {
                indicies[(i * 3) + 0] = i + 0;
                indicies[(i * 3) + 1] = i + 1;
                indicies[(i * 3) + 2] = i + 2;
            }
            order = !order;
        }

        for (int i = 0; i < rays.Count; i++)
        {
            verts[i] = transform.InverseTransformPoint(rays[i]);
            normals[i] = Vector3.forward;
        }

        var mesh = new Mesh();

        mesh.Clear();
        mesh.vertices = verts;
        // mesh.normals = normals;
        mesh.triangles = indicies;
        
        Graphics.DrawMesh(mesh, transform.localToWorldMatrix, material, 0);
    }
}