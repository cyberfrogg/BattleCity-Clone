using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCount : MonoBehaviour
{
    [SerializeField]private List<GameObject> _children;

    void Start()
    {
        _children = new List<GameObject>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            _children.Add(child.gameObject);
        }
    }
    public void EnableChildren()
    {
        foreach (var child in _children)
        {
            child.SetActive(true);
        }
    }
}
