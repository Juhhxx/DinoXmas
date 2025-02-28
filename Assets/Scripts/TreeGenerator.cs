using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private Vector2    _spawnTime;

    public bool GameOn { get; set; }
    public int Points { get; set; }
    private List<Trees> _treeList;

    

    private void Start()
    {
        StartCoroutine(GenerateTrees());
    }
    private void Update()
    {
        UpdateTreeSpeed();
        if (!GameOn)
        {
            StopCoroutine(GenerateTrees());
            Debug.Log("STOP SPAWNING");
        }
    }
    private void UpdateTreeSpeed()
    {
        foreach (Trees tree in _treeList)
            tree?.SetGameSpeed(Points);
    }
    public void RemoveTree(Trees tree)
    {
        _treeList.Remove(tree);
    }
    private IEnumerator GenerateTrees()
    {
        while(true)
        {
            Debug.Log("SPAWN TREE");

            GameObject newTree = Instantiate(_treePrefab, transform.position, Quaternion.identity);
            Trees tree = newTree.GetComponent<Trees>();

            _treeList.Add(tree);
            tree.TreeGenerator = this;

            float s = Random.Range(_spawnTime.x,_spawnTime.y);
            Debug.Log($"Next tree in {s}");

            yield return new WaitForSeconds(s);
        }
    }
}
