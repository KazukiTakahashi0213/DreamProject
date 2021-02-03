using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMap : ObjectMoveMap
{
    [SerializeField] private PlayerEntryZone _entry_zone = null;

    private void Start()
    {
        Init();
        AllSceneManager.GetInstance().inputProvider_ = new KeyBoardInactiveInputProvider();
    }

    void Update()
    {
        AllSceneManager allSceneMgr = AllSceneManager.GetInstance();

        if (!is_move) return;//falseは動けない

        //話しかける
        if (allSceneMgr.inputProvider_.SelectEnter() && _entry_zone.is_collider) 
        {
            Debug.Log(_entry_zone._collision_object._message); 
        }

        //移動
        if      (allSceneMgr.inputProvider_.UpSelect())     MoveUp();
        else if (allSceneMgr.inputProvider_.DownSelect())   MoveDown();
        else if (allSceneMgr.inputProvider_.RightSelect())  MoveRight();
        else if (allSceneMgr.inputProvider_.LeftSelect())   MoveLeft();

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
