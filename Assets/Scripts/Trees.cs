using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 5.0f;
    [SerializeField] private float _gameVelocity;
    private Rigidbody2D            _rb;
    private TreeGenerator          _treeGenerator;
    public TreeGenerator           TreeGenerator
    {
        get => _treeGenerator;
        set => _treeGenerator = value;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        MoveTree();
    }
    private void MoveTree()
    {
        Vector2 currentSpeed = _rb.velocity;

        currentSpeed.x = -(_initialSpeed + _gameVelocity);
        
        _rb.velocity = currentSpeed;
    }
    public void SetGameSpeed(int points)
    {
        _gameVelocity = Mathf.Floor(points / 1000) * 10;
    }
    public void DeleteTree()
    {
        _treeGenerator.RemoveTree(this);
        Destroy(gameObject);
    }
}
