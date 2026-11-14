using System;
using UnityEngine;

public abstract class SpellActionData : ScriptableObject
{
    public string actionName;
    public string Description { get => GetDescripton(); }

    public virtual string GetDescripton()
    {
        throw new NotImplementedException();
    }
    public abstract void Act(ICharacter user, ICharacter target);
    
}
