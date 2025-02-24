using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private int currentLane = 1; // Initialize to middle lane (1)
    private float[] lanePositions = { -1.35f, 0f, 1.35f };
    private float laneMovementSpeed = 10f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    

    private Vector2 touchStart;
    private float minSwipeDistance = 50f;

    private void Awake()
    {
        // Set initial position to middle lane
        ResetPosition();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning()) return;

        HandleInput();
        UpdatePosition();
    }

    private void HandleInput()
    {
        if (isMoving) return;

        // Handle mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                HandleSwipe(touch.position - touchStart);
            }
        }
        // Handle mouse input for testing in editor
        else if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleSwipe((Vector2)Input.mousePosition - touchStart);
        }
    }

    private void HandleSwipe(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0 && currentLane < 2)
            {
                MoveToLane(currentLane + 1);
            }
            else if (swipeDelta.x < 0 && currentLane > 0)
            {
                MoveToLane(currentLane - 1);
            }
        }
    }

    private void UpdatePosition()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            laneMovementSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    public void StartRunning()
    {
        if (animator != null)
        {
            animator.SetBool("playMode", true);
        }
        currentLane = 1;
        transform.position = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);
        MoveToLane(1);
    }

    private void MoveToLane(int lane)
    {
        currentLane = lane;
        targetPosition = new Vector3(lanePositions[lane], transform.position.y, transform.position.z);
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

    public void ResetPosition()
    {
        currentLane = 1;
        transform.position = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);
        isMoving = false;
        
        // Reset animation state
        if (animator != null)
        {
            animator.SetBool("playMode", false);
        }
    }
}
