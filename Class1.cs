using GTA;
using GTA.Math;
using GTA.Native;
using iFruitAddon2;
using Microsoft.Win32;
using MMI_SP.Properties;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using System.Xml.Linq;
using SE;
using System.Drawing;
using System.Linq;
using MMI_SP.iFruit;
using System.Reflection;
using System.Net;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using System.Media;
using System.CodeDom.Compiler;
using System.ComponentModel;


namespace MMI_SP
{
    internal static class Config
    {
        private static string _baseDir = AppDomain.CurrentDomain.BaseDirectory + "\\MMI";
        private static string _configFile = Config._baseDir + "\\config.ini";
        private static string _bannerImage = Config._baseDir + "\\banner.png";
        private static string _insuranceImage = Config._baseDir + "\\insurance.png";
        private static string _languageFile = Config._baseDir + "\\default.xml";
        private static ScriptSettings _settings;

        public static ScriptSettings Settings
        {
            get => Config._settings;
            private set => Config._settings = value;
        }

        public static void Initialize()
        {
            if (!Directory.Exists(Config._baseDir))
            {
                Logger.Log((object)"Creating config directory.");
                Directory.CreateDirectory(Config._baseDir);
            }
            if (!File.Exists(Config._configFile))
            {
                Logger.Log((object)"Creating config file.");
                File.WriteAllText(Config._configFile, Resources.config);
            }
            if (!File.Exists(Config._bannerImage))
            {
                Logger.Log((object)"Creating banner image file.");
                Resources.banner.Save(Config._bannerImage);
            }
            if (!File.Exists(Config._insuranceImage))
            {
                Logger.Log((object)"Creating insurance image file.");
                Resources.insurance.Save(Config._insuranceImage);
            }
            if (!File.Exists(Config._languageFile))
            {
                Logger.Log((object)"Creating default language file.");
                File.WriteAllText(Config._languageFile, Resources._default);
            }
            if (File.Exists(Config._configFile))
                Config._settings = ScriptSettings.Load(Config._configFile);
            else
                Logger.Log((object)("Error: Settings.Initialize - Config file cannot be found! " + Config._configFile));
        }
    }
    public static class Debug
    {
        public static int GetPhoneHandle()
        {
            switch ((uint)Game.Player.Character.Model.Hash)
            {
                case 225514697:
                    return Function.Call<int>(Hash._0x11FE353CF9733E6F, (InputArgument)"cellphone_ifruit");
                case 2602752943:
                    return Function.Call<int>(Hash._0x11FE353CF9733E6F, (InputArgument)"cellphone_badger");
                case 2608926626:
                    return Function.Call<int>(Hash._0x11FE353CF9733E6F, (InputArgument)"cellphone_facade");
                default:
                    return Function.Call<int>(Hash._0x11FE353CF9733E6F, (InputArgument)"cellphone_ifruit");
            }
        }

        public static void DisplayCallUI(int handle, string contactName = "Test contact", string picName = "CELL_300")
        {
            Function.Call(Hash._0xF6E48914C7A8694E, (InputArgument)handle, (InputArgument)"SET_DATA_SLOT");
            Function.Call(Hash._0xC3D0841A0CC546A6, (InputArgument)4);
            Function.Call(Hash._0xC3D0841A0CC546A6, (InputArgument)0);
            Function.Call(Hash._0xC3D0841A0CC546A6, (InputArgument)3);
            Function.Call(Hash._0x80338406F3475E55, (InputArgument)"STRING");
            Function.Call(Hash._0x761B77454205A61D, (InputArgument)contactName, (InputArgument) (- 1));
            Function.Call(Hash._0x362E2D3FE93A9959);
            Function.Call(Hash._0x80338406F3475E55, (InputArgument)"CELL_2000");
            Function.Call(Hash._0x6C188BE134E074AA, (InputArgument)picName);
            Function.Call(Hash._0x362E2D3FE93A9959);
            Function.Call(Hash._0x80338406F3475E55, (InputArgument)"STRING");
            Function.Call(Hash._0x761B77454205A61D, (InputArgument)"DIALING...", (InputArgument) (- 1));
            Function.Call(Hash._0x362E2D3FE93A9959);
            Function.Call(Hash._0xC6796A8FFA375E53);
            Function.Call(Hash._0xF6E48914C7A8694E, (InputArgument)handle, (InputArgument)"DISPLAY_VIEW");
            Function.Call(Hash._0xC3D0841A0CC546A6, (InputArgument)4);
            Function.Call(Hash._0xC6796A8FFA375E53);
        }

        public static void ShowVehicleInfo(GTA.Vehicle veh, float x = 0.825f, float y = 0.65f)
        {
            GTA.Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
            if (!((Entity)veh != (Entity)null))
                return;
            SE.UI.DrawText((object)"Last Vehicle", 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("Last Handle: " + veh.Handle.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            if ((Entity)currentVehicle != (Entity)null)
            {
                SE.UI.DrawText((object)("Current Handle: " + Game.Player.Character.CurrentVehicle.Handle.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
                y += 0.025f;
            }
            SE.UI.DrawText((object)("Driveable: " + veh.IsDriveable.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("Persistent: " + veh.IsPersistent.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("MissionEntity: " + Function.Call<bool>(Hash._0x0A7B270912999B3C, (InputArgument)veh).ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("ModelHash: " + veh.Model.Hash.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("GameplayCamera: " + GameplayCamera.IsRendering.ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            y += 0.025f;
            SE.UI.DrawText((object)("Assuré: " + InsuranceManager.IsVehicleInsured(Tools.GetVehicleIdentifier(veh)).ToString()), 0, false, x, y, 0.4f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        }

        public static void DebugNotification(
          string picture,
          string title,
          string subtitle,
          string message)
        {
            Function.Call(Hash._0xDFA2EF8E04127DD5, (InputArgument)picture, (InputArgument)false);
            while (!Function.Call<bool>(Hash._0x0145F696AAAAD2E4, (InputArgument)picture))
                GTA.Script.Yield();
            Function.Call(Hash._0x202709F4C58A0424, (InputArgument)"STRING");
            Function.Call(Hash._0x6C188BE134E074AA, (InputArgument)message);
            Function.Call(Hash._0x1CCD9A37359072CF, (InputArgument)picture, (InputArgument)picture, (InputArgument)false, (InputArgument)4, (InputArgument)title, (InputArgument)subtitle);
            Function.Call(Hash._0x2ED7843F8F801023, (InputArgument)false, (InputArgument)true);
        }
    }
    internal static class DialogueManager
    {
        private static List<DialogueManager.Speech> OfficeHiCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BEVHILLS_01_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BEVHILLS_01_WHITE_MINI_02", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BEVHILLS_02_WHITE_FULL_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BEVHILLS_02_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "A_F_M_BUSINESS_02_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BUSINESS_02_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BEVHILLS_01_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BEVHILLS_02_WHITE_FULL_01", "SPEECH_PARAMS_FORCE")
    };
        private static List<DialogueManager.Speech> OfficeNiceCarCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEVHILLS_02_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEVHILLS_02_WHITE_FULL_02", "SPEECH_PARAMS_STANDARD")
    };
        private static List<DialogueManager.Speech> OfficeSomethingCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BEVHILLS_01_WHITE_MINI_02", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("GENERIC_HOWS_IT_GOING", "A_F_M_BEVHILLS_02_WHITE_MINI_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("PED_RANT_01", "A_F_M_BUSINESS_02_WHITE_MINI_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("CHALLENGE_ACCEPTED_GENERIC", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("CHAT_RESP", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("GENERIC_WHATEVER", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEVHILLS_02_WHITE_FULL_01", "SPEECH_PARAMS_STANDARD"),
      new DialogueManager.Speech("NICE_CAR", "A_F_M_BEVHILLS_02_WHITE_FULL_02", "SPEECH_PARAMS_STANDARD")
    };
        private static List<DialogueManager.Speech> OfficeByeCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("GENERIC_BYE", "A_F_M_BUSINESS_02_WHITE_MINI_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GOODBYE_ACROSS_STREET", "A_F_M_BUSINESS_02_WHITE_MINI_01", "SPEECH_PARAMS_FORCE")
    };
        private static List<DialogueManager.Speech> OfficeNaughtyCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("CHALLENGE_THREATEN", "A_F_M_BEACH_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_HI", "S_F_Y_HOOKER_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("HOOKER_OFFER_SERVICE", "S_F_Y_HOOKER_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE")
    };
        private static List<DialogueManager.Speech> OfficeNaughtyByeCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("SEX_FINISHED", "S_F_Y_HOOKER_01_WHITE_FULL_01", "SPEECH_PARAMS_FORCE")
    };
        private static List<DialogueManager.Speech> DriverByeCollection = new List<DialogueManager.Speech>()
    {
      new DialogueManager.Speech("GENERIC_BYE", "S_M_M_AUTOSHOP_01_WHITE_01", "SPEECH_PARAMS_FORCE"),
      new DialogueManager.Speech("GENERIC_BYE", "S_M_M_GENERICMECHANIC_01_BLACK_MINI_01", "SPEECH_PARAMS_FORCE")
    };

        internal static List<DialogueManager.Speech> GetSpeechList(DialogueManager.SpeechType type)
        {
            List<DialogueManager.Speech> speechList = new List<DialogueManager.Speech>();
            switch (type)
            {
                case DialogueManager.SpeechType.OfficeHi:
                    speechList.AddRange((IEnumerable<DialogueManager.Speech>)DialogueManager.OfficeHiCollection);
                    break;
                case DialogueManager.SpeechType.OfficeBye:
                    speechList.AddRange((IEnumerable<DialogueManager.Speech>)DialogueManager.OfficeByeCollection);
                    break;
                case DialogueManager.SpeechType.OfficeNiceCar:
                    speechList.AddRange((IEnumerable<DialogueManager.Speech>)DialogueManager.OfficeNiceCarCollection);
                    break;
                case DialogueManager.SpeechType.OfficeSomething:
                    speechList.AddRange((IEnumerable<DialogueManager.Speech>)DialogueManager.OfficeSomethingCollection);
                    break;
                case DialogueManager.SpeechType.DriverBye:
                    speechList.AddRange((IEnumerable<DialogueManager.Speech>)DialogueManager.DriverByeCollection);
                    break;
            }
            return speechList;
        }

        internal enum SpeechType
        {
            OfficeHi,
            OfficeBye,
            OfficeNiceCar,
            OfficeSomething,
            OfficeNaughty,
            OfficeNaughtyBye,
            DriverBye,
        }

        internal class Speech
        {
            internal string Name;
            internal string Voice;
            internal string Param;
            internal int Index;

            public Speech(string speechName, string voiceName, string speechParam, int i = 0)
            {
                this.Name = speechName;
                this.Voice = voiceName;
                this.Param = speechParam;
                this.Index = i;
            }
        }
    }
    internal class IncomingVehicle
    {
        public GTA.Vehicle vehicle;
        public Ped driver;
        public Vector3 destination;
        public int calledTime;
        public int price;
        public bool recovered;
        public object[] additional;
        public bool hasReachedDestination;
        public InsuranceManager.EntityPosition originalPosition = new InsuranceManager.EntityPosition(Vector3.Zero, 0.0f);
        private static readonly List<PedHash> _drivers = new List<PedHash>()
    {
      PedHash.Car3Guy2,
      PedHash.Xmech01SMY,
      PedHash.Autoshop01SMM,
      PedHash.Autoshop02SMM
    };

        public static List<PedHash> Drivers => IncomingVehicle._drivers;

        public IncomingVehicle(
          GTA.Vehicle veh,
          Ped ped,
          Vector3 dest,
          int cost,
          bool isRecovered,
          object[] add = null)
        {
            this.vehicle = veh;
            this.driver = ped;
            this.destination = dest;
            this.calledTime = Game.GameTime;
            this.price = cost;
            this.recovered = isRecovered;
            this.additional = add;
        }

        public IncomingVehicle(
          GTA.Vehicle veh,
          Ped ped,
          Vector3 dest,
          int cost,
          bool isRecovered,
          Vector3 originPos,
          float originHeading,
          object[] add = null)
        {
            this.vehicle = veh;
            this.driver = ped;
            this.destination = dest;
            this.calledTime = Game.GameTime;
            this.price = cost;
            this.recovered = isRecovered;
            this.originalPosition.position = originPos;
            this.originalPosition.heading = originHeading;
            this.additional = add;
        }

        public static IncomingVehicle BringHelicopter(GTA.Vehicle veh, int cost, bool recoveredVehicle)
        {
            float num = 80f;
            Vector3 position = Game.Player.Character.Position;
            do
            {
                position = position.Around((float)InsuranceObserver.BringVehicleRadius);
            }
            while ((double)Game.Player.Character.Position.DistanceTo(position) < (double)(int)((double)InsuranceObserver.BringVehicleRadius * 0.95));
            position.Z += GTA.World.GetGroundHeight(position) + num;
            veh.Position = position;
            veh.Heading = (position - Game.Player.Character.Position).ToHeading();
            veh.PreviouslyOwnedByPlayer = true;
            veh.EngineRunning = true;
            Function.Call(Hash._0xA178472EBB8AE60D, (InputArgument)veh);
            Vector3 offsetInWorldCoords = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0.0f, 1.5f, -1f));
            Ped driver = IncomingVehicle.CreateDriver(veh);
            Function.Call(Hash._0xDAD029E187A2BEB4, (InputArgument)driver, (InputArgument)veh, (InputArgument)0, (InputArgument)Game.Player.Character, (InputArgument)offsetInWorldCoords.X, (InputArgument)offsetInWorldCoords.Y, (InputArgument)offsetInWorldCoords.Z, (InputArgument)20, (InputArgument)30f, (InputArgument)15f, (InputArgument)(offsetInWorldCoords - veh.Position).ToHeading(), (InputArgument) (- 1), (InputArgument) (- 1), (InputArgument) (- 1), (InputArgument)32);
            return new IncomingVehicle(veh, driver, offsetInWorldCoords, cost, recoveredVehicle);
        }

        public static IncomingVehicle BringPlane(GTA.Vehicle veh, int cost, bool recoveredVehicle)
        {
            float num = 80f;
            Vector3 offsetInWorldCoords1 = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0.0f, (float)(-5 * InsuranceObserver.BringVehicleRadius), 0.0f));
            offsetInWorldCoords1.Z = (double)GTA.World.GetGroundHeight(offsetInWorldCoords1) + (double)num >= (double)Game.Player.Character.Position.Z ? GTA.World.GetGroundHeight(offsetInWorldCoords1) + 200f : Game.Player.Character.Position.Z + num;
            veh.Position = offsetInWorldCoords1;
            veh.Heading = Game.Player.Character.Heading;
            veh.PreviouslyOwnedByPlayer = true;
            veh.EngineRunning = true;
            veh.ApplyForceRelative(new Vector3(0.0f, 20f, 0.0f));
            Vector3 offsetInWorldCoords2 = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0.0f, -110f, 0.0f));
            Vector3 offsetInWorldCoords3 = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0.0f, -40f, 0.0f));
            Ped driver = IncomingVehicle.CreateDriver(veh);
            Function.Call(Hash._0xBF19721FA34D32C0, (InputArgument)driver, (InputArgument)veh, (InputArgument)offsetInWorldCoords2.X, (InputArgument)offsetInWorldCoords2.Y, (InputArgument)offsetInWorldCoords2.Z, (InputArgument)offsetInWorldCoords3.X, (InputArgument)offsetInWorldCoords3.Y, (InputArgument)offsetInWorldCoords3.Z);
            return new IncomingVehicle(veh, driver, offsetInWorldCoords3, cost, recoveredVehicle);
        }

        public static void BringBoat(GTA.Vehicle veh, int cost, bool recoveredVehicle)
        {
            Vector3 position = Game.Player.Character.Position;
            OutputArgument outputArgument1 = new OutputArgument();
            OutputArgument outputArgument2 = new OutputArgument();
            Function.Call(Hash._0xFF071FB798B803B0, (InputArgument)position.X, (InputArgument)position.Y, (InputArgument)position.Z, (InputArgument)outputArgument1, (InputArgument)outputArgument2, (InputArgument)3, (InputArgument)3f, (InputArgument)0);
            Vector3 result1 = outputArgument1.GetResult<Vector3>();
            float result2 = outputArgument2.GetResult<float>();
            veh.Position = result1;
            veh.Heading = result2;
            veh.PreviouslyOwnedByPlayer = true;
        }

        public static IncomingVehicle BringVehicle(GTA.Vehicle veh, int cost, bool recoveredVehicle)
        {
            Vector3 position1 = Game.Player.Character.Position;
            do
            {
                position1 = position1.Around((float)InsuranceObserver.BringVehicleRadius);
            }
            while ((double)Game.Player.Character.Position.DistanceTo(position1) < (double)(int)((double)InsuranceObserver.BringVehicleRadius * 0.8));
            InsuranceManager.EntityPosition vehicleSpawnLocation = Tools.GetVehicleSpawnLocation(position1);
            veh.Position = vehicleSpawnLocation.position;
            veh.Heading = vehicleSpawnLocation.heading;
            veh.PreviouslyOwnedByPlayer = true;
            veh.EngineRunning = true;
            Vector3 position2 = Tools.GetVehicleSpawnLocation(Game.Player.Character.Position).position;
            Ped driver = IncomingVehicle.CreateDriver(veh);
            driver.Task.DriveTo(veh, position2, 0.0f, 10f, 2883621);
            return new IncomingVehicle(veh, driver, position2, cost, recoveredVehicle);
        }

        private static Ped CreateDriver(GTA.Vehicle vehicle)
        {
            Vector3 position = vehicle.Position;
            position.X += 5f;
            Ped ped = GTA.World.CreatePed((Model)IncomingVehicle.Drivers[new System.Random().Next(0, IncomingVehicle.Drivers.Count - 1)], position);
            ped.BlockPermanentEvents = true;
            Function.Call(Hash._0x70A2D1137C8ED7C9, (InputArgument)ped, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x9F7794730795E019, (InputArgument)ped, (InputArgument)17, (InputArgument)1);
            ped.IsPersistent = true;
            ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            return ped;
        }
    }
    public class InsuranceManager
    {
        private static InsuranceManager _instance;
        private static float _insuranceMult = 1f;
        private static float _recoverMult = 1f;
        private static float _stolenMult = 1f;
        private static int _bringVehicleBasePrice = 200;
        private static bool _bringVehicleInstant = false;
        private static List<InsuranceManager.EntityPosition> _spawnListVehicle = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(-225.2716f, -1182.783f, 22.49698f), 2.36f),
      new InsuranceManager.EntityPosition(new Vector3(-229.9406f, -1182.361f, 22.49209f), 6.144f),
      new InsuranceManager.EntityPosition(new Vector3(-234.6615f, -1182.197f, 22.48984f), 355.5509f),
      new InsuranceManager.EntityPosition(new Vector3(-244.1168f, -1179.623f, 22.5177f), 308.1156f),
      new InsuranceManager.EntityPosition(new Vector3(-243.4413f, -1173.07f, 22.53329f), 271.4005f),
      new InsuranceManager.EntityPosition(new Vector3(-243.5148f, -1166.511f, 22.56954f), 242.3607f),
      new InsuranceManager.EntityPosition(new Vector3(-237.2427f, -1162.784f, 22.47172f), 183.7536f),
      new InsuranceManager.EntityPosition(new Vector3(-232.8058f, -1162.548f, 22.44885f), 182.2262f),
      new InsuranceManager.EntityPosition(new Vector3(-228.4865f, -1162.615f, 22.45386f), 181.4573f),
      new InsuranceManager.EntityPosition(new Vector3(-150.4142f, -1166.01f, 24.73805f), 177.0276f),
      new InsuranceManager.EntityPosition(new Vector3(-143.6111f, -1163.825f, 24.76486f), 160.3781f),
      new InsuranceManager.EntityPosition(new Vector3(-136.2873f, -1183.153f, 24.7363f), 78.20843f),
      new InsuranceManager.EntityPosition(new Vector3(-136.9411f, -1177.181f, 24.72224f), 102.267f),
      new InsuranceManager.EntityPosition(new Vector3(-246.5937f, -1150.561f, 22.62461f), 269.4836f),
      new InsuranceManager.EntityPosition(new Vector3(-238.7069f, -1150.786f, 22.62887f), 269.3971f),
      new InsuranceManager.EntityPosition(new Vector3(-232.8114f, -1150.434f, 22.54277f), 272.1211f),
      new InsuranceManager.EntityPosition(new Vector3(-211.5235f, -1150.303f, 22.55123f), 268.1985f),
      new InsuranceManager.EntityPosition(new Vector3(-198.5835f, -1150.331f, 22.54078f), 269.7671f)
    };
        private static List<InsuranceManager.EntityPosition> _spawnListVehicleLong = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(-157.9389f, -1162.761f, 24.11157f), 0.6600574f),
      new InsuranceManager.EntityPosition(new Vector3(-236.0531f, -1149.395f, 23.04231f), 269.1866f),
      new InsuranceManager.EntityPosition(new Vector3(-174.2821f, -1149.661f, 23.17635f), 269.3501f),
      new InsuranceManager.EntityPosition(new Vector3(-200.4261f, -1182.882f, 23.1067f), 90.51575f)
    };
        private static List<InsuranceManager.EntityPosition> _spawnListHeli = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(-746.6312f, -1432.797f, 4.71605f), 231.0658f),
      new InsuranceManager.EntityPosition(new Vector3(-763.4095f, -1453.074f, 4.722716f), 234.3286f),
      new InsuranceManager.EntityPosition(new Vector3(-746.3437f, -1469.839f, 4.718675f), 322.4937f),
      new InsuranceManager.EntityPosition(new Vector3(-721.1809f, -1473.602f, 4.717093f), 49.87125f),
      new InsuranceManager.EntityPosition(new Vector3(-700.242f, -1447.846f, 4.71675f), 53.22678f),
      new InsuranceManager.EntityPosition(new Vector3(-723.8517f, -1442.887f, 4.716637f), 139.5879f)
    };
        private static List<InsuranceManager.EntityPosition> _spawnListPlane = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(1638.067f, 3234.868f, 40.11113f), 103.8905f),
      new InsuranceManager.EntityPosition(new Vector3(1558.921f, 3155.603f, 40.23004f), 134.3105f),
      new InsuranceManager.EntityPosition(new Vector3(1430.566f, 3111.669f, 40.23326f), 103.7299f),
      new InsuranceManager.EntityPosition(new Vector3(2071.546f, 4786.328f, 40.79108f), 115.6482f)
    };
        private static List<InsuranceManager.EntityPosition> _spawnListBoat = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(-989.812f, -1395.955f, 0.3117422f), 197.2292f),
      new InsuranceManager.EntityPosition(new Vector3(-998.601f, -1400.204f, -0.01398028f), 200.5666f),
      new InsuranceManager.EntityPosition(new Vector3(-982.6636f, -1392.835f, -0.1012118f), 200.7435f),
      new InsuranceManager.EntityPosition(new Vector3(-973.8835f, -1389.073f, -0.07558359f), 196.9216f),
      new InsuranceManager.EntityPosition(new Vector3(-965.5593f, -1385.88f, 0.1165133f), 197.4063f),
      new InsuranceManager.EntityPosition(new Vector3(-955.8226f, -1383.237f, -0.06844078f), 199.7236f),
      new InsuranceManager.EntityPosition(new Vector3(-930.0321f, -1374.57f, 0.024976f), 196.3813f),
      new InsuranceManager.EntityPosition(new Vector3(-911.7566f, -1368.049f, 0.01486713f), 205.7693f),
      new InsuranceManager.EntityPosition(new Vector3(-845.9328f, -1362.563f, -0.1105901f), 287.3574f),
      new InsuranceManager.EntityPosition(new Vector3(-858.0338f, -1328.371f, -0.05082685f), 290.1963f),
      new InsuranceManager.EntityPosition(new Vector3(-897.3489f, -1336.532f, 0.0469994f), 115.3039f),
      new InsuranceManager.EntityPosition(new Vector3(-915.5237f, -1343.745f, 0.1156863f), 111.007f),
      new InsuranceManager.EntityPosition(new Vector3(-948.0477f, -1355.498f, 0.124268f), 106.5388f),
      new InsuranceManager.EntityPosition(new Vector3(-836.3871f, -1389.433f, 0.03498344f), 290.8258f),
      new InsuranceManager.EntityPosition(new Vector3(-785.4754f, -1440.365f, 0.07404654f), 322.9755f),
      new InsuranceManager.EntityPosition(new Vector3(-772.8375f, -1424.96f, 0.07902782f), 317.0082f),
      new InsuranceManager.EntityPosition(new Vector3(-774.7501f, -1385.55f, 0.09457491f), 50.25464f),
      new InsuranceManager.EntityPosition(new Vector3(-753.9382f, -1362.394f, -0.04268733f), 50.26961f),
      new InsuranceManager.EntityPosition(new Vector3(-724.7479f, -1327.435f, 0.02641343f), 52.78964f),
      new InsuranceManager.EntityPosition(new Vector3(-855.8542f, -1485.853f, 0.0313217f), 108.474f)
    };
        private static List<InsuranceManager.EntityPosition> _spawnListMilitary = new List<InsuranceManager.EntityPosition>()
    {
      new InsuranceManager.EntityPosition(new Vector3(-1594.426f, 3185.479f, 30.40495f), 147.6925f),
      new InsuranceManager.EntityPosition(new Vector3(-1603.479f, 3203.978f, 30.41406f), 171.5964f),
      new InsuranceManager.EntityPosition(new Vector3(-1615.621f, 3169.568f, 29.66991f), 223.9812f),
      new InsuranceManager.EntityPosition(new Vector3(-1580.501f, 3156.202f, 30.64534f), 154.0106f),
      new InsuranceManager.EntityPosition(new Vector3(-1565.606f, 3131.228f, 32.23048f), 142.0033f),
      new InsuranceManager.EntityPosition(new Vector3(-1630.897f, 2980.762f, 32.45866f), 251.8481f),
      new InsuranceManager.EntityPosition(new Vector3(-1565.328f, 3020.8f, 32.43408f), 121.0561f),
      new InsuranceManager.EntityPosition(new Vector3(-1668.656f, 3081.12f, 30.85717f), 231.5131f)
    };
        private static string _dbFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\db.xml";
        private XElement _dbFile;

        public static InsuranceManager GetCurrentInstance() => InsuranceManager._instance;

        public static float InsuranceMult
        {
            get => InsuranceManager._insuranceMult;
            set => InsuranceManager._insuranceMult = value;
        }

        public static float RecoverMult
        {
            get => InsuranceManager._recoverMult;
            set => InsuranceManager._recoverMult = value;
        }

        public static float StolenMult
        {
            get => InsuranceManager._stolenMult;
            set => InsuranceManager._stolenMult = value;
        }

        public static int BringVehicleBasePrice
        {
            get => InsuranceManager._bringVehicleBasePrice;
            set => InsuranceManager._bringVehicleBasePrice = value;
        }

        public static bool BringVehicleInstant
        {
            get => InsuranceManager._bringVehicleInstant;
            set => InsuranceManager._bringVehicleInstant = value;
        }

        public event InsuranceManager.VehicleIsNowInsured Insured;

        protected virtual void Raise_VehicleIsNowInsured(InsuranceManager sender, GTA.Vehicle veh)
        {
            InsuranceManager.VehicleIsNowInsured insured = this.Insured;
            if (insured == null)
                return;
            insured(this, veh);
        }

        public event InsuranceManager.VehicleNoLongerInsured Canceled;

        protected virtual void Raise_VehicleNoLongerInsured(InsuranceManager sender, string vehID)
        {
            InsuranceManager.VehicleNoLongerInsured canceled = this.Canceled;
            if (canceled == null)
                return;
            canceled(this, vehID);
        }

        public event InsuranceManager.VehicleRecovered Recovered;

        protected virtual void Raise_VehicleHasBeenRecovered(
          InsuranceManager sender,
          GTA.Vehicle veh,
          Blip blip)
        {
            InsuranceManager.VehicleRecovered recovered = this.Recovered;
            if (recovered == null)
                return;
            recovered(this, veh, blip);
        }

        public InsuranceManager()
        {
            InsuranceManager._instance = this;
            if (!File.Exists(InsuranceManager._dbFilePath))
                InsuranceManager.CreateDBFile();
            try
            {
                this._dbFile = XElement.Load(InsuranceManager._dbFilePath);
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: InsuranceManager - Cannot load database file. " + ex.Message));
            }
        }

        private static void CreateDBFile()
        {
            FileInfo fileInfo = new FileInfo(InsuranceManager._dbFilePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            XDocument xdocument = new XDocument(new XDeclaration("1.0", Encoding.UTF8.HeaderName, string.Empty), new object[1]
            {
        (object) new XComment("Xml Document")
            });
            xdocument.Add((object)new XElement((XName)"MMI"));
            xdocument.Save(fileInfo.FullName);
        }

        private void SaveDBFile()
        {
            if (!File.Exists(InsuranceManager._dbFilePath))
                InsuranceManager.CreateDBFile();
            this._dbFile.Save(InsuranceManager._dbFilePath);
        }

        private void AddVehicleToDB(GTA.Vehicle veh)
        {
            XElement content;
            if (this._dbFile.Element((XName)"Vehicles") == null)
            {
                content = new XElement((XName)"Vehicles");
                this._dbFile.Add((object)content);
            }
            else
                content = this._dbFile.Element((XName)"Vehicles");
            content.Add((object)this.GenerateVehicleSection(veh));
            this.SaveDBFile();
            this.Insured(this, veh);
        }

        private void RemoveVehicleFromDB(string vehIdentifier)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                if (this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier) != null)
                    this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Remove();
                else
                    Logger.Log((object)("Error: RemoveVehicleFromDB - Cannot find the section " + vehIdentifier));
            }
            else
                Logger.Log((object)"Error: RemoveVehicleFromDB - Cannot find the section Vehicles");
            this.SaveDBFile();
            this.Raise_VehicleNoLongerInsured(this, vehIdentifier);
        }

        internal string ChangeVehicleLicensePlate(string vehIdentifier, string newPlate)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement1 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                if (xelement1 != null)
                {
                    XElement xelement2 = xelement1.Element((XName)"Plate");
                    if (xelement2 != null)
                    {
                        if (xelement2.Element((XName)"NumberPlate") != null)
                        {
                            XElement xelement3 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General");
                            if (xelement3 != null)
                            {
                                string str = xelement3.Element((XName)"Model").Value;
                                if (vehIdentifier.IndexOf(str) > 0)
                                {
                                    string name = vehIdentifier.Remove(vehIdentifier.IndexOf(str) + str.Length) + newPlate.Replace(" ", "_");
                                    XElement content = new XElement((XName)name);
                                    XElement xelement4 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                                    content.Add((object)xelement4.Elements());
                                    content.Element((XName)"Plate").Element((XName)"NumberPlate").SetValue((object)newPlate);
                                    xelement4.Remove();
                                    this._dbFile.Element((XName)"Vehicles").Add((object)content);
                                    this.SaveDBFile();
                                    return name;
                                }
                                Logger.Log((object)("Error: ChangeVehicleLicensePlate - Unable to find the modelHash for the vehicle " + vehIdentifier + "."));
                            }
                            else
                                Logger.Log((object)("Error: ChangeVehicleLicensePlate - General section is missing for the vehicle " + vehIdentifier + "."));
                        }
                        else
                            Logger.Log((object)("Error: ChangeVehicleLicensePlate - NumberPlate section is missing for the vehicle " + vehIdentifier + "."));
                    }
                    else
                        Logger.Log((object)("Error: ChangeVehicleLicensePlate - Plate section is missing for the vehicle " + vehIdentifier + "."));
                }
                else
                    Logger.Log((object)("Error: ChangeVehicleLicensePlate - The vehicle identifier cannot be found: " + vehIdentifier));
            }
            return "";
        }

        internal string GetVehicleLicensePlate(string vehIdentifier)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement1 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                if (xelement1 != null)
                {
                    XElement xelement2 = xelement1.Element((XName)"Plate");
                    if (xelement2 != null && xelement2.Element((XName)"NumberPlate") != null)
                        return xelement2.Element((XName)"NumberPlate").Value;
                }
                else
                    Logger.Log((object)("Error: GetVehicleLicensePlate - The vehicle identifier cannot be found: " + vehIdentifier));
            }
            return "";
        }

        internal string GetVehicleFriendlyName(string vehIdentifier, bool showClassName = true)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                if (xelement != null)
                {
                    try
                    {
                        int num = int.Parse(xelement.Element((XName)"General").Element((XName)"Model").Value);
                        string gxtEntry1 = Game.GetGXTEntry("VEH_CLASS_" + Function.Call<int>(Hash._0xDEDF1C8BD47C2200, (InputArgument)num).ToString());
                        string str = xelement.Element((XName)"Plate").Element((XName)"NumberPlate").Value;
                        string gxtEntry2 = Game.GetGXTEntry(Function.Call<string>(Hash._0xB215AAC32D25D019, (InputArgument)num));
                        string vehicleFriendlyName;
                        if (showClassName)
                            vehicleFriendlyName = gxtEntry2 + " - " + str + " (" + gxtEntry1 + ")";
                        else
                            vehicleFriendlyName = gxtEntry2 + " - " + str;
                        return vehicleFriendlyName;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log((object)("Error: GetVehicleFriendlyName - Cannot convert model hash to int: " + ex.Message));
                    }
                }
                else
                    Logger.Log((object)("Error: GetVehicleFriendlyName - The vehicle identifier cannot be found: " + vehIdentifier));
            }
            return "Unknown";
        }

        internal string GetVehicleModelName(string vehIdentifier)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                if (xelement != null)
                {
                    try
                    {
                        return Game.GetGXTEntry(Function.Call<string>(Hash._0xB215AAC32D25D019, (InputArgument)int.Parse(xelement.Element((XName)"General").Element((XName)"Model").Value)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log((object)("Error: GetVehicleFriendlyName - Cannot convert model hash to int: " + ex.Message));
                    }
                }
                else
                    Logger.Log((object)("Error: GetVehicleFriendlyName - The vehicle identifier cannot be found: " + vehIdentifier));
            }
            return "Unknown";
        }

        internal int GetVehicleInsuranceCost(string vehIdentifier, InsuranceManager.Multiplier mode)
        {
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                if (xelement != null)
                {
                    if (xelement.Element((XName)"General") != null)
                    {
                        int num1 = int.Parse(xelement.Element((XName)"General").Element((XName)"Cost").Value);
                        float num2 = 1f;
                        switch (mode)
                        {
                            case InsuranceManager.Multiplier.Insurance:
                                num2 = InsuranceManager._insuranceMult;
                                break;
                            case InsuranceManager.Multiplier.Recover:
                                num2 = InsuranceManager._recoverMult;
                                break;
                            case InsuranceManager.Multiplier.Stolen:
                                num2 = InsuranceManager._stolenMult;
                                break;
                        }
                        return (int)((double)num1 * (double)num2);
                    }
                    Logger.Log((object)("Error: GetVehicleInsuranceCost - General section is missing for the vehicle " + vehIdentifier + "."));
                }
                else
                    Logger.Log((object)("Error: GetVehicleInsuranceCost - Vehicle " + vehIdentifier + " not found in database."));
            }
            else
                Logger.Log((object)"Error: GetVehicleInsuranceCost - No vehicles found in database file.");
            return 0;
        }

        internal void InsureVehicle(GTA.Vehicle veh) => this.AddVehicleToDB(veh);

        internal void CancelVehicle(string vehIdentifier)
        {
            foreach (GTA.Vehicle allVehicle in GTA.World.GetAllVehicles())
            {
                if (Tools.GetVehicleIdentifier(allVehicle) == vehIdentifier)
                {
                    if (allVehicle.CurrentBlip != (Blip)null)
                        allVehicle.CurrentBlip.Remove();
                    allVehicle.IsPersistent = false;
                }
            }
            this.RemoveVehicleFromDB(vehIdentifier);
        }

        internal void RecoverVehicle(string vehIdentifier)
        {
            GTA.Vehicle vehicleFromDb = this.CreateVehicleFromDB(vehIdentifier);
            if ((Entity)vehicleFromDb != (Entity)null)
            {
                if (vehicleFromDb.Exists())
                {
                    vehicleFromDb.IsPersistent = true;
                    Blip blip = InsuranceManager.AddVehicleBlip(vehicleFromDb);
                    this.SetVehicleStatusToDB(Tools.GetVehicleIdentifier(vehicleFromDb), "Alive");
                    this.Raise_VehicleHasBeenRecovered(this, vehicleFromDb, blip);
                }
                else
                    Logger.Log((object)"Error : RecoverVehicle - The vehicle doesn't exist.");
            }
            else
                Logger.Log((object)"Error: RecoverVehicle - The vehicle value is null.");
        }

        internal string GetVehicleOwner(string vehIdentifier)
        {
            string vehicleOwner = "";
            if (this._dbFile.Element((XName)"Vehicles") != null && this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier) != null && this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General") != null && this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General").Element((XName)"Owner") != null)
                vehicleOwner = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General").Element((XName)"Owner").Value;
            return vehicleOwner;
        }

        internal bool IsVehicleInDB(GTA.Vehicle veh)
        {
            return this.IsVehicleInDB(Tools.GetVehicleIdentifier(veh));
        }

        internal bool IsVehicleInDB(string vehIdentifier)
        {
            bool flag = false;
            if (this._dbFile.Element((XName)"Vehicles") != null && this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier) != null)
                flag = true;
            return flag;
        }

        internal List<string> GetInsuredVehicles(string characterName, bool dead)
        {
            List<string> insuredVehicles = new List<string>();
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                foreach (XElement element in this._dbFile.Element((XName)"Vehicles").Elements())
                {
                    if (element.Element((XName)"General").Element((XName)"Owner").Value == characterName)
                    {
                        if (dead)
                        {
                            if (element.Element((XName)"General").Element((XName)"Status").Value == "Dead")
                                insuredVehicles.Add(element.Name.ToString());
                        }
                        else if (element.Element((XName)"General").Element((XName)"Status").Value == "Alive")
                            insuredVehicles.Add(element.Name.ToString());
                    }
                }
            }
            return insuredVehicles;
        }

        internal void SetVehicleStatusToDB(string vehIdentifier, string status)
        {
            if (this._dbFile.Element((XName)"Vehicles") == null || this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier) == null || this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General") == null)
                return;
            this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier).Element((XName)"General").Element((XName)"Status").SetValue((object)status);
            this.SaveDBFile();
        }

        public static int GetVehicleInsuranceCost(GTA.Vehicle veh, InsuranceManager.Multiplier mode = InsuranceManager.Multiplier.Insurance)
        {
            float num = 1f;
            switch (mode)
            {
                case InsuranceManager.Multiplier.Insurance:
                    num = InsuranceManager._insuranceMult;
                    break;
                case InsuranceManager.Multiplier.Recover:
                    num = InsuranceManager._recoverMult;
                    break;
                case InsuranceManager.Multiplier.Stolen:
                    num = InsuranceManager._stolenMult;
                    break;
            }
            return (int)((double)(0 + Price.GetVehicleModelPrice(veh) + Price.GetVehicleSizePrice(veh) + Price.GetVehicleModsPrice(veh)) * (double)num);
        }

        public static bool IsVehicleInsured(GTA.Vehicle veh)
        {
            return InsuranceManager.IsVehicleInsured(Tools.GetVehicleIdentifier(veh));
        }

        public static bool IsVehicleInsured(string vehIdentifier)
        {
            bool flag = false;
            if (File.Exists(InsuranceManager._dbFilePath))
            {
                XElement xelement = XElement.Load(InsuranceManager._dbFilePath);
                if (xelement.Element((XName)"Vehicles") != null && xelement.Element((XName)"Vehicles").Element((XName)vehIdentifier) != null)
                    flag = true;
            }
            return flag;
        }

        public static bool IsVehicleInsurable(GTA.Vehicle veh)
        {
            return veh.IsAlive && !SE.Vehicle.IsPlayerOfficialVehicle(veh) && !veh.Model.IsTrain;
        }

        public static Blip AddVehicleBlip(GTA.Vehicle veh)
        {
            BlipSprite blipSprite = BlipSprite.PersonalVehicleCar;
            if (veh.Model.IsBike || veh.Model.IsBicycle)
                blipSprite = BlipSprite.PersonalVehicleBike;
            else if (veh.Model.Hash == Game.GenerateHash("RHINO") || veh.Model.Hash == Game.GenerateHash("KHANJALI"))
                blipSprite = BlipSprite.Tank;
            else if (veh.Model.IsHelicopter)
                blipSprite = BlipSprite.Helicopter;
            else if (veh.Model.IsPlane)
                blipSprite = BlipSprite.ArmsTraffickingAir;
            else if (veh.Model.IsBoat)
                blipSprite = BlipSprite.Speedboat;
            else if (veh.ClassType == VehicleClass.Military)
                blipSprite = BlipSprite.GunCar;
            Blip blip = Function.Call<Blip>(Hash._0x5CDE92C702A8FCE7, (InputArgument)veh);
            blip.Sprite = blipSprite;
            blip.Color = BlipColor.White;
            blip.Name = InsuranceManager.GetVehicleFriendlyName(veh);
            blip.IsFlashing = true;
            if (blipSprite == BlipSprite.ArmsTraffickingAir || blipSprite == BlipSprite.Tank || blipSprite == BlipSprite.Speedboat || blipSprite == BlipSprite.GunCar)
                blip.Rotation = (int)veh.Rotation.Z;
            return blip;
        }

        public static string GetVehicleFriendlyName(GTA.Vehicle veh, bool showClassName = true)
        {
            if (!((Entity)veh != (Entity)null) || !veh.Exists())
                return "Unknown";
            Model model = veh.Model;
            string gxtEntry1 = Game.GetGXTEntry("VEH_CLASS_" + Function.Call<int>(Hash._0xDEDF1C8BD47C2200, (InputArgument)model.Hash).ToString());
            model = veh.Model;
            string gxtEntry2 = Game.GetGXTEntry(Function.Call<string>(Hash._0xB215AAC32D25D019, (InputArgument)model.Hash));
            string vehicleFriendlyName;
            if (showClassName)
                vehicleFriendlyName = gxtEntry2 + " - " + veh.NumberPlate + " (" + gxtEntry1 + ")";
            else
                vehicleFriendlyName = gxtEntry2 + " - " + veh.NumberPlate;
            return vehicleFriendlyName;
        }

        public static InsuranceManager.EntityPosition GetVehicleRecoverNode(GTA.Vehicle veh)
        {
            List<InsuranceManager.EntityPosition> entityPositionList = new List<InsuranceManager.EntityPosition>();
            if (veh.Model.IsHelicopter || veh.Model.IsCargobob)
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListHeli);
            else if (veh.Model.IsPlane)
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListPlane);
            else if (veh.Model.IsBoat)
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListBoat);
            else if (veh.ClassType == VehicleClass.Military)
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListMilitary);
            else if ((double)veh.Model.GetDimensions().Y > 7.4000000953674316)
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListVehicleLong);
            else
                entityPositionList.AddRange((IEnumerable<InsuranceManager.EntityPosition>)InsuranceManager._spawnListVehicle);
            System.Random random = new System.Random();
            while (entityPositionList.Count > 0)
            {
                int index = random.Next(0, entityPositionList.Count - 1);
                InsuranceManager.EntityPosition vehicleRecoverNode = entityPositionList[index];
                if (!Function.Call<bool>(Hash._0xE54E209C35FFA18D, (InputArgument)vehicleRecoverNode.position.X, (InputArgument)vehicleRecoverNode.position.Y, (InputArgument)vehicleRecoverNode.position.Z, (InputArgument)5f, (InputArgument)5f, (InputArgument)5f, (InputArgument)0))
                    return vehicleRecoverNode;
                entityPositionList.Remove(vehicleRecoverNode);
            }
            InsuranceManager.EntityPosition vehicleRecoverNode1 = InsuranceManager._spawnListVehicle[random.Next(0, InsuranceManager._spawnListVehicle.Count - 1)];
            Function.Call(Hash._0x01C7B9B38428AEB6, (InputArgument)vehicleRecoverNode1.position.X, (InputArgument)vehicleRecoverNode1.position.Y, (InputArgument)vehicleRecoverNode1.position.Z, (InputArgument)1f, (InputArgument)false, (InputArgument)false, (InputArgument)false, (InputArgument)false, (InputArgument)false);
            GTA.Vehicle closestVehicle = GTA.World.GetClosestVehicle(vehicleRecoverNode1.position, 1f);
            InsuranceObserver.RemoveRecoverBlip(closestVehicle);
            closestVehicle.IsPersistent = false;
            closestVehicle.Delete();
            return vehicleRecoverNode1;
        }

        private XElement GenerateVehicleSection(GTA.Vehicle veh)
        {
            XElement vehicleSection = new XElement((XName)Tools.GetVehicleIdentifier(veh));
            XElement content1 = new XElement((XName)"General");
            content1.Add((object)new XElement((XName)"Owner", (object)SE.Player.GetCurrentCharacterName(true)));
            content1.Add((object)new XElement((XName)"Status", (object)"Alive"));
            content1.Add((object)new XElement((XName)"Model", (object)veh.Model.Hash.ToString()));
            content1.Add((object)new XElement((XName)"Cost", (object)InsuranceManager.GetVehicleInsuranceCost(veh).ToString()));
            vehicleSection.Add((object)content1);
            XElement content2 = new XElement((XName)"Plate");
            content2.Add((object)new XElement((XName)"NumberPlate", (object)veh.NumberPlate));
            content2.Add((object)new XElement((XName)"NumberPlateType", (object)(int)veh.NumberPlateType));
            vehicleSection.Add((object)content2);
            XElement content3 = new XElement((XName)"Wheels");
            content3.Add((object)new XElement((XName)"WheelType", (object)veh.WheelType));
            vehicleSection.Add((object)content3);
            XElement content4 = new XElement((XName)"Mods");
            if (Function.Call<int>(Hash._0x33F2E3FE70EAAE1D, (InputArgument)veh) != 0)
            {
                foreach (VehicleMod modType in Enum.GetValues(typeof(VehicleMod)))
                {
                    XElement content5 = new XElement((XName)"Mod", (object)new XAttribute((XName)"Name", (object)modType));
                    content5.SetValue((object)veh.GetMod(modType));
                    content4.Add((object)content5);
                }
                foreach (VehicleToggleMod toggleMod in Enum.GetValues(typeof(VehicleToggleMod)))
                {
                    XElement content6 = new XElement((XName)"ToggleMod", (object)new XAttribute((XName)"Name", (object)toggleMod));
                    if (veh.IsToggleModOn(toggleMod))
                        content6.SetValue((object)true);
                    else
                        content6.SetValue((object)false);
                    content4.Add((object)content6);
                }
            }
            content4.Add((object)new XElement((XName)"FrontTiresCustom", (object)Function.Call<bool>(Hash._0xB3924ECD70E095DC, (InputArgument)veh, (InputArgument)23)));
            content4.Add((object)new XElement((XName)"RearTiresCustom", (object)Function.Call<bool>(Hash._0xB3924ECD70E095DC, (InputArgument)veh, (InputArgument)24)));
            content4.Add((object)new XElement((XName)"WindowTint", (object)(int)veh.WindowTint));
            vehicleSection.Add((object)content4);
            XElement content7 = new XElement((XName)"Tires");
            try
            {
                content7.Add((object)new XElement((XName)"TireSmokeColor", (object)ColorTranslator.ToHtml(veh.TireSmokeColor)));
            }
            catch (Exception ex)
            {
                content7.Add((object)new XElement((XName)"TireSmokeColor", (object)ColorTranslator.ToHtml(Color.White)));
                Logger.Log((object)("Warning: GenerateVehicleSection - TireSmokeColor is wrong: " + ex.Message));
            }
            content7.Add((object)new XElement((XName)"CanTiresBurst", (object)veh.CanTiresBurst));
            vehicleSection.Add((object)content7);
            XElement content8 = new XElement((XName)"Neons");
            try
            {
                content8.Add((object)new XElement((XName)"NeonLightsColor", (object)ColorTranslator.ToHtml(veh.NeonLightsColor)));
            }
            catch (Exception ex)
            {
                content8.Add((object)new XElement((XName)"NeonLightsColor", (object)ColorTranslator.ToHtml(Color.White)));
                Logger.Log((object)("Warning: GenerateVehicleSection - NeonLightsColor is wrong: " + ex.Message));
            }
            for (int index = 0; index < 4; ++index)
            {
                if (veh.IsNeonLightsOn((VehicleNeonLight)index))
                    content8.Add((object)new XElement((XName)"VehicleNeonLight", (object)index));
            }
            vehicleSection.Add((object)content8);
            XElement content9 = new XElement((XName)"Colors");
            content9.Add((object)new XElement((XName)"IsPrimaryColorCustom", (object)veh.IsPrimaryColorCustom));
            content9.Add((object)new XElement((XName)"IsSecondaryColorCustom", (object)veh.IsSecondaryColorCustom));
            content9.Add((object)new XElement((XName)"PrimaryColor", (object)veh.PrimaryColor));
            content9.Add((object)new XElement((XName)"SecondaryColor", (object)veh.SecondaryColor));
            content9.Add((object)new XElement((XName)"PearlescentColor", (object)veh.PearlescentColor));
            content9.Add((object)new XElement((XName)"RimColor", (object)veh.RimColor));
            content9.Add((object)new XElement((XName)"ColorCombination", (object)veh.ColorCombination));
            content9.Add((object)new XElement((XName)"CustomPrimaryColor", (object)ColorTranslator.ToHtml(veh.CustomPrimaryColor)));
            content9.Add((object)new XElement((XName)"CustomSecondaryColor", (object)ColorTranslator.ToHtml(veh.CustomSecondaryColor)));
            content9.Add((object)new XElement((XName)"DashboardColor", (object)veh.DashboardColor));
            content9.Add((object)new XElement((XName)"TrimColor", (object)veh.TrimColor));
            vehicleSection.Add((object)content9);
            if (veh.IsConvertible)
            {
                XElement content10 = new XElement((XName)"Convertible");
                content10.Add((object)new XElement((XName)"ConvertibleRoofState", (object)veh.RoofState));
                vehicleSection.Add((object)content10);
            }
            XElement content11 = new XElement((XName)"Extra");
            for (int index = 1; index < 15; ++index)
            {
                if (veh.IsExtraOn(index))
                    content11.Add((object)new XElement((XName)"ID", (object)index));
            }
            vehicleSection.Add((object)content11);
            XElement content12 = new XElement((XName)"Livery");
            content12.Add((object)new XElement((XName)"ID", (object)veh.Livery));
            vehicleSection.Add((object)content12);
            if (SE.Vehicle.GetVehicleLivery2(veh) > 0)
            {
                XElement content13 = new XElement((XName)"Livery2");
                content13.Add((object)new XElement((XName)"ID", (object)SE.Vehicle.GetVehicleLivery2(veh)));
                vehicleSection.Add((object)content13);
            }
            return vehicleSection;
        }

        public void UpdateVehicleToDB(GTA.Vehicle veh)
        {
            string vehicleIdentifier = Tools.GetVehicleIdentifier(veh);
            if (this._dbFile.Element((XName)"Vehicles") != null)
            {
                XElement xelement1 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehicleIdentifier);
                if (xelement1 != null)
                {
                    xelement1.Element((XName)"General")?.Element((XName)"Cost").SetValue((object)InsuranceManager.GetVehicleInsuranceCost(veh).ToString());
                    XElement xelement2 = xelement1.Element((XName)"Plate");
                    if (xelement2 != null)
                    {
                        if (xelement2.Element((XName)"NumberPlateType") != null)
                            xelement2.Element((XName)"NumberPlateType").SetValue((object)(int)veh.NumberPlateType);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - NumberPlateType not found.");
                    }
                    XElement xelement3 = xelement1.Element((XName)"Wheels");
                    if (xelement3 != null)
                    {
                        if (xelement3.Element((XName)"WheelType") != null)
                            xelement3.Element((XName)"WheelType").SetValue((object)veh.WheelType);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - WheelType not found.");
                    }
                    XElement xelement4 = xelement1.Element((XName)"Mods");
                    if (xelement4 != null)
                    {
                        xelement4.RemoveAll();
                        foreach (VehicleMod modType in Enum.GetValues(typeof(VehicleMod)))
                        {
                            XElement content = new XElement((XName)"Mod", (object)new XAttribute((XName)"Name", (object)modType));
                            content.SetValue((object)veh.GetMod(modType));
                            xelement4.Add((object)content);
                        }
                        foreach (VehicleToggleMod toggleMod in Enum.GetValues(typeof(VehicleToggleMod)))
                        {
                            XElement content = new XElement((XName)"ToggleMod", (object)new XAttribute((XName)"Name", (object)toggleMod));
                            if (veh.IsToggleModOn(toggleMod))
                                content.SetValue((object)true);
                            else
                                content.SetValue((object)false);
                            xelement4.Add((object)content);
                        }
                        xelement4.Add((object)new XElement((XName)"FrontTiresCustom", (object)Function.Call<bool>(Hash._0xB3924ECD70E095DC, (InputArgument)veh, (InputArgument)23)));
                        xelement4.Add((object)new XElement((XName)"RearTiresCustom", (object)Function.Call<bool>(Hash._0xB3924ECD70E095DC, (InputArgument)veh, (InputArgument)24)));
                        xelement4.Add((object)new XElement((XName)"WindowTint", (object)(int)veh.WindowTint));
                    }
                    XElement xelement5 = xelement1.Element((XName)"Tires");
                    if (xelement5 != null)
                    {
                        if (xelement5.Element((XName)"TireSmokeColor") != null)
                        {
                            try
                            {
                                xelement5.Element((XName)"TireSmokeColor").SetValue((object)ColorTranslator.ToHtml(veh.TireSmokeColor));
                            }
                            catch (Exception ex)
                            {
                                xelement5.Element((XName)"TireSmokeColor").SetValue((object)ColorTranslator.ToHtml(Color.White));
                                Logger.Log((object)("Warning: GenerateVehicleSection - TireSmokeColor is wrong: " + ex.Message));
                            }
                        }
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - TireSmokeColor not found.");
                        if (xelement5.Element((XName)"CanTiresBurst") != null)
                            xelement5.Element((XName)"CanTiresBurst").SetValue((object)veh.CanTiresBurst);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - CanTiresBurst not found.");
                    }
                    XElement xelement6 = xelement1.Element((XName)"Neons");
                    if (xelement6 != null)
                    {
                        xelement6.RemoveAll();
                        try
                        {
                            xelement6.Add((object)new XElement((XName)"NeonLightsColor", (object)ColorTranslator.ToHtml(veh.NeonLightsColor)));
                        }
                        catch (Exception ex)
                        {
                            xelement6.Add((object)new XElement((XName)"NeonLightsColor", (object)ColorTranslator.ToHtml(Color.White)));
                            Logger.Log((object)("Warning: GenerateVehicleSection - NeonLightsColor is wrong: " + ex.Message));
                        }
                        for (int index = 0; index < 4; ++index)
                        {
                            if (veh.IsNeonLightsOn((VehicleNeonLight)index))
                                xelement6.Add((object)new XElement((XName)"VehicleNeonLight", (object)index));
                        }
                    }
                    XElement xelement7 = xelement1.Element((XName)"Colors");
                    if (xelement7 != null)
                    {
                        if (xelement7.Element((XName)"IsPrimaryColorCustom") != null)
                            xelement7.Element((XName)"IsPrimaryColorCustom").SetValue((object)veh.IsPrimaryColorCustom);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - IsPrimaryColorCustom not found.");
                        if (xelement7.Element((XName)"IsSecondaryColorCustom") != null)
                            xelement7.Element((XName)"IsSecondaryColorCustom").SetValue((object)veh.IsSecondaryColorCustom);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - IsSecondaryColorCustom not found.");
                        if (xelement7.Element((XName)"PrimaryColor") != null)
                            xelement7.Element((XName)"PrimaryColor").SetValue((object)veh.PrimaryColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - PrimaryColor not found.");
                        if (xelement7.Element((XName)"SecondaryColor") != null)
                            xelement7.Element((XName)"SecondaryColor").SetValue((object)veh.SecondaryColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - SecondaryColor not found.");
                        if (xelement7.Element((XName)"PearlescentColor") != null)
                            xelement7.Element((XName)"PearlescentColor").SetValue((object)veh.PearlescentColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - PearlescentColor not found.");
                        if (xelement7.Element((XName)"RimColor") != null)
                            xelement7.Element((XName)"RimColor").SetValue((object)veh.RimColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - RimColor not found.");
                        if (xelement7.Element((XName)"ColorCombination") != null)
                            xelement7.Element((XName)"ColorCombination").SetValue((object)veh.ColorCombination);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - ColorCombination not found.");
                        if (xelement7.Element((XName)"CustomPrimaryColor") != null)
                            xelement7.Element((XName)"CustomPrimaryColor").SetValue((object)ColorTranslator.ToHtml(veh.CustomPrimaryColor));
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - CustomPrimaryColor not found.");
                        if (xelement7.Element((XName)"CustomSecondaryColor") != null)
                            xelement7.Element((XName)"CustomSecondaryColor").SetValue((object)ColorTranslator.ToHtml(veh.CustomSecondaryColor));
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - CustomSecondaryColor not found.");
                        if (xelement7.Element((XName)"DashboardColor") != null)
                            xelement7.Element((XName)"DashboardColor").SetValue((object)veh.DashboardColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - DashboardColor not found.");
                        if (xelement7.Element((XName)"TrimColor") != null)
                            xelement7.Element((XName)"TrimColor").SetValue((object)veh.TrimColor);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - TrimColor not found.");
                    }
                    if (veh.IsConvertible)
                    {
                        XElement xelement8 = xelement1.Element((XName)"Convertible");
                        if (xelement8 != null)
                        {
                            if (xelement8.Element((XName)"ConvertibleRoofState") != null)
                                xelement8.Element((XName)"ConvertibleRoofState").SetValue((object)veh.RoofState);
                            else
                                Logger.Log((object)"Error: UpdateVehicleToDB - NeonLightsColor not found.");
                        }
                    }
                    XElement xelement9 = xelement1.Element((XName)"Extra");
                    if (xelement9 != null)
                    {
                        xelement9.RemoveAll();
                        for (int index = 1; index < 15; ++index)
                        {
                            if (veh.IsExtraOn(index))
                                xelement9.Add((object)new XElement((XName)"ID", (object)index));
                        }
                    }
                    XElement xelement10 = xelement1.Element((XName)"Livery");
                    if (xelement10 != null)
                    {
                        if (xelement10.Element((XName)"ID") != null)
                            xelement10.Element((XName)"ID").SetValue((object)veh.Livery);
                        else
                            Logger.Log((object)"Error: UpdateVehicleToDB - Livery ID not found.");
                    }
                    if (SE.Vehicle.GetVehicleLivery2(veh) > 0)
                    {
                        XElement xelement11 = xelement1.Element((XName)"Livery2");
                        if (xelement11 != null)
                        {
                            if (xelement11.Element((XName)"ID") != null)
                                xelement11.Element((XName)"ID").SetValue((object)SE.Vehicle.GetVehicleLivery2(veh));
                            else
                                Logger.Log((object)"Error: UpdateVehicleToDB - Livery2 ID not found.");
                        }
                    }
                    this.SaveDBFile();
                }
                else
                    Logger.Log((object)("Error: UpdateVehicleToDB - Unable to find the current vehicle section in DB: " + vehicleIdentifier));
            }
            else
                Logger.Log((object)"Error: UpdateVehicleToDB - The \"vehicles\" section doesn't exist in the DB file!");
        }

        public GTA.Vehicle CreateVehicleFromDB(string vehIdentifier)
        {
            try
            {
                if (this._dbFile.Element((XName)"Vehicles") != null)
                {
                    if (this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier) != null)
                    {
                        XElement xelement1 = this._dbFile.Element((XName)"Vehicles").Element((XName)vehIdentifier);
                        int hash = int.Parse(xelement1.Element((XName)"General").Element((XName)"Model").Value);
                        Model model = new Model(hash);
                        GTA.Vehicle vehicle = GTA.World.CreateVehicle((Model)hash, new Vector3(0.0f, 0.0f, 0.0f), 0.0f);
                        vehicle.IsPersistent = true;
                        Function.Call(Hash._0x0DC7CABAB1E9B67E, (InputArgument)vehicle, (InputArgument)true);
                        InsuranceManager.EntityPosition vehicleRecoverNode = InsuranceManager.GetVehicleRecoverNode(vehicle);
                        vehicle.Position = vehicleRecoverNode.position;
                        vehicle.Heading = vehicleRecoverNode.heading;
                        if (xelement1.Element((XName)"Plate") != null)
                        {
                            vehicle.NumberPlate = xelement1.Element((XName)"Plate").Element((XName)"NumberPlate").Value;
                            vehicle.NumberPlateType = (NumberPlateType)int.Parse(xelement1.Element((XName)"Plate").Element((XName)"NumberPlateType").Value);
                        }
                        if (xelement1.Element((XName)"Wheels") != null)
                            vehicle.WheelType = (VehicleWheelType)Enum.Parse(typeof(VehicleWheelType), xelement1.Element((XName)"Wheels").Element((XName)"WheelType").Value);
                        if (xelement1.Element((XName)"Mods") != null)
                        {
                            vehicle.InstallModKit();
                            if (xelement1.Element((XName)"Mods").Elements((XName)"Mod") != null)
                            {
                                foreach (XElement element in xelement1.Element((XName)"Mods").Elements((XName)"Mod"))
                                {
                                    bool variations = false;
                                    VehicleMod modType = (VehicleMod)Enum.Parse(typeof(VehicleMod), element.Attribute((XName)"Name").Value);
                                    int modIndex = int.Parse(element.Value);
                                    switch (modType)
                                    {
                                        case VehicleMod.FrontWheels:
                                            variations = bool.Parse(xelement1.Element((XName)"Mods").Element((XName)"FrontTiresCustom").Value);
                                            break;
                                        case VehicleMod.BackWheels:
                                            variations = bool.Parse(xelement1.Element((XName)"Mods").Element((XName)"RearTiresCustom").Value);
                                            break;
                                    }
                                    vehicle.SetMod(modType, modIndex, variations);
                                }
                                vehicle.WindowTint = (VehicleWindowTint)int.Parse(xelement1.Element((XName)"Mods").Element((XName)"WindowTint").Value);
                            }
                            if (xelement1.Element((XName)"Mods").Elements((XName)"ToggleMod") != null)
                            {
                                foreach (XElement element in xelement1.Element((XName)"Mods").Elements((XName)"ToggleMod"))
                                {
                                    VehicleToggleMod toggleMod = (VehicleToggleMod)Enum.Parse(typeof(VehicleToggleMod), element.Attribute((XName)"Name").Value);
                                    bool toggle = bool.Parse(element.Value);
                                    vehicle.ToggleMod(toggleMod, toggle);
                                }
                            }
                        }
                        if (xelement1.Element((XName)"Tires") != null)
                        {
                            vehicle.TireSmokeColor = ColorTranslator.FromHtml(xelement1.Element((XName)"Tires").Element((XName)"TireSmokeColor").Value);
                            vehicle.CanTiresBurst = bool.Parse(xelement1.Element((XName)"Tires").Element((XName)"CanTiresBurst").Value);
                        }
                        if (xelement1.Element((XName)"Neons") != null)
                        {
                            if (xelement1.Element((XName)"Neons").Element((XName)"NeonLightsColor") != null)
                                vehicle.NeonLightsColor = ColorTranslator.FromHtml(xelement1.Element((XName)"Neons").Element((XName)"NeonLightsColor").Value);
                            else
                                Logger.Log((object)"Error: CreateVehicleFromDB - Cannot find element NeonLightsColor");
                            foreach (XElement element in xelement1.Element((XName)"Neons").Elements((XName)"VehicleNeonLight"))
                                vehicle.SetNeonLightsOn((VehicleNeonLight)int.Parse(element.Value), true);
                        }
                        XElement xelement2 = xelement1.Element((XName)"Colors");
                        if (xelement2 != null)
                        {
                            int num = bool.Parse(xelement2.Element((XName)"IsPrimaryColorCustom").Value) ? 1 : 0;
                            bool flag = bool.Parse(xelement2.Element((XName)"IsSecondaryColorCustom").Value);
                            vehicle.ClearCustomPrimaryColor();
                            vehicle.ClearCustomSecondaryColor();
                            if (num != 0)
                                vehicle.CustomPrimaryColor = ColorTranslator.FromHtml(xelement2.Element((XName)"CustomPrimaryColor").Value);
                            if (flag)
                                vehicle.CustomSecondaryColor = ColorTranslator.FromHtml(xelement2.Element((XName)"CustomSecondaryColor").Value);
                            vehicle.PrimaryColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"PrimaryColor").Value);
                            vehicle.SecondaryColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"SecondaryColor").Value);
                            vehicle.PearlescentColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"PearlescentColor").Value);
                            vehicle.RimColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"RimColor").Value);
                            vehicle.DashboardColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"DashboardColor").Value);
                            vehicle.TrimColor = (VehicleColor)Enum.Parse(typeof(VehicleColor), xelement2.Element((XName)"TrimColor").Value);
                        }
                        if (xelement1.Element((XName)"Convertible") != null && vehicle.IsConvertible)
                            vehicle.RoofState = (VehicleRoofState)Enum.Parse(typeof(VehicleRoofState), xelement1.Element((XName)"Convertible").Element((XName)"ConvertibleRoofState").Value);
                        if (xelement1.Element((XName)"Extra") != null)
                        {
                            for (int extra = 1; extra < 15; ++extra)
                                vehicle.ToggleExtra(extra, false);
                            foreach (XElement element in xelement1.Element((XName)"Extra").Elements((XName)"ID"))
                                vehicle.ToggleExtra(int.Parse(element.Value), true);
                        }
                        if (xelement1.Element((XName)"Livery") != null)
                            vehicle.Livery = int.Parse(xelement1.Element((XName)"Livery").Element((XName)"ID").Value);
                        if (xelement1.Element((XName)"Livery2") != null)
                            SE.Vehicle.SetVehicleLivery2(vehicle, int.Parse(xelement1.Element((XName)"Livery2").Element((XName)"ID").Value));
                        return vehicle;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: CreateVehicleFromDB - " + ex.Message));
            }
            return (GTA.Vehicle)null;
        }

        public class EntityPosition
        {
            public Vector3 position;
            public float heading;

            public EntityPosition(Vector3 pos, float h)
            {
                this.position = pos;
                this.heading = h;
            }
        }

        public delegate void VehicleIsNowInsured(InsuranceManager sender, GTA.Vehicle veh);

        public delegate void VehicleNoLongerInsured(InsuranceManager sender, string vehIdentifier);

        public delegate void VehicleRecovered(InsuranceManager sender, GTA.Vehicle veh, Blip blip);

        public enum Multiplier
        {
            Insurance,
            Recover,
            Stolen,
        }

        public enum SpawnNode
        {
            Vehicle,
            Helicopter,
            Plane,
            Boat,
        }
    }
    public class InsuranceObserver : GTA.Script
    {
        private static bool _initialized = false;
        private static InsuranceObserver _instance;
        private static List<GTA.Vehicle> _insuredVehList = new List<GTA.Vehicle>();
        private static List<GTA.Vehicle> _recoveredVehList = new List<GTA.Vehicle>();
        private static bool _persistentVehicles;
        private static Dictionary<string, Blip> _blipsToRemove = new Dictionary<string, Blip>();
        private InsuranceManager _im;
        private List<IncomingVehicle> _incomingVehicles = new List<IncomingVehicle>();
        private GTA.Vehicle _previousVehicle;
        internal static int BringVehicleTimeout = 5;
        internal static int BringVehicleRadius = 100;
        private int _timerInsurance;
        private int _timerDetectInsuredVehicles;
        private int _timerRecoveredVehicle;
        private int _timerIncomingVehicle;
        private int _delayDetectInsuredVehicles = 3000;

        public event InsuranceObserver.InsuredVehicleDetected Detected;

        protected virtual void Raise_InsuredVehicleDetected(InsuranceObserver sender, GTA.Vehicle veh)
        {
            InsuranceObserver.InsuredVehicleDetected detected = this.Detected;
            if (detected == null)
                return;
            detected(this, veh);
        }

        public static bool Initialized
        {
            get => InsuranceObserver._initialized;
            private set => InsuranceObserver._initialized = value;
        }

        public static InsuranceObserver GetCurrentInstance() => InsuranceObserver._instance;

        public static List<GTA.Vehicle> InsuredVehList
        {
            get => InsuranceObserver._insuredVehList;
            set => InsuranceObserver._insuredVehList = value;
        }

        public static List<GTA.Vehicle> RecoveredVehList
        {
            get => InsuranceObserver._recoveredVehList;
            set => InsuranceObserver._recoveredVehList = value;
        }

        public static bool PersistentVehicles
        {
            get => InsuranceObserver._persistentVehicles;
            set => InsuranceObserver._persistentVehicles = value;
        }

        public static Dictionary<string, Blip> BlipsToRemove
        {
            get => InsuranceObserver._blipsToRemove;
            set => InsuranceObserver._blipsToRemove = value;
        }

        internal List<IncomingVehicle> IncomingVehicles
        {
            get => this._incomingVehicles;
            set => this._incomingVehicles = value;
        }

        public int DelayDetectInsuredVehicles
        {
            get => this._delayDetectInsuredVehicles;
            private set => this._delayDetectInsuredVehicles = value;
        }

        public InsuranceObserver()
        {
            InsuranceObserver._instance = this;
            this.Tick += new EventHandler(this.Initialize);
        }

        private void Initialize(object sender, EventArgs e)
        {
            while (!MMI.Initialized)
                GTA.Script.Yield();
            this._im = new InsuranceManager();
            this._im.Insured += new InsuranceManager.VehicleIsNowInsured(this.OnVehicleInsured);
            this._im.Recovered += new InsuranceManager.VehicleRecovered(this.OnVehicleRecovered);
            this._im.Canceled += new InsuranceManager.VehicleNoLongerInsured(this.OnVehicleCanceled);
            InsuranceObserver._initialized = true;
            this.Tick -= new EventHandler(this.Initialize);
            this.Tick += new EventHandler(this.OnTick);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (this._timerInsurance <= Game.GameTime)
            {
                this.UpdateInsurance();
                this._timerInsurance = Game.GameTime + 1000;
            }
            if (this._timerRecoveredVehicle <= Game.GameTime)
            {
                this.UpdateRecoveredVehicles();
                this._timerRecoveredVehicle = Game.GameTime + 3000;
            }
            if (this._timerDetectInsuredVehicles <= Game.GameTime)
            {
                this.CheckForInsuredVehicles();
                this._timerDetectInsuredVehicles = Game.GameTime + this.DelayDetectInsuredVehicles;
            }
            if (this.IncomingVehicles.Count > 0 && this._timerIncomingVehicle <= Game.GameTime)
            {
                this.UpdateIncomingVehicles();
                this._timerIncomingVehicle = Game.GameTime + 1000;
            }
            if (!((Entity)this._previousVehicle != (Entity)Game.Player.Character.CurrentVehicle))
                return;
            this._previousVehicle = Game.Player.Character.CurrentVehicle;
            if (!((Entity)this._previousVehicle != (Entity)null))
                return;
            InsuranceObserver.RemoveRecoverBlip(this._previousVehicle);
            if (!InsuranceManager.IsVehicleInsurable(this._previousVehicle))
                return;
            if (InsuranceManager.IsVehicleInsured(this._previousVehicle))
                SE.UI.DrawTexture(AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\insurance.png", 4500, 0.955f, 0.83f, Color.FromArgb(35, 199, 128));
            else
                SE.UI.DrawTexture(AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\insurance.png", 4500, 0.955f, 0.83f, Color.FromArgb(190, 0, 50));
        }

        protected override void Dispose(bool A_0)
        {
            if (!A_0)
                return;
            this.ClearAllBlips();
            this.RemovePersistence();
        }

        private void ClearAllBlips()
        {
            for (int index = InsuranceObserver.BlipsToRemove.Count - 1; index >= 0; --index)
            {
                Blip blip = InsuranceObserver.BlipsToRemove.ElementAt<KeyValuePair<string, Blip>>(index).Value;
                if (blip != (Blip)null && blip.Exists())
                    blip.Remove();
            }
        }

        internal static void RemoveRecoverBlip(GTA.Vehicle veh)
        {
            Blip blip = (Blip)null;
            InsuranceObserver.BlipsToRemove.TryGetValue(Tools.GetVehicleIdentifier(veh), out blip);
            if (!(blip != (Blip)null))
                return;
            blip.Remove();
            InsuranceObserver.BlipsToRemove.Remove(Tools.GetVehicleIdentifier(veh));
        }

        private void RemovePersistence()
        {
            for (int index = InsuranceObserver.RecoveredVehList.Count - 1; index >= 0; --index)
            {
                if (!InsuranceObserver.PersistentVehicles)
                    InsuranceObserver.RecoveredVehList.ElementAt<GTA.Vehicle>(index).IsPersistent = false;
            }
        }

        private void CheckForInsuredVehicles()
        {
            foreach (GTA.Vehicle allVehicle in GTA.World.GetAllVehicles())
            {
                if (!allVehicle.IsDead && !InsuranceObserver.InsuredVehList.Contains(allVehicle))
                {
                    if (allVehicle.NumberPlate == "46EEK572")
                        allVehicle.NumberPlate = SE.Vehicle.GetRandomNumberPlate();
                    if (this._im.IsVehicleInDB(Tools.GetVehicleIdentifier(allVehicle)))
                    {
                        InsuranceObserver.InsuredVehList.Add(allVehicle);
                        this.Raise_InsuredVehicleDetected(this, allVehicle);
                    }
                }
            }
        }

        private void UpdateInsurance()
        {
            for (int index = InsuranceObserver.InsuredVehList.Count - 1; index >= 0; --index)
            {
                GTA.Vehicle veh = InsuranceObserver.InsuredVehList.ElementAt<GTA.Vehicle>(index);
                if (veh.Exists())
                {
                    if (veh.IsDead)
                    {
                        string vehicleIdentifier = Tools.GetVehicleIdentifier(veh);
                        SE.UI.DrawNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("NotifyVehicleDestroyedTitle"), Translator.GetString("NotifyVehicleDestroyedSubtitle"));
                        this._im.SetVehicleStatusToDB(vehicleIdentifier, "Dead");
                        this._im.UpdateVehicleToDB(veh);
                        veh.IsPersistent = false;
                        InsuranceObserver.InsuredVehList.RemoveAt(index);
                        InsuranceObserver.RemoveRecoverBlip(veh);
                        break;
                    }
                    if ((Entity)Game.Player.Character.CurrentVehicle == (Entity)veh && GameplayCamera.IsRendering)
                        this._im.UpdateVehicleToDB(veh);
                    if (InsuranceObserver.PersistentVehicles)
                        veh.IsPersistent = true;
                }
                else
                    InsuranceObserver.InsuredVehList.RemoveAt(index);
            }
        }

        private void UpdateRecoveredVehicles()
        {
            for (int index = InsuranceObserver.RecoveredVehList.Count - 1; index >= 0; --index)
            {
                GTA.Vehicle vehicle = InsuranceObserver.RecoveredVehList.ElementAt<GTA.Vehicle>(index);
                if ((Entity)Game.Player.LastVehicle == (Entity)vehicle || !vehicle.Exists() || vehicle.IsDead)
                {
                    if (vehicle.Exists())
                    {
                        if (vehicle.IsAlive)
                            SE.UI.DrawNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("NotifyVehicleRecoveredTitle"), Translator.GetString("NotifyVehicleRecoveredSubtitle"));
                        if (!InsuranceObserver.PersistentVehicles)
                            vehicle.IsPersistent = false;
                    }
                    InsuranceObserver.RecoveredVehList.RemoveAt(index);
                }
            }
        }

        private void UpdateIncomingVehicles()
        {
            for (int index1 = this.IncomingVehicles.Count - 1; index1 >= 0; --index1)
            {
                IncomingVehicle incomingVehicle = this.IncomingVehicles[index1];
                if (incomingVehicle.vehicle.CurrentBlip.Sprite == BlipSprite.ArmsTraffickingAir || incomingVehicle.vehicle.CurrentBlip.Sprite == BlipSprite.Tank || incomingVehicle.vehicle.CurrentBlip.Sprite == BlipSprite.Speedboat || incomingVehicle.vehicle.CurrentBlip.Sprite == BlipSprite.GunCar)
                    incomingVehicle.vehicle.CurrentBlip.Rotation = (int)incomingVehicle.vehicle.Rotation.Z;
                if (incomingVehicle.vehicle.IsDead)
                {
                    this.CannotBringVehicle(incomingVehicle, InsuranceManager.GetVehicleInsuranceCost(incomingVehicle.vehicle, InsuranceManager.Multiplier.Recover));
                    break;
                }
                if (incomingVehicle.vehicle.Model.IsHelicopter)
                {
                    if (incomingVehicle.driver.IsInVehicle(incomingVehicle.vehicle) && (double)incomingVehicle.vehicle.Speed <= 0.5 && (double)incomingVehicle.vehicle.Position.Z - (double)GTA.World.GetGroundHeight(incomingVehicle.vehicle.Position) <= 5.0)
                    {
                        incomingVehicle.driver.Task.LeaveVehicle();
                        Function.Call(Hash._0xBB8DE8CF6A8DD8BB, (InputArgument)incomingVehicle.driver);
                        break;
                    }
                }
                else if (incomingVehicle.vehicle.Model.IsPlane)
                {
                    if (incomingVehicle.driver.IsInVehicle(incomingVehicle.vehicle) && (double)incomingVehicle.vehicle.Speed <= 5.0 && (double)incomingVehicle.vehicle.Position.Z - (double)GTA.World.GetGroundHeight(incomingVehicle.vehicle.Position) <= 10.0)
                    {
                        incomingVehicle.driver.Task.LeaveVehicle();
                        Function.Call(Hash._0xBB8DE8CF6A8DD8BB, (InputArgument)incomingVehicle.driver);
                        break;
                    }
                }
                else if (incomingVehicle.driver.IsInVehicle(incomingVehicle.vehicle) && ((double)incomingVehicle.driver.Position.DistanceTo(incomingVehicle.destination) <= 5.0 && (double)incomingVehicle.driver.Position.Z - (double)incomingVehicle.destination.Z <= 2.0 || (double)incomingVehicle.driver.Position.DistanceTo(Game.Player.Character.Position) <= 5.0 && (double)incomingVehicle.driver.Position.Z - (double)Game.Player.Character.Position.Z <= 2.0))
                {
                    incomingVehicle.driver.Task.LeaveVehicle();
                    Function.Call(Hash._0xBB8DE8CF6A8DD8BB, (InputArgument)incomingVehicle.driver);
                    break;
                }
                if (!incomingVehicle.driver.IsInVehicle(incomingVehicle.vehicle))
                {
                    incomingVehicle.driver.IsPersistent = false;
                    incomingVehicle.driver.MarkAsNoLongerNeeded();
                    incomingVehicle.driver.Task.WanderAround();
                    this.IncomingVehicles.Remove(incomingVehicle);
                    if ((double)incomingVehicle.driver.Position.DistanceTo(Game.Player.Character.Position) >= 8.0)
                        break;
                    System.Random random = new System.Random();
                    List<DialogueManager.Speech> speechList = new List<DialogueManager.Speech>((IEnumerable<DialogueManager.Speech>)DialogueManager.GetSpeechList(DialogueManager.SpeechType.DriverBye));
                    int maxValue = speechList.Count - 1;
                    int index2 = random.Next(0, maxValue);
                    DialogueManager.Speech speech = speechList[index2];
                    Function.Call(Hash._0x3523634255FC3318, (InputArgument)incomingVehicle.driver, (InputArgument)speech.Name, (InputArgument)speech.Voice, (InputArgument)speech.Param, (InputArgument)speech.Index);
                    break;
                }
                if (Game.GameTime - incomingVehicle.calledTime > InsuranceObserver.BringVehicleTimeout * 60000)
                {
                    this.CannotBringVehicle(incomingVehicle);
                    break;
                }
            }
        }

        internal void BringVehicleToPlayer(GTA.Vehicle veh, int cost, bool instant = false)
        {
            if (veh.Exists())
            {
                bool recoveredVehicle = InsuranceObserver.RecoveredVehList.Contains(veh);
                if (this.IncomingVehicles.Count > 0)
                {
                    foreach (IncomingVehicle incomingVehicle in this.IncomingVehicles)
                    {
                        if ((Entity)incomingVehicle.vehicle == (Entity)veh)
                        {
                            incomingVehicle.driver.Task.ClearAllImmediately();
                            incomingVehicle.driver.IsPersistent = false;
                            incomingVehicle.driver.Delete();
                            if (!incomingVehicle.recovered)
                                InsuranceObserver.RemoveRecoverBlip(incomingVehicle.vehicle);
                            else
                                incomingVehicle.vehicle.Repair();
                        }
                    }
                }
                if (instant || veh.Model.Hash == Game.GenerateHash("HYDRA"))
                {
                    if (veh.Model.IsBoat)
                    {
                        IncomingVehicle.BringBoat(veh, cost, recoveredVehicle);
                    }
                    else
                    {
                        InsuranceManager.EntityPosition vehicleSpawnLocation = Tools.GetVehicleSpawnLocation(Game.Player.Character.Position);
                        veh.Position = vehicleSpawnLocation.position;
                        veh.Heading = vehicleSpawnLocation.heading;
                    }
                    if (recoveredVehicle)
                        return;
                    string vehicleIdentifier = Tools.GetVehicleIdentifier(veh);
                    if (InsuranceObserver.BlipsToRemove.ContainsKey(vehicleIdentifier))
                    {
                        Blip blip = InsuranceObserver.BlipsToRemove[vehicleIdentifier];
                        if (blip != (Blip)null && blip.Exists())
                            blip.Remove();
                        InsuranceObserver.BlipsToRemove[vehicleIdentifier] = InsuranceManager.AddVehicleBlip(veh);
                    }
                    else
                        InsuranceObserver.BlipsToRemove.Add(vehicleIdentifier, InsuranceManager.AddVehicleBlip(veh));
                }
                else
                {
                    Model model = veh.Model;
                    if (!model.IsCargobob)
                    {
                        model = veh.Model;
                        if (!model.IsHelicopter)
                        {
                            model = veh.Model;
                            if (model.IsPlane)
                            {
                                this.IncomingVehicles.Add(IncomingVehicle.BringPlane(veh, cost, recoveredVehicle));
                                goto label_29;
                            }
                            else
                            {
                                model = veh.Model;
                                if (model.IsBoat)
                                {
                                    IncomingVehicle.BringBoat(veh, cost, recoveredVehicle);
                                    goto label_29;
                                }
                                else
                                {
                                    this.IncomingVehicles.Add(IncomingVehicle.BringVehicle(veh, cost, recoveredVehicle));
                                    goto label_29;
                                }
                            }
                        }
                    }
                    this.IncomingVehicles.Add(IncomingVehicle.BringHelicopter(veh, cost, recoveredVehicle));
                label_29:
                    if (recoveredVehicle)
                        return;
                    string vehicleIdentifier = Tools.GetVehicleIdentifier(veh);
                    if (InsuranceObserver.BlipsToRemove.ContainsKey(vehicleIdentifier))
                    {
                        Blip blip = InsuranceObserver.BlipsToRemove[vehicleIdentifier];
                        if (blip != (Blip)null && blip.Exists())
                            blip.Remove();
                        InsuranceObserver.BlipsToRemove[vehicleIdentifier] = InsuranceManager.AddVehicleBlip(veh);
                    }
                    else
                        InsuranceObserver.BlipsToRemove.Add(vehicleIdentifier, InsuranceManager.AddVehicleBlip(veh));
                }
            }
            else
                Logger.Log((object)"Error: BringVehicleToPlayer - The vehicle doesn't exist!");
        }

        internal void CannotBringVehicle(IncomingVehicle incoming, int refund = 0)
        {
            SE.UI.DrawNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("BringVehicle"), Translator.GetString("NotifyBringVehicleCancel"));
            if (refund == 0)
                SE.Player.AddCashToPlayer(incoming.price);
            else
                SE.Player.AddCashToPlayer(refund + incoming.price);
            incoming.driver.Delete();
            if (incoming.originalPosition.position != Vector3.Zero)
            {
                InsuranceObserver.RemoveRecoverBlip(incoming.vehicle);
                if (!incoming.vehicle.IsDead)
                {
                    incoming.vehicle.Position = incoming.originalPosition.position;
                    incoming.vehicle.Heading = incoming.originalPosition.heading;
                    incoming.vehicle.EngineRunning = false;
                    incoming.vehicle.Repair();
                }
                this.IncomingVehicles.Remove(incoming);
            }
            else
            {
                if (!incoming.vehicle.IsDead)
                {
                    InsuranceManager.EntityPosition vehicleRecoverNode = InsuranceManager.GetVehicleRecoverNode(incoming.vehicle);
                    incoming.vehicle.Position = vehicleRecoverNode.position;
                    incoming.vehicle.Heading = vehicleRecoverNode.heading;
                    incoming.vehicle.EngineRunning = false;
                    incoming.vehicle.Repair();
                }
                else
                    InsuranceObserver.RemoveRecoverBlip(incoming.vehicle);
                this.IncomingVehicles.Remove(incoming);
            }
        }

        internal static List<GTA.Vehicle> GetBringableVehicles()
        {
            List<GTA.Vehicle> bringableVehicles = new List<GTA.Vehicle>();
            bringableVehicles.AddRange((IEnumerable<GTA.Vehicle>)InsuranceObserver.RecoveredVehList);
            foreach (GTA.Vehicle insuredVeh in InsuranceObserver.InsuredVehList)
            {
                if (!bringableVehicles.Contains(insuredVeh))
                    bringableVehicles.Add(insuredVeh);
            }
            if ((Entity)Game.Player.Character.CurrentVehicle != (Entity)null && bringableVehicles.Contains(Game.Player.Character.CurrentVehicle))
                bringableVehicles.Remove(Game.Player.Character.CurrentVehicle);
            return bringableVehicles;
        }

        private void OnVehicleInsured(InsuranceManager sender, GTA.Vehicle veh)
        {
            if (InsuranceObserver.InsuredVehList.Contains(veh))
                return;
            InsuranceObserver.InsuredVehList.Add(veh);
            if (!InsuranceObserver.PersistentVehicles)
                return;
            veh.IsPersistent = true;
        }

        private void OnVehicleCanceled(InsuranceManager sender, string vehID)
        {
            foreach (GTA.Vehicle insuredVeh in InsuranceObserver.InsuredVehList)
            {
                if (Tools.GetVehicleIdentifier(insuredVeh) == vehID)
                {
                    InsuranceObserver.InsuredVehList.Remove(insuredVeh);
                    if (!InsuranceObserver.PersistentVehicles)
                        break;
                    insuredVeh.IsPersistent = false;
                    break;
                }
            }
        }

        private void OnVehicleRecovered(InsuranceManager sender, GTA.Vehicle veh, Blip blip)
        {
            if (!InsuranceObserver.RecoveredVehList.Contains(veh))
                InsuranceObserver.RecoveredVehList.Add(veh);
            if (!InsuranceObserver.BlipsToRemove.ContainsValue(blip) && !InsuranceObserver.BlipsToRemove.ContainsKey(Tools.GetVehicleIdentifier(veh)))
                InsuranceObserver.BlipsToRemove.Add(Tools.GetVehicleIdentifier(veh), blip);
            if (!InsuranceObserver.PersistentVehicles)
                return;
            veh.IsPersistent = true;
        }

        public delegate void InsuredVehicleDetected(InsuranceObserver sender, GTA.Vehicle veh);
    }

    internal class MenuMMI
    {
        private MenuPool _menuPool;
        private UIMenu _mainMenu = new UIMenu("", "Menu");
        private string _banner = "scripts\\MMI\\banner.png";
        private bool _openedFromiFruit;
        private InsuranceManager _insurance = InsuranceManager.GetCurrentInstance();
        private InsuranceObserver _observer = InsuranceObserver.GetCurrentInstance();
        private UIMenuItem _itemInsure;
        private UIMenu _submenuRecover;
        private UIMenu _submenuStolen;
        private UIMenu _submenuCancel;
        private UIMenu _submenuPlate;
        private UIMenu _submenuBring;

        internal void MenuPoolProcessMenus() => this._menuPool.ProcessMenus();

        internal UIMenu GetMainmenu() => this._mainMenu;

        public bool OpenedFromiFruit
        {
            get => this._openedFromiFruit;
            private set => this._openedFromiFruit = value;
        }

        public MenuMMI()
        {
            this._menuPool = new MenuPool();
            this._menuPool.Add(this._mainMenu);
        }

        internal void Show()
        {
            this._mainMenu.Visible = true;
            Function.Call(Hash._0xFC695459D4D0E219, (InputArgument)0.5f, (InputArgument)0.5f);
        }

        internal void Create()
        {
            if (File.Exists(this._banner))
                this._mainMenu.SetBannerType(this._banner);
            if (this.OpenedFromiFruit)
            {
                if (iFruitMMI.CaniFruitInsure)
                    this.BuildItemInsure();
                if (iFruitMMI.CaniFruitCancel)
                    this.CreateMenuCancel(this._mainMenu);
                if (iFruitMMI.CaniFruitRecover)
                    this.CreateMenuRecover(this._mainMenu);
                if (iFruitMMI.CaniFruitStolen)
                    this.CreateMenuStolen(this._mainMenu);
                if (iFruitMMI.CaniFruitPlate)
                    this.CreateMenuPlate(this._mainMenu);
                this.CreateMenuBring(this._mainMenu);
            }
            else
            {
                this.BuildItemInsure();
                this.CreateMenuCancel(this._mainMenu);
                this.CreateMenuRecover(this._mainMenu);
                this.CreateMenuStolen(this._mainMenu);
                this.CreateMenuPlate(this._mainMenu);
            }
            this._menuPool.RefreshIndex();
        }

        internal void Reset(bool iFruit = false)
        {
            this.OpenedFromiFruit = iFruit;
            if (this._mainMenu != null)
                this._mainMenu.MenuItems.Clear();
            this.Create();
        }

        private void RefreshMenuIndex(UIMenu menu, string itemDescription)
        {
            if (menu == null)
                return;
            if (menu.MenuItems.Count <= 0)
            {
                menu.AddItem(new UIMenuItem(Translator.GetString("Empty"), itemDescription)
                {
                    Enabled = false
                });
                menu.CurrentSelection = 0;
            }
            else if (menu.CurrentSelection > menu.MenuItems.Count - 1)
                menu.CurrentSelection = 0;
            menu.UpdateScaleform();
        }

        private void BuildItemInsure()
        {
            GTA.Vehicle lastVehicle = Game.Player.LastVehicle;
            if (!lastVehicle.Exists())
                return;
            int cost = InsuranceManager.GetVehicleInsuranceCost(lastVehicle);
            if (!InsuranceManager.IsVehicleInsured(Tools.GetVehicleIdentifier(lastVehicle)))
            {
                if (InsuranceManager.IsVehicleInsurable(lastVehicle))
                {
                    this._itemInsure = new UIMenuItem(Translator.GetString("InsureVehicle"), Translator.GetString("InsureVehicleDesc") + "\n" + SE.Vehicle.GetVehicleFriendlyName(lastVehicle, false) + ".");
                    this._itemInsure.SetRightLabel(cost.ToString() + "$");
                    this._mainMenu.AddItem(this._itemInsure);
                }
                else
                {
                    this._itemInsure = new UIMenuItem(Translator.GetString("InsureVehicle"), Translator.GetString("VehicleWrongType") + " " + SE.Vehicle.GetVehicleFriendlyName(lastVehicle) + ".");
                    this._itemInsure.Enabled = false;
                    this._mainMenu.AddItem(this._itemInsure);
                }
            }
            else
            {
                this._itemInsure = new UIMenuItem(Translator.GetString("InsureVehicle"), Translator.GetString("VehicleAlreadyInsured") + "\n" + SE.Vehicle.GetVehicleFriendlyName(lastVehicle, false) + ".");
                this._itemInsure.Enabled = false;
                this._mainMenu.AddItem(this._itemInsure);
            }
            this._mainMenu.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
            {
                if (item != this._itemInsure || !((Entity)Game.Player.LastVehicle != (Entity)null) || !Game.Player.LastVehicle.Exists() || InsuranceManager.IsVehicleInsured(Tools.GetVehicleIdentifier(Game.Player.LastVehicle)) || !InsuranceManager.IsVehicleInsurable(Game.Player.LastVehicle))
                    return;
                if (SE.Player.AddCashToPlayer(-1 * cost))
                {
                    this.InsureVehicle(Game.Player.LastVehicle);
                }
                else
                {
                    if (this.OpenedFromiFruit)
                        MMISound.Play(MMISound.SoundFamily.NoMoney);
                    GTA.UI.Notify(Translator.GetString("NotifyNoMoney"));
                }
            });
        }

        private void RefreshItemInsure()
        {
            if (this._itemInsure == null)
                return;
            GTA.Vehicle lastVehicle = Game.Player.LastVehicle;
            if ((Entity)lastVehicle != (Entity)null)
            {
                if (lastVehicle.Exists())
                {
                    if (!InsuranceManager.IsVehicleInsured(Tools.GetVehicleIdentifier(lastVehicle)))
                    {
                        if (!InsuranceManager.IsVehicleInsurable(lastVehicle))
                            return;
                        int vehicleInsuranceCost = InsuranceManager.GetVehicleInsuranceCost(lastVehicle);
                        this._itemInsure.Text = Translator.GetString("InsureVehicle");
                        this._itemInsure.SetRightLabel(vehicleInsuranceCost.ToString() + "$");
                        this._itemInsure.Description = Translator.GetString("InsureVehicleDesc") + "\n" + SE.Vehicle.GetVehicleFriendlyName(lastVehicle, false) + ".";
                        this._itemInsure.Enabled = true;
                    }
                    else
                    {
                        this._itemInsure.Text = Translator.GetString("InsureVehicle");
                        this._itemInsure.Description = Translator.GetString("VehicleAlreadyInsured") + "\n" + SE.Vehicle.GetVehicleFriendlyName(lastVehicle, false) + ".";
                        this._itemInsure.Enabled = false;
                    }
                }
                else
                    this._mainMenu.RemoveItemAt(0);
            }
            else
                this._mainMenu.RemoveItemAt(0);
        }

        private void InsureVehicle(GTA.Vehicle veh)
        {
            if (this.OpenedFromiFruit)
                MMISound.Play(MMISound.SoundFamily.Okay);
            this._insurance.InsureVehicle(veh);
            GTA.UI.Notify(Translator.GetString("NotifyVehicleIsInsured"));
            this._itemInsure.Enabled = false;
            this.RefreshMenuIndex(this._submenuCancel, Translator.GetString("CancelInsuranceItemEmptyDesc"));
            this.RebuildMenuCancel();
            this.RefreshMenuIndex(this._submenuStolen, Translator.GetString("StolenVehicleItemEmptyDesc"));
            this.RebuildMenuStolen();
            if (this.OpenedFromiFruit)
            {
                this.RefreshMenuIndex(this._submenuBring, Translator.GetString("BringVehicleItemEmptyDesc"));
                this.RebuildMenuBring();
            }
            this.RefreshMenuIndex(this._submenuPlate, Translator.GetString("PlateChangeItemEmptyDesc"));
            this.RebuildMenuPlate();
        }

        private void CreateMenuCancel(UIMenu menu)
        {
            this._submenuCancel = this._menuPool.AddSubMenu(menu, Translator.GetString("CancelInsurance"), Translator.GetString("CancelInsuranceDesc"));
            if (File.Exists(this._banner))
                this._submenuCancel.SetBannerType(this._banner);
            this.RebuildMenuCancel();
        }

        private void RebuildMenuCancel()
        {
            this._submenuCancel.Clear();
            List<string> insuredVehicles = this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), false);
            insuredVehicles.AddRange((IEnumerable<string>)this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), true));
            if (insuredVehicles.Count > 0)
            {
                foreach (string str in insuredVehicles)
                {
                    string vehID = str;
                    UIMenuItem cancelContract = new UIMenuItem(this._insurance.GetVehicleModelName(vehID), Translator.GetString("CancelInsuranceItemDesc"));
                    cancelContract.SetRightLabel(this._insurance.GetVehicleLicensePlate(vehID));
                    this._submenuCancel.AddItem(cancelContract);
                    this._submenuCancel.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
                    {
                        if (item != cancelContract)
                            return;
                        if (this.OpenedFromiFruit)
                            MMISound.Play(MMISound.SoundFamily.Okay);
                        this._insurance.CancelVehicle(vehID);
                        GTA.UI.Notify(Translator.GetString("NotifyCanceled"));
                        cancelContract.Enabled = false;
                        this._submenuCancel.RemoveItemAt(index);
                        this.RefreshMenuIndex(this._submenuCancel, Translator.GetString("CancelInsuranceItemEmptyDesc"));
                        this.RefreshItemInsure();
                        this.RefreshMenuIndex(this._submenuRecover, Translator.GetString("RecoverVehicleItemEmptyDesc"));
                        this.RebuildMenuRecover();
                        this.RefreshMenuIndex(this._submenuStolen, Translator.GetString("StolenVehicleItemEmptyDesc"));
                        this.RebuildMenuStolen();
                        this.RefreshMenuIndex(this._submenuPlate, Translator.GetString("PlateChangeItemEmptyDesc"));
                        this.RebuildMenuPlate();
                        if (!this.OpenedFromiFruit)
                            return;
                        this.RefreshMenuIndex(this._submenuBring, Translator.GetString("BringVehicleItemEmptyDesc"));
                        this.RebuildMenuBring();
                    });
                }
            }
            else
                this._submenuCancel.AddItem(new UIMenuItem(Translator.GetString("Empty"), Translator.GetString("CancelInsuranceItemEmptyDesc"))
                {
                    Enabled = false
                });
        }

        private void CreateMenuRecover(UIMenu menu)
        {
            this._submenuRecover = this._menuPool.AddSubMenu(menu, Translator.GetString("RecoverVehicle"), Translator.GetString("RecoverVehicleDesc"));
            if (File.Exists(this._banner))
                this._submenuRecover.SetBannerType(this._banner);
            this.RebuildMenuRecover();
        }

        private void RebuildMenuRecover()
        {
            this._submenuRecover.Clear();
            List<string> insuredVehicles = this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), true);
            if (insuredVehicles.Count > 0)
            {
                foreach (string str in insuredVehicles)
                {
                    string vehID = str;
                    int cost = this._insurance.GetVehicleInsuranceCost(vehID, InsuranceManager.Multiplier.Recover);
                    UIMenuItem recoverVehicle = new UIMenuItem(this._insurance.GetVehicleFriendlyName(vehID, false), Translator.GetString("NotifyDeliverVehicle"));
                    recoverVehicle.SetRightLabel(cost.ToString() + "$");
                    this._submenuRecover.AddItem(recoverVehicle);
                    this._submenuRecover.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
                    {
                        if (item != recoverVehicle)
                            return;
                        if (SE.Player.AddCashToPlayer(-1 * cost))
                        {
                            if (this.OpenedFromiFruit)
                                MMISound.Play(MMISound.SoundFamily.Okay);
                            this._insurance.RecoverVehicle(vehID);
                            GTA.UI.Notify(Translator.GetString("NotifyDeliverVehicle"));
                            recoverVehicle.Enabled = false;
                            this._submenuRecover.RemoveItemAt(index);
                            this.RefreshMenuIndex(this._submenuRecover, Translator.GetString("RecoverVehicleItemEmptyDesc"));
                            this.RebuildMenuStolen();
                            if (!this.OpenedFromiFruit)
                                return;
                            this.RebuildMenuBring();
                        }
                        else
                        {
                            if (this.OpenedFromiFruit)
                                MMISound.Play(MMISound.SoundFamily.NoMoney);
                            GTA.UI.Notify(Translator.GetString("NotifyNoMoney"));
                        }
                    });
                }
            }
            else
                this._submenuRecover.AddItem(new UIMenuItem(Translator.GetString("Empty"), Translator.GetString("RecoverVehicleItemEmptyDesc"))
                {
                    Enabled = false
                });
        }

        private void CreateMenuStolen(UIMenu menu)
        {
            this._submenuStolen = this._menuPool.AddSubMenu(menu, Translator.GetString("StolenVehicle"), Translator.GetString("StolenVehicleDesc"));
            if (File.Exists(this._banner))
                this._submenuStolen.SetBannerType(this._banner);
            this.RebuildMenuStolen();
        }

        private void RebuildMenuStolen()
        {
            this._submenuStolen.Clear();
            List<string> insuredVehicles = this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), false);
            if (insuredVehicles.Count > 0)
            {
                foreach (string str in insuredVehicles)
                {
                    string vehID = str;
                    int cost = this._insurance.GetVehicleInsuranceCost(vehID, InsuranceManager.Multiplier.Stolen);
                    UIMenuItem stolenVehicle = new UIMenuItem(this._insurance.GetVehicleFriendlyName(vehID, false), Translator.GetString("NotifyDeliverVehicle"));
                    stolenVehicle.SetRightLabel(cost.ToString() + "$");
                    this._submenuStolen.AddItem(stolenVehicle);
                    this._submenuStolen.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
                    {
                        if (item != stolenVehicle)
                            return;
                        if (SE.Player.AddCashToPlayer(-1 * cost))
                        {
                            if (this.OpenedFromiFruit)
                                MMISound.Play(MMISound.SoundFamily.Okay);
                            foreach (GTA.Vehicle allVehicle in GTA.World.GetAllVehicles())
                            {
                                if (Tools.GetVehicleIdentifier(allVehicle) == vehID)
                                {
                                    if (allVehicle.CurrentBlip != (Blip)null)
                                        allVehicle.CurrentBlip.Remove();
                                    allVehicle.Delete();
                                }
                            }
                            this._insurance.RecoverVehicle(vehID);
                            GTA.UI.Notify(Translator.GetString("NotifyDeliverVehicle"));
                            stolenVehicle.Enabled = false;
                            this._submenuStolen.RemoveItemAt(index);
                            this.RefreshMenuIndex(this._submenuStolen, Translator.GetString("StolenVehicleItemEmptyDesc"));
                            if (!this.OpenedFromiFruit)
                                return;
                            this.RefreshMenuIndex(this._submenuBring, Translator.GetString("BringVehicleItemEmptyDesc"));
                            this.RebuildMenuBring();
                        }
                        else
                        {
                            if (this.OpenedFromiFruit)
                                MMISound.Play(MMISound.SoundFamily.NoMoney);
                            GTA.UI.Notify(Translator.GetString("NotifyNoMoney"));
                        }
                    });
                }
            }
            else
                this._submenuStolen.AddItem(new UIMenuItem(Translator.GetString("Empty"), Translator.GetString("StolenVehicleItemEmptyDesc"))
                {
                    Enabled = false
                });
        }

        private void CreateMenuPlate(UIMenu menu)
        {
            this._submenuPlate = this._menuPool.AddSubMenu(menu, Translator.GetString("PlateChange"), Translator.GetString("PlateChangeDesc"));
            if (File.Exists(this._banner))
                this._submenuPlate.SetBannerType(this._banner);
            this.RebuildMenuPlate();
        }

        private void RebuildMenuPlate()
        {
            int price = 1000;
            this._submenuPlate.Clear();
            List<string> insuredVehicles = this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), false);
            insuredVehicles.AddRange((IEnumerable<string>)this._insurance.GetInsuredVehicles(SE.Player.GetCurrentCharacterName(true), true));
            if (insuredVehicles.Count > 0)
            {
                foreach (string str1 in insuredVehicles)
                {
                    string vehID = str1;
                    UIMenuItem changePlate = new UIMenuItem(this._insurance.GetVehicleFriendlyName(vehID, false));
                    changePlate.SetRightLabel(price.ToString() + "$");
                    this._submenuPlate.AddItem(changePlate);
                    this._submenuPlate.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
                    {
                        if (item != changePlate)
                            return;
                        if (this.OpenedFromiFruit)
                            MMISound.Play(MMISound.SoundFamily.Okay);
                        string key = vehID;
                        string vehicleLicensePlate = this._insurance.GetVehicleLicensePlate(vehID);
                        string upperInvariant = Game.GetUserInput(vehicleLicensePlate, 7).PadRight(8).ToUpperInvariant();
                        if (SE.Vehicle.IsValidPlateNumber(upperInvariant))
                        {
                            if (!(upperInvariant != vehicleLicensePlate) || !(upperInvariant != ""))
                                return;
                            if (SE.Player.AddCashToPlayer(-1 * price))
                            {
                                string str2 = this._insurance.ChangeVehicleLicensePlate(vehID, upperInvariant);
                                item.Text = this._insurance.GetVehicleFriendlyName(str2, false);
                                for (int index1 = InsuranceObserver.InsuredVehList.Count - 1; index1 >= 0; --index1)
                                {
                                    if (Tools.GetVehicleIdentifier(InsuranceObserver.InsuredVehList[index1]) == vehID)
                                    {
                                        InsuranceObserver.InsuredVehList[index1].NumberPlate = upperInvariant;
                                        InsuranceObserver.InsuredVehList.RemoveAt(index1);
                                    }
                                }
                                if (InsuranceObserver.BlipsToRemove.ContainsKey(key))
                                {
                                    Blip blip = InsuranceObserver.BlipsToRemove[key];
                                    InsuranceObserver.BlipsToRemove.Remove(key);
                                    InsuranceObserver.BlipsToRemove.Add(str2, blip);
                                }
                                GTA.UI.Notify(Translator.GetString("NotifyPlateChanged") + "~n~[" + vehicleLicensePlate + "] => [" + upperInvariant + "]");
                                item.Enabled = false;
                                BigMessageThread.MessageInstance.ShowSimpleShard(Translator.GetString("PlateChangeUpdateDB"), Translator.GetString("PlateChangeUpdateDBDesc"), this._observer.DelayDetectInsuredVehicles + 1000);
                                GTA.Script.Wait(this._observer.DelayDetectInsuredVehicles + 1000);
                                this.RefreshItemInsure();
                                this.RebuildMenuCancel();
                                this.RebuildMenuRecover();
                                this.RebuildMenuStolen();
                                if (this.OpenedFromiFruit)
                                    this.RebuildMenuBring();
                                this.RebuildMenuPlate();
                            }
                            else
                            {
                                if (this.OpenedFromiFruit)
                                    MMISound.Play(MMISound.SoundFamily.NoMoney);
                                GTA.UI.Notify(Translator.GetString("NotifyNoMoney"));
                            }
                        }
                        else
                            GTA.UI.Notify(Translator.GetString("NotifyWrongPlate"));
                    });
                }
            }
            else
                this._submenuPlate.AddItem(new UIMenuItem(Translator.GetString("Empty"), Translator.GetString("PlateChangeItemEmptyDesc"))
                {
                    Enabled = false
                });
        }

        private void CreateMenuBring(UIMenu menu)
        {
            this._submenuBring = this._menuPool.AddSubMenu(menu, Translator.GetString("BringVehicle"), Translator.GetString("BringVehicleDesc"));
            if (File.Exists(this._banner))
                this._submenuBring.SetBannerType(this._banner);
            this.RebuildMenuBring();
        }

        private void RebuildMenuBring()
        {
            this._submenuBring.Clear();
            if (InsuranceObserver.GetBringableVehicles().Count > 0)
            {
                foreach (GTA.Vehicle bringableVehicle in InsuranceObserver.GetBringableVehicles())
                {
                    GTA.Vehicle veh = bringableVehicle;
                    string vehicleIdentifier = Tools.GetVehicleIdentifier(veh);
                    if (SE.Player.GetCurrentCharacterName(true) == this._insurance.GetVehicleOwner(vehicleIdentifier))
                    {
                        int cost = (int)((double)Game.Player.Character.Position.DistanceTo(veh.Position) / 1000.0 * (double)InsuranceManager.BringVehicleBasePrice);
                        UIMenuItem bringVehicle = new UIMenuItem(this._insurance.GetVehicleFriendlyName(vehicleIdentifier, false), Translator.GetString("BringVehicleDesc"));
                        bringVehicle.SetRightLabel(cost.ToString() + "$");
                        this._submenuBring.AddItem(bringVehicle);
                        this._submenuBring.OnItemSelect += (ItemSelectEvent)((sender, item, index) =>
                        {
                            if (item != bringVehicle)
                                return;
                            if (SE.Player.AddCashToPlayer(-1 * cost))
                            {
                                if (this.OpenedFromiFruit)
                                    MMISound.Play(MMISound.SoundFamily.Okay);
                                this._observer.BringVehicleToPlayer(veh, cost, InsuranceManager.BringVehicleInstant);
                                bringVehicle.Enabled = false;
                                GTA.UI.Notify(Translator.GetString("NotifyBringVehicle"));
                                this._submenuBring.RemoveItemAt(index);
                                this.RefreshMenuIndex(this._submenuBring, Translator.GetString("BringVehicleItemEmptyDesc"));
                            }
                            else
                            {
                                if (this.OpenedFromiFruit)
                                    MMISound.Play(MMISound.SoundFamily.NoMoney);
                                GTA.UI.Notify(Translator.GetString("NotifyNoMoney"));
                            }
                        });
                    }
                }
            }
            else
                this._submenuBring.AddItem(new UIMenuItem(Translator.GetString("Empty"), Translator.GetString("BringVehicleItemEmptyDesc"))
                {
                    Enabled = false
                });
        }
    }
    internal class MMI : GTA.Script
    {
        private static bool _initialized = false;
        private static bool _checkForUpdate = true;
        private static bool _showSHVDNNotification = true;
        private static bool _showFileNotification = true;
        private static bool _showVisualCNotification = true;
        private static bool _showNETFrameworkNotification = true;

        public static bool Initialized
        {
            get => MMI._initialized;
            private set => MMI._initialized = value;
        }

        public static bool CheckForUpdate
        {
            get => MMI._checkForUpdate;
            private set => MMI._checkForUpdate = value;
        }

        public static bool ShowSHVDNNotification
        {
            get => MMI._showSHVDNNotification;
            private set => MMI._showSHVDNNotification = value;
        }

        public static bool ShowFileNotification
        {
            get => MMI._showFileNotification;
            private set => MMI._showFileNotification = value;
        }

        public static bool ShowVisualCNotification
        {
            get => MMI._showVisualCNotification;
            private set => MMI._showVisualCNotification = value;
        }

        public static bool ShowNETFrameworkNotification
        {
            get => MMI._showNETFrameworkNotification;
            private set => MMI._showNETFrameworkNotification = value;
        }

        public MMI() => this.Tick += new EventHandler(this.Initialize);

        private void Initialize(object sender, EventArgs e)
        {
            Logger.ResetLogFile();
            while (Game.IsLoading)
                GTA.Script.Yield();
            while (Game.IsScreenFadingIn)
                GTA.Script.Yield();
            this.LoadConfigValues();
            if (this.ArePrerequisitesInstalled() && MMI.CheckForUpdate && this.IsUpdateAvailable())
                this.NotifyNewUpdate();
            MMI._initialized = true;
            this.Tick -= new EventHandler(this.Initialize);
        }

        private bool ArePrerequisitesInstalled()
        {
            bool flag = true;
            Version version = new Version("4.8.0.0");
            DateTime dateTime = new DateTime(2017, 12, 19);
            Dictionary<string, Version> dictionary = new Dictionary<string, Version>()
      {
        {
          "SHVDN-Extender.dll",
          new Version("1.0.0.1")
        },
        {
          "iFruitAddon2.dll",
          new Version("2.0.1.0")
        },
        {
          "NativeUI.dll",
          new Version("1.7.0.0")
        }
      };
            foreach (string key in dictionary.Keys)
            {
                string str = AppDomain.CurrentDomain.BaseDirectory + "\\" + key;
                if (System.IO.File.Exists(str))
                {
                    FileInfo fileInfo = new FileInfo(str);
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(str);
                    if (new Version(versionInfo.ProductVersion).CompareTo(dictionary[key]) < 0)
                    {
                        if (MMI.ShowFileNotification)
                            Debug.DebugNotification("CHAR_BLOCKED", "MMI-SP", key + " v" + versionInfo.ProductVersion + " is outdated!", "Download and install the latest version.");
                        Logger.Log((object)("Error: " + key + " v" + versionInfo.ProductVersion + " is outdated!"));
                        flag = false;
                    }
                }
                else
                {
                    if (MMI.ShowFileNotification)
                        Debug.DebugNotification("CHAR_BLOCKED", "MMI-SP", key + " is missing!", "Download and install this file before starting the game.");
                    Logger.Log((object)("Error: " + key + " is missing!"));
                    flag = false;
                }
            }
            string str1 = AppDomain.CurrentDomain.BaseDirectory + "\\..\\ScriptHookVDotNet2.dll";
            if (System.IO.File.Exists(str1) && new FileInfo(str1).LastWriteTime < dateTime)
            {
                if (MMI.ShowSHVDNNotification)
                    Debug.DebugNotification("CHAR_BLOCKED", "MMI-SP", "ScriptHookVDotNet2 is outdated!", "Download and install the latest version.");
                Logger.Log((object)"Error: ScriptHookVDotNet2 is outdated!");
                flag = false;
            }
            if (!Tools.IsVisualCVersionHigherOrEqual(Tools.VisualCVersion.Visual_2015))
            {
                if (MMI.ShowVisualCNotification)
                    Debug.DebugNotification("CHAR_BLOCKED", "MMI-SP", "Microsoft Visual C++ is missing!", "Download and install version 2015 or 2017 x64.");
                Logger.Log((object)"Error: Microsoft Visual C++ 2015 and 2017 x64 is missing!");
                flag = false;
            }
            if (Tools.GetNETFrameworkVersion().CompareTo(version) < 0)
            {
                if (MMI.ShowNETFrameworkNotification)
                    Debug.DebugNotification("CHAR_BLOCKED", "MMI-SP", "Microsoft .NET Framework is outdated!", "Download and install version 4.8 or later.");
                Logger.Log((object)"Error: Microsoft .NET Framework is outdated!");
                flag = false;
            }
            return flag;
        }

        private void LoadConfigValues()
        {
            Config.Initialize();
            SE.UI.DrawTexture(AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\insurance.png", 1000, 2f, 2f, Color.FromArgb(35, 199, 128));
            MMI.CheckForUpdate = Config.Settings.GetValue<bool>("Check", "CheckForUpdate", true);
            MMI.ShowSHVDNNotification = Config.Settings.GetValue<bool>("Check", "ShowSHVDNNotification", true);
            MMI.ShowFileNotification = Config.Settings.GetValue<bool>("Check", "ShowFileNotification", true);
            MMI.ShowVisualCNotification = Config.Settings.GetValue<bool>("Check", "ShowVisualCNotification", true);
            MMI.ShowNETFrameworkNotification = Config.Settings.GetValue<bool>("Check", "ShowNETFrameworkNotification", true);
            Translator.Initialize(AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\" + Config.Settings.GetValue("General", "language", "default") + ".xml");
            InsuranceObserver.PersistentVehicles = Config.Settings.GetValue<bool>("General", "PersistentInsuredVehicles", false);
            InsuranceManager.BringVehicleBasePrice = Config.Settings.GetValue<int>("BringVehicle", "BringVehicleBasePrice", 200);
            InsuranceObserver.BringVehicleRadius = Config.Settings.GetValue<int>("BringVehicle", "BringVehicleRadius", 100);
            InsuranceObserver.BringVehicleTimeout = Config.Settings.GetValue<int>("BringVehicle", "BringVehicleTimeout", 5);
            InsuranceManager.BringVehicleInstant = Config.Settings.GetValue<bool>("BringVehicle", "BringVehicleInstant", false);
            float result1 = 1f;
            float result2 = 1f;
            float result3 = 1f;
            if (!float.TryParse(Config.Settings.GetValue<string>("Insurance", "InsuranceCostMultiplier", "1.0"), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result1))
                Logger.Log((object)"Error: MMI - Unable to read float value InsuranceCostMultiplier in config file.");
            else
                InsuranceManager.InsuranceMult = Math.Abs(result1);
            if (!float.TryParse(Config.Settings.GetValue<string>("Insurance", "RecoverCostMultiplier", "1.0"), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result2))
                Logger.Log((object)"Error: MMI - Unable to read float value RecoverCostMultiplier in config file.");
            else
                InsuranceManager.RecoverMult = Math.Abs(result2);
            if (!float.TryParse(Config.Settings.GetValue<string>("Insurance", "StolenCostMultiplier", "1.0"), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result3))
                Logger.Log((object)"Error: MMI - Unable to read float value StolenCostMultiplier in config file.");
            else
                InsuranceManager.StolenMult = Math.Abs(result3);
            MMISound.Volume = Config.Settings.GetValue<int>("General", "PhoneVolume", 25);
            iFruitMMI.CaniFruitInsure = Config.Settings.GetValue<bool>("iFruit", "CaniFruitInsure", false);
            iFruitMMI.CaniFruitCancel = Config.Settings.GetValue<bool>("iFruit", "CaniFruitCancel", false);
            iFruitMMI.CaniFruitRecover = Config.Settings.GetValue<bool>("iFruit", "CaniFruitRecover", true);
            iFruitMMI.CaniFruitStolen = Config.Settings.GetValue<bool>("iFruit", "CaniFruitStolen", false);
            iFruitMMI.CaniFruitPlate = Config.Settings.GetValue<bool>("iFruit", "CaniFruitPlate", false);
        }

        private bool IsUpdateAvailable()
        {
            try
            {
                WebClient webClient = new WebClient();
                Version version1 = new Version(webClient.DownloadString("https://raw.githubusercontent.com/Bob74/MMI-SP/master/version").Replace("\r", "").Replace("\n", ""));
                webClient.Dispose();
                Version version2 = Assembly.GetExecutingAssembly().GetName().Version;
                return version1.CompareTo(version2) > 0;
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: IsUpdateAvailable - " + ex.Message));
            }
            return false;
        }

        private void NotifyNewUpdate()
        {
            Translator.GetString("UpdateText");
            try
            {
                string message1 = new WebClient().DownloadString("https://raw.githubusercontent.com/Bob74/MMI-SP/master/versionlog");
                if (message1.Length > 90)
                {
                    string[] strArray = message1.Split('\n');
                    string message2 = "";
                    foreach (string str in strArray)
                    {
                        if (message2.Length + str.Length <= 90)
                        {
                            message2 = !(message2 != "") ? str : message2 + "~n~" + str;
                        }
                        else
                        {
                            Debug.DebugNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("UpdateAvailable"), message2);
                            message2 = str;
                        }
                    }
                    if (!(message2 != "") || !(message2 != "\r") || !(message2 != "\n") || !(message2 != "\r\n"))
                        return;
                    Debug.DebugNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("UpdateAvailable"), message2);
                }
                else
                    Debug.DebugNotification("char_mp_mors_mutual", "MORS MUTUAL INSURANCE", Translator.GetString("UpdateAvailable"), message1);
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: NotifyNewUpdate - " + ex.Message));
            }
        }
    }
    internal static class Price
    {
        private static int GetVehicleClassPrice(GTA.Vehicle veh)
        {
            switch (veh.ClassType)
            {
                case VehicleClass.Compacts:
                    return 5000;
                case VehicleClass.Sedans:
                    return 6000;
                case VehicleClass.SUVs:
                    return 8000;
                case VehicleClass.Coupes:
                    return 6500;
                case VehicleClass.Muscle:
                    return 7500;
                case VehicleClass.SportsClassics:
                    return 18000;
                case VehicleClass.Sports:
                    return 9500;
                case VehicleClass.Super:
                    return 12000;
                case VehicleClass.Motorcycles:
                    return 6000;
                case VehicleClass.OffRoad:
                    return 8500;
                case VehicleClass.Industrial:
                    return 6000;
                case VehicleClass.Utility:
                    return 5000;
                case VehicleClass.Vans:
                    return 5500;
                case VehicleClass.Cycles:
                    return 500;
                case VehicleClass.Boats:
                    return 5000;
                case VehicleClass.Helicopters:
                    return 10000;
                case VehicleClass.Planes:
                    return 5000;
                case VehicleClass.Service:
                    return 5000;
                case VehicleClass.Emergency:
                    return 10000;
                case VehicleClass.Military:
                    return 25000;
                case VehicleClass.Commercial:
                    return 5000;
                default:
                    return 8000;
            }
        }

        public static int GetVehicleModelPrice(GTA.Vehicle veh)
        {
            int vehicleClassPrice = Price.GetVehicleClassPrice(veh);
            if (veh.Model.Hash == Game.GenerateHash("DUSTER"))
                vehicleClassPrice += -4000;
            else if (veh.Model.Hash == Game.GenerateHash("RHINO") || veh.Model.Hash == Game.GenerateHash("KHANJALI"))
                vehicleClassPrice += 25000;
            else if (veh.Model.Hash == Game.GenerateHash("HYDRA") || veh.Model.Hash == Game.GenerateHash("LAZER"))
                vehicleClassPrice += 20000;
            else if (veh.Model.Hash == Game.GenerateHash("BUZZARD"))
                vehicleClassPrice += 8000;
            else if (veh.Model.Hash == Game.GenerateHash("ANNIHILATOR"))
                vehicleClassPrice += 15000;
            else if (veh.Model.Hash == Game.GenerateHash("DUMP"))
                vehicleClassPrice += 15000;
            else if (veh.Model.Hash == Game.GenerateHash("REBEL"))
                vehicleClassPrice += -2000;
            else if (veh.Model.Hash == Game.GenerateHash("SURFER2"))
                vehicleClassPrice += -2000;
            else if (veh.Model.Hash == Game.GenerateHash("TORNADO3") || veh.Model.Hash == Game.GenerateHash("TORNADO4"))
                vehicleClassPrice += -13000;
            else if (veh.Model.Hash == Game.GenerateHash("TORNADO6"))
                vehicleClassPrice += -11000;
            else if (veh.Model.Hash == Game.GenerateHash("PEYOTE"))
                vehicleClassPrice += -6000;
            else if (veh.Model.Hash == Game.GenerateHash("TRACTOR"))
                vehicleClassPrice += -4200;
            else if (veh.Model.Hash == Game.GenerateHash("VOODOO2"))
                vehicleClassPrice += -4000;
            else if (veh.Model.Hash == Game.GenerateHash("RUINER2"))
                vehicleClassPrice += 13000;
            else if (veh.Model.Hash == Game.GenerateHash("RUINER3"))
                vehicleClassPrice += -6000;
            else if (veh.Model.Hash == Game.GenerateHash("DELUXO"))
                vehicleClassPrice += 13000;
            else if (veh.Model.Hash == Game.GenerateHash("EMPEROR2"))
                vehicleClassPrice += -3000;
            else if (veh.Model.Hash == Game.GenerateHash("BFINJECTION"))
                vehicleClassPrice += -4000;
            else if (veh.Model.Hash == Game.GenerateHash("JOURNEY"))
                vehicleClassPrice += -4000;
            else if (veh.Model.Hash == Game.GenerateHash("RATBIKE"))
                vehicleClassPrice += -3000;
            else if (veh.Model.Hash == Game.GenerateHash("RATLOADER"))
                vehicleClassPrice += -3000;
            return vehicleClassPrice;
        }

        public static int GetVehicleSizePrice(GTA.Vehicle veh)
        {
            int num1 = 0;
            Vector3 dimensions = veh.Model.GetDimensions();
            int vehicleSizePrice;
            if (veh.Model.IsPlane)
                vehicleSizePrice = (double)dimensions.Y >= 10.0 ? ((double)dimensions.Y >= 20.0 ? ((double)dimensions.Y >= 30.0 ? ((double)dimensions.Y >= 50.0 ? ((double)dimensions.Y >= 70.0 ? num1 + 30000 : num1 + 20000) : num1 + 13000) : num1 + 10000) : num1 + 6000) : num1 + 1000;
            else if (veh.Model.IsBoat)
            {
                vehicleSizePrice = (double)dimensions.Y >= 5.0 ? ((double)dimensions.Y >= 10.0 ? num1 + 10000 : num1 + 3000) : num1 - 4000;
            }
            else
            {
                if (veh.Model.IsBike || veh.Model.IsBicycle || veh.Model.IsQuadbike || veh.Model.IsHelicopter)
                    return 0;
                if (veh.Model.IsCargobob)
                    return 5000;
                int num2 = (double)dimensions.Y >= 3.0 ? ((double)dimensions.Y >= 5.0 ? ((double)dimensions.Y >= 8.0 ? ((double)dimensions.Y >= 10.0 ? ((double)dimensions.Y >= 15.0 ? num1 + 10000 : num1 + 3000) : num1 + 1500) : num1 + 700) : num1) : num1 - 4000;
                vehicleSizePrice = (double)dimensions.X >= 1.5 ? ((double)dimensions.X >= 3.5 ? ((double)dimensions.X >= 7.0 ? num2 + 10000 : num2 + 6000) : num2 + 2000) : num2 - 2500;
            }
            return vehicleSizePrice;
        }

        public static int GetVehicleModsPrice(GTA.Vehicle veh)
        {
            int vehicleModsPrice = 0;
            if (veh.IsToggleModOn(VehicleToggleMod.Turbo))
                vehicleModsPrice += 2000;
            if (veh.IsToggleModOn(VehicleToggleMod.TireSmoke))
                vehicleModsPrice += 485;
            if (veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights))
                vehicleModsPrice += 960;
            if (veh.WheelType == VehicleWheelType.HighEnd)
                vehicleModsPrice += 200;
            if (veh.WheelType == VehicleWheelType.Sport)
                vehicleModsPrice += 120;
            if (veh.WheelType == VehicleWheelType.Tuner)
                vehicleModsPrice += 100;
            if (veh.GetMod(VehicleMod.Armor) > -1)
                vehicleModsPrice += 500;
            if (veh.GetMod(VehicleMod.Brakes) > -1)
                vehicleModsPrice += 500;
            if (veh.GetMod(VehicleMod.Engine) > -1)
                vehicleModsPrice += 720;
            if (veh.GetMod(VehicleMod.Transmission) > -1)
                vehicleModsPrice += 630;
            if (veh.WindowTint == VehicleWindowTint.LightSmoke)
                vehicleModsPrice += 170;
            if (veh.WindowTint == VehicleWindowTint.DarkSmoke)
                vehicleModsPrice += 275;
            if (veh.WindowTint == VehicleWindowTint.Limo)
                vehicleModsPrice += 300;
            if (veh.WindowTint == VehicleWindowTint.PureBlack)
                vehicleModsPrice += 355;
            if (veh.WindowTint == VehicleWindowTint.Green)
                vehicleModsPrice += 355;
            if (veh.IsPrimaryColorCustom)
                vehicleModsPrice += 700;
            if (veh.IsSecondaryColorCustom)
                vehicleModsPrice += 500;
            if (veh.IsConvertible)
                vehicleModsPrice += 525;
            if (!veh.CanTiresBurst)
                vehicleModsPrice += 810;
            if (veh.Livery > -1)
                vehicleModsPrice += 500;
            return vehicleModsPrice;
        }
    }
    public static class Tools
    {
        private static readonly Dictionary<Tools.VisualCVersion, Version> visualCVersion = new Dictionary<Tools.VisualCVersion, Version>()
    {
      {
        Tools.VisualCVersion.Visual_2017,
        new Version("14.1.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2015,
        new Version("14.0.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2013,
        new Version("12.0.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2012,
        new Version("11.0.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2010,
        new Version("10.0.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2008,
        new Version("9.0.0.0")
      },
      {
        Tools.VisualCVersion.Visual_2005,
        new Version("8.0.0.0")
      }
    };

        public static string ToHexString(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in Encoding.Unicode.GetBytes(str))
                stringBuilder.Append(num.ToString("X2"));
            return stringBuilder.ToString();
        }

        public static string FromHexString(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int index = 0; index < bytes.Length; ++index)
                bytes[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16);
            return Encoding.Unicode.GetString(bytes);
        }

        public static bool IsVisualCVersionHigherOrEqual(Tools.VisualCVersion visualC)
        {
            Version version = Tools.visualCVersion[visualC];
            string[] strArray = new string[2]
            {
        "msvcp*.dll",
        "msvcr*.dll"
            };
            List<string> stringList = new List<string>();
            foreach (string searchPattern in strArray)
                stringList.AddRange((IEnumerable<string>)Directory.GetFiles(Environment.SystemDirectory, searchPattern));
            foreach (string fileName in stringList)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (new Version(FileVersionInfo.GetVersionInfo(fileName).ProductVersion).CompareTo(version) >= 0)
                    return true;
            }
            return false;
        }

        public static Version GetNETFrameworkVersion()
        {
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                if (registryKey != null && registryKey.GetValue("Version") != null)
                    return new Version((string)registryKey.GetValue("Version"));
                if (registryKey == null || registryKey.GetValue("Release") == null)
                    return new Version("0.0.0.0");
                int num = (int)registryKey.GetValue("Release");
                if (num >= 461308)
                    return new Version("4.7.1.0");
                return num >= 460798 ? new Version("4.7.0.0") : new Version("4.0.0.0");
            }
        }

        public static string GetVehicleIdentifier(GTA.Vehicle veh)
        {
            return (SE.Player.GetCurrentCharacterName() + veh.Model.Hash.ToString() + veh.NumberPlate).Replace(" ", "_");
        }

        public static InsuranceManager.EntityPosition GetVehicleSpawnLocation(Vector3 position)
        {
            for (int index = 0; index < 22; ++index)
            {
                OutputArgument outputArgument1 = new OutputArgument();
                OutputArgument outputArgument2 = new OutputArgument();
                OutputArgument outputArgument3 = new OutputArgument();
                Function.Call(Hash._0x80CA6A8B6C094CC4, (InputArgument)position.X, (InputArgument)position.Y, (InputArgument)position.Z, (InputArgument)index, (InputArgument)outputArgument2, (InputArgument)outputArgument3, (InputArgument)outputArgument1, (InputArgument)9, (InputArgument)3.0, (InputArgument)2.5);
                Vector3 result1 = outputArgument2.GetResult<Vector3>();
                float result2 = outputArgument3.GetResult<float>();
                if (!Function.Call<bool>(Hash._0xE54E209C35FFA18D, (InputArgument)result1.X, (InputArgument)result1.Y, (InputArgument)result1.Z, (InputArgument)5f, (InputArgument)5f, (InputArgument)5f, (InputArgument)0))
                    return new InsuranceManager.EntityPosition(result1, result2);
            }
            return new InsuranceManager.EntityPosition(position, 0.0f);
        }

        public enum VisualCVersion
        {
            Visual_2017,
            Visual_2015,
            Visual_2013,
            Visual_2012,
            Visual_2010,
            Visual_2008,
            Visual_2005,
        }
    }
    internal static class Trailer
    {
        private static Ped _michael;
        private static Ped _franklin;
        private static Ped _trevor;
        private static Ped _freemode;
        private static Vector3 _michaelPosition = new Vector3(-787.1056f, 185.9241f, 72.83529f);
        private static float _michaelHeading = 58.53875f;
        private static Vector3 _franklinPosition = new Vector3(-18.88375f, -1451.604f, 30.58212f);
        private static float _franklinHeading = 223.4324f;
        private static Vector3 _franklinCarPosition = new Vector3(-25.07652f, -1450.024f, 30.1692f);
        private static float _franklinCarHeading = 183.715f;
        private static Vector3 _trevorPosition = new Vector3(1984.025f, 3817.162f, 32.28379f);
        private static float _trevorHeading = 228.1426f;
        private static Vector3 _freemodePosition = new Vector3(-777.3974f, 282.0237f, 85.77721f);
        private static float _freemodeHeading = 179.5031f;
        private static Vector3 _freemodeTrevorPosition = new Vector3(-778.6523f, 282.0237f, 85.78682f);
        private static float _freemodeTrevorHeading = 179.5031f;

        public static void CleanUp()
        {
            if ((Entity)Trailer._michael != (Entity)null)
                Trailer._michael.Delete();
            if ((Entity)Trailer._franklin != (Entity)null)
                Trailer._franklin.Delete();
            if ((Entity)Trailer._trevor != (Entity)null)
                Trailer._trevor.Delete();
            if (!((Entity)Trailer._freemode != (Entity)null))
                return;
            Trailer._freemode.Delete();
        }

        public static void Spawn(PedHash hash)
        {
            switch (hash)
            {
                case PedHash.Michael:
                    Trailer._michael = GTA.World.CreatePed((Model)PedHash.Michael, Trailer._michaelPosition, Trailer._michaelHeading);
                    Trailer.SetupMichael();
                    break;
                case PedHash.FreemodeMale01:
                    if ((Entity)Trailer._freemode != (Entity)null)
                        Trailer._freemode.Delete();
                    Trailer._freemode = GTA.World.CreatePed((Model)PedHash.FreemodeMale01, Trailer._freemodePosition, Trailer._freemodeHeading);
                    if ((Entity)Trailer._trevor != (Entity)null)
                        Trailer._trevor.Delete();
                    Trailer._trevor = GTA.World.CreatePed((Model)PedHash.Trevor, Trailer._freemodeTrevorPosition, Trailer._freemodeTrevorHeading);
                    Trailer.SetFreemodeClothes();
                    break;
                case PedHash.Franklin:
                    if ((Entity)Trailer._franklin != (Entity)null)
                        Trailer._franklin.Delete();
                    Trailer._franklin = GTA.World.CreatePed((Model)PedHash.Franklin, Trailer._franklinPosition, Trailer._franklinHeading);
                    Trailer.SetupFranklin();
                    break;
                case PedHash.Trevor:
                    if ((Entity)Trailer._trevor != (Entity)null)
                        Trailer._trevor.Delete();
                    Trailer._trevor = GTA.World.CreatePed((Model)PedHash.Trevor, Trailer._trevorPosition, Trailer._trevorHeading);
                    break;
            }
        }

        private static void SetupMichael()
        {
            Function.Call(Hash._0x93376B65A266EB5F, (InputArgument)Trailer._michael, (InputArgument)1, (InputArgument)5, (InputArgument)0, (InputArgument)0);
        }

        private static void SetupFranklin()
        {
            GTA.Vehicle vehicle = GTA.World.CreateVehicle((Model)VehicleHash.Buffalo2, Trailer._franklinCarPosition, Trailer._franklinCarHeading);
            vehicle.PrimaryColor = VehicleColor.PureWhite;
            vehicle.WindowTint = VehicleWindowTint.Limo;
            vehicle.DirtLevel = 0.0f;
            vehicle.MarkAsNoLongerNeeded();
        }

        private static void SetFreemodeClothes()
        {
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)8, (InputArgument)3, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)0, (InputArgument)4, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)2, (InputArgument)2, (InputArgument)4, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)3, (InputArgument)1, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)4, (InputArgument)0, (InputArgument)1, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)6, (InputArgument)0, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)Trailer._freemode, (InputArgument)11, (InputArgument)4, (InputArgument)0, (InputArgument)0);
            Function.Call(Hash._0x93376B65A266EB5F, (InputArgument)Trailer._freemode, (InputArgument)1, (InputArgument)3, (InputArgument)4, (InputArgument)0);
        }

        public static void TrevorAttackFreemode()
        {
            Trailer._freemode.Health = 1;
            if (!((Entity)Trailer._trevor != (Entity)null) || !((Entity)Trailer._freemode != (Entity)null))
                return;
            Trailer._trevor.Task.FightAgainst(Trailer._freemode);
        }

        public static void CharacterCurse(PedHash hash, string insult = "GENERIC_CURSE_HIGH")
        {
            switch (hash)
            {
                case PedHash.Michael:
                    if (!((Entity)Trailer._michael != (Entity)null))
                        break;
                    Function.Call(Hash._0x3523634255FC3318, (InputArgument)Trailer._michael, (InputArgument)insult, (InputArgument)"MICHAEL_NORMAL", (InputArgument)"SPEECH_PARAMS_FORCE", (InputArgument)0);
                    break;
                case PedHash.FreemodeMale01:
                    if (!((Entity)Trailer._freemode != (Entity)null))
                        break;
                    Trailer._freemode.Task.PlayAnimation("mp_celebration@idles@male", "celebration_idle_m_b");
                    break;
                case PedHash.Franklin:
                    if (!((Entity)Trailer._franklin != (Entity)null))
                        break;
                    Function.Call(Hash._0x3523634255FC3318, (InputArgument)Trailer._franklin, (InputArgument)insult, (InputArgument)"FRANKLIN_NORMAL", (InputArgument)"SPEECH_PARAMS_FORCE", (InputArgument)0);
                    break;
                case PedHash.Trevor:
                    if (!((Entity)Trailer._trevor != (Entity)null))
                        break;
                    Function.Call(Hash._0x3523634255FC3318, (InputArgument)Trailer._trevor, (InputArgument)insult, (InputArgument)"TREVOR_NORMAL", (InputArgument)"SPEECH_PARAMS_FORCE", (InputArgument)3);
                    break;
            }
        }

        public static void TrevorFight()
        {
            if (!((Entity)Trailer._freemode != (Entity)null))
                return;
            Trailer._trevor.Task.FightAgainst(Trailer._freemode);
        }
    }
    internal static class Translator
    {
        private static string _languageFilePath;
        private static XElement _languageFile;
        private static List<Translator.LocalizedString> _strings = new List<Translator.LocalizedString>();

        public static void Initialize(string fileName)
        {
            Translator._languageFilePath = fileName;
            if (!File.Exists(Translator._languageFilePath))
            {
                Logger.Log((object)("Error: Language file does not exist! " + Translator._languageFilePath + " (Check the language value in the config file and check if the file exist)"));
                SE.UI.DrawNotification("MMI-SP: ERROR - Language file does not exist! See \"GTA V\\MMI-SP.log\"");
            }
            else
            {
                Translator._languageFile = XElement.Load(Translator._languageFilePath);
                Translator.GetAllStrings();
            }
        }

        public static string GetString(string ID)
        {
            Translator.LocalizedString localizedString = Translator._strings.Find((Predicate<Translator.LocalizedString>)(x => x.ID == ID));
            return localizedString != null ? Translator.ReplaceVariablesInString(localizedString.value) : "UNKNOWN";
        }

        private static void GetAllStrings()
        {
            if (Translator._languageFile.Element((XName)"Strings") != null)
            {
                foreach (XElement element1 in Translator._languageFile.Element((XName)"Strings").Elements())
                {
                    if (element1 != null)
                    {
                        foreach (XElement element2 in element1.Elements())
                            Translator._strings.Add(new Translator.LocalizedString(element2.Name.LocalName, element2.Value));
                    }
                }
            }
            else
                Logger.Log((object)"Error: Translator.GetAllStrings - Incomplete language file (cannot find \"Strings\").");
        }

        private static string ReplaceVariablesInString(string str)
        {
            GTA.Vehicle lastVehicle = Game.Player.LastVehicle;
            int vehicleInsuranceCost;
            if (str.Contains("$VehicleInsuranceCost"))
            {
                string str1 = str;
                vehicleInsuranceCost = InsuranceManager.GetVehicleInsuranceCost(lastVehicle);
                string newValue = vehicleInsuranceCost.ToString();
                str = str1.Replace("$VehicleInsuranceCost", newValue);
            }
            if (str.Contains("$VehicleRecoverCost"))
            {
                string str2 = str;
                vehicleInsuranceCost = InsuranceManager.GetVehicleInsuranceCost(lastVehicle, InsuranceManager.Multiplier.Recover);
                string newValue = vehicleInsuranceCost.ToString();
                str = str2.Replace("$VehicleRecoverCost", newValue);
            }
            if (str.Contains("$VehicleStolenCost"))
            {
                string str3 = str;
                vehicleInsuranceCost = InsuranceManager.GetVehicleInsuranceCost(lastVehicle, InsuranceManager.Multiplier.Stolen);
                string newValue = vehicleInsuranceCost.ToString();
                str = str3.Replace("$VehicleStolenCost", newValue);
            }
            if (str.Contains("$VehicleFriendlyName"))
                str = str.Replace("$VehicleFriendlyName", SE.Vehicle.GetVehicleFriendlyName(lastVehicle, false));
            if (str.Contains("$VehicleFriendlyNameFull"))
                str = str.Replace("$VehicleFriendlyNameFull", SE.Vehicle.GetVehicleFriendlyName(lastVehicle));
            if (str.Contains("$InsureVehicle"))
                str = str.Replace("$InsureVehicle", Translator.GetString("InsureVehicle"));
            if (str.Contains("$CancelInsurance"))
                str = str.Replace("$CancelInsurance", Translator.GetString("CancelInsurance"));
            if (str.Contains("$RecoverVehicle"))
                str = str.Replace("$RecoverVehicle", Translator.GetString("RecoverVehicle"));
            if (str.Contains("$StolenVehicle"))
                str = str.Replace("$StolenVehicle", Translator.GetString("StolenVehicle"));
            if (str.Contains("$BringVehicle"))
                str = str.Replace("$BringVehicle", Translator.GetString("BringVehicle"));
            if (str.Contains("$PlateChange"))
                str = str.Replace("$PlateChange", Translator.GetString("PlateChange"));
            return str;
        }

        private class LocalizedString
        {
            public string ID { get; set; }

            public string value { get; set; }

            public LocalizedString(string id, string str)
            {
                this.ID = id;
                this.value = str;
            }
        }
    }
}
namespace MMI_SP.Agency
{
    internal class Agency : GTA.Script
    {
        private Blip _agencyBlip;
        private static Vector3 _position = new Vector3(-825.7242f, -261.2752f, 37f);
        private MenuMMI _menuMMI;
        private Office _office;
        private bool _isPlayerInCutscene;
        private static Vector3 _officePlayerPos = new Vector3(120f, -620.5f, 206.35f);
        private TimeSpan _officeLastCreation = new TimeSpan(0L);
        private ItemsManager.OfficeItemsCollection _officeLastItemsCollection = new ItemsManager.OfficeItemsCollection();
        private int _timerRandomSpeech;

        public static Vector3 Position => MMI_SP.Agency.Agency._position;

        internal Vector3 GetPosition() => MMI_SP.Agency.Agency._position;

        internal MenuMMI GetMenu() => this._menuMMI;

        public static Vector3 OfficePlayerPos => MMI_SP.Agency.Agency._officePlayerPos;

        public Agency() => this.Tick += new EventHandler(this.Initialize);

        private void Initialize(object sender, EventArgs e)
        {
            while (!InsuranceObserver.Initialized)
                GTA.Script.Yield();
            this._agencyBlip = this.CreateBlip();
            this.CreateMenuMMI();
            if ((double)Game.Player.Character.Position.DistanceTo(MMI_SP.Agency.Agency.OfficePlayerPos) <= 2.0)
                this.ErrorCancelAgency(false);
            this.Tick -= new EventHandler(this.Initialize);
            this.Tick += new EventHandler(this.OnTick);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (this._isPlayerInCutscene)
                Function.Call(Hash._0x719FF505F097FD20);
            if (this._office != null)
            {
                if (this._timerRandomSpeech <= Game.GameTime && this._timerRandomSpeech != 0)
                {
                    this._office.NpcSay(DialogueManager.SpeechType.OfficeSomething);
                    this._timerRandomSpeech = Game.GameTime + new System.Random(Game.GameTime).Next(10000, 20000);
                }
                else if (this._timerRandomSpeech == 0)
                    this._timerRandomSpeech = Game.GameTime + new System.Random(Game.GameTime).Next(10000, 20000);
            }
            else
                this._timerRandomSpeech = 0;
            if (this._menuMMI != null)
                this._menuMMI.MenuPoolProcessMenus();
            this.DisplayAgencyThisFrame();
        }

        protected override void Dispose(bool A_0)
        {
            if (!A_0)
                return;
            if (this._office != null)
            {
                this._office.CleanUp();
                this._office = (Office)null;
            }
            if (!this._agencyBlip.Exists())
                return;
            this._agencyBlip.Remove();
        }

        private void DisplayAgencyThisFrame()
        {
            if ((double)Game.Player.Character.Position.DistanceTo(MMI_SP.Agency.Agency._position) >= 4.0 || Game.Player.Character.IsInVehicle())
                return;
            if (Game.Player.WantedLevel > 0)
            {
                SE.UI.DisplayHelpTextThisFrame(Translator.GetString("AgencyEntryWanted"));
            }
            else
            {
                SE.UI.DisplayHelpTextThisFrame(Translator.GetString("AgencyEntry"));
                if (!Game.IsControlJustReleased(1, GTA.Control.Context))
                    return;
                try
                {
                    this.EnterAgency();
                }
                catch (Exception ex)
                {
                    Logger.Log((object)("Error: DisplayAgencyThisFrame - " + ex.Message));
                    GTA.UI.Notify("MMI-SP: Error while creating the office.");
                    this.ErrorCancelAgency();
                    this._menuMMI.Reset();
                    this._menuMMI.Show();
                }
            }
        }

        private Blip CreateBlip()
        {
            Blip blip = GTA.World.CreateBlip(MMI_SP.Agency.Agency._position);
            blip.Sprite = BlipSprite.Michael;
            blip.Color = BlipColor.NetPlayer1;
            blip.Name = "Mors Mutual Insurance";
            blip.IsShortRange = true;
            return blip;
        }

        private void CreateMenuMMI()
        {
            this._menuMMI = new MenuMMI();
            this._menuMMI.GetMainmenu().OnMenuClose += (MenuCloseEvent)(sender =>
            {
                if (this._office.itemsCollection.Type == ItemsManager.CollectionType.Night)
                    this._office.NpcSay(DialogueManager.SpeechType.OfficeNaughtyBye);
                else
                    this._office.NpcSay(DialogueManager.SpeechType.OfficeBye);
                this.ExitAgency();
            });
        }

        private void EnterAgency()
        {
            try
            {
                this._menuMMI.Reset();
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: EnterAgency - " + ex.Message));
                GTA.UI.Notify("MMI-SP: Error with module NativeUI!");
                this.ErrorCancelAgency();
                return;
            }
            this._isPlayerInCutscene = true;
            Cutscenes.EnteringAgency();
            Game.Player.Character.Position = MMI_SP.Agency.Agency.OfficePlayerPos;
            Game.Player.Character.FreezePosition = true;
            Function.Call(Hash._0x4448EB75B4904BDB, (InputArgument)MMI_SP.Agency.Agency.OfficePlayerPos.X, (InputArgument)MMI_SP.Agency.Agency.OfficePlayerPos.Y, (InputArgument)MMI_SP.Agency.Agency.OfficePlayerPos.Z);
            SE.UI.WaitAndhideUI(1000);
            try
            {
                this._menuMMI.Show();
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: EnterAgency - " + ex.Message));
                GTA.UI.Notify("MMI-SP: Error with module NativeUI!");
                this.ErrorCancelAgency();
                return;
            }
            int days1 = this._officeLastCreation.Days;
            TimeSpan currentDayTime = GTA.World.CurrentDayTime;
            int days2 = currentDayTime.Days;
            if (days1 == days2)
            {
                int hours1 = this._officeLastCreation.Hours;
                currentDayTime = GTA.World.CurrentDayTime;
                int hours2 = currentDayTime.Hours;
                if (hours1 == hours2 && this._officeLastItemsCollection.Count > 0)
                {
                    this._office = new Office(this._officeLastItemsCollection);
                    goto label_11;
                }
            }
            this._office = new Office();
            this._officeLastCreation = GTA.World.CurrentDayTime;
            if (this._officeLastItemsCollection != null)
                this._officeLastItemsCollection.DeleteItems();
            this._officeLastItemsCollection = new ItemsManager.OfficeItemsCollection(this._office.itemsCollection);
        label_11:
            if (this._office.itemsCollection.Type == ItemsManager.CollectionType.Night)
                this._office.NpcSay(DialogueManager.SpeechType.OfficeNaughty);
            else
                this._office.NpcSay(DialogueManager.SpeechType.OfficeHi);
        }

        private void ExitAgency()
        {
            Game.FadeScreenOut(1000);
            SE.UI.WaitAndhideUI(1000);
            this._office.CleanUp();
            this._office = (Office)null;
            Game.Player.Character.FreezePosition = false;
            Game.Player.Character.Position = MMI_SP.Agency.Agency._position;
            Function.Call(Hash._0x4448EB75B4904BDB, (InputArgument)MMI_SP.Agency.Agency._position.X, (InputArgument)MMI_SP.Agency.Agency._position.Y, (InputArgument)MMI_SP.Agency.Agency._position.Z);
            GTA.Script.Wait(1000);
            Cutscenes.LeavingAgency();
            this._isPlayerInCutscene = false;
        }

        private void ErrorCancelAgency(bool menu = true)
        {
            this._isPlayerInCutscene = false;
            Game.Player.Character.Position = MMI_SP.Agency.Agency._position;
            Game.Player.Character.FreezePosition = false;
            Game.FadeScreenIn(1000);
            GTA.World.RenderingCamera = (GTA.Camera)null;
            if (!menu)
                return;
            this._menuMMI.Reset();
            this._menuMMI.Show();
        }
    }
    public static class Cutscenes
    {
        public static void EnteringAgency()
        {
            Vector3 vector3_1 = new Vector3(-825.7242f, -261.2752f, 37f);
            Vector3 position1 = new Vector3(-826.7672f, -255.3226f, 40.54334f);
            Vector3 target = new Vector3(-825.814f, -265.1871f, 37.62714f);
            Vector3 vector3_2 = new Vector3(-822.528f, -260f, 35.79341f);
            Vector3 position2 = vector3_1 with { Z = 40f };
            float num = 130.3831f;
            int duration = 2500;
            GTA.Camera camera = GTA.World.CreateCamera(position1, new Vector3(0.0f, 0.0f, 0.0f), GameplayCamera.FieldOfView);
            camera.PointAt(target);
            Game.Player.Character.Weapons.Select(WeaponHash.Unarmed, true);
            Game.Player.Character.Position = vector3_2;
            Game.Player.Character.Heading = num;
            Game.Player.Character.Task.LookAt(position2, duration);
            Function.Call(Hash._0x477D5D63E63ECA5D, (InputArgument)Game.Player, (InputArgument)1f, (InputArgument)duration, (InputArgument)1f, (InputArgument)1, (InputArgument)0);
            GTA.World.RenderingCamera = camera;
            SE.UI.WaitAndhideUI(duration - 1000);
            Game.FadeScreenOut(1000);
            SE.UI.WaitAndhideUI(1000);
            GTA.World.RenderingCamera = (GTA.Camera)null;
            camera.IsActive = false;
            camera.Destroy();
        }

        public static void LeavingAgency()
        {
            float num1 = 305.54f;
            int num2 = 2000;
            Game.Player.Character.Heading = num1;
            GameplayCamera.RelativeHeading = 0.0f;
            GameplayCamera.RelativePitch = 0.0f;
            Function.Call(Hash._0x477D5D63E63ECA5D, (InputArgument)Game.Player, (InputArgument)1f, (InputArgument)num2, (InputArgument)1f, (InputArgument)1, (InputArgument)0);
            Game.FadeScreenIn(1000);
        }
    }

    internal static class ItemsManager
    {
        internal static readonly ItemsManager.OfficeItemsCollection Empty = new ItemsManager.OfficeItemsCollection();
        internal static readonly ItemsManager.OfficeItemsCollection Normal1 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Normal, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 215f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115.3f, -619.3f, 205.8f), new Vector3(-90f, 0.0f, -110f)),
      new ItemsManager.OfficeItem("p_amb_clipboard_01", new Vector3(115.2f, -619.7f, 205.698f), new Vector3(-90f, 0.0f, -130f)),
      new ItemsManager.OfficeItem("prop_pencil_01", new Vector3(115.15f, -619.72f, 205.88f), new Vector3(0.0f, 0.0f, 60f)),
      new ItemsManager.OfficeItem("prop_cs_envolope_01", new Vector3(114.9f, -620.5f, 205.865f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_envolope_01", new Vector3(114.75f, -620.5f, 205.875f), new Vector3(-180f, 0.0f, 60f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Normal2 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Normal, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 210f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115.15f, -619.65f, 205.82f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("p_amb_clipboard_01", new Vector3(115.2f, -619.7f, 205.698f), new Vector3(-90f, 0.0f, -130f)),
      new ItemsManager.OfficeItem("prop_cs_envolope_01", new Vector3(114.65f, -620.5f, 205.875f), new Vector3(-180f, 0.0f, -180f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Normal3 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Normal, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 200f)),
      new ItemsManager.OfficeItem("prop_cs_tablet", new Vector3(115.3f, -619.3f, 205.75f), new Vector3(-90f, 0.0f, -30f)),
      new ItemsManager.OfficeItem("prop_candy_pqs", new Vector3(114.7f, -620.25f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_fib_coffee", new Vector3(115.2f, -619.7f, 205.865f), new Vector3(0.0f, 0.0f, -30f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Normal4 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Normal, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 215f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115.15f, -619.65f, 205.8f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_cs_lipstick", new Vector3(115.2f, -619.4f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_nail_file", new Vector3(115.25f, -619.42f, 205.854f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_barry_table_detail", new Vector3(114.6f, -620.6f, 205.87f), new Vector3(0.0f, 0.0f, -30f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Normal5 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Normal, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 215f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115f, -619.9f, 205.83f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_notepad_02", new Vector3(115.1f, -619.65f, 205.87f), new Vector3(0.0f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_cs_pills", new Vector3(114.65f, -620.28f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_fag_packet_01", new Vector3(114.78f, -620.28f, 205.82f), new Vector3(90f, 0.0f, -160f)),
      new ItemsManager.OfficeItem("prop_cs_book_01", new Vector3(114.7f, -620.45f, 205.73f), new Vector3(-90f, 0.0f, 90f)),
      new ItemsManager.OfficeItem("prop_ashtray_01", new Vector3(114.67f, -620.5f, 205.9f), new Vector3(0.0f, 0.0f, -30f)),
      new ItemsManager.OfficeItem("prop_cs_envolope_01", new Vector3(115.5f, -619.3f, 205.865f), new Vector3(180f, 0.0f, 60f)),
      new ItemsManager.OfficeItem("prop_cs_binder_01", new Vector3(115.5f, -619.3f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Midday1 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Midday, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 230f)),
      new ItemsManager.OfficeItem("prop_cs_tablet", new Vector3(115.2f, -619.5f, 205.75f), new Vector3(-90f, 0.0f, -30f)),
      new ItemsManager.OfficeItem("prop_fib_coffee", new Vector3(114.7f, -620.4f, 205.865f), new Vector3(0.0f, 0.0f, 90f)),
      new ItemsManager.OfficeItem("prop_amb_donut", new Vector3(114.6f, -620.3f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Midday2 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Midday, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_02_closed", new Vector3(115f, -620.3f, 205.87f), new Vector3(0.0f, 0.0f, 190f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115.15f, -619.65f, 205.8f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_a4_sheet_01", new Vector3(115f, -619.85f, 205.87f), new Vector3(0.0f, 0.0f, -20f)),
      new ItemsManager.OfficeItem("prop_sandwich_01", new Vector3(115f, -619.8f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_ld_can_01", new Vector3(114.95f, -619.95f, 205.865f), new Vector3(0.0f, 0.0f, 90f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Midday3 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Midday, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_02_closed", new Vector3(115f, -620.3f, 205.87f), new Vector3(0.0f, 0.0f, 190f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115f, -620.15f, 205.845f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_a4_sheet_01", new Vector3(115.2f, -619.65f, 205.87f), new Vector3(0.0f, 0.0f, -50f)),
      new ItemsManager.OfficeItem("prop_cs_burger_01", new Vector3(115.2f, -619.6f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_bs_cup", new Vector3(115.15f, -619.75f, 205.865f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_paper_bag_small", new Vector3(114.65f, -620.35f, 205.875f), new Vector3(0.0f, 0.0f, 70f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Midday4 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Midday, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 210f)),
      new ItemsManager.OfficeItem("prop_cs_crisps_01", new Vector3(115.2f, -619.5f, 205.75f), new Vector3(-90f, 0.0f, 100f)),
      new ItemsManager.OfficeItem("prop_ecola_can", new Vector3(115.2f, -619.8f, 205.865f), new Vector3(0.0f, 0.0f, 0.0f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Midday5 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Midday, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 210f)),
      new ItemsManager.OfficeItem("prop_food_tray_01", new Vector3(115.3f, -619.5f, 205.88f), new Vector3(0.0f, 0.0f, 45f)),
      new ItemsManager.OfficeItem("prop_taco_02", new Vector3(115.2f, -619.6f, 205.89f), new Vector3(0.0f, 0.0f, 10f)),
      new ItemsManager.OfficeItem("prop_taco_01", new Vector3(115.3f, -619.55f, 205.89f), new Vector3(0.0f, 0.0f, 25f)),
      new ItemsManager.OfficeItem("prop_ecola_can", new Vector3(115.35f, -619.45f, 205.89f), new Vector3(0.0f, 0.0f, 25f)),
      new ItemsManager.OfficeItem("prop_paper_bag_small", new Vector3(115.35f, -619.35f, 205.89f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_barry_table_detail", new Vector3(114.7f, -620.8f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f))
    });
        internal static readonly ItemsManager.OfficeItemsCollection Night1 = new ItemsManager.OfficeItemsCollection(ItemsManager.CollectionType.Night, new List<ItemsManager.OfficeItem>()
    {
      new ItemsManager.OfficeItem("v_club_officechair", new Vector3(114.45f, -619.45f, 205.05f), new Vector3(0.0f, 0.0f, 63f)),
      new ItemsManager.OfficeItem("prop_laptop_01a", new Vector3(115f, -620.1f, 205.87f), new Vector3(0.0f, 0.0f, 215f)),
      new ItemsManager.OfficeItem("prop_npc_phone", new Vector3(115f, -619.9f, 205.83f), new Vector3(-90f, 0.0f, -120f)),
      new ItemsManager.OfficeItem("prop_cs_coke_line", new Vector3(115.27f, -619.38f, 205.863f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_coke_line", new Vector3(115.26f, -619.42f, 205.863f), new Vector3(0.0f, 0.0f, 5f)),
      new ItemsManager.OfficeItem("prop_meth_bag_01", new Vector3(115.4f, -619.35f, 205.81f), new Vector3(90f, 0.0f, 60f)),
      new ItemsManager.OfficeItem("prop_cs_whiskey_bottle", new Vector3(115.45f, -619f, 205.86f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("p_whiskey_notop_empty", new Vector3(115.6f, -619.3f, 205.78f), new Vector3(-90f, 180f, -30f)),
      new ItemsManager.OfficeItem("prop_cs_cash_note_01", new Vector3(114.65f, -620.32f, 205.877f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_cash_note_01", new Vector3(114.65f, -620.38f, 205.87f), new Vector3(0.0f, 0.0f, 50f)),
      new ItemsManager.OfficeItem("prop_cash_pile_02", new Vector3(114.78f, -620.28f, 205.87f), new Vector3(0.0f, 0.0f, -160f)),
      new ItemsManager.OfficeItem("prop_blackjack_01", new Vector3(115.2f, -619.95f, 205.87f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_panties", new Vector3(114.7f, -620.57f, 205.86f), new Vector3(0.0f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("prop_cs_amanda_shoe", new Vector3(114.5f, -620.6f, 205.87f), new Vector3(0.0f, 90f, -40f)),
      new ItemsManager.OfficeItem("prop_bong_01", new Vector3(112.6f, -620.81f, 205.5f), new Vector3(-15f, -20f, -60f)),
      new ItemsManager.OfficeItem("prop_cs_dildo_01", new Vector3(111.85f, -619.3f, 206.01f), new Vector3(90f, 0.0f, 0.0f)),
      new ItemsManager.OfficeItem("v_res_d_dildo_f", new Vector3(111.9f, -619.13f, 206.05f), new Vector3(0.0f, 0.0f, 30f))
    });

        internal static List<ItemsManager.OfficeItemsCollection> GetItemsCollection(
          ItemsManager.CollectionType type)
        {
            switch (type)
            {
                case ItemsManager.CollectionType.Normal:
                    return new List<ItemsManager.OfficeItemsCollection>()
          {
            ItemsManager.Normal1,
            ItemsManager.Normal2,
            ItemsManager.Normal3,
            ItemsManager.Normal4,
            ItemsManager.Normal5
          };
                case ItemsManager.CollectionType.Midday:
                    return new List<ItemsManager.OfficeItemsCollection>()
          {
            ItemsManager.Midday1,
            ItemsManager.Midday2,
            ItemsManager.Midday3,
            ItemsManager.Midday4,
            ItemsManager.Midday5
          };
                case ItemsManager.CollectionType.Night:
                    return new List<ItemsManager.OfficeItemsCollection>()
          {
            ItemsManager.Night1
          };
                default:
                    return new List<ItemsManager.OfficeItemsCollection>()
          {
            ItemsManager.Empty
          };
            }
        }

        internal enum CollectionType
        {
            Normal,
            Midday,
            Night,
            Empty,
        }

        internal class OfficeItemsCollection : List<ItemsManager.OfficeItem>
        {
            internal ItemsManager.CollectionType Type;

            public OfficeItemsCollection()
              : base((IEnumerable<ItemsManager.OfficeItem>)new List<ItemsManager.OfficeItem>())
            {
                this.Type = ItemsManager.CollectionType.Empty;
            }

            public OfficeItemsCollection(ItemsManager.OfficeItemsCollection itemsCollection)
              : base((IEnumerable<ItemsManager.OfficeItem>)new List<ItemsManager.OfficeItem>())
            {
                this.Type = itemsCollection.Type;
                this.AddRange((IEnumerable<ItemsManager.OfficeItem>)itemsCollection);
            }

            public OfficeItemsCollection(
              ItemsManager.CollectionType t,
              List<ItemsManager.OfficeItem> list)
              : base((IEnumerable<ItemsManager.OfficeItem>)new List<ItemsManager.OfficeItem>())
            {
                this.Type = t;
                this.AddRange((IEnumerable<ItemsManager.OfficeItem>)list);
            }

            public void InitAll()
            {
                foreach (ItemsManager.OfficeItem officeItem in (List<ItemsManager.OfficeItem>)this)
                    officeItem.Init();
            }

            public void DeleteItems()
            {
                for (int i = this.Count - 1; i >= 0; --i)
                    this.DeleteItemAt(i);
            }

            public void DeleteItemAt(int i)
            {
                if (this[i].prop.Exists())
                    this[i].prop.Delete();
                this.RemoveAt(i);
            }
        }

        internal class OfficeItem
        {
            public Vector3 position;
            public Vector3 rotation;
            public string modelName;
            public Prop prop;

            public OfficeItem(string model, Vector3 pos, Vector3 rot)
            {
                this.modelName = model;
                this.position = pos;
                this.rotation = rot;
            }

            public Prop Init()
            {
                for (int index = 0; index < 5; ++index)
                {
                    this.prop = GTA.World.CreateProp((Model)this.modelName, this.position, this.rotation, false, false);
                    if ((Entity)this.prop != (Entity)null && this.prop.Exists())
                    {
                        this.prop.FreezePosition = true;
                        this.prop.IsPersistent = true;
                        return this.prop;
                    }
                }
                Logger.Log((object)"Error: OfficeItem Init - prop is null!");
                return (Prop)null;
            }
        }
    }
    internal class Office
    {
        internal ItemsManager.OfficeItemsCollection itemsCollection;
        private Vector3 officeCameraPos = new Vector3(116f, -620.5f, 206.35f);
        private GTA.Camera officeCamera;
        private Vector3 npcPos = new Vector3(114.35f, -619.3748f, 204.5f);
        private Vector3 npcRot = new Vector3(0.0f, 0.0f, -120f);
        private string npcModel = "a_f_y_business_01";
        private Ped npc;
        private Weather officeWeather;

        public Office()
        {
            this.itemsCollection = new ItemsManager.OfficeItemsCollection(this.GetItemsCollection());
            this.BuildOffice();
        }

        public Office(ItemsManager.OfficeItemsCollection collection)
        {
            this.itemsCollection = new ItemsManager.OfficeItemsCollection(collection);
            this.BuildOffice();
        }

        private void BuildOffice()
        {
            this.officeWeather = GTA.World.Weather;
            GTA.World.Weather = Weather.Clouds;
            this.officeCamera = GTA.World.CreateCamera(this.officeCameraPos, new Vector3(0.0f, 0.0f, 0.0f), GameplayCamera.FieldOfView);
            this.npc = this.CreateNpc();
            if (this.itemsCollection.Type == ItemsManager.CollectionType.Night)
                Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)this.npc, (InputArgument)2, (InputArgument)0, (InputArgument)2, (InputArgument)0);
            this.SetNpcAI();
            foreach (ItemsManager.OfficeItem items in (List<ItemsManager.OfficeItem>)this.itemsCollection)
            {
                if ((Entity)this.npc != (Entity)null)
                {
                    Prop prop = items.Init();
                    if ((Entity)prop != (Entity)null)
                    {
                        if (prop.Exists() && this.npc.Exists())
                            this.npc.SetNoCollision((Entity)prop, true);
                    }
                    else
                        Logger.Log((object)"Error: BuildOffice Props - prop is null!");
                }
                else
                    Logger.Log((object)"Error: BuildOffice Props - npc is null!");
            }
            if ((Entity)this.npc != (Entity)null)
            {
                if (this.npc.Exists())
                    this.officeCamera.PointAt(this.npc, 12844);
            }
            else
            {
                this.officeCamera.PointAt(this.npcPos);
                Logger.Log((object)"Error: BuildOffice Camera - npc is null!");
            }
            GTA.World.RenderingCamera = this.officeCamera;
            Game.FadeScreenIn(1000);
            SE.UI.WaitAndhideUI(1000);
        }

        internal void CleanUp()
        {
            GTA.World.RenderingCamera = (GTA.Camera)null;
            this.officeCamera.IsActive = false;
            this.officeCamera.Destroy();
            GTA.World.Weather = this.officeWeather;
            if ((Entity)this.npc != (Entity)null)
            {
                if (this.npc.Exists())
                {
                    this.npc.Task.ClearAllImmediately();
                    this.npc.Delete();
                }
            }
            else
                Logger.Log((object)"Error: CleanUp - npc is null!");
            this.itemsCollection.DeleteItems();
        }

        private ItemsManager.OfficeItemsCollection GetItemsCollection()
        {
            System.Random random = new System.Random(Game.GameTime);
            List<ItemsManager.OfficeItemsCollection> officeItemsCollectionList = new List<ItemsManager.OfficeItemsCollection>();
            ItemsManager.OfficeItemsCollection itemsCollection;
            do
            {
                TimeSpan currentDayTime = GTA.World.CurrentDayTime;
                if (currentDayTime.Hours >= 2)
                {
                    currentDayTime = GTA.World.CurrentDayTime;
                    if (currentDayTime.Hours < 12)
                    {
                        officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Normal));
                        goto label_11;
                    }
                }
                currentDayTime = GTA.World.CurrentDayTime;
                if (currentDayTime.Hours >= 12)
                {
                    currentDayTime = GTA.World.CurrentDayTime;
                    if (currentDayTime.Hours < 14)
                    {
                        officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Midday));
                        officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Normal));
                        goto label_11;
                    }
                }
                currentDayTime = GTA.World.CurrentDayTime;
                if (currentDayTime.Hours >= 14)
                {
                    currentDayTime = GTA.World.CurrentDayTime;
                    if (currentDayTime.Hours < 0)
                    {
                        officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Normal));
                        goto label_11;
                    }
                }
                officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Normal));
                officeItemsCollectionList.AddRange((IEnumerable<ItemsManager.OfficeItemsCollection>)ItemsManager.GetItemsCollection(ItemsManager.CollectionType.Night));
            label_11:
                itemsCollection = officeItemsCollectionList[random.Next(0, officeItemsCollectionList.Count - 1)];
            }
            while (itemsCollection.Type == ItemsManager.CollectionType.Empty);
            return itemsCollection;
        }

        private Ped CreateNpc()
        {
            Ped ped = GTA.World.CreatePed((Model)this.npcModel, this.npcPos);
            if ((Entity)ped != (Entity)null)
            {
                if (ped.Exists())
                {
                    ped.IsPersistent = true;
                    Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)ped, (InputArgument)0, (InputArgument)0, (InputArgument)0, (InputArgument)0);
                    Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)ped, (InputArgument)2, (InputArgument)1, (InputArgument)0, (InputArgument)0);
                    Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)ped, (InputArgument)3, (InputArgument)1, (InputArgument)0, (InputArgument)0);
                    Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)ped, (InputArgument)4, (InputArgument)0, (InputArgument)1, (InputArgument)0);
                    Function.Call(Hash._0x262B14F48D29DE80, (InputArgument)ped, (InputArgument)6, (InputArgument)0, (InputArgument)0, (InputArgument)0);
                    Function.Call(Hash._0x93376B65A266EB5F, (InputArgument)ped, (InputArgument)1, (InputArgument)0, (InputArgument)0, (InputArgument)0);
                }
            }
            else
                Logger.Log((object)"Error: CreateNpc - npc is null!");
            return ped;
        }

        private void SetNpcAI()
        {
            if ((Entity)this.npc != (Entity)null)
            {
                if (this.npc.Exists())
                {
                    this.npc.Task.PlayAnimation("amb@prop_human_seat_chair@female@arms_folded@base", "base", 1f, -1, AnimationFlags.Loop);
                    this.npc.FreezePosition = true;
                    this.npc.Position = this.npcPos;
                    this.npc.Rotation = this.npcRot;
                    this.npc.Task.LookAt(this.officeCameraPos);
                }
                else
                    Logger.Log((object)"Error: SetNpcAI - npc doesn't exist!");
            }
            else
                Logger.Log((object)"Error: SetNpcAI - npc is null!");
        }

        internal void NpcSay(DialogueManager.SpeechType type)
        {
            System.Random random = new System.Random();
            List<DialogueManager.Speech> speechList = new List<DialogueManager.Speech>((IEnumerable<DialogueManager.Speech>)DialogueManager.GetSpeechList(type));
            int maxValue = speechList.Count - 1;
            int index = random.Next(0, maxValue);
            DialogueManager.Speech speech = speechList[index];
            Function.Call(Hash._0x3523634255FC3318, (InputArgument)this.npc, (InputArgument)speech.Name, (InputArgument)speech.Voice, (InputArgument)speech.Param, (InputArgument)speech.Index);
        }
    }
}
namespace MMI_SP.iFruit
{
    internal class iFruitMMI : GTA.Script
    {
        private CustomiFruit _iFruit;
        private MenuMMI _menuiFruit;
        private MenuConfig _menuConfig;
        public static bool CaniFruitInsure = false;
        public static bool CaniFruitCancel = false;
        public static bool CaniFruitRecover = true;
        public static bool CaniFruitStolen = false;
        public static bool CaniFruitPlate = false;

        public iFruitMMI()
        {
            this._iFruit = new CustomiFruit();
            this.Tick += new EventHandler(this.Initialize);
        }

        private void Initialize(object sender, EventArgs e)
        {
            while (!InsuranceObserver.Initialized)
                GTA.Script.Yield();
            this._menuiFruit = new MenuMMI();
            this._menuConfig = new MenuConfig();
            GTA.Script.Wait(2000);
            iFruitContact iFruitContact1 = new iFruitContact("Mors Mutual Insurance");
            iFruitContact1.Answered += new ContactAnsweredEvent(this.ContactAnswered);
            iFruitContact1.DialTimeout = 4000;
            iFruitContact1.Active = true;
            iFruitContact1.Icon = ContactIcon.MP_MorsMutual;
            iFruitContact iFruitContact2 = new iFruitContact(Translator.GetString("ConfigMenuContact"));
            iFruitContact2.Answered += new ContactAnsweredEvent(this.ContactAnsweredConfig);
            iFruitContact2.DialTimeout = 0;
            iFruitContact2.Active = true;
            iFruitContact2.Icon = ContactIcon.MP_FmContact;
            this._iFruit.Contacts.Add(iFruitContact1);
            this._iFruit.Contacts.Add(iFruitContact2);
            this.Tick -= new EventHandler(this.Initialize);
            this.Tick += new EventHandler(this.OnTick);
        }

        private void OnTick(object sender, EventArgs e)
        {
            try
            {
                if (this._menuiFruit != null)
                    this._menuiFruit.MenuPoolProcessMenus();
                if (this._menuConfig != null)
                    this._menuConfig.MenuPoolProcessMenus();
            }
            catch (DivideByZeroException ex)
            {
            }
            catch (Exception ex)
            {
                Logger.Log((object)ex);
            }
            this._iFruit.Update();
        }

        protected override void Dispose(bool A_0)
        {
            if (!A_0)
                return;
            this._iFruit.Contacts.ForEach((Action<iFruitContact>)(x => x.EndCall()));
        }

        internal void ContactAnswered(iFruitContact contact)
        {
            try
            {
                this._menuiFruit.Reset(true);
                this._menuiFruit.Show();
                this._menuiFruit.GetMainmenu().OnMenuClose += new MenuCloseEvent(this.MenuClosed);
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: ContactAnswered - " + ex.Message + "\r\n" + ex.StackTrace));
                GTA.UI.Notify("MMI-SP: Error with module NativeUI!");
            }
            MMISound.Play(MMISound.SoundFamily.Hello);
            this._iFruit.Close(2000);
        }

        internal void MenuClosed(object sender)
        {
            MMISound.Play(MMISound.SoundFamily.Bye);
            this._menuiFruit.GetMainmenu().OnMenuClose -= new MenuCloseEvent(this.MenuClosed);
        }

        internal void ContactAnsweredConfig(iFruitContact contact)
        {
            try
            {
                this._menuConfig.Show();
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: ContactAnswered - " + ex.Message));
                GTA.UI.Notify("MMI-SP: Error with module NativeUI!");
            }
            this._iFruit.Close();
        }
    }

    internal class MenuConfig
    {
        private static string _menuTitle = Translator.GetString("ConfigMenuTitle");
        private static readonly Point _offset = SE.UI.GetScreenCoordinatesFromFloat(0.565f, 0.2f);
        private MenuPool _menuPool;
        private UIMenu _mainMenu = new UIMenu(MenuConfig._menuTitle, Translator.GetString("ConfigMenuSubtitle"), MenuConfig._offset);
        public string MenuTitle
        {
            get => MenuConfig._menuTitle;
            set => MenuConfig._menuTitle = value;
        }
        internal void MenuPoolProcessMenus() { _menuPool.ProcessMenus(); }

        //Main Base

        public MenuConfig()
        {
            this._menuPool = new MenuPool();
            this._menuPool.Add(this._mainMenu);
            UIMenu menu1 = this.AddSubMenu(this._menuPool, this._mainMenu, MenuConfig._menuTitle, Translator.GetString("ConfigMenuItemGeneral"), MenuConfig._offset);
            this.AddMenuConfigLanguage(menu1, "Language", Config.Settings.GetValue("General", "language", "default"), Translator.GetString("ConfigMenuGeneralLanguage"));
            this.AddMenuConfigCheckbox(menu1, "General", "PersistentInsuredVehicles", InsuranceObserver.PersistentVehicles, Translator.GetString("ConfigMenuGeneralPersistent"));
            UIMenu menu2 = this.AddSubMenu(this._menuPool, this._mainMenu, MenuConfig._menuTitle, "iFruit", MenuConfig._offset);
            this.AddMenuConfigList(menu2, "iFruit", "PhoneVolume", MMISound.Volume, Translator.GetString("ConfigMenuiFruitPhoneVolume"), 0, 100, 5);
            this.AddMenuConfigCheckbox(menu2, "iFruit", "CaniFruitInsure", iFruitMMI.CaniFruitInsure, Translator.GetString("ConfigMenuiFruitInsure"));
            this.AddMenuConfigCheckbox(menu2, "iFruit", "CaniFruitCancel", iFruitMMI.CaniFruitCancel, Translator.GetString("ConfigMenuiFruitCancel"));
            this.AddMenuConfigCheckbox(menu2, "iFruit", "CaniFruitRecover", iFruitMMI.CaniFruitRecover, Translator.GetString("ConfigMenuiFruitRecover"));
            this.AddMenuConfigCheckbox(menu2, "iFruit", "CaniFruitStolen", iFruitMMI.CaniFruitStolen, Translator.GetString("ConfigMenuiFruitStolen"));
            this.AddMenuConfigCheckbox(menu2, "iFruit", "CaniFruitPlate", iFruitMMI.CaniFruitPlate, Translator.GetString("ConfigMenuiFruitPlate"));
            UIMenu menu3 = this.AddSubMenu(this._menuPool, this._mainMenu, MenuConfig._menuTitle, Translator.GetString("ConfigMenuItemNotify"), MenuConfig._offset);
            this.AddMenuConfigCheckbox(menu3, "Check", "CheckForUpdate", MMI.CheckForUpdate, Translator.GetString("ConfigMenuNotifyUpdate"));
            this.AddMenuConfigCheckbox(menu3, "Check", "ShowSHVDNNotification", MMI.ShowSHVDNNotification, Translator.GetString("ConfigMenuNotifySHVDN"));
            this.AddMenuConfigCheckbox(menu3, "Check", "ShowFileNotification", MMI.ShowFileNotification, Translator.GetString("ConfigMenuNotifyFile"));
            this.AddMenuConfigCheckbox(menu3, "Check", "ShowVisualCNotification", MMI.ShowSHVDNNotification, Translator.GetString("ConfigMenuNotifyVisualC"));
            this.AddMenuConfigCheckbox(menu3, "Check", "ShowNETFrameworkNotification", MMI.ShowSHVDNNotification, Translator.GetString("ConfigMenuNotifyNET"));
            menu3.AddItem(new UIMenuItem(Translator.GetString("ConfigMenuNotifyReboot"))
            {
                Enabled = false
            });
            UIMenu menu4 = this.AddSubMenu(this._menuPool, this._mainMenu, MenuConfig._menuTitle, Translator.GetString("ConfigMenuItemInsurance"), MenuConfig._offset);
            this.AddMenuConfigList(menu4, "Insurance", "InsuranceCostMultiplier", InsuranceManager.InsuranceMult, this.GetCostMultiplierDescription("InsuranceCostMultiplier"), 0.0f, 100f, 0.1f);
            this.AddMenuConfigList(menu4, "Insurance", "RecoverCostMultiplier", InsuranceManager.RecoverMult, this.GetCostMultiplierDescription("RecoverCostMultiplier"), 0.0f, 100f, 0.1f);
            this.AddMenuConfigList(menu4, "Insurance", "StolenCostMultiplier", InsuranceManager.StolenMult, this.GetCostMultiplierDescription("StolenCostMultiplier"), 0.0f, 100f, 0.1f);
            UIMenu menu5 = this.AddSubMenu(this._menuPool, this._mainMenu, MenuConfig._menuTitle, Translator.GetString("ConfigMenuItemBringVehicle"), MenuConfig._offset);
            this.AddMenuConfigList(menu5, "BringVehicle", "BringVehicleBasePrice", InsuranceManager.BringVehicleBasePrice, Translator.GetString("ConfigMenuBringVehiclePrice"), 0, 2000, 50);
            this.AddMenuConfigBringVehicleInstant(menu5, "BringVehicle", "BringVehicleInstant", InsuranceManager.BringVehicleInstant, "");
            this.AddMenuConfigList(menu5, "BringVehicle", "BringVehicleRadius", InsuranceObserver.BringVehicleRadius, Translator.GetString("ConfigMenuBringVehicleRadius"), 10, 2000, 5);
            this.AddMenuConfigList(menu5, "BringVehicle", "BringVehicleTimeout", InsuranceObserver.BringVehicleTimeout, Translator.GetString("ConfigMenuBringVehicleTimeout"), 1, 30, 1);
        }

        internal void Show()
        {
            this._mainMenu.Visible = true;
            Function.Call(Hash._0xFC695459D4D0E219, (InputArgument)0.5f, (InputArgument)0.5f);
        }



        private void AddMenuConfigLanguage(UIMenu menu, string key, string value, string description)
        {
            bool found = false;
            int counter = 0;
            List<dynamic> languages = new List<dynamic>();

            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\MMI\\", "*.xml"))
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Name != "db.xml")
                {
                    string name = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    languages.Add(name);
                    if (!found)
                        if (string.Compare(value, name, true) == 0)
                            found = true;
                        else
                            counter++;
                }
            }

            UIMenuListItem listItem = new UIMenuListItem(key, languages, counter, description);
            menu.AddItem(listItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == listItem)
                {
                    Config.Settings.SetValue("General", key, item.Items[index].ToString());
                    Config.Settings.Save();
                }
            };
        }

        private void AddMenuConfigBringVehicleInstant(UIMenu menu, string section, string key, bool isChecked, string description)
        {
            string textTrue = T.GetString("ConfigMenuBringVehicleInstantTrue");
            string textFalse = T.GetString("ConfigMenuBringVehicleInstantFalse");

            UIMenuCheckboxItem notifyItem = new UIMenuCheckboxItem(key, isChecked, description);
            if (notifyItem.Checked)
                notifyItem.Description = textTrue;
            else
                notifyItem.Description = textFalse;

            menu.AddItem(notifyItem);
            menu.OnCheckboxChange += (sender, item, index) =>
            {
                if (item == notifyItem)
                {
                    Config.Settings.SetValue(section, key, item.Checked);
                    Config.UpdateValue(key, item.Checked);
                    Config.Settings.Save();
                    if (item.Checked)
                        item.Description = textTrue;
                    else
                        item.Description = textFalse;
                }
            };
        }

        private void AddMenuConfigList(UIMenu menu, string section, string key, float value, string description, float startValue, float stopValue, float increment)
        {
            bool found = false;
            int counter = 0;
            List<dynamic> values = new List<dynamic>();

            for (float i = startValue; i < stopValue; i += increment)
            {
                values.Add(Math.Round(i, 1, MidpointRounding.AwayFromZero));
                if (!found)
                    if (Math.Round(value, 1, MidpointRounding.AwayFromZero) == Math.Round(i, 1, MidpointRounding.AwayFromZero))
                        found = true;
                    else
                        counter++;
            }

            UIMenuListItem listItem = new UIMenuListItem(key, values, counter, description);
            menu.AddItem(listItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == listItem)
                {
                    Config.Settings.SetValue(section, key, ((float)item.Items[index]).ToString().ToString().Replace(",", "."));
                    Config.UpdateValue(key, (float)item.Items[index]);
                    Config.Settings.Save();
                }
            };
        }
        private void AddMenuConfigList(UIMenu menu, string section, string key, int value, string description, int startValue, int stopValue, int increment)
        {
            bool found = false;
            int counter = 0;
            List<dynamic> values = new List<dynamic>();

            for (int i = startValue; i <= stopValue; i += increment)
            {
                values.Add(i);
                if (!found)
                    if (value == i)
                        found = true;
                    else
                        counter++;
            }

            UIMenuListItem listItem = new UIMenuListItem(key, values, counter, description);
            menu.AddItem(listItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == listItem)
                {
                    Config.Settings.SetValue(section, key, (int)item.Items[index]);
                    Config.UpdateValue(key, (int)item.Items[index]);
                    Config.Settings.Save();
                }
            };
        }
        private void AddMenuConfigCheckbox(UIMenu menu, string section, string key, bool isChecked, string description)
        {
            UIMenuCheckboxItem notifyItem = new UIMenuCheckboxItem(key, isChecked, description);
            menu.AddItem(notifyItem);
            menu.OnCheckboxChange += (sender, item, index) =>
            {
                if (item == notifyItem)
                {
                    Config.Settings.SetValue(section, key, item.Checked);
                    Config.UpdateValue(key, item.Checked);
                    Config.Settings.Save();
                }
            };
        }

        private string GetCostMultiplierDescription(string costType)
        {
            GTA.Vehicle playerVehicle = Game.Player.LastVehicle;
            if (playerVehicle != null)
            {
                if (string.Compare(costType, "InsuranceCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceInsurance") + " " + T.GetString("ConfigMenuInsuranceInsuranceEx");
                else if (string.Compare(costType, "RecoverCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceRecover") + " " + T.GetString("ConfigMenuInsuranceRecoverEx");
                else if (string.Compare(costType, "StolenCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceStolen") + " " + T.GetString("ConfigMenuInsuranceStolenEx");
            }
            else
            {
                if (string.Compare(costType, "InsuranceCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceInsurance");
                else if (string.Compare(costType, "RecoverCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceRecover");
                else if (string.Compare(costType, "StolenCostMultiplier", true) == 0)
                    return T.GetString("ConfigMenuInsuranceStolen");
            }
            return "";
        }

        // Workaround since NativeUI.MenuPool doesn't have an AddSubMenu function supporting menu Offset
        private UIMenu AddSubMenu(MenuPool pool, UIMenu menu, string title, string text, Point offset)
        {
            var item = new UIMenuItem(text);
            menu.AddItem(item);
            var submenu = new UIMenu(title, text, offset);
            pool.Add(submenu);
            menu.BindMenuToItem(submenu, item);
            return submenu;
        }

    }
    internal static class MMISound
    {
        private static int _volume = 25;
        private static Random _rnd = new Random();
        private static List<UnmanagedMemoryStream> _helloList = new List<UnmanagedMemoryStream>()
    {
      Resources.Start_HelloThisIsMMI,
      Resources.Start_MMIExpectUnexpected,
      Resources.Start_MMIHereToHelp,
      Resources.Start_MMIHowCanHelp,
      Resources.Start_MMIHowCanIBeService,
      Resources.Start_MMIPeaceOfMind,
      Resources.Start_MMITrust,
      Resources.Start_WhatCanIDo,
      Resources.Start_WhatCanIHelpYouWith
    };
        private static List<UnmanagedMemoryStream> _byeList = new List<UnmanagedMemoryStream>()
    {
      Resources.End_ByeNow,
      Resources.End_DriveSafe,
      Resources.End_NiceDay,
      Resources.End_NiveDay2,
      Resources.End_SoLong,
      Resources.End_StaySafe
    };
        private static List<UnmanagedMemoryStream> _okayList = new List<UnmanagedMemoryStream>()
    {
      Resources.Mid_ICanDoThat,
      Resources.Mid_ILookIntoit,
      Resources.Mid_IWillDoMyBest,
      Resources.Mid_Okay,
      Resources.Mid_Sure,
      Resources.Mid_WeCanDoThat,
      Resources.Mid_WeCanHandleThat
    };
        private static List<UnmanagedMemoryStream> _noMoneyList = new List<UnmanagedMemoryStream>()
    {
      Resources.NoMoney
    };

        public static int Volume
        {
            get => MMISound._volume;
            set => MMISound._volume = value;
        }

        public static void Play(MMISound.SoundFamily family)
        {
            List<UnmanagedMemoryStream> unmanagedMemoryStreamList = new List<UnmanagedMemoryStream>();
            switch (family)
            {
                case MMISound.SoundFamily.Hello:
                    unmanagedMemoryStreamList.AddRange((IEnumerable<UnmanagedMemoryStream>)MMISound._helloList);
                    break;
                case MMISound.SoundFamily.Okay:
                    unmanagedMemoryStreamList.AddRange((IEnumerable<UnmanagedMemoryStream>)MMISound._okayList);
                    break;
                case MMISound.SoundFamily.Bye:
                    unmanagedMemoryStreamList.AddRange((IEnumerable<UnmanagedMemoryStream>)MMISound._byeList);
                    break;
                case MMISound.SoundFamily.NoMoney:
                    unmanagedMemoryStreamList.AddRange((IEnumerable<UnmanagedMemoryStream>)MMISound._noMoneyList);
                    break;
            }
            int index = MMISound._rnd.Next(0, unmanagedMemoryStreamList.Count - 1);
            try
            {
                UnmanagedMemoryStream baseStream = unmanagedMemoryStreamList[index];
                baseStream.Position = 0L;
                WaveStream waveStream = new WaveStream((Stream)baseStream);
                if (MMISound._volume < 0)
                    MMISound._volume = 0;
                if (MMISound._volume > 100)
                    MMISound._volume = 100;
                waveStream.Volume = MMISound._volume;
                new SoundPlayer((Stream)waveStream).Play();
            }
            catch (Exception ex)
            {
                Logger.Log((object)("Error: MMISound.Play - " + family.ToString() + " n°" + index.ToString() + ". " + ex.Message));
            }
        }

        public enum SoundFamily
        {
            Hello,
            Okay,
            Bye,
            NoMoney,
        }
    }
}
namespace MMI_SP.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (MMI_SP.Properties.Resources.resourceMan == null)
                    MMI_SP.Properties.Resources.resourceMan = new ResourceManager("MMI_SP.Properties.Resources", typeof(MMI_SP.Properties.Resources).Assembly);
                return MMI_SP.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => MMI_SP.Properties.Resources.resourceCulture;
            set => MMI_SP.Properties.Resources.resourceCulture = value;
        }

        internal static string _default
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetString(nameof(_default), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static Bitmap banner
        {
            get
            {
                return (Bitmap)MMI_SP.Properties.Resources.ResourceManager.GetObject(nameof(banner), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static string config
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetString(nameof(config), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_ByeNow
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_ByeNow), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_DriveSafe
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_DriveSafe), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_NiceDay
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_NiceDay), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_NiveDay2
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_NiveDay2), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_SoLong
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_SoLong), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream End_StaySafe
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(End_StaySafe), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static Bitmap insurance
        {
            get
            {
                return (Bitmap)MMI_SP.Properties.Resources.ResourceManager.GetObject(nameof(insurance), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Mid_ICanDoThat
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_ICanDoThat), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Mid_ILookIntoit
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_ILookIntoit), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Mid_IWillDoMyBest
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_IWillDoMyBest), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Mid_Okay
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_Okay), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream Mid_Sure
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_Sure), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream Mid_WeCanDoThat
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_WeCanDoThat), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Mid_WeCanHandleThat
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Mid_WeCanHandleThat), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream NoMoney
        {
            get => MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(NoMoney), MMI_SP.Properties.Resources.resourceCulture);
        }

        internal static UnmanagedMemoryStream Start_HelloThisIsMMI
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_HelloThisIsMMI), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMIExpectUnexpected
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMIExpectUnexpected), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMIHereToHelp
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMIHereToHelp), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMIHowCanHelp
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMIHowCanHelp), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMIHowCanIBeService
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMIHowCanIBeService), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMIPeaceOfMind
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMIPeaceOfMind), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_MMITrust
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_MMITrust), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_WhatCanIDo
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_WhatCanIDo), MMI_SP.Properties.Resources.resourceCulture);
            }
        }

        internal static UnmanagedMemoryStream Start_WhatCanIHelpYouWith
        {
            get
            {
                return MMI_SP.Properties.Resources.ResourceManager.GetStream(nameof(Start_WhatCanIHelpYouWith), MMI_SP.Properties.Resources.resourceCulture);
            }
        }
    }
}



