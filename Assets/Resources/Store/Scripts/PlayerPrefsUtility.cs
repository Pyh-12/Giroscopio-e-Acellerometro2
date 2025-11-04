using UnityEngine;

public class ClearPlayerPrefsManager : MonoBehaviour
{
    [SerializeField] private bool clearOnStart = false;

    private void Awake()
    {
        if (clearOnStart)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs limpos com sucesso!");
        }
    }
}
