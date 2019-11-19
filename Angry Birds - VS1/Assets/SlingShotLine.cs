using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotLine : MonoBehaviour
{

    private LineRenderer lineRendererFront;
    private LineRenderer lineRendererBack;

    private void Start()
    {
        lineRendererFront = GameObject.Find("LineRendererFront").GetComponent<LineRenderer>();
        lineRendererBack = GameObject.Find("LineRendererBack").GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRendererFront.SetPosition(0, new Vector3(lineRendererFront.transform.position.x, lineRendererFront.transform.position.y, 0f));
    }

    public void setLineRendererTarget()
    {

    }

}
