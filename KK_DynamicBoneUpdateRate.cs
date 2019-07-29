using Harmony;
using BepInEx;
using System.ComponentModel;

[BepInPlugin(nameof(KK_DynamicBoneUpdateRate), nameof(KK_DynamicBoneUpdateRate), "1.0")]
public class KK_DynamicBoneUpdateRate : BaseUnityPlugin {

    #region Config properties
        [DisplayName("DynamicBone Modifier Enable")]
        [Description("Enable the plugin and set the DynamicBone update rate.")]
        public static ConfigWrapper<bool> PluginEnabled { get; private set; }

        [DisplayName("DynamicBone Update Rate")]
        [Description("Stops boobs and skirt jiggle when above 60FPS. Change this value to your screen refresh rate and run with VSync for best results. Applies instantly.")]
        [AcceptableValueRange(1, 240, false)]
        public static ConfigWrapper<int> BoneUpdateRate { get; private set; }
    #endregion

    void Awake() {
        HarmonyInstance.Create(nameof(KK_DynamicBoneUpdateRate)).PatchAll(typeof(KK_DynamicBoneUpdateRate));

        PluginEnabled = new ConfigWrapper<bool>("DynamicBone Modifier Enable", this, false);
        BoneUpdateRate = new ConfigWrapper<int>("DynamicBone Update Rate", this, 60);
    }

    [HarmonyPrefix, HarmonyPatch(typeof(DynamicBone), "UpdateDynamicBones")]
    public static void Pre_DynamicBone_UpdateDynamicBones(DynamicBone __instance) {
        if (PluginEnabled.Value)
            __instance.m_UpdateRate = BoneUpdateRate.Value;
    }


    [HarmonyPrefix, HarmonyPatch(typeof(DynamicBone_Ver02), "UpdateDynamicBones")]
    public static void Pre_DynamicBone_Ver02_UpdateDynamicBones(DynamicBone_Ver02 __instance) {
        if (PluginEnabled.Value)
            __instance.UpdateRate = BoneUpdateRate.Value;
    }
}
