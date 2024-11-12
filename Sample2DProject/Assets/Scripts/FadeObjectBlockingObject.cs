using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FadeObjectBlockingObject : MonoBehaviour
{
    [Header("Editable Attributes")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float fadedAlpha = 0.33f;
    [SerializeField] private float checksPerSecond = 10;
    [SerializeField] private int fadeFPS = 30;
    [SerializeField] private FadeMode fadeMode = FadeMode.Fade;

    [Header("Read Only Data")]
    [SerializeField] private List<FadingObject> objectsBlockingView = new List<FadingObject>();

    private Dictionary<FadingObject, Coroutine> runningCoroutines = new Dictionary<FadingObject, Coroutine>();
    private Dictionary<Transform, List<FadingObject>> playerBlockedObjects = new Dictionary<Transform, List<FadingObject>>();

    private RaycastHit[] hits = new RaycastHit[10];
    [SerializeField] private Transform mainCamera;

    private void Start()
    {
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();
        List<Transform> playerTransforms = new List<Transform>();
        foreach (PlayerInput input in playerInputs)
        {
            playerTransforms.Add(input.transform);
            playerBlockedObjects.Add(input.transform, new List<FadingObject>());
        }
        StartCoroutine(CheckForObjects(playerTransforms));
    }

    
    //checks for objects blocking each player
    private IEnumerator CheckForObjects(List<Transform> playerTransforms)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / checksPerSecond);

        while (true)
        {
            // Track objects that are currently hit for each player
            Dictionary<FadingObject, bool> currentHits = new Dictionary<FadingObject, bool>();

            foreach (Transform player in playerTransforms)
            {
                // Raycast from camera to each player
                int numHits = Physics.RaycastNonAlloc(
                    mainCamera.position,
                    (player.position - mainCamera.position).normalized,
                    hits,
                    Vector3.Distance(mainCamera.position, player.position),
                    layerMask
                );

                Debug.DrawRay(mainCamera.position, (player.position - mainCamera.position), Color.green);

                if (numHits > 0)
                {
                    for (int i = 0; i < numHits; i++)
                    {
                        FadingObject fadingObject = GetFadingObjectFromHit(hits[i]);
                        if (fadingObject != null)
                        {
                            currentHits[fadingObject] = true;

                            if (!playerBlockedObjects[player].Contains(fadingObject))
                            {
                                StartFadeOut(fadingObject);
                                playerBlockedObjects[player].Add(fadingObject);
                            }
                        }
                    }
                }
            }

            FadeObjectsNoLongerBeingHit(currentHits);
            ClearHits();

            yield return wait;
        }
    }

    //starts an objects fadeout
    private void StartFadeOut(FadingObject fadingObject)
    {
        if (runningCoroutines.ContainsKey(fadingObject))
        {
            if (runningCoroutines[fadingObject] != null)
            {
                StopCoroutine(runningCoroutines[fadingObject]);
            }
            runningCoroutines.Remove(fadingObject);
        }

        runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
        if (!objectsBlockingView.Contains(fadingObject))
        {
            objectsBlockingView.Add(fadingObject);
        }
    }

    /// <summary>
    /// Fades objects back in manager
    /// </summary>
    /// <param name="currentHits"></param>
    private void FadeObjectsNoLongerBeingHit(Dictionary<FadingObject, bool> currentHits)
    {
        for (int i = objectsBlockingView.Count - 1; i >= 0; i--)
        {
            FadingObject fadingObject = objectsBlockingView[i];

            if (!currentHits.ContainsKey(fadingObject))
            {
                if (runningCoroutines.ContainsKey(fadingObject))
                {
                    if (runningCoroutines[fadingObject] != null)
                    {
                        StopCoroutine(runningCoroutines[fadingObject]);
                    }
                    runningCoroutines.Remove(fadingObject);
                }

                runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));

                objectsBlockingView.Remove(fadingObject);
                foreach (var playerObjects in playerBlockedObjects.Values)
                {
                    playerObjects.Remove(fadingObject);
                }
            }
        }
    }

    private void ClearHits()
    {
        RaycastHit emptyHit = new RaycastHit();
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i] = emptyHit;
        }
    }

    // Enum for fade mode
    public enum FadeMode
    {
        Transparent,
        Fade
    }

    // Get the fading object from a hit
    private FadingObject GetFadingObjectFromHit(RaycastHit hit)
    {
        return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
    }

    /// <summary>
    /// Fades the object out. STANDARD SHADER!!!!!!!
    /// </summary>
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

                fadingObject.materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
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
}
