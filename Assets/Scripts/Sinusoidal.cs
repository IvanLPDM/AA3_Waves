using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OndaSinusoidal
{
    public float amplitud = 1f;       
    public float longitudOnda = 2f;  
    public float frecuencia = 1f;    
    public float fase = 0f;         
    public Vector2 direccion = Vector2.right; 
}

public class Sinusoidal : MonoBehaviour
{
    public OndaSinusoidal onda;

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
        float velocidad = onda.frecuencia * onda.longitudOnda; // v = f * L

        for (int i = 0; i < verticesOriginales.Length; i++)
        {
            Vector3 vertice = verticesOriginales[i];
            Vector2 posicionXZ = new Vector2(vertice.x, vertice.z);
            float direccionDot = Vector2.Dot(posicionXZ, onda.direccion.normalized);

            float altura = onda.amplitud * Mathf.Sin(
                (2 * Mathf.PI / onda.longitudOnda) * (direccionDot - velocidad * tiempo) + onda.fase
            );

            verticesDeformados[i] = new Vector3(vertice.x, altura, vertice.z);
        }

        mesh.vertices = verticesDeformados;
        mesh.RecalculateNormals();
    }
}
