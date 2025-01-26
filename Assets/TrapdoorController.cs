using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenTrapdoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseTrapdoor();
        }
    }

    private void OpenTrapdoor()
    {
        animator.SetBool("isOpen", true);
    }

    private void CloseTrapdoor()
    {
        animator.SetBool("isOpen", false);
    }
}
