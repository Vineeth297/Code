using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float angle;
    [SerializeField] private float height;
    public Color meshColor = Color.yellow;

    public int scanFrequency = 30;
    public LayerMask layers;

    private Collider[] _colliders = new Collider[50];

    private Mesh _mesh;
    private int _count;
    private float _scanInterval;
    private float _scanTimer;
    
    private void Start()
    {
        _scanInterval = 1.0f / scanFrequency;
    }

    void Update()
    {
        _scanTimer += Time.deltaTime;
        if (_scanTimer < 0)
        {
            _scanTimer += _scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, distance, _colliders, layers, QueryTriggerInteraction.Collide);
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numOfTriangles = (segments * 4) + 2 + 2;
        int numOfVertices = numOfTriangles * 3;
        
        Vector3[] vertices = new Vector3[numOfVertices];
        int[] triangles = new int[numOfVertices];
        
        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0f, -angle, 0f) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vertex = 0;
        //Left
        vertices[vertex++] = bottomCenter;
        vertices[vertex++] = bottomLeft;
        vertices[vertex++] = topLeft;

        vertices[vertex++] = topLeft;
        vertices[vertex++] = topCenter;
        vertices[vertex++] = bottomCenter;

        //Right
        vertices[vertex++] = bottomCenter;
        vertices[vertex++] = topCenter;
        vertices[vertex++] = topRight;

        vertices[vertex++] = topRight;
        vertices[vertex++] = bottomRight;
        vertices[vertex++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0f, currentAngle + deltaAngle, 0f) * Vector3.forward * distance;
            
            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;
            
            //Front
            vertices[vertex++] = bottomLeft;
            vertices[vertex++] = bottomRight;
            vertices[vertex++] = topRight;
        
            vertices[vertex++] = topRight;
            vertices[vertex++] = topLeft;
            vertices[vertex++] = bottomLeft;
        
            //Top
            vertices[vertex++] = topCenter;
            vertices[vertex++] = topLeft;
            vertices[vertex++] = topRight;
        
            //Bottom
            vertices[vertex++] = bottomCenter;
            vertices[vertex++] = bottomRight;
            vertices[vertex++] = bottomLeft;
            
            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numOfVertices; i++)
        {
            triangles[i] = i;
        }
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }

    private void OnValidate()
    {
        _mesh = CreateMesh();
        _scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (_mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(_mesh,transform.position,transform.rotation);
        }
        
        Gizmos.DrawWireSphere(transform.position,distance);
        for (int i = 0; i < _count; i++)
        {    
            Gizmos.DrawSphere(_colliders[i].transform.position,1f);
        }
    }
}
