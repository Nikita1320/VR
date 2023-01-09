using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x,0,direction.y) * moveSpeed * Time.deltaTime;
    }
}
