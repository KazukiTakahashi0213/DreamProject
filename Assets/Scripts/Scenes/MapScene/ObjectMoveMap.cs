using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveMap : MonoBehaviour
{
    [SerializeField] UpdateGameObject updateGameObject_ = null;
    [SerializeField] EventSpriteRenderer eventSpriteRenderer_ = null;

    public float speed = 1.0f;//移動スピード

    public MapData.MAP_STATUS ObjectType = MapData.MAP_STATUS.PLAYER;//オブジェクトの種類

    protected MapData _map = null;

    protected Vector2 _now_pos = Vector2.zero;
    protected Vector2 _next = Vector2.zero;

    public bool is_move { get; set; } = true;//falseは動けない(バトル中、話しかけてる最中など)

    public UpdateGameObject GetUpdateGameObject() { return updateGameObject_; }
    public EventSpriteRenderer GetEventSpriteRenderer() { return eventSpriteRenderer_; }

    protected void Init()//継承したクラスのスタート関数で必ず呼び出すこと
    {
        _map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapData>();
        _now_pos = transform.position;
    }

    protected virtual void MoveUp()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x, _now_pos.y + 1, 0);
        if (_map.MoveCheck(next_pos, ObjectType))
        {
            _next.y = 1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }
    protected virtual void MoveDown()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x, _now_pos.y - 1, 0);
        if (_map.MoveCheck(next_pos, ObjectType))
        {
            _next.y = -1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }
    protected virtual void MoveRight()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x + 1, _now_pos.y, 0);
        if (_map.MoveCheck(next_pos, ObjectType))
        {
            _next.x = 1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }

    protected virtual void MoveLeft()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x - 1, _now_pos.y, 0);
        if (_map.MoveCheck(next_pos, ObjectType))
        {
            _next.x = -1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }

    protected bool TransMove()
    {
        if (_next == Vector2.zero) return false;

        //上
        if (_next.y == 1)
        {
            transform.Translate(_next * speed * Time.deltaTime);
            if (_now_pos.y + _next.y <= transform.position.y)
            {
                transform.position = new Vector2(_now_pos.x, _now_pos.y + _next.y);
                _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
                _now_pos = transform.position;
                _next = Vector2.zero;
            }
        }

        //下
        if (_next.y == -1)
        {
            transform.Translate(_next * speed * Time.deltaTime);
            if (_now_pos.y + _next.y >= transform.position.y)
            {
                transform.position = new Vector2(_now_pos.x, _now_pos.y + _next.y);
                _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
                _now_pos = transform.position;
                _next = Vector2.zero;
            }
        }

        //右
        if (_next.x == 1)
        {
            transform.Translate(_next * speed * Time.deltaTime);
            if (_now_pos.x + _next.x <= transform.position.x)
            {
                transform.position = new Vector2(_now_pos.x + _next.x, _now_pos.y);
                _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
                _now_pos = transform.position;
                _next = Vector2.zero;
            }
        }

        //左
        if (_next.x == -1)
        {
            transform.Translate(_next * speed * Time.deltaTime);
            if (_now_pos.x + _next.x >= transform.position.x)
            {
                transform.position = new Vector2(_now_pos.x + _next.x, _now_pos.y);
                _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
                _now_pos = transform.position;
                _next = Vector2.zero;
            }
        }

        return true;
    }

    public void MapMoveUp(int addValue) {
        for (int i = 0; i < addValue; ++i) {
            var next_pos = new Vector3(_now_pos.x, _now_pos.y + 1, 0);

            _next.y = 1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);

            _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
            _next = Vector2.zero;

            _now_pos = next_pos;
        }

        _map.DebugLogDataString();
    }
    public void MapMoveDown(int addValue) {
        for (int i = 0; i < addValue; ++i) {
            var next_pos = new Vector3(_now_pos.x, _now_pos.y - 1, 0);

            _next.y = -1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);

            _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
            _next = Vector2.zero;

            _now_pos = next_pos;
        }

        _map.DebugLogDataString();
    }
    public void MapMoveRight(int addValue) {
        for (int i = 0; i < addValue; ++i) {
            var next_pos = new Vector3(_now_pos.x + 1, _now_pos.y, 0);

            _next.x = 1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);

            _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
            _next = Vector2.zero;

            _now_pos = next_pos;
        }

        _map.DebugLogDataString();
    }
    public void MapMoveLeft(int addValue) {
        for (int i = 0; i < addValue; ++i) {
            var next_pos = new Vector3(_now_pos.x - 1, _now_pos.y, 0);

            _next.x = -1;
            _map.MemoryNextTileMapStatus(next_pos);
            _map.SetMapStatus(next_pos, ObjectType);

            _map.SetMapStatus(_now_pos, _map.GetNextTileMapStatus());
            _next = Vector2.zero;

            _now_pos = next_pos;
        }

        _map.DebugLogDataString();
    }
}
