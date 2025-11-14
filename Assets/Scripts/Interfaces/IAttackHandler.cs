using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IAttackHandler
{
    public abstract void Initialize(WeaponHandler _w);
}
