using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 4f;
	public float jumpDelay = 1f;

	private bool canjump;
	public bool isjumping;
    private Rigidbody rb;
	public float countDown;
	public bool endJump = true;
	public PlayerMoveController playerMoveControllerClass;

	public GameObject stand, run, jump;

    // Start is called before the first frame update
    void Start()
    {
        canjump = true;
        rb = GetComponent<Rigidbody>();
        countDown = jumpDelay;
    }

    // Update is called once per frame
    void Update()
    {
	    if(isjumping && countDown > 0){
		    stand.gameObject.SetActive(false);
	        run.gameObject.SetActive(false);
            countDown -= Time.deltaTime;
        }
        else{
        	canjump = true;
        	isjumping = false;
            endJump = true;
        	countDown = jumpDelay;
            if (playerMoveControllerClass.leftController.getTouch())
            {
	            jump.gameObject.SetActive(false);
	            run.gameObject.SetActive(true);
	            stand.gameObject.SetActive(false);
            }
            if (!playerMoveControllerClass.leftController.getTouch())
            {
	            jump.gameObject.SetActive(false);
	            stand.gameObject.SetActive(true);
	            run.gameObject.SetActive(false);
            }
            
        }
    }

    public void StartLetsJump()
    {
	    if (!canjump) return;
	    canjump = false;
	    isjumping = true;
	    endJump = false;
	    rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
	    jump.gameObject.SetActive(true);
    }
}
