using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMap : ObjectMoveMap
{
    [SerializeField] private PlayerEntryZone _entry_zone = null;

    IInputProvider input = new KeyBoardNormalInputProvider();

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if (!is_move) return;//falseは動けない

        //話しかける
        if (input.SelectEnter() && _entry_zone.is_collider) 
        {
            Debug.Log(_entry_zone._collision_object._message); 
        }

        //移動
        if      (Input.GetKey(KeyCode.UpArrow))     MoveUp();
        else if (Input.GetKey(KeyCode.DownArrow))   MoveDown();
        else if (Input.GetKey(KeyCode.RightArrow))  MoveRight();
        else if (Input.GetKey(KeyCode.LeftArrow))   MoveLeft();

        //移動できれば移動する
        TransMove();
    }

    protected override void MoveUp()
    {
        if (_next != Vector2.zero) return;
        _entry_zone.SetEntryZonePosUp();
        base.MoveUp();
    }
    protected override void MoveDown()
    {
        if (_next != Vector2.zero) return;
        _entry_zone.SetEntryZonePosDown();
        base.MoveDown();
    }
    protected override void MoveRight()
    {
        if (_next != Vector2.zero) return;
        _entry_zone.SetEntryZonePosRight();
        base.MoveRight();
    }
    protected override void MoveLeft()
    {
        if (_next != Vector2.zero) return;
        _entry_zone.SetEntryZonePosLeft();
        base.MoveLeft();
    }
}
