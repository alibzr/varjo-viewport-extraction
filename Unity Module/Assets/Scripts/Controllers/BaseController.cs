using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseController : MonoBehaviour
{
    protected abstract void Apply(Environment environment);

    public void NewEnvironment(Environment environment)
    {
        Apply(environment);
    }
}
