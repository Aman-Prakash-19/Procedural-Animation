using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [SerializeField] LegController frontLeftLeg1Stepper;
    [SerializeField] LegController frontLeftLeg2Stepper;
    [SerializeField] LegController frontRightLeg1Stepper;
    [SerializeField] LegController frontRightLeg2Stepper;
    [SerializeField] LegController backLeftLeg1Stepper;
    [SerializeField] LegController backLeftLeg2Stepper;
    [SerializeField] LegController backRightLeg1Stepper;
    [SerializeField] LegController backRightLeg2Stepper;
    

    // Only allow diagonal leg pairs to step together
    IEnumerator LegUpdateCoroutine()
    {
        // Run continuously
        while (true)
        {
            // Try moving one diagonal pair of legs
            do
            {
                frontLeftLeg1Stepper.TryMove();
                frontRightLeg2Stepper.TryMove();
                backLeftLeg2Stepper.TryMove();
                backRightLeg1Stepper.TryMove();
                // Wait a frame
                yield return null;

                // Stay in this loop while either leg is moving.
                // If only one leg in the pair is moving, the calls to TryMove() will let
                // the other leg move if it wants to.
            } while (backRightLeg1Stepper.Moving || frontLeftLeg1Stepper.Moving || backLeftLeg2Stepper.Moving || frontRightLeg2Stepper.Moving);

            // Do the same thing for the other diagonal pair
            do
            {
                frontLeftLeg2Stepper.TryMove();
                frontRightLeg1Stepper.TryMove();
                backLeftLeg1Stepper.TryMove();
                backRightLeg2Stepper.TryMove();
                // Wait a frame
                yield return null;

                // Stay in this loop while either leg is moving.
                // If only one leg in the pair is moving, the calls to TryMove() will let
                // the other leg move if it wants to.
            } while (backLeftLeg1Stepper.Moving || frontLeftLeg2Stepper.Moving || backRightLeg2Stepper.Moving || frontRightLeg1Stepper.Moving);
        }
    }

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }
}
