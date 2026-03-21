using Gaskellgames;
using UnityEngine;

namespace Ryadevn
{
    [RequireComponent(typeof(BoxCollider))]
    public class Water : MonoBehaviour
    {
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        [SerializeField] private float _size = 10;
        [SerializeField] private int _value = 6;

        [SerializeField] private float _amplitude = 1;
        [SerializeField] private float _frequency = .3f;
        [SerializeField] private float _lenght = 5;
        [SerializeField] private Vector3 _origin;

        private Vector3[] _vertices;
        private double _time;
        private Mesh _mesh;
        private MeshFilter _filter;

        private void Awake()
        {
            MakeMesh();
        }

        private void Update()
        {
            _time += Time.deltaTime;
            ClaculateWave();
        }

        [Button]
        private void MakeMesh()
        {
            _filter = GetComponent<MeshFilter>();
            _mesh = new();
            _vertices = new Vector3[_width * _height * _value];
            int[] triangles = new int[_width * _height * _value];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _vertices[(x + y * _width) * _value + 0] = new(-(_width * _size) / 2 + x * _size, 0, _height * _size / 2 - y * _size);
                    _vertices[(x + y * _width) * _value + 1] = new(-(_width * _size) / 2 + x * _size + _width, 0, _height * _size / 2 - y * _size);
                    _vertices[(x + y * _width) * _value + 2] = new(-(_width * _size) / 2 + x * _size, 0, _height * _size / 2 - y * _size - _height);

                    _vertices[(x + y * _width) * _value + 3] = new(-(_width * _size) / 2 + x * _size, 0, _height * _size / 2 - y * _size - _height);
                    _vertices[(x + y * _width) * _value + 4] = new(-(_width * _size) / 2 + x * _size + _width, 0, _height * _size / 2 - y * _size);
                    _vertices[(x + y * _width) * _value + 5] = new(-(_width * _size) / 2 + x * _size + _width, 0, _height * _size / 2 - y * _size - _height);

                    triangles[(x + y * _width) * _value + 0] = (x + y * _width) * _value;
                    triangles[(x + y * _width) * _value + 1] = (x + y * _width) * _value + 1;
                    triangles[(x + y * _width) * _value + 2] = (x + y * _width) * _value + 2;
                    triangles[(x + y * _width) * _value + 3] = (x + y * _width) * _value + 3;
                    triangles[(x + y * _width) * _value + 4] = (x + y * _width) * _value + 4;
                    triangles[(x + y * _width) * _value + 5] = (x + y * _width) * _value + 5;
                }
            }

            _mesh.MarkDynamic();
            _mesh.vertices = _vertices;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
            _filter.mesh = _mesh;
        }

        private void ClaculateWave()
        {
            for (int i = 0; i < _vertices.Length; i++)
                _vertices[i].y = _amplitude * Mathf.Sin(2 * Mathf.PI * (Vector3.Distance(_origin, _vertices[i]) / _lenght - (float)_time * _frequency));

            _mesh.vertices = _vertices;
            _mesh.RecalculateNormals();
            _filter.mesh = _mesh;
        }

    }
}