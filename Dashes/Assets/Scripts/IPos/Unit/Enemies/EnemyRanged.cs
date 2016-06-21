using UnityEngine;
using System.Collections;

public class EnemyRanged : AINavigation {
    
    public IUnit target;

    public override void Update()
    {
        Update(target);
    }
}
