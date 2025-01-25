using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public TopDownMovement topDownMovement;

    private void Awake()
    {
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        Vector3 moveDirection = topDownMovement.GetMoveDirection();
        bool isWalking = moveDirection != Vector3.zero;
        bool isCarryingItem = topDownMovement.IsCarryingItem();

        if (isWalking)
        {
            if (isCarryingItem)
            {
                animator.SetBool("isWalkingWithItem", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isWalkingWithItem", false);
                animator.SetBool("isIdle", false);
            }
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingWithItem", false);
        }
    }
}
