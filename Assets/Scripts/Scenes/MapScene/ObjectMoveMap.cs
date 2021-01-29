using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveMap : MonoBehaviour
{
    public float speed = 1.0f;//移動スピード

    [SerializeField]
    MapData.MAP_STATUS ObjectType = MapData.MAP_STATUS.HUMAN;//オブジェクトの種類

    protected MapData _map = null;

    protected Vector2 _now_pos = Vector2.zero;
    protected Vector2 _next = Vector2.zero;

    public bool is_move { get; set; } = true;//falseは動けない(バトル中、話しかけてる最中など)

    protected void Init()//継承したクラスのスタート関数で必ず呼び出すこと
    {
        _map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapData>();
        _now_pos = transform.position;
    }

    protected virtual void MoveUp()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x, _now_pos.y + 1, 0);
        if (_map.MoveCheck(next_pos))
        {
            _next.y = 1;
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }
    protected virtual void MoveDown()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x, _now_pos.y - 1, 0);
        if (_map.MoveCheck(next_pos))
        {
            _next.y = -1;
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }
    protected virtual void MoveRight()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x + 1, _now_pos.y, 0);
        if (_map.MoveCheck(next_pos))
        {
            _next.x = 1;
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }

    protected virtual void MoveLeft()
    {
        if (_next != Vector2.zero) return;
        var next_pos = new Vector3(_now_pos.x - 1, _now_pos.y, 0);
        if (_map.MoveCheck(next_pos))
        {
            _next.x = -1;
            _map.SetMapStatus(next_pos, ObjectType);
        }
    }

    protected void TransMove()
    {
        //上
        if (_next.y == 1)
        {
            transform.Translate(_next * speed * Time.deltaTime);
            if (_now_pos.y + _next.y <= transform.position.y)
            {
                transform.position = new Vector2(_now_pos.x, _now_pos.y + _next.y);
                _map.SetMapStatus(_now_pos, MapData.MAP_STATUS.FLOOR);
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
                _map.SetMapStatus(_now_pos, MapData.MAP_STATUS.FLOOR);
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
                _map.SetMapStatus(_now_pos, MapData.MAP_STATUS.FLOOR);
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
                _map.SetMapStatus(_now_pos, MapData.MAP_STATUS.FLOOR);
                _now_pos = transform.position;
                _next = Vector2.zero;
            }
        }
    }

}
