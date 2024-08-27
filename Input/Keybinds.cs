using GameNetcodeStuff;
using HarmonyLib;
using LethalBelt.Patches;
using ReservedItemSlotCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LethalBelt.Input {

    [HarmonyPatch]
    internal static class Keybinds
    {
        public static PlayerControllerB localPlayerController { get { return StartOfRound.Instance?.localPlayerController; } }

        public static InputActionAsset Asset;
        public static InputActionMap ActionMap;

        static InputAction ActivateFlashlightAction;

        [HarmonyPatch(typeof(PreInitSceneScript), "Awake")]
        [HarmonyPrefix]
        public static void AddToKeybindMenu()
        {

            LethalBelt.Logger.LogDebug("Kebinds Initilized");

            if (InputUtilsCompat.Enabled)
            {
                Asset = InputUtilsCompat.Asset;
                ActionMap = Asset.actionMaps[0];
                ActivateFlashlightAction = InputUtilsCompat.ToggleFlashlightHotkey;
            }
            else
            {

                Asset = ScriptableObject.CreateInstance<InputActionAsset>();
                ActionMap = new InputActionMap("LethalBelt");
                Asset.AddActionMap(ActionMap);

                ActivateFlashlightAction = ActionMap.AddAction("LethalBelt.ToggleFlashlight", binding: "<keyboard>/f");


            }
        }


        [HarmonyPatch(typeof(StartOfRound), "OnEnable")]
        [HarmonyPostfix]
        public static void OnEnable()
        {

            Asset.Enable();
            ActivateFlashlightAction.performed += OnActivateFlashlightPerformed;

        }

        [HarmonyPatch(typeof(StartOfRound), "OnDisable")]
        [HarmonyPostfix]
        public static void OnDisable()
        {

            Asset.Disable();
            ActivateFlashlightAction.performed -= OnActivateFlashlightPerformed;

        }

        private static void OnActivateFlashlightPerformed(InputAction.CallbackContext context)
        {
            if (localPlayerController == null || !localPlayerController.isPlayerControlled || (localPlayerController.IsServer && !localPlayerController.isHostPlayerObject))
                return;

            if (!context.performed || ShipBuildModeManager.Instance.InBuildMode || localPlayerController.inTerminalMenu)
                return;

            if (ReservedPlayerData.localPlayerData.timeSinceSwitchingSlots < 0.075f)
                return;

            if (localPlayerController.isTypingChat || localPlayerController.inTerminalMenu || localPlayerController.quickMenuManager.isMenuOpen || localPlayerController.isPlayerDead || localPlayerController.isGrabbingObjectAnimation || ReservedPlayerData.localPlayerData.isGrabbingReservedItem)
                return;

            FlashlightItem mainFlashlight = FlashlightPatcher.GetMainFlashlight(localPlayerController);
            if (!mainFlashlight)
            {
                mainFlashlight = FlashlightPatcher.GetFirstFlashlightItem(localPlayerController);
                if (!mainFlashlight)
                    return;
            }

            bool activate = !mainFlashlight.isBeingUsed;
            if (activate && mainFlashlight != FlashlightPatcher.GetCurrentlySelectedFlashlight(localPlayerController))
                localPlayerController.pocketedFlashlight = mainFlashlight;

            mainFlashlight.UseItemOnClient(activate);
            Traverse.Create(localPlayerController).Field("timeSinceSwitchingSlots").SetValue(0);
        }


    }
}

