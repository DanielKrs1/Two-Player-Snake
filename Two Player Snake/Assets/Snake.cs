using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2int _direction = Vector2int.right;

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.W)){
            _direction = Vector2int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            _direction = Vector2int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            _direction = Vector2int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            _direction = Vector2int.right;
        }
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3int(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,

            0.0f
        );
    }



}
