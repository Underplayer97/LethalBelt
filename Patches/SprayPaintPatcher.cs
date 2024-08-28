using GameNetcodeStuff;
using HarmonyLib;
using LethalLib;
using ReservedItemSlotCore.Data;
using ReservedItemSlotCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LethalBelt.Patches {
    [HarmonyPatch]
    internal class SprayPaintPatcher {

        public static PlayerControllerB localPlayerController { get { return StartOfRound.Instance?.localPlayerController; } }

        public static PlayerControllerB GetPreviousPlayerHeldBy(SprayPaintItem sprayPaintItem) => (PlayerControllerB)Traverse.Create(sprayPaintItem).Field("previousPlayerHeldBy").GetValue();

        public static SprayPaintItem GetMainSprayPaint(PlayerControllerB playerController) => GetCurrentlySelectedSprayPaint(playerController) ?? GetReservedSprayPaint(playerController);
        public static SprayPaintItem GetReservedSprayPaint(PlayerControllerB playerController) => SessionManager.TryGetUnlockedItemSlotData(LethalBelt.utilitySlotData1.slotName, out var itemSlot) && ReservedPlayerData.allPlayerData.TryGetValue(playerController, out var playerData) ? playerData.GetReservedItem(itemSlot) as SprayPaintItem : null;
        public static SprayPaintItem GetCurrentlySelectedSprayPaint(PlayerControllerB playerController) => playerController.currentItemSlot >= 0 && playerController.currentItemSlot < playerController.ItemSlots.Length ? playerController?.ItemSlots[playerController.currentItemSlot] as SprayPaintItem : null;


        //TODO: Get it working with the two other slots


    }
}
