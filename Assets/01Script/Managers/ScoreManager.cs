using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���ݿ� �ʿ��� ��ġ�� �����ϴ� �Ŵ���. 
// �÷��̾� HP, �÷��̾� ��ȭ��, ������ ����, ������ ������ �������.... 
// �ش� ��ġ���� UI��� �ǽð� ������ �ǵ���, �̺�Ʈ �߻���Ű�� ����. 

// delegate => action Ȱ���ϴ� ���.�� ���Ŀ�...  
public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore; // ���� ���� ����. 
    public static event ScoreChange OnChangeJamCount;
    public static event ScoreChange OnChangeHP;
    public static event ScoreChange OnChangeBomb;

    private int score; // ���ӿ��� �÷��̾ ������ ����, ����óġ�ϰų�, ������ ��������������. 
    private int curHP; // �÷��̾� �� HP
    private int maxHP; // �÷��̾� �ִ� HP
    private int jamCount; // ���� ���� ����. 
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
    {// score�� ��ȭ�� ���涧 �ڵ����� �κ����̼� �߻�. 
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
        Debug.Log("���� ���� Event�� ĳġ�߽��ϴ�." + enemyInfo.gameObject.name );
        // ���߿� ���� ����� �����ϴ� ���� ������ ���� ���� ����. 
        SetScore = Score + 10;
    }

    private void HandleJamPickup()
    {
        SetScore = Score + 7;

        jamCount++;
        OnChangeJamCount?.Invoke(jamCount);

    }





}
