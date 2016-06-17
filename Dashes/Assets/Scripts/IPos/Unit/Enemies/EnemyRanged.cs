using UnityEngine;
using System.Collections;

public class EnemyRanged : AINavigation {
    
    public IUnit target;

    public override void Update()
    {
            Update(target);
    }

    public override void Fire(Vector2 pos)
    {
        new Arrow (damage, Rot, Pos,target);
    }
}
