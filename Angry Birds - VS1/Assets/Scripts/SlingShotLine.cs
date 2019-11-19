using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotLine : MonoBehaviour
{
    private LineRenderer lineRendererFront;
    private LineRenderer lineRendererBack;

    private Transform centrePoint;

    [SerializeField] private GameObject currentBird;

    private void Start()
    {
        centrePoint = GameObject.Find("CentrePoint").transform;
        lineRendererFront = GameObject.Find("LineRendererFront").GetComponent<LineRenderer>();
        lineRendererBack = GameObject.Find("LineRendererBack").GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(currentBird != null)
        {
            lineRendererFront.SetPosition(0, new Vector3(lineRendererFront.transform.position.x, lineRendererFront.transform.position.y, 0f));
            lineRendererBack.SetPosition(0, new Vector3(lineRendererBack.transform.position.x, lineRendererBack.transform.position.y, 0f));
            lineRendererFront.SetPosition(1, new Vector3(currentBird.transform.position.x, currentBird.transform.position.y, 0f));
            lineRendererBack.SetPosition(1, new Vector3(currentBird.transform.position.x, currentBird.transform.position.y, 0f));
        }        
    }

    public void setLineRendererActive(bool active)
    {
        if(active == false)
        {
            lineRendererBack.enabled = false;
            lineRendererFront.enabled = false;
        }
        else if(active == true)
        {
            lineRendererBack.enabled = true;
            lineRendererFront.enabled = true;
        }
    }

    public void setCurrentBird(GameObject bird)
    {
        currentBird = bird;
    }

    public void setBird(GameObject bird = null)
    {
        currentBird = bird;
        StartCoroutine(spawnNewBird());
    }

    private IEnumerator spawnNewBird()
    {
        yield return new WaitForSeconds(1);
        if(currentBird != null)
        {
            Instantiate(currentBird, centrePoint.position, centrePoint.rotation);
        }
    }
}
