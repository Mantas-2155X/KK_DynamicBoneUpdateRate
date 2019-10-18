using Harmony;
using BepInEx;

using UnityEngine;
using System.ComponentModel;

using JetBrains.Annotations;

[BepInPlugin(nameof(KK_DynamicBoneUpdateRate), nameof(KK_DynamicBoneUpdateRate), "2.0")]
public class KK_DynamicBoneUpdateRate : BaseUnityPlugin
{
    [DisplayName("DynamicBone Update Rate")]
    [Description("DynamicBone Update Rate")]
    [AcceptableValueRange(60, 144, false)]
    private static ConfigWrapper<int> newUpdateRate { get; set; }
    
    private void Awake() {
        HarmonyInstance.Create(nameof(KK_DynamicBoneUpdateRate)).PatchAll(typeof(KK_DynamicBoneUpdateRate));
        newUpdateRate = new ConfigWrapper<int>("DynamicBone-update-rate", this, 60);
    }

    [HarmonyPostfix, HarmonyPatch(typeof(DynamicBone))]
    [UsedImplicitly]
    public static void DynamicBone_Constructor_Patch(DynamicBone __instance)
    {
        __instance.m_UpdateRate = newUpdateRate.Value;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(DynamicBone), "Start")]
    [UsedImplicitly]
    public static void DynamicBone_Start_Patch(DynamicBone __instance)
    {
        __instance.m_UpdateRate = newUpdateRate.Value;
        __instance.m_Gravity = new Vector3(
            (__instance.m_Gravity.x * 60f) / newUpdateRate.Value, 
            (__instance.m_Gravity.y * 60f) / newUpdateRate.Value, 
            (__instance.m_Gravity.z * 60f) / newUpdateRate.Value
        );
    }
    
    [HarmonyPostfix, HarmonyPatch(typeof(DynamicBone_Ver02))]
    [UsedImplicitly]
    public static void DynamicBone_Ver02_Constructor_Patch(DynamicBone_Ver02 __instance)
    {
        __instance.UpdateRate = newUpdateRate.Value;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(DynamicBone_Ver02), "setGravity")]
    [UsedImplicitly]
    public static void DynamicBone_Ver02_setGravity_Patch(ref Vector3 _gravity)
    {
        _gravity = new Vector3(
            (_gravity.x * 60f) / newUpdateRate.Value, 
            (_gravity.y * 60f) / newUpdateRate.Value, 
            (_gravity.z * 60f) / newUpdateRate.Value
            );
    }
}
