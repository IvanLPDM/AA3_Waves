using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OndaGerstner
{
    public float amplitud = 1f;
    public float longitudOnda = 2f;
    public float frecuencia = 1f;
    public float fase = 0f;
    public Vector2 direccion = Vector2.right;
    public float intensidad = 1f; 
}
public class Gerstner : MonoBehaviour
{
    public OndaGerstner[] ondas;

    private Mesh mesh;
    private Vector3[] verticesOriginales;
    private Vector3[] verticesDeformados;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verticesOriginales = mesh.vertices;
        verticesDeformados = new Vector3[verticesOriginales.Length];
    }

    void Update()
    {
        float tiempo = Time.time;

        for (int i = 0; i < verticesOriginales.Length; i++)
        {
            Vector3 vertice = verticesOriginales[i];
            Vector3 desplazamiento = Vector3.zero;

            foreach (var onda in ondas)
            {
                Vector2 dir = onda.direccion.normalized;
                float k = 2 * Mathf.PI / onda.longitudOnda;
                float v = onda.frecuencia * onda.longitudOnda;
                Vector2 posXZ = new Vector2(vertice.x, vertice.z);
                float theta = k * (Vector2.Dot(posXZ, dir) - v * tiempo) + onda.fase;

                float cosTheta = Mathf.Cos(theta);
                float sinTheta = Mathf.Sin(theta);

                desplazamiento.x += onda.intensidad * onda.amplitud * dir.x * cosTheta;
                desplazamiento.y += onda.amplitud * sinTheta;
                desplazamiento.z += onda.intensidad * onda.amplitud * dir.y * cosTheta;
            }

            verticesDeformados[i] = new Vector3(vertice.x, 0, vertice.z) + desplazamiento;
        }

        mesh.vertices = verticesDeformados;
        mesh.RecalculateNormals();
    }
}
