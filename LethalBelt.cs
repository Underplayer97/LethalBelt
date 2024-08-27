using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ReservedItemSlotCore.Data;
using System.Collections.Generic;
using UnityEngine;
using LethalBelt.Input;
using LethalBelt.Config;

namespace LethalBelt {
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("FlipMods.ReservedItemSlotCore", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class LethalBelt : BaseUnityPlugin {
        public static LethalBelt Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        public static ReservedItemSlotData beltSlotData;
        public static ReservedItemData FlashlightData;
        public static ReservedItemData proFlashlightData;

        public static List<ReservedItemData> addionalItemData = new List<ReservedItemData>();
        public static bool IsModLoaded(string guid) => BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(guid);

        private void Awake() {
            Logger = base.Logger;
            Instance = this;

            ConfigSettings.BindConfigSettings();
            CreateReservedItemSlots();
            CreateAddionalReservedItemSlots();

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        void CreateReservedItemSlots() {

            beltSlotData = ReservedItemSlotData.CreateReservedItemSlotData("flashlight", ConfigSettings.overrideItemSlotPriority
                .Value, ConfigSettings.overridePurchasePrice.Value);

            FlashlightData = beltSlotData.AddItemToReservedItemSlot(new ReservedItemData("Flashlight", PlayerBone.Spine3,
                new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
            proFlashlightData = beltSlotData.AddItemToReservedItemSlot(new ReservedItemData("Pro_flashlight", PlayerBone.Spine3, 
                new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));

        }

        void CreateAddionalReservedItemSlots() {
            string[] addionalItemNames = ConfigSettings.ParseAdditionalItems();
            foreach (string itemName in addionalItemNames) {

                if (!beltSlotData.ContainsItem(itemName)) {

                    Logger.LogWarning("Adding" + itemName + "to reserved slot");
                    var itemData = new ReservedItemData(itemName);
                    addionalItemData.Add(itemData);
                    beltSlotData.AddItemToReservedItemSlot(itemData);

                }


            }

        }


        internal static void Patch() {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch() {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
