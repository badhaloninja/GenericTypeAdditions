using BaseX;
using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Collections.Generic;

namespace GenericTypeAdditions
{
    public class GenericTypeAdditions : NeosMod
    {
        public override string Name => "GenericTypeAdditions";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/GenericTypeAdditions";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.GenericTypeAdditions");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(GenericTypes), "GetTypes")]
        class GenericTypes_GetTypes_Patch
        {
            private static Dictionary<GenericTypes.Group, Type[]> GenericTypeAdditions = new Dictionary<GenericTypes.Group, Type[]>(){
                {GenericTypes.Group.Primitives, new Type[] {
                    typeof(Uri)
                }},
                {GenericTypes.Group.NeosPrimitives, new Type[] {
                    typeof(Uri)
                }},
                {GenericTypes.Group.CommonEnums, new Type[]
                {
                    typeof(AudioDistanceSpace),
                    typeof(CloudX.Shared.OwnerType)
                }},
                {GenericTypes.Group.NeosPrimitivesAndEnums, new Type[] {
                    typeof(Uri),
                    typeof(AudioDistanceSpace),
                    typeof(CloudX.Shared.OwnerType)
                }},
            };
            public static void Postfix(GenericTypes.Group group, ref Type[] __result)
            {
                Type[] additions;
                if (GenericTypeAdditions.TryGetValue(group, out additions))
                {
                    __result = __result.Append(additions);
                }

            }
        }
    }
}