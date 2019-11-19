using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    private SlingShotLine slingShotLine;
    [SerializeField] private bool isPressed;

    private float releaseDelay;
    private float maxDragDistance = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpringJoint2D sj;
    [SerializeField] private Rigidbody2D slingRb;

    [SerializeField] private GameObject bird1;
    [SerializeField] private GameObject bird2;

    private void Awake()
    {
        slingShotLine = GameObject.Find("SlingShot").GetComponent<SlingShotLine>();
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        sj.connectedBody = GameObject.Find("CentrePoint").GetComponent<Rigidbody2D>();

        slingRb = sj.connectedBody;
    }

    private void Start()
    {
        slingShotLine.setCurrentBird(gameObject);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        releaseDelay = 1 / (sj.frequency * 4);

        slingShotLine.setLineRendererActive(true);

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
        rb.constraints = RigidbodyConstraints2D.None;
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
        slingShotLine.setLineRendererActive(false);
        sj.enabled = false;
        slingShotLine.setBird(bird1);
    }
}
