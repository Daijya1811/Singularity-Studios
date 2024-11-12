using System;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{
    //renderers and their materials
    public List<Renderer> renderers = new List<Renderer>();
    public Vector3 objectPosition;
    public List<Material> materials = new List<Material>();

    [HideInInspector] public float initialAlpha;

    
    /// <summary>
    /// Methods for IEquatable (needed for generic collections)
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(FadingObject other)
    {
        return objectPosition.Equals(other.objectPosition);
    }

    public override int GetHashCode()
    {
        return objectPosition.GetHashCode();
    }

    /// <summary>
    /// Gets all the children renderers, and the materials associated with the renderer
    /// </summary>
    private void Awake()
    {
        objectPosition = transform.position;
        if (renderers.Count == 0)
            renderers.AddRange(GetComponentsInChildren<Renderer>());
        for (int i = 0; i < renderers.Count; i++)
        {

            materials.AddRange(renderers[i].materials);
        }
        
        initialAlpha = materials[0].color.a;
    }
}
    
