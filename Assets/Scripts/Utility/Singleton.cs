using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T s_Instance;
  private static readonly object s_Lock = new object();

  public static T instance
  {
    get
    {
      lock (s_Lock) {
        if (s_Instance == null) {
          s_Instance = FindObjectOfType<T>();
        }

        return s_Instance;
      }
    }
  }
}