using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static UnityAction OnPlayerJumped;

    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    private int currentState;
    private bool canControl;
    private bool clicked;

    [Header(" Physics ")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody rig;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushTorque;
    [SerializeField] private float tuckTorque;
    [SerializeField] private float angleAdjustmentSpeed;


    private void Awake()
    {
        PlayerDetection.OnFinishLineHit += FinishLineHitCallback;
        PlayerDetection.OnGameoverHit += GameoverHitCallback;

        UIManager.onGameSet += EnableControl;
    }

    private void OnDestroy()
    {
        PlayerDetection.OnFinishLineHit -= FinishLineHitCallback;
        PlayerDetection.OnGameoverHit -= GameoverHitCallback;

        UIManager.onGameSet -= EnableControl;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ManageTransform();

        if (!UIManager.IsGame())
            return;
        
        if (!canControl)
            return;

        ManageControl();
    }

    public void EnableControl()
    {
        canControl = true;
    }

    public void DisableControl()
    {
        canControl = false;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            if (currentState == 0)
                PrepareFlip();

            else if (IsFlipping())
            {
                Tuck();
                ApplyTuckTorque();
            }

            clicked = true;
        }
        else if (Input.GetMouseButtonUp(0) && clicked)
        {
            if (IsTucking())
                rig.angularVelocity /= 4;


            ApplyLocalVerticalForce();
            ApplyTorque();

            Flip();
            
            ManageLanding();

            clicked = false;
        }
    }

    private void ManageTransform()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        AdjustRotation();

        if (IsTucking() && IsGrounded())
            AdjustRotationWhenTucking();
    }

    private void AdjustRotation()
    {
        Quaternion currentRotation = transform.rotation;
        currentRotation.y = 0;
        currentRotation.z = 0;

        transform.rotation = currentRotation;
    }

    private void AdjustRotationWhenTucking()
    {
        Vector3 targetDirection = Vector3.up;
        Vector3 currentDirection = transform.up;
        float rotateAmount = Vector3.Cross(targetDirection, currentDirection).x;

        rig.AddTorque(-rotateAmount * angleAdjustmentSpeed * Time.deltaTime * Vector3.right);
    }

    private void ManageLanding()
    {
        //print(transform.localEulerAngles.x);
    }


    private void ApplyLocalVerticalForce()
    {
        if (!IsGrounded())
        {
            //rig.velocity = transform.up * pushForce / 4 + Vector3.forward / 2;
            return;
        }
            

        rig.velocity = transform.up * pushForce + Vector3.forward;
        Taptic.Light();

        OnPlayerJumped?.Invoke();
    }

    private void ApplyTorque()
    {
        rig.AddTorque(Vector3.right * pushTorque * 500);
        //rig.angularVelocity = Vector3.right * pushTorque;
    }

    private void ApplyTuckTorque()
    {
        rig.AddTorque(Vector3.right * tuckTorque * 500);
        //rig.angularVelocity = Vector3.right * tuckTorque;
    }

    private void PrepareFlip()
    {
        SetAnimatorState(1);
    }

    private void Flip()
    {
        SetAnimatorState(2);
    }

    private void Tuck()
    {
        SetAnimatorState(3);
    }

    private void SetAnimatorState(int state)
    {
        currentState = state;
        animator.SetInteger("State", state);
    }

    private void FinishLineHitCallback()
    {
        canControl = false;
        LeanTween.delayedCall(1, StopPlayer);
        LeanTween.delayedCall(2, () => UIManager.setLevelCompleteDelegate(3));

        Taptic.Medium();
    }

    private void StopPlayer()
    {
        SetAnimatorState(0);
        animator.Play("Idle");

        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.identity;
    }

    private void GameoverHitCallback()
    {
        if (!canControl)
            return;

        canControl = false;

        UIManager.setGameoverDelegate?.Invoke();
    }

    private bool IsFlipping()
    {
        return currentState == 2;
    }

    private bool IsTucking()
    {
        return currentState == 3;
    }

    public bool IsGrounded()
    {
        float detectionRadius = 1f;

        Collider[] result = new Collider[1];
        return Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, result, groundLayer) > 0;
    }
}
