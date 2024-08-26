using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interface 단독으로 인스턴스를 만들어낼수 없어요. => 상속으로써만 포함.
// 상속했을때 멤버의 메소드를 의무적으로 재정의 해야만 한다.
public interface IInputHandler
{
    // 입력을 받아오는 역할. 
    Vector2 GetInput();
}
