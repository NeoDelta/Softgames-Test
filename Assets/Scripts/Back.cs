using UnityEngine;

public class Back : MonoBehaviour
{
    public void GoTo(LevelSO level)
    {
        LoadSceneManager.Instance.LoadLevel(level);
    }
}
