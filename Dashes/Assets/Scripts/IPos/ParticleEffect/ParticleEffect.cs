using UnityEngine;
using System.Collections;

public class ParticleEffect {

    ParticleSystem _pSystem;

    public ParticleEffect(ParticleSystem pSystem)
    {
        _pSystem = pSystem;
    }
    
    public void Emit(int i,Vector2 pos)
    {
        _pSystem.gameObject.transform.position = pos;
        _pSystem.Emit(i);
    }

}
