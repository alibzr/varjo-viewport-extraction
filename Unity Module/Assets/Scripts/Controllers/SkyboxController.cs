using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo;
using Varjo.XR;

public class SkyboxController : BaseController
{
    protected override void Apply(Environment environment)
    {
        // Set the new texture
        RenderSettings.skybox.mainTexture = environment.m_Background;
        VarjoRendering.SetFocusScalingFactor(environment.m_FocusScalingFactor);
        VarjoRendering.SetContextScalingFactor(environment.m_ContextScalingFactor);

    }
}
