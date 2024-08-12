using UnityEngine;

namespace Main
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start() => DontDestroyOnLoad(gameObject);
    }
}