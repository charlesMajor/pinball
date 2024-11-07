using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerController : MonoBehaviour
{
    private bool isInRespawner;
    private float inRespawnerTimer = 0f;
    private const float inRespawnerLimit = 2f;
    private GameObject ball;

    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            isInRespawner = true;
            ball = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            isInRespawner = false;
            ball = null;
            inRespawnerTimer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ManageBallInDeadZone();
    }

    private void ManageBallInDeadZone()
    {
        if (isInRespawner)
        {
            inRespawnerTimer += Time.deltaTime;

            if (inRespawnerTimer > inRespawnerLimit)
            {
                isInRespawner = false;
                gameManager.loseBall();
                if (!gameManager.hasNoMoreBalls())
                {
                    ball.transform.position = new Vector3(18, 0.55f, 13);
                }
                else
                {
                    ball.SetActive(false);
                }
            }
        }
    }
}