using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveUnits : MonoBehaviour {


    public float speed;
    private Animator animator;
    private Vector3 toPoint;
    private float time;
    private float walkingTime;
    private bool isStopWalking;




    // Use this for initialization
    void Start () {
        animator = transform.gameObject.GetComponent<Animator>();
        walkingTime = 0;
        isStopWalking = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            iTween.Stop(transform.gameObject);
            animator.SetBool("attack", true);
        }
        if (Input.GetKeyUp("space"))
        {
            animator.SetBool("attack", false);
        }
            if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            toPoint = new Vector3(mousePos.x, mousePos.y, 0);
            time = Vector3.Distance(transform.position, toPoint) / speed;
            if (toPoint.x < transform.position.x)
            {
                animator.SetInteger("motion", 1);
            }
            else
            {
                animator.SetInteger("motion", 2);
            }
            
            iTween.MoveTo(transform.gameObject, iTween.Hash("position", toPoint, "time", time, "easetype", "linear", "oncomplete", "Complete"));
            walkingTime = time;
       
            if (!isStopWalking)
            {
                StartCoroutine(stopWalking());
                isStopWalking = true;
            }
            
        }
        
    }

    IEnumerator stopWalking()
    {
        while(walkingTime > 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            walkingTime-= 0.1f;
        }
        
        animator.SetInteger("motion", 0);
        isStopWalking = false;
    }

}
