using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyRobot : MonoBehaviour
{
    [SerializeField] private Vector2Int moveRangeRadius;
    [SerializeField] private int visionRange;
    [SerializeField] private float walkTime;
    [SerializeField] private float runTime; 
    private Transform playerTransform;

    /// <summary> マップの範囲 </summary>
    [SerializeField] private Vector2Int mapRange;
    
    /// <summary> 1マスの距離 </summary>
    [SerializeField] private float edgeLength;

    /// <summary> 影響マップ生成用 </summary>
    private InfluenceMap _influenceMap;

    /// <summary> 巡回中心座標 </summary>
    private Vector2Int _initialPosition;

    /// <summary> 二次元整数座標で敵ロボットのtransformのpositionにアクセスする </summary>
    private Vector2Int EnemyPosition
    {
        get
        {
            var position = transform.position;
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.z);
            return new Vector2Int(x, y);
        }
        set => transform.position = new Vector3(value.x, transform.position.y, value.y);
    }

    /// <summary> 二次元整数座標でプレイヤーのtransformのpositionにアクセスする </summary>
    private Vector2Int PlayerPosition
    {
        get
        {
            var position = playerTransform.position;
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.z);
            return new Vector2Int(x, y);
        }
        set => playerTransform.position = new Vector3(value.x, transform.position.y, value.y);
    }

    /// <summary> playerを見つけたときtrue </summary>
    private bool FindPlayer()
    {
        Vector2Int diff = EnemyPosition - PlayerPosition;
        if (diff.x == 0 || diff.y == 0)
        {
            int pd = _influenceMap.PolygonalDistance(EnemyPosition, PlayerPosition,
                position => _passableMap[position.x, position.y]);
            int mag = Mathf.Abs(diff.x + diff.y);
            if (pd == mag)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary> true:追跡ステート false:巡回ステート </summary>
    private bool IsChase =>
        Mathf.Abs(PlayerPosition.x - _initialPosition.x) <= moveRangeRadius.x &&
        Mathf.Abs(PlayerPosition.y - _initialPosition.y) <= moveRangeRadius.y;

    // maps
    /// <summary> 通行可能座標インデックスがtrue </summary>
    private bool[,] _passableMap;
    
    // Start is called before the first frame update
    void Start()
    {
        _influenceMap = new InfluenceMap(mapRange.x, mapRange.y);
        CSVReader csvreader = GameObject.FindWithTag("MainCamera").GetComponent<CSVReader>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _passableMap = csvreader.LoadMap(mapRange);
        _initialPosition = EnemyPosition;
        StartCoroutine(StateManage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> 各ステートの管理 </summary>
    private IEnumerator StateManage()
    {
        while (true)
        {
            // 巡回する
            Debug.Log("Patrol Phase");
            // プレイヤーを見つけるまで
            yield return RandomWalk();
            // プレイヤーを追いかける
            Debug.Log("Chase Phase");
            yield return ChasePlayer();
        }
    }

    /// <summary> 巡回ステート時の処理 </summary>
    private IEnumerator RandomWalk()
    {
        while (!FindPlayer())
        {
            // 距離マップ
            int[,] distanceMap = new int[mapRange.x, mapRange.y];
            // 目的地ランダマイズ
            Vector2Int destination = RandomDestination();
            Debug.Log("random destination"+destination);
            // 移動マップ記録
            _influenceMap.DetureMatrixOperate(destination, j => _passableMap[j.x, j.y],
                (xCount, yCount, distance) => { distanceMap[xCount, yCount] = distance; });
            // 移動
            int step = distanceMap[EnemyPosition.x, EnemyPosition.y];
            Vector2Int stepPos = EnemyPosition;
            while (EnemyPosition != destination && !FindPlayer())
            {
                _influenceMap.NextPoint(EnemyPosition, (x, y) =>
                {
                    if (step > distanceMap[x, y] && _passableMap[x, y])
                    {
                        step = distanceMap[x, y];
                        stepPos = new Vector2Int(x,y);
                    }
                });
                yield return Move(stepPos - EnemyPosition, walkTime);
            }
        }
    }
    
    /// <summary> 追跡ステート時の処理 </summary>
    private IEnumerator ChasePlayer()
    {
        int[,] distanceMap = new int[mapRange.x, mapRange.y];
        do 
        {
            // 探索
            _influenceMap.DetureMatrixOperate(PlayerPosition, j => _passableMap[j.x, j.y], 
            (x, y, distance) => distanceMap[x,y] = distance);
            int step = distanceMap[EnemyPosition.x, EnemyPosition.y];
            Vector2Int stepPos = EnemyPosition;
            _influenceMap.NextPoint(EnemyPosition, (x, y) =>
            {
                if (step > distanceMap[x, y] && _passableMap[x,y])
                {
                    step = distanceMap[x, y];
                    stepPos = new Vector2Int(x, y);
                }
            });
            yield return Move(stepPos - EnemyPosition, runTime);
        } while (IsChase);
    }

    /// <summary> 1マス移動 </summary>
    /// <param name="direction">方向長さ1にすること</param>
    /// <param name="moveTime">一マス移動にかかる秒数。</param>
    private IEnumerator Move(Vector2Int direction, float moveTime)
    {
        float tim = 0;
        Vector3 start = transform.position;
        Vector3 end = transform.position + edgeLength * new Vector3(direction.x, 0, direction.y);
        while (tim <= 1)
        {
            tim += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, end, tim);
            yield return null;
        }
    }

    /// <summary> ランダムウォークの目的地設定 </summary>
    private Vector2Int RandomDestination()
    {
        List<Vector2Int> lotteryCoords = new List<Vector2Int>();
        _influenceMap.MatrixOperate((x, y) =>
        {
            if (Mathf.Abs(x - _initialPosition.x) <= moveRangeRadius.x && Mathf.Abs(y - _initialPosition.y) <= moveRangeRadius.y)
            {
                if (_passableMap[x, y])
                {
                    lotteryCoords.Add(new Vector2Int(x, y));
                }
            }
        });
        return lotteryCoords[Random.Range(0, lotteryCoords.Count)];
    }
}
