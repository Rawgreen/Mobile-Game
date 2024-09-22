using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement System/Achievement")]
public class Achievement : ScriptableObject
{
    public string title;
    public string description;
    public bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log($"Achievement Unlocked: {title} - {description}");
    }
}
