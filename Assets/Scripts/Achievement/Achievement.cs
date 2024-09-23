using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "New Achievement", order = 10)]
public class Achievement : ScriptableObject
{
    public string title;
    public string description;
    public bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
    }
}
