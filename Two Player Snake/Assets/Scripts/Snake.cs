using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int _direction = Vector2Int.right;

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.W)){
            _direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            _direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            _direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            _direction = Vector2Int.right;
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x) + _direction.x, Mathf.Round(transform.position.y) + _direction.y);
    }



}
