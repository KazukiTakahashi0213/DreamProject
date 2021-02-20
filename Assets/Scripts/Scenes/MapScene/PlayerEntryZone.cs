using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntryZone : MonoBehaviour
{
    [SerializeField] public Vector2 Up = Vector2.zero;
    [SerializeField] public Vector2 Down = Vector2.zero;
    [SerializeField] public Vector2 Right = Vector2.zero;
    [SerializeField] public Vector2 Left = Vector2.zero;

    public EventMoveMap _collision_object = null; 

    public bool is_collider { get; set; } = false;

    public void SetEntryZonePosUp() { transform.localPosition = Up; }
    public void SetEntryZonePosDown() { transform.localPosition = Down; }
    public void SetEntryZonePosRight() { transform.localPosition = Right; }
    public void SetEntryZonePosLeft() { transform.localPosition = Left; }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Event"
            || collision.gameObject.tag == "Human")
        {
            is_collider = true;
            _collision_object = collision.gameObject.GetComponent<EventMoveMap>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Event"
            || collision.gameObject.tag == "Human")
        {
            is_collider = false;
            _collision_object = null;
        }
    }
}
