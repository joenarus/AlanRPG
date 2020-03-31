using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{

    public int value;
    public int InitialValue;

    [NonSerialized]
    public int RuneTimeValue;

    public void OnAfterDeserialize() {
        //RuneTimeValue = InitialValue;
    }
    public void OnBeforeSerialize() {

    }
}
