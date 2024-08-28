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
        public static ReservedItemSlotData utilitySlotData1;
        public static ReservedItemSlotData utilitySlotData2;
        public static ReservedItemSlotData utilitySlotData3;

        public static ReservedItemData beltData; //Belt
        public static ReservedItemData flashlightData; //Flashlight
        public static ReservedItemData proFlashlightData; //ProFlashlight
        public static ReservedItemData lockpickerData; // Lockpicker
        public static ReservedItemData tzpInhalentData; //TZP
        public static ReservedItemData weedKillerData; //Weed
        public static ReservedItemData keyData; //Key
        public static ReservedItemData walkieData; //Walkie-Talkie
        public static ReservedItemData sprayPaintData; //Spray Paint

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

            //Belt Slot
            beltSlotData = ReservedItemSlotData.CreateReservedItemSlotData("beltSlot", ConfigSettings.overrideItemSlotPriority.Value, ConfigSettings.overridePurchasePrice.Value);

            //Item not implemented yet
            //beltData = beltSlotData.AddItemToReservedItemSlot(new ReservedItemData("UtilityBelt")); //Need to also add the playerbone locations


            //Slot 1
            utilitySlotData1 = ReservedItemSlotData.CreateReservedItemSlotData("utilitySlot1", ConfigSettings.overrideItemSlotPriority.Value, ConfigSettings.overridePurchasePrice.Value);

            flashlightData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
            proFlashlightData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Pro-flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));         
            sprayPaintData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Spray paint", PlayerBone.Hips, new Vector3(0.26f, -0.05f, 0.2f), new Vector3(-105, 0, 0)));
            walkieData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Walkie-talkie", PlayerBone.Spine3, new Vector3(0.15f, -0.05f, 0.25f), new Vector3(0, -90, 100)));
            tzpInhalentData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("TZP-Inhalent"));
            lockpickerData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Lockpicker"));
            weedKillerData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Weed killer"));
            keyData = utilitySlotData1.AddItemToReservedItemSlot(new ReservedItemData("Key"));
            
            //Slot 2
            utilitySlotData2 = ReservedItemSlotData.CreateReservedItemSlotData("utilitySlot2", ConfigSettings.overrideItemSlotPriority.Value, ConfigSettings.overridePurchasePrice.Value);

            flashlightData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
            proFlashlightData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Pro-flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
            sprayPaintData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Spray paint", PlayerBone.Hips, new Vector3(0.26f, -0.05f, 0.2f), new Vector3(-105, 0, 0)));
            walkieData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Walkie-talkie", PlayerBone.Spine3, new Vector3(0.15f, -0.05f, 0.25f), new Vector3(0, -90, 100)));
            tzpInhalentData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("TZP-Inhalent"));
            lockpickerData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Lockpicker"));
            weedKillerData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Weed killer"));
            keyData = utilitySlotData2.AddItemToReservedItemSlot(new ReservedItemData("Key"));

           //Slot 
           utilitySlotData3 = ReservedItemSlotData.CreateReservedItemSlotData("utilitySlot3", ConfigSettings.overrideItemSlotPriority.Value, ConfigSettings.overridePurchasePrice.Value);

           flashlightData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
           proFlashlightData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Pro-flashlight", PlayerBone.Spine3, new Vector3(0.2f, 0.25f, 0), new Vector3(90, 0, 0)));
           sprayPaintData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Spray paint", PlayerBone.Hips, new Vector3(0.26f, -0.05f, 0.2f), new Vector3(-105, 0, 0)));
           walkieData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Walkie-talkie", PlayerBone.Spine3, new Vector3(0.15f, -0.05f, 0.25f), new Vector3(0, -90, 100)));
           tzpInhalentData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("TZP-Inhalent"));
           lockpickerData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Lockpicker"));
           weedKillerData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Weed killer"));
           keyData = utilitySlotData3.AddItemToReservedItemSlot(new ReservedItemData("Key"));

        }

        void CreateAddionalReservedItemSlots() {
            string[] addionalItemNames = ConfigSettings.ParseAdditionalItems();
            foreach (string itemName in addionalItemNames) {

                if (!utilitySlotData1.ContainsItem(itemName)) {

                    Logger.LogWarning("Adding" + itemName + "to reserved slot");
                    var itemData = new ReservedItemData(itemName);
                    addionalItemData.Add(itemData);
                    utilitySlotData1.AddItemToReservedItemSlot(itemData);

                }

                if (!utilitySlotData2.ContainsItem(itemName)){
                    Logger.LogWarning("Adding" + itemName + "to reserved slot");
                    var itemData = new ReservedItemData(itemName);
                    addionalItemData.Add(itemData);                    
                    utilitySlotData2.AddItemToReservedItemSlot(itemData);
                    
                }

                if (!utilitySlotData3.ContainsItem(itemName))
                {

                    Logger.LogWarning("Adding" + itemName + "to reserved slot");
                    var itemData = new ReservedItemData(itemName);
                    addionalItemData.Add(itemData);
                    utilitySlotData3.AddItemToReservedItemSlot(itemData);

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
