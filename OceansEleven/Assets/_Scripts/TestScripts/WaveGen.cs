using UnityEngine;
using System.Collections;

public class WaveGen : MonoBehaviour
{
     public float _scale = 0.1f;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _noiseStrength = 1f;
    [SerializeField] private float _noiseWalk = 1f;
    [SerializeField] private float _textureOffsetSpeed = 0.1f;

    private Mesh _mesh;
    private MeshCollider _meshCollider;
    private Material _waveMaterial;
    private Vector3[] _baseHeight;

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _meshCollider = GetComponent<MeshCollider>();
        _waveMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (_baseHeight == null)
        {
            _baseHeight = _mesh.vertices;
        }

        Vector3[] vertices = new Vector3[_baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = _baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * _speed + _baseHeight[i].x + _baseHeight[i].y + _baseHeight[i].z) * _scale;
            vertex.y += Mathf.PerlinNoise(_baseHeight[i].x + _noiseWalk, _baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * _noiseStrength;
            vertices[i] = vertex;
        }
        _mesh.vertices = vertices;
        _mesh.RecalculateNormals();

        //Collider
        _meshCollider.sharedMesh = _mesh;

        //Material offset
        float offset = (Time.time * _speed) * _textureOffsetSpeed;
        _waveMaterial.SetTextureOffset("_MainTex", new Vector2(-offset, -offset));
    }

    IEnumerator LerpWave(float waveTarget, float timeSpeed=0.01f)
    {
        float startTime = Time.time;
        while (_scale < waveTarget)
        {
            //transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            _scale = Mathf.Lerp(_scale, waveTarget, (Time.time - startTime) * timeSpeed);
            yield return null;
        }
    }
}