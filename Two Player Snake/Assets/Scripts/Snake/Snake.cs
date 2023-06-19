using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Snake : MonoBehaviour {
    [SerializeField] private BodySegment bodySegment;
    [SerializeField] private ControlType controlType;
    [SerializeField] private int initialBodyLength;
    [SerializeField] private float moveInterval;
    [SerializeField] private Vector2Int initialDirection;
    [SerializeField] private int keyBufferCapacity;

    //movement
    private Queue<KeyCode> keyBuffer = new Queue<KeyCode>();
    private KeyCode upKey;
    private SpriteRenderer spriteRenderer;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private float speed = 1;
    private Vector2Int direction;
    private float moveTimer;
    private Color normalColor;
    private Color currentColor;

    private bool invincible = false;
    private bool frozen = false;

    private int freezeStack;
    private int invincibleStack;
    private int reverseStack;

    //body
    private readonly List<BodySegment> body = new List<BodySegment>();
    private int segmentsLeftToGrow;

    private void Start() {
        direction = initialDirection;
        Grow(initialBodyLength);
        spriteRenderer = GetComponent<SpriteRenderer>();

        normalColor = spriteRenderer.color;
        currentColor = normalColor;

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
        if (keyBuffer.Count < keyBufferCapacity && Time.timeScale > 0f) {
            if (Input.GetKeyDown(upKey)) {
                keyBuffer.Enqueue(upKey);
            } else if (Input.GetKeyDown(downKey)) {
                keyBuffer.Enqueue(downKey);
            } else if (Input.GetKeyDown(leftKey)) {
                keyBuffer.Enqueue(leftKey);
            } else if (Input.GetKeyDown(rightKey)) {
                keyBuffer.Enqueue(rightKey);
            }
        }

        if(!frozen){
            moveTimer += Time.deltaTime;
        }

        if (moveTimer >= moveInterval / speed && !frozen) {
            moveTimer -= moveInterval / speed;

            if (keyBuffer.Count > 0) {
                Vector2Int newDirection = Vector2Int.zero;

                while (keyBuffer.Count > 0 && newDirection == Vector2Int.zero) {
                    KeyCode keyCode = keyBuffer.Dequeue();

                    if (keyCode == upKey && direction != Vector2Int.down)
                        newDirection = Vector2Int.up;
                    else if (keyCode == downKey && direction != Vector2Int.up)
                        newDirection = Vector2Int.down;
                    else if (keyCode == leftKey && direction != Vector2Int.right)
                        newDirection = Vector2Int.left;
                    else if (keyCode == rightKey && direction != Vector2Int.left)
                        newDirection = Vector2Int.right;
                }

                if (newDirection != Vector2Int.zero)
                    direction = newDirection;
            }

            Vector3 oldHeadPosition = transform.position;
            transform.position += new Vector3(direction.x, direction.y);
            spriteRenderer.color = currentColor;

            if (segmentsLeftToGrow > 0) {
                segmentsLeftToGrow--;
                BodySegment newSegment = Instantiate(bodySegment, new Vector3(oldHeadPosition.x, oldHeadPosition.y), Quaternion.identity);
                newSegment.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
                body.Insert(0, newSegment);
            } else {
                Vector3 oldPosition = oldHeadPosition;

                foreach (BodySegment bodySegment in body) {
                    bodySegment.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
                    Vector3 olderPosition = bodySegment.transform.position;
                    bodySegment.transform.position = oldPosition;
                    oldPosition = olderPosition;
                }
            }

            Vector2 mapSize = Map.instance.size;
            float halfMapSizeX = (mapSize.x - 1f) / 2f;
            float halfMapSizey = (mapSize.y - 1f) / 2f;
            float x = transform.position.x;
            float y = transform.position.y;

            if (x > halfMapSizeX || x < -halfMapSizeX || y > halfMapSizey || y < -halfMapSizey)
                EndScreen.instance.GameOver(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<BodySegment>()&&!invincible) {
            EndScreen.instance.GameOver(this);
        } else if (collision.GetComponent<Snake>()&&!invincible) {
            EndScreen.instance.GameOver(null);
        }
    }

    public void Grow(int growAmount) {
        segmentsLeftToGrow += growAmount;
    }

    public void Teleport(){
        Vector2 teleportLocation = Map.instance.GetRandomPosition();

        while (Physics2D.OverlapPoint(teleportLocation) != null) 
        {
            teleportLocation = Map.instance.GetRandomPosition();
        }

        transform.position = new Vector3(teleportLocation.x, teleportLocation.y);
    }

    public void Freeze(){
        frozen = true;
        currentColor.b+=0.75f;
        currentColor.g+=0.75f;
        freezeStack++;
        if(freezeStack==1){
            spriteRenderer.color = currentColor;
            foreach (BodySegment bodySegment in body) 
            {
                bodySegment.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
            }
        }
        StartCoroutine(UnFreeze());
    }

    public IEnumerator UnFreeze(){
        yield return new WaitForSeconds(1.5f);
        freezeStack--;
        if(freezeStack==0)
        {
            currentColor.b-=0.75f;
            currentColor.g-=0.75f;
            frozen = false;
        }
    }

    public void SpeedIncrease(float speed){
        this.speed += speed;
        currentColor.r+=0.75f;
        currentColor.b+=0.75f;
        currentColor.g+=0.75f;
        StartCoroutine(Flash());
    }

    public IEnumerator Flash(){
        yield return new WaitForSeconds(0.1f);
        currentColor.r-=0.75f;
        currentColor.b-=0.75f;
        currentColor.g-=0.75f;        
    }

    public void SetInvincible(){
        invincible = true;
        invincibleStack++;
        if(invincibleStack==1){
            currentColor.g+=0.45f;
            currentColor.r+=0.45f;
        }
        StartCoroutine(StopInvincible());
    }

    public IEnumerator StopInvincible(){
        yield return new WaitForSeconds(3.0f);
        invincibleStack--;
        if(invincibleStack==0)
        {
            currentColor.g-=0.45f;
            currentColor.r-=0.45f;
            invincible = false;
        }
    }

    public void ReverseControls() {
        keyBuffer.Clear();
        
        reverseStack++;
        if(reverseStack==1){  
            switch (controlType) {
                case ControlType.WASD:
                    upKey = KeyCode.S;
                    downKey = KeyCode.W;
                    leftKey = KeyCode.D;
                    rightKey = KeyCode.A;
                    break;
                case ControlType.ArrowKeys:
                    upKey = KeyCode.DownArrow;
                    downKey = KeyCode.UpArrow;
                    leftKey = KeyCode.RightArrow;
                    rightKey = KeyCode.LeftArrow;
                    break;
            }      
            currentColor.g+=0.6f;
            currentColor.r+=0.75f;
        }
        StartCoroutine(UnReverse());
    }

    public IEnumerator UnReverse(){
        yield return new WaitForSeconds(3.0f);
        reverseStack--;
        if(reverseStack==0){
            currentColor.g-=0.6f;
            currentColor.r-=0.75f;
            switch (controlType) 
            {
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
    }
}

public enum ControlType {
    WASD,
    ArrowKeys
}