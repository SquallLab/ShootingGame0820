using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interface �ܵ����� �ν��Ͻ��� ������ �����. => ������νḸ ����.
// ��������� ����� �޼ҵ带 �ǹ������� ������ �ؾ߸� �Ѵ�.
public interface IInputHandler
{
    // �Է��� �޾ƿ��� ����. 
    Vector2 GetInput();
}
