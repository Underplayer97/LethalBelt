using LethalCompanyInputUtils.Api;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace LethalBelt.Input {
    internal class InGameKeybinds : LcInputActions {

        internal static InGameKeybinds Instance = new InGameKeybinds();
        internal static InputActionAsset GetAsset() => Instance.Asset;

        [InputAction("<Keyboard>/f", Name = "[LethalBelt]\nToggle flashlight")]
        public InputAction ToggleFlashlightHotkey { get; set; }

    }
}
