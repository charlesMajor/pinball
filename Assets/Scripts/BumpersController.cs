using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpersController : MonoBehaviour
{
    private Rigidbody ballRigidBody;
    [SerializeField] GameManager gameManager;
    [SerializeField] int scoreWhenHit = 100;
    [SerializeField] float intensity = 25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ballRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            gameManager.augmentScore(scoreWhenHit);

            Vector3 newForce = CreateNewForce(ballRigidBody.transform.position, intensity);
            ballRigidBody.AddForce(newForce, ForceMode.Impulse);
        }
    }

    private Vector3 CreateNewForce(Vector3 otherObjectPosition, float intensity)
    {
        return new Vector3(otherObjectPosition.x - transform.position.x,
                           otherObjectPosition.y - transform.position.y,
                           otherObjectPosition.z - transform.position.z).normalized * intensity;
    }
}
