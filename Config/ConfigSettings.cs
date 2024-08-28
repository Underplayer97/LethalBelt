using BepInEx.Configuration;
using LethalLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LethalBelt.Config {
    public class ConfigSettings {

        public static ConfigEntry<int> overrideItemSlotPriority;
        public static ConfigEntry<int> overridePurchasePrice;
        public static ConfigEntry<string> additionalItemsInSlot;

        public static ConfigEntry<int> numUtilitySlots;
        public static ConfigEntry<int> overrideExtraAmmoSlotPriceIncrease;
        public static ConfigEntry<string> removeItemsFromSlot;

        public static ConfigEntry<bool> disableUtilitySlot;
        public static ConfigEntry<bool> addKeySlot;
        public static ConfigEntry<int> overrideKeySlotPriority;
        public static ConfigEntry<int> overrideKeySlotPrice;
        public static ConfigEntry<string> addAdditionalItemsToKeySlot;
        public static ConfigEntry<bool> moveLockpickerToKeySlot;
        public static ConfigEntry<float> sprayPaintCapacityMultiplier;

        public static Dictionary<string, ConfigEntryBase> currentConfigEntries = new Dictionary<string, ConfigEntryBase>();

        public static void BindConfigSettings() {

            LethalBelt.Logger.LogInfo("Configs bound");

            overrideItemSlotPriority = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "FlashlightSlotPriorityOverride", 200, "[Host] Manually set the priority for this item slot. Higher priority slots will come first in the reserved item slots, which will appear below the other slots. Negative priority items will appear on the left side of the screen, this is disabled in the core mod's config."));
            overridePurchasePrice = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "FlashlightSlotPriceOverride", 200, "[Host] Manually set the price for this item in the store. Setting 0 will force this item to be unlocked immediately after the game starts."));
            additionalItemsInSlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "AdditionalItemsInSlot", "", "[Host] Syntax: \"Item1,Item name2\" (without quotes). When adding items, use the item's name as it appears in game. Include spaces if there are spaces in the item name. Adding items that do not exist, or that are from a mod which is not enabled will not cause any problems.\nNOTE: IF YOU ARE USING A TRANSLATION MOD, YOU MAY NEED TO ADD THE TRANSLATED NAME OF ANY ITEM YOU WANT IN THIS SLOT."));

            numUtilitySlots = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "NumUtilitySlots", 1, "[Host only] Sets the amount of reserved utility slots. Consider changing the priority if the priority of additional utility slots conflicts with other slots."));
            overrideExtraAmmoSlotPriceIncrease = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "ExtraUtilitySlotsPriceIncreaseOverride", 20, "[Host only] If multiple utility slots are added, and purchasing slots is enabled, the price for each additional utility slot will go up by this amount."));
            removeItemsFromSlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "RemoveItemsFromSlot", "", "[Host only] Syntax: \"Item1,Item name2\" (without quotes). Removes the specified items from this reserved item slot.When removing items, use the item's name as it appears in game. Include spaces if there are spaces in the item name. Adding items that do not exist, or that are from a mod which is not enabled will not cause any problems.\nCURRENT ITEMS IN SLOT: \"Extension ladder\", \"Lockpicker\", \"Jetpack\", \"Stun grenade\", \"Homemade flashbang\", \"TZP-Inhalant\", \"Radar-booster\", \"Remote Radar\", \"Utility Belt\", \"Hacking Tool\", \"Pinger\", \"Portable Tele\", \"Advanced Portable Tele\", \"Peeper\", \"Medkit\", \"Binoculars\", \"Mapper\", \"Toothpaste\" (you got a problem with toothpaste??)"));

            disableUtilitySlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "DisableUtilitySlot", false, "[Host only] Disables the utility slot. Use this if you only want the reserved key slot."));
            addKeySlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "AddKeySlot", false, "[Host only] Adds a reserved item slot for the key item. By default, the slot will appear on the left side of the screen, unless given a positive item slot priority."));
            overrideKeySlotPriority = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "KeySlotPriorityOverride", -40, "[Host only] Manually set the priority for the key item slot. Higher priority slots will come first in the reserved item slots, which will appear below the other slots. Negative priority items will appear on the left side of the screen, this is disabled in the core mod's config."));
            overrideKeySlotPrice = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "KeySlotPriceOverride", 50, "[Host only] Manually set the price for the key item in the store. Setting 0 will force this item to be unlocked immediately after the game starts."));
            moveLockpickerToKeySlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "MoveLockpickerToKeySlot", false, "[Host only] Moves the lockpicker to the key slot. This setting will do nothing if the reserved key slot is disabled in the config."));
            addAdditionalItemsToKeySlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "AdditionalItemsInKeySlot", "", "[Host only] Syntax: \"Item1,Item name2\" (without quotes). When adding items, use the item's name as it appears in game. Include spaces if there are spaces in the item name. Adding items that do not exist, or that are from a mod which is not enabled will not cause any problems.\nNOTE: IF YOU ARE USING A TRANSLATION MOD, YOU MAY NEED TO ADD THE TRANSLATED NAME OF ANY ITEM YOU WANT IN THIS SLOT."));
            sprayPaintCapacityMultiplier = AddConfigEntry(LethalBelt.Instance.Config.Bind("Client-side", "SprayPaintCapacityMultiplier", 10f, "Extends the max capacity of spraypaint cans by this multiplier. This setting will soon be host only, and will sync with all non-host clients."));
            overrideItemSlotPriority = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "SprayPaintPriorityOverride", 25, "Manually set the priority for this item slot. Higher priority slots will come first in the reserved item slots, which will appear below the other slots. Negative priority items will appear on the left side of the screen, this is disabled in the core mod's config."));
            overridePurchasePrice = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "SprayPaintSlotPriceOverride", 50, "Manually set the price for this item in the store. Setting 0 will force this item to be unlocked immediately after the game starts."));
            additionalItemsInSlot = AddConfigEntry(LethalBelt.Instance.Config.Bind("Server-side", "AdditionalItemsInSlot", "", "Syntax: \"Item1,Item name2\" (without quotes). When adding items, use the item's name as it appears in game. Include spaces if there are spaces in the item name. Adding items that do not exist, or that are from a mod which is not enabled will not cause any problems.\nNOTE: IF YOU ARE USING A TRANSLATION MOD, YOU MAY NEED TO ADD THE TRANSLATED NAME OF ANY ITEM YOU WANT IN THIS SLOT."));


            additionalItemsInSlot.Value = additionalItemsInSlot.Value.Replace(", ", ",");

            TryRemoveOldConfigSettings();

        }

        public static ConfigEntry<T> AddConfigEntry<T>(ConfigEntry<T> configEntry)
        {
            currentConfigEntries.Add(configEntry.Definition.Key, configEntry);
            return configEntry;
        }

        public static string[] ParseAdditionalItems() => ReservedItemSlotCore.Config.ConfigSettings.ParseItemNames(additionalItemsInSlot.Value);
        public static string[] ParseRemoveItems() => ReservedItemSlotCore.Config.ConfigSettings.ParseItemNames(removeItemsFromSlot.Value);
        public static string[] ParseAdditionalKeyItems() => ReservedItemSlotCore.Config.ConfigSettings.ParseItemNames(addAdditionalItemsToKeySlot.Value);

        public static void TryRemoveOldConfigSettings()
        {
            HashSet<string> headers = new HashSet<string>();
            HashSet<string> keys = new HashSet<string>();

            foreach (ConfigEntryBase entry in currentConfigEntries.Values)
            {
                headers.Add(entry.Definition.Section);
                keys.Add(entry.Definition.Key);
            }

            try
            {

                ConfigFile config = LethalBelt.Instance.Config;
                string filepath = config.ConfigFilePath;

                if (File.Exists(filepath))
                {
                    string contents = File.ReadAllText(filepath);
                    string[] lines = File.ReadAllLines(filepath); // Because contents.Split('\n') is adding strange characters...

                    string currentHeader = "";

                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = lines[i].Replace("\n", "");
                        if (lines[i].Length <= 0)
                            continue;

                        if (lines[i].StartsWith("["))
                        {
                            if (currentHeader != "" && !headers.Contains(currentHeader))
                            {
                                currentHeader = "[" + currentHeader + "]";
                                int index0 = contents.IndexOf(currentHeader);
                                int index1 = contents.IndexOf(lines[i]);
                                contents = contents.Remove(index0, index1 - index0);
                            }
                            currentHeader = lines[i].Replace("[", "").Replace("]", "").Trim();
                        }

                        else if (currentHeader != "")
                        {
                            if (i <= (lines.Length - 4) && lines[i].StartsWith("##"))
                            {
                                int numLinesEntry = 1;
                                while (i + numLinesEntry < lines.Length && lines[i + numLinesEntry].Length > 3) // 3 because idc
                                    numLinesEntry++;

                                if (headers.Contains(currentHeader))
                                {
                                    int indexAssignOperator = lines[i + numLinesEntry - 1].IndexOf("=");
                                    string key = lines[i + numLinesEntry - 1].Substring(0, indexAssignOperator - 1);
                                    if (!keys.Contains(key))
                                    {
                                        int index0 = contents.IndexOf(lines[i]);
                                        int index1 = contents.IndexOf(lines[i + numLinesEntry - 1]) + lines[i + numLinesEntry - 1].Length;
                                        contents = contents.Remove(index0, index1 - index0);
                                    }
                                }
                                i += (numLinesEntry - 1);
                            }
                            else if (lines[i].Length > 3)
                                contents = contents.Replace(lines[i], "");
                        }
                    }

                    if (!headers.Contains(currentHeader))
                    {
                        currentHeader = "[" + currentHeader + "]";
                        int index0 = contents.IndexOf(currentHeader);
                        contents = contents.Remove(index0, contents.Length - index0);
                    }

                    while (contents.Contains("\n\n\n"))
                        contents = contents.Replace("\n\n\n", "\n\n");

                    File.WriteAllText(filepath, contents);
                    config.Reload();
                }
            }
            catch { } 
        }
    }

}

