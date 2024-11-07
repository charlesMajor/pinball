using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActionsController : MonoBehaviour
{
    [SerializeField] int amountMultiplicator;
    [SerializeField] int amountPoints;
    [SerializeField] GameManager gameManager;
    [SerializeField] bool isSprite;
    private bool coroutineActive = false;
    private bool gaveMultiplicator = false;
    private Mesh startingMesh;
    private Sprite startingSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (isSprite)
        {
            startingSprite = GetComponentInParent<SpriteRenderer>().sprite;
        }
        else
        {
            startingMesh = GetComponentInParent<MeshFilter>().mesh;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (!coroutineActive)
            {
                if (isSprite)
                {
                    GetComponentInParent<demoSpriteBlink>().StartCoroutine("BlinkTarget");
                }
                else
                {
                    GetComponentInParent<demoTargetBlink>().StartCoroutine("BlinkTarget");
                }
                coroutineActive = true;
            }
            
            Debug.Log("Adding " + amountPoints + " to points");
            gameManager.augmentScore(amountPoints);
            if (!gaveMultiplicator)
            {
                Debug.Log("Adding " + amountMultiplicator + " to multiplicator");
                gameManager.augmentMultiplicator(amountMultiplicator);
                gaveMultiplicator = true;
            }
        }
    }

    public void resetLights()
    {
        Debug.Log("Reset lights");
        coroutineActive = false;
        gaveMultiplicator = false;

        if (isSprite)
        {
            GetComponentInParent<demoSpriteBlink>().StopAllCoroutines();
            GetComponentInParent<SpriteRenderer>().sprite = startingSprite;
        }
        else
        {
            GetComponentInParent<demoTargetBlink>().StopAllCoroutines();
            GetComponentInParent<MeshFilter>().mesh = startingMesh;
        }
    }
}
