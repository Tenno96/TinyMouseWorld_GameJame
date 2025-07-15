using UnityEngine;

public class AnimInstance : MonoBehaviour
{

    private Character ownerCharacter;
    private Animator ownerAnimator;

    void Start()
    {
        ownerCharacter = GetComponent<Character>();
        ownerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ownerAnimator.SetFloat("movementSpeed", ownerCharacter.GetCharacterMovementSpeed());
        ownerAnimator.SetBool("isGround", ownerCharacter.IsGround);
        ownerAnimator.SetBool("IsJump", ownerCharacter.IsJump);
        ownerAnimator.SetBool("IsFalling", ownerCharacter.IsFalling);
    }
}
