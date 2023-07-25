using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Diagnostics;
using static UnityEngine.UI.Image;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FieldOfView : CusMonoBehaviour
{
    
    public LayerMask layerMask;
    public float maxviewDistance;
    public float fov;
    public float speed;
    [SerializeField]
    public bool seenPlayer;
    public bool isPeek;

    private Mesh mesh;
    private Vector3 origin;
    private Vector3 origin2;
    private float viewDistance;
    public bool viewChecked = false;

    public static event Action OnSeenPlayer;

    protected override void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        viewDistance = 0f;
        seenPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        FOV();
        if (isPeek)
        {
            viewDisChange();
        }
    }
    
    private Vector3 getVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float getAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 360) n += 360;
        return n;
    }

    private void FOV()
    {
        int rayCount = 50;
        float angle = getAngleFromVector(-transform.up) + fov / 2f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        origin = transform.position;
        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, getVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + getVectorFromAngle(angle) * viewDistance;

            }
            else
            {
                vertex = raycastHit2D.point;

                if (raycastHit2D.collider.transform.GetComponent<Player>() != null)
                {
                    seenPlayer = true;
                    OnSeenPlayer.Invoke();
                }
            }
            vertices[vertexIndex] = transform.InverseTransformPoint(vertex);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void viewDisChange()
    {
        viewChecked = false;
        //seenPlayer = false;
        if (viewDistance <= maxviewDistance)
        {
            viewDistance += Time.deltaTime * speed;
        }
        else
        {
            viewDistance = 0f;
            isPeek = false;
            viewChecked = true;
        }
    }

}
