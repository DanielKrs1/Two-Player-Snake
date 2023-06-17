using UnityEngine;

public class Snake : MonoBehaviour {
    [SerializeField] private ControlType controlType;
    [SerializeField] private float moveInterval;
    [SerializeField] private Vector2Int initialDirection;

    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private Vector2Int position;
    private Vector2Int direction;
    private float moveTimer;
    private bool canChangeDirection = true;

    private void Start() {
        direction = initialDirection;
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        switch (controlType) {
            case ControlType.WASD:
                upKey = KeyCode.W;
                downKey = KeyCode.S;
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                break;
            case ControlType.ArrowKeys:
                upKey = KeyCode.UpArrow;
                downKey = KeyCode.DownArrow;
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                break;
        }
    }

    private void Update() { 
        if (canChangeDirection) {
            if (Input.GetKeyDown(upKey) && direction != Vector2Int.down) {
                direction = Vector2Int.up;
                canChangeDirection = false;
            } else if (Input.GetKeyDown(downKey) && direction != Vector2Int.up) {
                direction = Vector2Int.down;
                canChangeDirection = false;
            } else if (Input.GetKeyDown(leftKey) && direction != Vector2Int.right) {
                direction = Vector2Int.left;
                canChangeDirection = false;
            } else if (Input.GetKeyDown(rightKey) && direction != Vector2Int.left) {
                direction = Vector2Int.right;
                canChangeDirection = false;
            }
        }

        moveTimer += Time.deltaTime;

        if (moveTimer >= moveInterval) {
            moveTimer -= moveInterval;
            position += direction;
            transform.position = new Vector3(position.x, position.y);
            canChangeDirection = true;
        }
    }
}

public enum ControlType {
    WASD,
    ArrowKeys
}