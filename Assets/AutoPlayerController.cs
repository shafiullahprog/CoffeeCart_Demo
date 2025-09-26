using UnityEngine;
using System.Collections;

public class AutoPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Transform resourcePoint;
    public Transform coffeeMachinePoint;
    public Transform servingCounterPoint;

    [Header("State")]
    public PlayerState currentState = PlayerState.MovingToResource;

    public enum PlayerState
    {
        MovingToResource,
        AtResource,
        MovingToCoffeeMachine,
        AtCoffeeMachine,
        MovingToServingCounter,
        AtServingCounter
    }

    private Transform currentTarget;

    private void Start()
    {
        SetTarget(resourcePoint);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        Vector3 direction = currentTarget.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == resourcePoint)
            {
                currentState = PlayerState.AtResource;
                StartCoroutine(AtResourceRoutine());
            }
            else if (currentTarget == coffeeMachinePoint)
            {
                currentState = PlayerState.AtCoffeeMachine;
                StartCoroutine(AtCoffeeMachineRoutine());
            }
            else if (currentTarget == servingCounterPoint)
            {
                currentState = PlayerState.AtServingCounter;
                StartCoroutine(AtServingCounterRoutine());
            }
        }
    }

    private void SetTarget(Transform target)
    {
        currentTarget = target;

        // Update state based on target
        if (target == resourcePoint)
            currentState = PlayerState.MovingToResource;
        else if (target == coffeeMachinePoint)
            currentState = PlayerState.MovingToCoffeeMachine;
        else if (target == servingCounterPoint)
            currentState = PlayerState.MovingToServingCounter;
    }

    private IEnumerator AtResourceRoutine()
    {
        // Simulate collecting beans
        yield return new WaitForSeconds(1f);

        // After collecting, move to coffee machine
        SetTarget(coffeeMachinePoint);
    }

    private IEnumerator AtCoffeeMachineRoutine()
    {
        // Simulate processing beans
        yield return new WaitForSeconds(2f);

        // After processing, move to serving counter
        SetTarget(servingCounterPoint);
    }

    private IEnumerator AtServingCounterRoutine()
    {
        // Simulate serving customer
        yield return new WaitForSeconds(1f);

        // After serving, move back to resource
        SetTarget(resourcePoint);
    }
}