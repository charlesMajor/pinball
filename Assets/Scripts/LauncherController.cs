using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    private bool holdLaunch = false;
    private float holdLaunchTimer = 0f;
    private const float holdLaunchLimit = 2f;
    private Rigidbody ballRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ballRigidbody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ballRigidbody = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ManageLaunchButtonDown();
        ManageLaunchButtonUp();
    }

    private void ManageLaunchButtonDown()
    {
        if (Input.GetButtonDown("Jump")) holdLaunch = true;

        if (holdLaunch)
        {
            holdLaunchTimer += Time.deltaTime;

            if (holdLaunchTimer > holdLaunchLimit)
            {
                print("Launcher is at max force");
                holdLaunchTimer = holdLaunchLimit;
            }
        }
    }

    private void ManageLaunchButtonUp()
    {
        if (Input.GetButtonUp("Jump"))
        {
            if (ballRigidbody != null)
            {
                Vector3 newForce = CreateNewForce(ballRigidbody.transform.position, holdLaunchTimer * 150f);
                ballRigidbody.AddForce(newForce, ForceMode.Impulse);
            }

            holdLaunchTimer = 0f;
            holdLaunch = false;
        }
    }

    private Vector3 CreateNewForce(Vector3 otherObjectPosition, float intensity)
    {
        return new Vector3(otherObjectPosition.x,
                           otherObjectPosition.y - transform.position.y,
                           otherObjectPosition.z - transform.position.z).normalized * intensity;
    }
}
