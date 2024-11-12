using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectBlockingObject : MonoBehaviour
{

    [Header("Editable Attributes")]
    [SerializeField] private LayerMask layerMask; 
    [SerializeField] private Transform player; 
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float fadedAlpha= 0.33f;
    [SerializeField] private float checksPerSecond = 10;
    [SerializeField] private int fadeFPS = 30;
    [SerializeField] private FadeMode fadeMode = FadeMode.Fade;
    [Header("Read Only Data")]
    [SerializeField] private List<FadingObject> objectsBlockingView = new List<FadingObject>();
    private Dictionary<FadingObject, Coroutine> runningCoroutines = new Dictionary<FadingObject, Coroutine>();
    private RaycastHit[] hits = new RaycastHit[10];
    

    private void Start()
    {
        StartCoroutine(CheckForObjects());
    }
    
    
    
    //I Hate this nested confusing code, but....it works
    private IEnumerator CheckForObjects()
    {
        //wait depending on number of checks
        WaitForSeconds wait = new WaitForSeconds(1f / checksPerSecond);

        while (true)
        {
            //raycast to player and save the hits
            int numHits = Physics.RaycastNonAlloc(mainCamera.transform.position,
                (player.transform.position - mainCamera.transform.position).normalized,
                hits, 
                Vector3.Distance(mainCamera.transform.position, player.transform.position), 
                layerMask);

            Debug.DrawRay(mainCamera.transform.position,
                (player.transform.position - mainCamera.transform.position),

                Color.green);
            
            
            //if hits is zero, get the objects fadingObject
            if (numHits > 0)
            {
                for (int i = 0; i < numHits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectFromHit(hits[i]);
                    if (fadingObject != null && !objectsBlockingView.Contains(fadingObject))
                    {
                        //if already fading in, stop that process and remove references
                        if (runningCoroutines.ContainsKey(fadingObject))
                        {
                            if (runningCoroutines[fadingObject] != null)
                            {
                                StopCoroutine(runningCoroutines[fadingObject]);
                            }

                            runningCoroutines.Remove(fadingObject);
                        }

                        //add a new coroutine and start fading
                        runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        objectsBlockingView.Add(fadingObject);
                    }
                }
            }

            FadeObjectsNoLongerBeingHit();
            ClearHits();
            
            yield return wait;
        }
    }

    private void ClearHits()
    {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i] = hit;
        }
    }


    /// <summary>
    /// Fades the object out. STANDARD SHADER!!!!!!! 
    /// </summary>
    /// <param name="fadingObject"></param>
    /// <returns></returns>
    private IEnumerator FadeObjectOut(FadingObject fadingObject)
    {
        float waitTime = 1f / fadeFPS;
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        int ticks = 1;

        //loop through and modify shader
        for (int i = 0; i < fadingObject.materials.Count; i++)
        {
            //modify material to support fade
            fadingObject.materials[i]
                .SetInt("_SrcBlend",
                    (int)UnityEngine.Rendering.BlendMode.SrcAlpha); // affects both "Transparent" and "Fade" options
            fadingObject.materials[i]
                .SetInt("_DstBlend",
                    (int)UnityEngine.Rendering.BlendMode
                        .OneMinusSrcAlpha); // affects both "Transparent" and "Fade" options
            fadingObject.materials[i].SetInt("_Zwrite", 0); // disable Z writing
            //blend or fully hide
            if (fadeMode == FadeMode.Fade)
            {
                fadingObject.materials[i].EnableKeyword("_ALPHABLEND_ON");
            }
            else
            {
                fadingObject.materials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            //update queue
            fadingObject.materials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

        //check if shader has a color
        if (fadingObject.materials[0].HasProperty("_Color"))
        {
            while (fadingObject.materials[0].color.a > fadedAlpha)
            {
                for (int i = 0; i < fadingObject.materials.Count; i++)
                {
                    if (fadingObject.materials[i].HasProperty("_Color"))
                    {
                        fadingObject.materials[i].color = new Color(fadingObject.materials[i].color.r,
                            fadingObject.materials[i].color.g,
                            fadingObject.materials[i].color.b,
                            Mathf.Lerp(fadingObject.initialAlpha, fadedAlpha, waitTime * ticks));
                    }
                }

                ticks++;
                yield return wait;
            }
        }

        //when done remove from dictionary and stop
        if (runningCoroutines.ContainsKey(fadingObject))
        {
            StopCoroutine(runningCoroutines[fadingObject]);
            runningCoroutines.Remove(fadingObject);

        }
    }


    /// <summary>
    /// Fades an object back in
    /// </summary>
    /// <param name="fadingObject"></param>
    /// <returns></returns>
    private IEnumerator FadeObjectIn(FadingObject fadingObject)
    {
        float waitTime = 1f / fadeFPS;
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        int ticks = 1;

        //check if shader has a color
        if (fadingObject.materials[0].HasProperty("_Color"))
        {
            while (fadingObject.materials[0].color.a < fadingObject.initialAlpha)
            {
                for (int i = 0; i < fadingObject.materials.Count; i++)
                {
                    if (fadingObject.materials[i].HasProperty("_Color"))
                    {
                        fadingObject.materials[i].color = new Color(fadingObject.materials[i].color.r,
                            fadingObject.materials[i].color.g,
                            fadingObject.materials[i].color.b,
                            Mathf.Lerp(fadedAlpha, fadingObject.initialAlpha, waitTime * ticks));
                    }
                }

                ticks++;
                yield return wait;
            }

            
            //reset to opaque values
            for (int i = 0; i < fadingObject.materials.Count; i++)
            {

                if (fadeMode == FadeMode.Fade)
                {
                    fadingObject.materials[i].DisableKeyword("_ALPHABLEND_ON");
                }
                else
                {
                    fadingObject.materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                }

                fadingObject.materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering. BlendMode.One); 
                fadingObject.materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero); 
                fadingObject.materials[i].SetInt("_Zwrite", 1);
                fadingObject.materials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
            }

            if (runningCoroutines.ContainsKey(fadingObject))
            {
                StopCoroutine(runningCoroutines[fadingObject]);
                runningCoroutines.Remove(fadingObject);
            }
        }

    }

    /// <summary>
    /// Fades objects back in managment
    /// </summary>
    private void FadeObjectsNoLongerBeingHit()
    {
        for (int i = 0; i < objectsBlockingView.Count; i++)
        {
            bool objectIsBeingHit = false;
            for (int j = 0; j < hits.Length; j++)
            {
                FadingObject fadingObject = GetFadingObjectFromHit(hits[j]);

                if (fadingObject != null && fadingObject == objectsBlockingView[i])
                {
                    objectIsBeingHit = true;
                    break;
                }
            }

            if (!objectIsBeingHit)
            {
                if (runningCoroutines.ContainsKey(objectsBlockingView[i]))
                {
                    if (runningCoroutines[objectsBlockingView[i]] != null)
                    {
                        StopCoroutine(runningCoroutines[objectsBlockingView[i]]);
                    }

                    runningCoroutines.Remove(objectsBlockingView[i]);
                }

                runningCoroutines.Add(objectsBlockingView[i], StartCoroutine(FadeObjectIn(objectsBlockingView[i])));
                objectsBlockingView.RemoveAt(i);
            }
        }
    }

    //Enum for fade mode in and out
    public enum FadeMode
    {
        Transparent,
        Fade
    }


    //gets the fading object if it exists null otherwise
    private FadingObject GetFadingObjectFromHit(RaycastHit hit)
    {
        return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
        
    }
}
