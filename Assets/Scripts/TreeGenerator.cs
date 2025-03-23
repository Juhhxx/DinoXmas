using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _treePrefab;
    [MinMaxSlider(0, 10)][SerializeField] private Vector2Int _spawnDistance;
    
    public bool GameOn { get; set; }
    public int Points { get; set; }
    private List<Trees> _treeList;
    private System.Random _rnd;

    private void Start()
    {
        _treeList = new List<Trees>();
        _rnd = new System.Random();

        // StartCoroutine(GenerateTrees());
    }
    private void Update()
    {
        UpdateTreeSpeed();
        CheckTreeDistance();
        if (!GameOn)
        {
            StopCoroutine(GenerateTrees());
            Debug.Log("STOP SPAWNING");
        }
    }
    private void UpdateTreeSpeed()
    {
        foreach (Trees tree in _treeList)
            tree.SetGameSpeed(Points);
    }
    private void CreateTree()
    {
        GameObject newTree = Instantiate(_treePrefab, transform.position, Quaternion.identity);
        Trees tree = newTree.GetComponent<Trees>();

        _treeList.Add(tree);
        tree.TreeGenerator = this;
    }
    public void RemoveTree(Trees tree)
    {
        _treeList.Remove(tree);
    }
    private void CheckTreeDistance()
    {
        int dist = _rnd.Next(_spawnDistance.x,_spawnDistance.y);

        Debug.DrawLine(transform.position, transform.right, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, dist);

        if (hit)
        {
            Trees t = hit.collider.gameObject.GetComponent<Trees>();

            if (!t)
            {
                CreateTree();
            }
        }

    }
    private IEnumerator GenerateTrees()
    {
        while(true)
        {
            Debug.Log("SPAWN TREE");

            CreateTree();

            float s = Random.Range(_spawnDistance.x,_spawnDistance.y);
            Debug.Log($"Next tree in {s}");

            yield return new WaitForSeconds(s);
        }
    }
}
