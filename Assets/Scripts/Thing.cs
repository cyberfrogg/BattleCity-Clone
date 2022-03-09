using System;
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
    public virtual void LookAt(Vector2 lookPosition)
    {
        Vector3 vectorDifference = (new Vector3(lookPosition.x, lookPosition.y, 0) + transform.position) - transform.position;
        vectorDifference.Normalize();

        float rot_z = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    /// <summary>
    /// OnCollisionEnter2D implimentation for 2D
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnThingCollidedEnter(Thing thing)
    {

    }

    /// <summary>
    /// OnCollisionExit2D implimentation for 2D
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnThingCollidedExit(Thing thing)
    {

    }

    /// <summary>
    /// Clones thing
    /// </summary>
    /// <returns></returns>
    public virtual object Clone()
    {
        return this.MemberwiseClone();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Thing collidedThing;

        if (collision.gameObject.TryGetComponent<Thing>(out collidedThing))
        {
            OnThingCollidedEnter(collidedThing);
        }
        else
        {
            //throw new MissingReferenceException("No [Thing] class on collided object");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Thing collidedThing;

        if (collision.gameObject.TryGetComponent<Thing>(out collidedThing))
        {
            OnThingCollidedExit(collidedThing);
        }
        else
        {
            //throw new MissingReferenceException("No [Thing] class on collided object");
        }
    }
}
