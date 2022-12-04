using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.ComponentModel;

#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad, DisplayName("Holding It")]
#endif
public class HoldingItInteraction : IInputInteraction
{
    private HashSet<InputControl> controls = new HashSet<InputControl>();

    public void Process(ref InputInteractionContext context)
    {
        if (controls.Contains(context.control))
        {
            controls.Remove(context.control);
            context.Canceled();
        }
        else
        {
            controls.Add(context.control);
            context.PerformedAndStayStarted();
        }

    }

    public void Reset()
    {
        controls = new HashSet<InputControl>();
    }

    static HoldingItInteraction()
    {
        InputSystem.RegisterInteraction<HoldingItInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // Will execute the static constructor as a side effect.
    }
}