using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Properties...
    private Rigidbody ballRB;
    public float speed = 5.0f;
    public bool isTravelling;
    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    //Mouse Input Properties...
    private float minSwipeDistance = 500.0f;
    private Vector2 swipeDirection;
    private Vector2 currentFrameSwipePos;
    private Vector2 lastFrameSwipePos;

    //Color Properties...
    private Color solveColor;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        solveColor = Random.ColorHSV(0.6f, 1.0f);
        GetComponent<MeshRenderer>().material.color = solveColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleton.isFinished == true)
        {
            //ballRB.velocity = Vector3.zero;
            return;
        }
        Debug.Log("Controller");
        if (isTravelling)
        {
            ballRB.velocity = travelDirection * speed;
            //nextCollisionPosition = Vector3.zero
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            GroundPiece groundPiece = hitColliders[i].GetComponent<GroundPiece>();
            if (groundPiece)
            {
                if (!(groundPiece.isColored))
                {
                    groundPiece.ChangeColor(solveColor);
                }
            }
            i++;
        }
        if (Vector3.Distance(nextCollisionPosition, transform.position) < 1)
        {
            isTravelling = false;
            nextCollisionPosition = Vector3.zero;
            travelDirection = Vector3.zero;
        }
        if (!isTravelling)
        {
            if (Input.GetMouseButton(0))
            {
                currentFrameSwipePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (currentFrameSwipePos != Vector2.zero)
                {
                    //Debug.Log("Mouse position is not equal to zero");
                    swipeDirection = currentFrameSwipePos - lastFrameSwipePos;
                    if (swipeDirection.sqrMagnitude > minSwipeDistance)
                    {
                        //Debug.Log("Swipe direction is valid");

                        swipeDirection.Normalize();
                        if (swipeDirection.x > -0.5f && swipeDirection.x < 0.5f)
                        {
                            //Debug.Log("Swipe up or down");
                            CanTravel(swipeDirection.y > 0 ? Vector3.forward : Vector3.back);
                        }
                        if (swipeDirection.y > -0.5f && swipeDirection.y < 0.5f)
                        {
                            //Debug.Log("Swipe left or right");
                            CanTravel(swipeDirection.x > 0 ? Vector3.right : Vector3.left);
                        }
                        lastFrameSwipePos = currentFrameSwipePos;
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                lastFrameSwipePos = Vector2.zero;
                currentFrameSwipePos = Vector2.zero;
            }
        }

    }
    void CanTravel(Vector3 direction)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, 100.0f);
        travelDirection = direction;
        nextCollisionPosition = hit.point;
        isTravelling = true;
    }
}
