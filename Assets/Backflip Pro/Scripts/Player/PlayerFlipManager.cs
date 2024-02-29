using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFlipManager : MonoBehaviour
{
    public static UnityAction OnPlayerFlipped;
    public static UnityAction OnPlayerHalfFlipped;

    [Header(" Managers ")]
    [SerializeField] private PlayerController playerController;

    [Header(" Elements ")]
    private Vector3 previousUpVector;
    float cumulativeAngle;
    int numberFlips;
    private bool previousGroundedState;
    private bool shouldCheckForFlips;

    [Header(" Settings ")]
    [SerializeField] private float limitLandingAngle = 20;

    private void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (previousGroundedState != playerController.IsGrounded() && playerController.IsGrounded())
            CheckForFlips();

        previousGroundedState = playerController.IsGrounded();


        if (!playerController.IsGrounded())
            ManageFlipCounter();
    }

    private void CheckForFlips()
    {
        float landingAngle = Vector3.Angle(transform.up, Vector3.up);
 

        // 1. Check if the player landed on his feet
        if (landingAngle > limitLandingAngle)
        {
            //cumulativeAngle = 0;
            numberFlips = 0;

            return;
        }

        if (numberFlips > 0)
        {
            OnPlayerFlipped?.Invoke();

            Debug.Log(numberFlips + " consecutive flips");
            PlusOneParticleSystem.PlayPlusOneParticles(transform.position);

            Taptic.Light();
        }

        numberFlips = 0;
        //cumulativeAngle = 0;

    }

    private void ManageFlipCounter()
    {
        float currentAngle = Vector3.SignedAngle(transform.up, previousUpVector, Vector3.right);

        cumulativeAngle += currentAngle;

        if (Vector3.Angle(transform.up, Vector3.up) > 170)
            OnPlayerHalfFlipped?.Invoke();

        if (Mathf.Abs(cumulativeAngle) > 320)
        {
            cumulativeAngle = 0;
            numberFlips++;
        }
        
        previousUpVector = transform.up;
    }
}
