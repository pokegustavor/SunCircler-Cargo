using HarmonyLib;
using PulsarModLoader;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SunCircler_Cargo
{
    internal class Patch
    {
        [HarmonyPatch(typeof(PLShipInfo), "CreateShipInstUIs")]
        internal class Assemble
        {
            public static bool requested = false;
            public static void Postfix(PLShipInfo __instance)
            {
                if (__instance != null && __instance.ShipTypeID == EShipType.E_CIVILIAN_STARTING_SHIP && (PhotonNetwork.isMasterClient || PulsarModLoader.MPModChecks.MPModCheckManager.Instance.NetworkedPeerHasMod(PhotonNetwork.masterClient, "Suncicler.Cargo")))
                {
                    GameObject LifeSupport = __instance.RepairableSystemInstances[3].gameObject;
                    if (LifeSupport != null)
                    {
                        LifeSupport.transform.localPosition = new Vector3(-1.949f, -0.7506f, 3.5464f);
                        __instance.SysInstUIRoots[3].transform.localPosition = new Vector3(-109.98f, 742.45f, 1901.72f);
                        int index = 12;
                        List<GameObject> cargo = new List<GameObject>();
                        cargo.AddRange(__instance.CargoBases);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                GameObject cargoslot = Object.Instantiate(__instance.CargoBases[0], Vector3.zero, __instance.CargoBases[0].transform.rotation);
                                Object.DontDestroyOnLoad(cargoslot);
                                cargo.Add(cargoslot);
                                cargoslot.transform.SetParent(__instance.InteriorDynamic.transform);
                                cargoslot.transform.localPosition = new Vector3(4.1615f + (i * 2), 13.4521f, 24.8196f + (j * 1.761f));
                                CargoObjectDisplay cargoDisplay = new CargoObjectDisplay
                                {
                                    RootObj = cargoslot,
                                    DisplayedItem = null,
                                    DisplayObj = null,
                                    Index = index,
                                    Hidden = false
                                };
                                __instance.CargoObjectDisplays.Add(cargoDisplay);
                                index++;
                            }
                        }
                        Object.Destroy(__instance.InteriorStatic.transform.GetChild(12).GetChild(34).gameObject);
                        __instance.CargoBases = cargo.ToArray();
                        __instance.MyStats.SetSlotLimit(ESlotType.E_COMP_CARGO, __instance.CargoBases.Length);
                        
                    }
                }
            }
        }
    }
}
