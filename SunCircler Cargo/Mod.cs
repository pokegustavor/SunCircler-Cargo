using PulsarModLoader;

namespace SunCircler_Cargo
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.0";

        public override string Author => "pokegustavo";

        public override string ShortDescription => "moves life support and adds extra cargo";

        public override string Name => "SunCicler Extra Cargo";

        public override string HarmonyIdentifier()
        {
            return "Suncicler.Cargo";
        }
    }
}
