using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMoveMap : EventMoveMap
{
    public enum MOVE_TYPE {NONE,RANDOM,}

    [SerializeField] MOVE_TYPE MoveType = MOVE_TYPE.NONE;//動く種類を設定

    [SerializeField] float _move_interval = 3.0f;//動く間隔

    private float _move_time = 0;

    void Start()
    {
        Init();
        EventInit();

        ObjectType = MapData.MAP_STATUS.HUMAN;
    }

    void Update()
    {
        if (!is_move) return;
        if (MoveType == MOVE_TYPE.NONE) return;

        _move_time += Time.deltaTime;
        if (_move_time > _move_interval)
        {
            _move_time = 0;
            var rand = Random.Range(0, 4);
            if (rand == 0) MoveUp();
            if (rand == 1) MoveDown();
            if (rand == 2) MoveRight();
            if (rand == 3) MoveLeft();
        }

        TransMove();
    }
}
