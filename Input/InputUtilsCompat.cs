using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace LethalBelt.Input {
    internal class InputUtilsCompat {
        internal static InputActionAsset Asset { get { return InGameKeybinds.GetAsset(); } }
        internal static bool Enabled => LethalBelt.IsModLoaded("com.rune580.LethalCompanyInputUtils");

        public static InputAction ToggleFlashlightHotkey => InGameKeybinds.Instance.ToggleFlashlightHotkey;

    }
}
