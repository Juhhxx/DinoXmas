using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEater : MonoBehaviour
{
    private void EatTree(Trees t)
    {
        t.DeleteTree();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Trees tree = other.gameObject.GetComponent<Trees>();

        if (tree != null)
            EatTree(tree);
    }
}
