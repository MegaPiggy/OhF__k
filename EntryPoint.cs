using HarmonyLib;
using SALT;
using SALT.Extensions;
using SALT.Registries;
using SALT.Config;
using SALT.Console;
using SALT.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace OhFuck
{
    public class EntryPoint : ModEntryPoint
    {
        /// <summary>THE EXECUTING ASSEMBLY</summary>
        public static Assembly execAssembly;

        public static AssetBundle bundle;

        public override void PreLoad()
        {
            // Gets the Assembly being executed
            execAssembly = Assembly.GetExecutingAssembly();
            HarmonyInstance.PatchAll(execAssembly);
            bundle = LoadAssetbundle("ohfuck");
        }

        public override void Load()
        {
            CharacterPack ame = CharacterRegistry.GetCharacter(Character.AMELIA).GetComponent<CharacterPack>();
            CharacterPack ame_mous = CharacterRegistry.GetCharacter(Character.AMELIA_MOUSTACHE).GetComponent<CharacterPack>();

            var clone = ame.deathSounds.ToList();

            AudioClip oh_fuck = bundle.LoadAsset<AudioClip>("oh_fuck");
            clone.Add(oh_fuck);

            ame.deathSounds = clone;
            ame_mous.deathSounds = clone;
        }

        public static AssetBundle LoadAssetbundle(string name)
        {
            Stream stream = execAssembly.GetManifestResourceStream(typeof(EntryPoint), name);
            if (stream == null)
                throw new Exception("AssetBundle " + name + " was not found");
            return AssetBundle.LoadFromStream(stream);
        }
    }
}
