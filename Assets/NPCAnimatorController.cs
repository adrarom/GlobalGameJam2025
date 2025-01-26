using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimatorController : MonoBehaviour
{
    public Animator animator;
    public Customer customer;

    // Start is called before the first frame update
    void Start()
    {
        if (customer == null)
        {
            customer = GetComponent<Customer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (customer.state)
        {
            case Customer.CustomerState.Entering:
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isSitting", false);
                break;
            case Customer.CustomerState.Seated:
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isSitting", true);
                break;
            case Customer.CustomerState.Waiting:
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                animator.SetBool("isSitting", false);
                break;
            case Customer.CustomerState.Served:
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                animator.SetBool("isSitting", false);
                break;
        }
    }
}
