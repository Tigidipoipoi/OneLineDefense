using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratedMeshScript : MonoBehaviour {
    #region Members
    public string m_MeshName = "My Awesome Mesh";
    public int m_Frequency = 5;
    public ColorPoint m_Center;
    public ColorPoint[] m_Points;

    private Mesh m_Mesh;
    private Vector3[] m_Vertices;
    private int[] m_Triangles;
    private Color[] m_Colors;
    #endregion

    public void Start() {
        m_Mesh = GetComponent<MeshFilter>().mesh;
        m_Mesh.name = m_MeshName;

        if (m_Points == null) {
            m_Points = new ColorPoint[0];
        }
        int pointCount = m_Frequency * m_Points.Length;
        m_Vertices = new Vector3[pointCount + 1];
        m_Colors = new Color[pointCount + 1];
        m_Triangles = new int[pointCount * 3];

        if (pointCount >= 3) {
            m_Vertices[0] = m_Center.Position;
            m_Colors[0] = m_Center.Color;
            float angle = -360.0f / m_Frequency;

            for (int repetitions = 0, i = 1, j = 1;
                repetitions < m_Frequency; ++repetitions) {
                for (int k = 0; k < m_Points.Length; ++k, ++i, j += 3) {
                    m_Vertices[i] = Quaternion.Euler(0.0f, 0.0f,
                        angle * (i - 1)) * m_Points[k].Position;
                    m_Colors[i] = m_Points[i].Color;

                    m_Triangles[j - 1] = 0;
                    m_Triangles[j] = i;
                    m_Triangles[j + 1] = i + 1;
                }
            }
            m_Triangles[m_Triangles.Length - 1] = 1;
        }

        m_Mesh.vertices = m_Vertices;
        m_Mesh.colors = m_Colors;
        m_Mesh.triangles = m_Triangles;
    }
}

[System.Serializable]
public struct ColorPoint {
    public Vector3 Position;
    public Color Color;
}
