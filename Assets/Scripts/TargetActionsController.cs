using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActionsController : MonoBehaviour
{
    [SerializeField] int amountMultiplicator;
    [SerializeField] int amountPoints;
    [SerializeField] GameManager gameManager;
    private bool coroutineActive = false;
    private bool gaveMultiplicator = false;
    private Mesh startingMesh;

    // Start is called before the first frame update
    void Start()
    {
        startingMesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!coroutineActive)
            {
                GetComponent<demoTargetBlink>().StartCoroutine("BlinkTarget");
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

    public void resetTargets()
    {
        Debug.Log("Reset target");
        coroutineActive = false;
        gaveMultiplicator = false;
        GetComponent<demoTargetBlink>().StopAllCoroutines();
        GetComponent<MeshFilter>().mesh = startingMesh;
    }
}
