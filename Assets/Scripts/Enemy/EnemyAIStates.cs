using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIStates : MonoBehaviour
{
    public abstract EnemyAIStates RunCurrentState();
}
