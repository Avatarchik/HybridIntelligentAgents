using System.Collections.Generic;
using UnityEngine;

public abstract class IntelligentAgent : MonoBehaviour
{
  public IList<ILearningMethod> learningMethods { get; set; }
  public bool finished { get; set; }

  public virtual void OnSpawn() { }

  public virtual void OnRecycle() { }

}
