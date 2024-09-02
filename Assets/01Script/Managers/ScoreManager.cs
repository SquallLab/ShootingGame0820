using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 전반에 필요한 수치들 관리하는 매니저. 
// 플레이어 HP, 플레이어 강화도, 게임의 점수, 습득한 보석의 갯수등등.... 
// 해당 수치들이 UI등에서 실시간 갱신이 되도록, 이벤트 발생시키는 역할. 

// delegate => action 활용하는 방법.은 추후에...  
public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore; // 게임 점수 변경. 
    public static event ScoreChange OnChangeJamCount;
    public static event ScoreChange OnChangeHP;
    public static event ScoreChange OnChangeBomb;

    private int score; // 게임에서 플레이어가 습득한 점수, 적을처치하거나, 보석을 습득했을때마다. 
    private int curHP; // 플레이어 현 HP
    private int maxHP; // 플레이어 최대 HP
    private int jamCount; // 보석 습득 갯수. 
    private int powerLevel;
    private int bombCount;

    //getter 
    public int Score => score;
    public int CurHP => curHP;
    public int MaxHP => maxHP;
    public int JamCount => jamCount;
    public int PowerLevel => powerLevel;
    public int BombCount => bombCount;

    //setter - 
    private int SetScore
    {// score가 변화가 생길때 자동으로 인보케이션 발생. 
        set
        {
            score = value;
            OnChangeScore?.Invoke(score);
        }
    }

    public void InitScoreSet()
    {
        SetScore = 0;

        curHP = maxHP = 5;
        OnChangeHP?.Invoke(curHP);

        powerLevel = 1;
        
        jamCount = 0;
        OnChangeJamCount?.Invoke(jamCount);

        bombCount = 3;
        OnChangeBomb?.Invoke(bombCount);
    }

    private void OnEnable()
    {
        Enemy.OnMonsterDied += HandleMonsetDied;
        DropItem_Jam.OnPickupJam += HandleJamPickup;
    }
    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsetDied;
        DropItem_Jam.OnPickupJam -= HandleJamPickup;
    }

    private void HandleMonsetDied(Enemy enemyInfo)
    {
        Debug.Log("몬스터 죽음 Event를 캐치했습니다." + enemyInfo.gameObject.name );
        // 나중에 몬스터 사망시 습득하는 게임 점수는 차등 적용 예정. 
        SetScore = Score + 10;
    }

    private void HandleJamPickup()
    {
        SetScore = Score + 7;

        jamCount++;
        OnChangeJamCount?.Invoke(jamCount);

    }





}
