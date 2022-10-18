using UnityEngine;

namespace Game.Data_SO
{
    [CreateAssetMenu(fileName = "StackGameSettings", menuName = "StackGameSettings")]
    public class StackGameSettings : ScriptableObject
    {
        [SerializeField] public float maxHp;
    }
}