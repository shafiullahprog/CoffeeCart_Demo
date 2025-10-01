using UnityEngine;
using System.Collections;


public enum PlayerState
{
    MovingToResource,
    AtResource,
    MovingToCoffeeMachine,
    AtCoffeeMachine,
    MovingToServingCounter,
    AtServingCounter
}

public class AutoPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("State")]
    public PlayerState currentState = PlayerState.MovingToResource;

    [SerializeField] private Transform targetPoint;
    private Transform currentTarget;

    public void GoToResource()
    {
        SetTarget(targetPoint, PlayerState.MovingToResource);
    }

    private void Update()
    {
        if (currentTarget != null)
            MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        Vector3 direction = currentTarget.position - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 50f * Time.deltaTime);
        }

        /*if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
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
        }*/
    }

    private void SetTarget(Transform target, PlayerState playerState)
    {
        currentState = playerState;
        currentTarget = target;

        // Update state based on target
        //if (target == resourcePoint)
        //    currentState = PlayerState.MovingToResource;
        //else if (target == coffeeMachinePoint)
        //    currentState = PlayerState.MovingToCoffeeMachine;
        //else if (target == servingCounterPoint)
        //    currentState = PlayerState.MovingToServingCounter;
    }

    /*private IEnumerator AtResourceRoutine()
    {
        yield return new WaitForSeconds(1f);
        SetTarget(coffeeMachinePoint);
    }

    private IEnumerator AtCoffeeMachineRoutine()
    {
        yield return new WaitForSeconds(2f);
        SetTarget(servingCounterPoint);
    }

    private IEnumerator AtServingCounterRoutine()
    {
        yield return new WaitForSeconds(1f);
        SetTarget(resourcePoint);
    }*/
}