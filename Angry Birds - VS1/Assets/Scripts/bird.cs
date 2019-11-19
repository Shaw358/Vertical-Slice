using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    [SerializeField] private bool isPressed;

    private float releaseDelay;
    private float maxDragDistance = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpringJoint2D sj;
    [SerializeField] private Rigidbody2D slingRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        sj.connectedBody = GameObject.Find("CentrePoint").GetComponent<Rigidbody2D>();

        slingRb = sj.connectedBody;
    }

    private void Start()
    {
        releaseDelay = 1 / (sj.frequency * 4);

        isPressed = false;
    }

    void Update()
    {
        if(isPressed)
        {
            DragBall();
        }
    }

    private void DragBall()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, slingRb.position);

        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePosition;
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        StartCoroutine(Release());
        rb.isKinematic = false;
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
    }
}
