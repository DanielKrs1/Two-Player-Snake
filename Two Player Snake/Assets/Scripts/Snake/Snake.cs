using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    [SerializeField] private BodySegment bodySegment;
    [SerializeField] private ControlType controlType;
    [SerializeField] private int initialBodyLength;
    [SerializeField] private float moveInterval;
    [SerializeField] private Vector2Int initialDirection;

    //movement
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private float speed = 1;
    private Vector2Int direction;
    private float moveTimer;
    private bool canChangeDirection = true;

    private bool invincible = false;
    private bool reversed = false;
    private bool frozen = false;

    //body
    private readonly List<BodySegment> body = new List<BodySegment>();
    private int segmentsLeftToGrow;

    private void Start() {
        direction = initialDirection;
        Grow(initialBodyLength);

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

        if (moveTimer >= moveInterval / speed && !frozen) {
            moveTimer -= moveInterval / speed;
            Vector3 oldHeadPosition = transform.position;
            transform.position += new Vector3(direction.x, direction.y);
            canChangeDirection = true;

            if (segmentsLeftToGrow > 0) {
                segmentsLeftToGrow--;
                BodySegment newSegment = Instantiate(bodySegment, new Vector3(oldHeadPosition.x, oldHeadPosition.y), Quaternion.identity);
                body.Insert(0, newSegment);
            } else {
                Vector3 oldPosition = oldHeadPosition;

                foreach (BodySegment bodySegment in body) {
                    Vector3 olderPosition = bodySegment.transform.position;
                    bodySegment.transform.position = oldPosition;
                    oldPosition = olderPosition;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<BodySegment>()) {
            EndScreen.instance.GameOver(this);
        } else if (collision.GetComponent<Snake>()) {
            EndScreen.instance.GameOver(null);
        }
    }

    public void Grow(int growAmount) {
        segmentsLeftToGrow += growAmount;
    }

    public void Teleport(){
        Vector2 teleportLocation = Map.instance.GetRandomPosition();

        while (Physics2D.OverlapPoint(teleportLocation) != null) {
            teleportLocation = Map.instance.GetRandomPosition();
        }

        transform.position = new Vector3(teleportLocation.x, teleportLocation.y);
    }

    public void Freeze(){
        frozen = !frozen;
    }

    public void SpeedIncrease(float speed){
        this.speed += speed;
    }

    public void SetInvincible(){
        invincible = !invincible;
    }

    public void ReverseControls(){
        if(!reversed){
            reversed = true;
            switch (controlType) {
                case ControlType.WASD:
                    upKey = KeyCode.S;
                    downKey = KeyCode.W;
                    leftKey = KeyCode.D;
                    rightKey = KeyCode.A;
                    return;
                case ControlType.ArrowKeys:
                    upKey = KeyCode.DownArrow;
                    downKey = KeyCode.UpArrow;
                    leftKey = KeyCode.RightArrow;
                    rightKey = KeyCode.LeftArrow;
                    return;
            }
        }else{
            reversed = false;
            switch (controlType) {
                case ControlType.WASD:
                    upKey = KeyCode.W;
                    downKey = KeyCode.S;
                    leftKey = KeyCode.A;
                    rightKey = KeyCode.D;
                    return;
                case ControlType.ArrowKeys:
                    upKey = KeyCode.UpArrow;
                    downKey = KeyCode.DownArrow;
                    leftKey = KeyCode.LeftArrow;
                    rightKey = KeyCode.RightArrow;
                    return;
            }
        }
    }
}

public enum ControlType {
    WASD,
    ArrowKeys
}