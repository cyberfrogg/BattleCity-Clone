using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene object class. (MonoBehaivor translator)
/// </summary>
public class Thing : MonoBehaviour, ICloneable
{
    /// <summary>
    /// Awake method translator of MonoBehaviour
    /// </summary>
    public virtual void Awake()
    {

    }

    /// <summary>
    /// Start method translator of MonoBehaviour
    /// </summary>
    public virtual void Start()
    {

    }

    /// <summary>
    /// Update method translator of MonoBehaviour
    /// </summary>
    public virtual void Update()
    {

    }


    /// <summary>
    /// LookAt implimentation for 2D
    /// </summary>
    /// <param name="moveVector"></param>
    public virtual void LookAt(Vector2 moveVector)
    {
        Vector3 vectorDifference = (new Vector3(moveVector.x, moveVector.y, 0) + transform.position) - transform.position;
        vectorDifference.Normalize();

        float rot_z = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    /// <summary>
    /// Clones thing
    /// </summary>
    /// <returns></returns>
    public virtual object Clone()
    {
        return this.MemberwiseClone();
    }



}
