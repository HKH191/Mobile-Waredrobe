using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using System.Windows.Forms;
using GTA.Native;
using GTA.Math;
using NativeUI;
using System.Drawing;
using iFruitAddon2;



namespace MobileWareDrobe
{
    public class Class1 :Script
    {
        public MenuPool Woredrobepool;
        public UIMenu WoredrobeMainMenu;
        public UIMenu WoredrobeMenu;
        public int CompMax;
        public int DrawableMax;
        public bool InShower;
        public bool sitting;
        ScriptSettings Config;
        public WeaponTint Liv;
        public int ID_O;
        public string ID_C;
        public int Comp;
        public Model OldCharacter;

        public string HostName;
        public BlipColor Blip_Colour;
        public string Uicolour;
        public System.Drawing.Color    MarkerColor;
        public string MarkerColorString;
        public int Casino_level;
        ScriptSettings ScriptConfig;
        public bool IsScriptEnabled;
        public List<Vector3> SeatPos = new List<Vector3>();
        public List<float> SeatRot = new List<float>();
        iFruitAddon2.CustomiFruit ifruit;
    
        public Class1()
        {

            ifruit = new iFruitAddon2.CustomiFruit()
            {
                CenterButtonColor = System.Drawing.Color.Orange,
                LeftButtonColor = System.Drawing.Color.LimeGreen,
                RightButtonColor = System.Drawing.Color.Purple,
                CenterButtonIcon = iFruitAddon2.SoftKeyIcon.Fire,
                LeftButtonIcon = iFruitAddon2.SoftKeyIcon.Police,
                RightButtonIcon = iFruitAddon2.SoftKeyIcon.Website
            };


            ifruit.SetWallpaper(new Wallpaper("char_facebook"));
            //or..
            ifruit.SetWallpaper(Wallpaper.BadgerDefault);

            var contact = new iFruitContact("Mobile Waredrobe");
            contact.Answered += loadMenu;
            contact.DialTimeout = 3000;
            contact.Active = true;

            //set custom icons by instantiating the ContactIcon class
            //set custom icons by instantiating the ContactIcon class
            contact.Icon = ContactIcon.Multiplayer;
            contact.Name = "Mobile Waredrobe";

            ifruit.Contacts.Add(contact);
            Interval = 1;
            Tick += OnTick;
            Aborted += OnShutdown;
            KeyDown += OnKeyDown;
            Setup();
        }
        public int CheckClothes(int T, int RComp, int RDraw, Ped P )
        {
            int Max = 0;
            if (T == 1)//check Drawable
            {
                if (Function.Call<bool>(Hash.IS_PED_COMPONENT_VARIATION_VALID, P, RComp, RDraw) == true)
                    Max = Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, P, RComp);
            }
            if (T == 2)//Check Texture
            {
                if (Function.Call<bool>(Hash.IS_PED_COMPONENT_VARIATION_VALID, P, RComp, RDraw) == true)
                    Max = Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, P, RComp, RDraw);
            }

            return Max;
        }
        public void Setoutfit( int i)
        {
          // Function.Call(Hash.SET_PED_PROP_INDEX, p, 0, 0, 0, 0);

            if (Game.Player.Character.Model != PedHash.FreemodeMale01)
                OldCharacter = Game.Player.Character.Model;

            Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, Game.Player.Character, 1.0);
            Function.Call(Hash.RESET_PED_STRAFE_CLIPSET, Game.Player.Character);
            var characterModel = new Model(PedHash.FreemodeMale01);
            characterModel.Request(500);
            if (characterModel.IsInCdImage && characterModel.IsValid)
            {
                while (!characterModel.IsLoaded) Script.Wait(100);
                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);

            }
            characterModel.MarkAsNoLongerNeeded();
            var ped = Game.Player.Character;
            Function.Call(Hash.SET_PED_PROP_INDEX, ped, -1, 0, 0, 17);//hat
            bool Found = false;
            string Color = ID_C;
            if (i == 0)
            {
                #region Solder Outfits
                if (Color.Equals("Outfit Default"))
                {
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Purple"))
                {
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 6, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 3, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Pink"))
                {
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 7, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Orange"))
                {
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 5, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }

                if (Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Soldier
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 125, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                #endregion
            }

            if (i == 1)
            {
                #region Cloaker Outfits
                if (Color.Equals("Outfit Default"))
                {
                    Found = true;
                    #region Cloaker
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 89, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 55, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 54, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Black"))
                {
                    Found = true;
                    #region Cloaker
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 89, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 55, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 54, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }

                #endregion
            }

            if (i == 2)
            {
                if (Color.Equals("Outfit Default"))
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Hacker 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Random Ran = new Random();
                    int Mask = Ran.Next(1, 100);
                    if (Mask <= 25)
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 38, 0, 1);
                    if (Mask > 25 && Mask <= 50)
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 112, 0, 1);
                    if (Mask > 50 && Mask <= 75)
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 46, 0, 1);
                    if (Mask > 75)
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 104, 25, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 125, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 68, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
            }
            if (i == 3)
            {
                #region Juggernaut
                if (Color.Equals("Blue"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 3, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 6, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 6, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 6, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);
                }
                if (Color.Equals("Green"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                if (Color.Equals("Red"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 5, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 5, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 5, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 5, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                if (Color.Equals("Orange"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);

                }
                if (Color.Equals("Purple"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 3, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 6, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 3, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                if (Color.Equals("Pink"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                if (Color.Equals("White"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 9, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 9, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 9, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 9, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                if (Color.Equals("Black") || Color.Equals("Outfit Default"))
                {
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 1, 91, 10, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 3, 46, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 4, 84, 10, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 6, 10, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 8, 97, 10, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 11, 186, 10, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, 12, 0, 0, 1);


                }
                #endregion
            }
            if (i == 4)
            {

                if (Color.Equals("Black"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 0, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 1, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 1, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 6, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("White"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 10, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 9, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 4, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 7, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 7, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 12, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 2, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 8, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Orange"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 13, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 9, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Purple"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 14, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 10, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Pink"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 15, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 11, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Red"))
                {
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 10, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 14, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }

                if (Color.Equals("Outfit Default") || Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Arenawar 1
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 275, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }

            }
            if (i == 5)
            {

                if (Color.Equals("Black"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 0, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 1, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 1, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 5, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("White"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 10, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 9, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 4, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 2, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 7, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 12, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 2, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 9, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Orange"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 13, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 6, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Purple"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 14, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 11, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Pink"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 15, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 10, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }
                if (Color.Equals("Red"))
                {
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 6, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 10, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 4, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }

                if (Color.Equals("Outfit Default") || Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Arenawar 2
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 142, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 19, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 107, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 84, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 3, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 276, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion
                }


            }
            if (i == 6)
            {
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Space Marine
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 134, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 147, 0, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 167, 0, 1);//Gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 113, 0, 1);//Legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 90, 0, 1);//Shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//Shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 286, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);

                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 1, 0, 135, 0);//hat
                    #endregion
                }
                if (Color.Equals("Outfit Default") || Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Space Marine
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 134, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 147, 0, 1);//Mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 167, 0, 1);//Gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 113, 0, 1);//Legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 90, 0, 1);//Shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//Shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 286, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);

                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 1, 0, 135, 0);//hat
                    #endregion
                }

            }
            if (i == 7)
            {
                #region Commando Outfits
                if (Color.Equals("Outfit Default"))
                {
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 4, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 1, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Purple"))
                {
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 6, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 3, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Pink"))
                {
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 7, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 4, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Orange"))
                {
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 5, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 2, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }

                if (Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }
                    Found = true;
                    #region Commando
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 115, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 17, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 69, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 128, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 130, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                #endregion
            }
            if (i == 8)
            {
                #region SpaceSuit Outfits

                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 8, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 8, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 8, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 8, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Purple"))
                {
                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 10, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 10, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 10, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 10, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 10, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Pink"))
                {
                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 11, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 11, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 11, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 11, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 11, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Orange"))
                {
                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 9, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 9, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 9, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 9, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 9, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 9, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }

                if (Found == false)
                {
                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 0, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Outfit Default") || Found == false)
                {
                    if (Found == false && !Color.Equals("Outfit Default"))
                    { UI.Notify("~y~Warning~w~ this Outfit did not have the specified color, setting to default, color chosen : " + Color); }

                    Found = true;
                    #region Space Suit
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 133, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 108, 0, 1);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 166, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 110, 0, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 88, 0, 1);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 2, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 283, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                #endregion
            }
            if (i == 9)
            {
                #region Tron Outfits
                if (Color.Equals("Outfit Default"))
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 0, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 0, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Green"))
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 1, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 1, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 1, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 1, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("White"))
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 9, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 7, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 7, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 7, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Blue"))
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 3, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 3, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 3, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 3, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                if (Color.Equals("Black"))
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 10, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 10, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 10, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 10, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }

                if (Found == false)
                {
                    Found = true;
                    #region Tron
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, 1);
                    Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 91, 0, 17);//hat
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 0, 0, 0);//mask
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 130, 0, 1);//gloves
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 77, 0, 1);//legs
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 55, 0, 0);//shoes
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 130, 0, 1);//shirt
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, 1);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 178, 0, 1);//Jacket
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
                    #endregion //Used Yes
                }
                #endregion
            }
        }
        void LoadIniFile(string iniName)
        {
            try
            {
                Config = ScriptSettings.Load(iniName);
              

            }

            catch (Exception e)
            {
                UI.Notify("~r~Error~w~:Casino.ini Failed To Load.");
            }
        }
        public void Setup()
        {

        
   


         



            Woredrobepool = new MenuPool();
            WoredrobeMainMenu = new UIMenu("Wardrobe", "Select an Option");
            Woredrobepool.Add(WoredrobeMainMenu);
            WoredrobeMenu = Woredrobepool.AddSubMenu(WoredrobeMainMenu, "Change Clothes");

        
            WareDrobe();
         
        }
        private void OnShutdown(object sender, EventArgs e)
        {
            var A_0 = true;
            if (A_0)
            {

                if (Game.Player.Character.Model == PedHash.FreemodeMale01)
                {
                    if (OldCharacter != null)
                    {
                        var characterModel = new Model(OldCharacter.Hash);
                        characterModel.Request(500);
                        if (characterModel.IsInCdImage && characterModel.IsValid)
                        {
                            while (!characterModel.IsLoaded) Script.Wait(100);
                            Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                            Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, Game.Player.Character.Handle);
                        }
                        characterModel.MarkAsNoLongerNeeded();

                        Game.Player.MaxArmor = 400;
                        Game.Player.Character.Armor = 0;
                        Game.Player.Character.Health = 200;
                        Game.Player.Character.MaxHealth = 200;
                    }

                }

            }
        }
        public void WareDrobe()
        {
            var SV = new List<dynamic>();
            SV.Add("Save");
            SV.Add("Load");

            var Slots = new List<dynamic>();
            Slots.Add("Slot1.ini");
            Slots.Add("Slot2.ini");
            Slots.Add("Slot3.ini");
            Slots.Add("Slot4.ini");
            Slots.Add("Slot5.ini");
            Slots.Add("Slot6.ini");
            Slots.Add("Slot7.ini");
            Slots.Add("Slot8.ini");
            Slots.Add("Slot9.ini");
            Slots.Add("Slot10.ini");

            var MeleeW = new List<dynamic>();
            MeleeW.Add(WeaponHash.Unarmed);
            MeleeW.Add(WeaponHash.Knife);
            MeleeW.Add(WeaponHash.Nightstick);
            MeleeW.Add(WeaponHash.Hammer);
            MeleeW.Add(WeaponHash.Hatchet);
            MeleeW.Add(WeaponHash.KnuckleDuster);
            MeleeW.Add(WeaponHash.Machete);
            MeleeW.Add(WeaponHash.PoolCue);
            MeleeW.Add(WeaponHash.Wrench);
            MeleeW.Add(WeaponHash.SwitchBlade);
            MeleeW.Add(WeaponHash.GolfClub);
            MeleeW.Add(WeaponHash.Flashlight);
            var Colours = new List<dynamic>();
            Colours.Add("Outfit Default");
            Colours.Add("Blue");
            Colours.Add("Green");
            Colours.Add("Red");
            Colours.Add("Orange");
            Colours.Add("Pink");
            Colours.Add("Purple");
            Colours.Add("White");
            Colours.Add("Black");
            var Outfits = new List<dynamic>();
            Outfits.Add("Soldier");
            Outfits.Add("Cloaker");
            Outfits.Add("Hacker");
            Outfits.Add("Juggernaut");
            Outfits.Add("Arena War A");
            Outfits.Add("Arena War B");
            Outfits.Add("Space Marine");
            Outfits.Add("Commando");
            Outfits.Add("Space Suit");
            Outfits.Add("Tron");
            var DrawUI = new List<dynamic>();
            var Draw = new List<dynamic>();

            var TexUI = new List<dynamic>();
            var Tex = new List<dynamic>();
            for (int i = 0; i < 999; i++)
            {
                Tex.Add(i);
                Draw.Add(i);
                TexUI.Add(i);
                DrawUI.Add(i);
            }
            var TF = new List<dynamic>();
            TF.Add("False");
            TF.Add("True");

            var OVM = new List<dynamic>();
            OVM.Add(PedHash.FreemodeFemale01);
            OVM.Add(PedHash.FreemodeMale01);
            OVM.Add(PedHash.Franklin);
            OVM.Add(PedHash.Trevor);
            OVM.Add(PedHash.Michael);


            var Clothes = new List<dynamic>();
            Clothes.Add(" 0 FACE");
            Clothes.Add("1 BEARD");
            Clothes.Add("2 HAIRCUT");
            Clothes.Add("3 SHIRT");
            Clothes.Add("4 PANTS");
            Clothes.Add("5 Hands / Gloves");
            Clothes.Add("6 SHOES");
            Clothes.Add("7 Eyes");
            Clothes.Add("8 Accessories");
            Clothes.Add("9 Mission Items/ Tasks");
            Clothes.Add("10 Decals");
            Clothes.Add("11 Collars and Inner Shirts");

            UIMenu submenu1 = Woredrobepool.AddSubMenu(WoredrobeMenu, "Change Outfit");
            UIMenuListItem O = new UIMenuListItem("", Outfits, 0);
            submenu1.AddItem(O);
            O.Description = "While Using this outfit you will not be able to purchase anything due to being the MP male";
            UIMenuListItem C = new UIMenuListItem("Color : ", Colours, 0);
            submenu1.AddItem(C);
            C.Description = "Use this Colour Whenever possible or use Default";
            UIMenuItem Set = new UIMenuItem("Wear Outfit ");
            submenu1.AddItem(Set);
            Set.Description = "~y~ Warning ~w~ Lag is normal while applying outfits, simple alt tab out to avoid crash";
            UIMenuItem Remove = new UIMenuItem("Remove Outift ");
            submenu1.AddItem(Remove);

            UIMenu submenu0 = Woredrobepool.AddSubMenu(WoredrobeMenu, "Change Clothes");

            UIMenu submenu2 = Woredrobepool.AddSubMenu(WoredrobeMenu, "Save/Load Outfit");
            UIMenuListItem SVL = new UIMenuListItem("Function ", SV, 0);
            submenu2.AddItem(SVL);
            UIMenuListItem Sl = new UIMenuListItem("Slot ", Slots, 0);
            submenu2.AddItem(Sl);
            UIMenuItem Get = new UIMenuItem("Save");
            submenu2.AddItem(Get);
            submenu2.OnItemSelect += (sender, item, index) => //Normal Weapons
            {
                if (item == Get)
                {
                    string LP = "";
                    if (Game.Player.Character.Model == PedHash.Franklin)
                    { LP = "scripts//MobileWaredobe//Waredrobe//Franklin//"; }
                    if (Game.Player.Character.Model == PedHash.Michael)
                    { LP = "scripts//MobileWaredobe//Waredrobe//Michael//"; }
                    if (Game.Player.Character.Model == PedHash.Trevor)
                    { LP = "scripts//MobileWaredobe//Waredrobe//Trevor//"; }
                    if (Game.Player.Character.Model == PedHash.FreemodeFemale01 || Game.Player.Character.Model == PedHash.FreemodeMale01)
                    { LP = "scripts//MobileWaredobe//Waredrobe//Mp//"; }
                    if (Game.Player.Character.Model != PedHash.FreemodeFemale01 && Game.Player.Character.Model != PedHash.FreemodeMale01 && Game.Player.Character.Model != PedHash.Franklin && Game.Player.Character.Model != PedHash.Trevor && Game.Player.Character.Model != PedHash.Michael)
                    { LP = "scripts//MobileWaredobe//Waredrobe//AnyPed//"; }
                    if (SVL.Index == 0)
                    {
                        Ped ped = Game.Player.Character;
                        Get.Text = "Save";
                        LoadIniFile(LP + Slots[Sl.Index]);

                        int HatComp = Function.Call<int>(Hash.GET_PED_PROP_INDEX, ped, 0);
                        int HatTex = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, ped, 0);
                        int HatPalette = Function.Call<int>(Hash.GET_PED_PALETTE_VARIATION, ped, 0);
                        Config.SetValue<int>("-1 HAT", "Hat_Drawable", HatComp);
                        Config.SetValue<int>("-1 Hat", "Hat_Tex", HatTex);
                        Config.SetValue<int>("-1 Hat", "Hat_Palette", HatPalette);

                        int Head_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 0);//Head
                        int Head_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 0);//
                        Config.SetValue<int>("0 FACE", "Head_Drawable", Head_Drawable);
                        Config.SetValue<int>("0 FACE", "Head_Palette", Head_Palette);


                        int BEARD_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 1);//Face
                        int BEARD_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 1);//
                        Config.SetValue<int>("1 BEARD", "BEARD_Drawable", BEARD_Drawable);
                        Config.SetValue<int>("1 BEARD", "BEARD_Palette", BEARD_Palette);


                        int HAIRCUT_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 2);//Face
                        int HAIRCUT_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 2);//
                        Config.SetValue<int>("2 HAIRCUT", "HAIRCUT_Drawable", HAIRCUT_Drawable);
                        Config.SetValue<int>("2 HAIRCUT", "HAIRCUT_Palette", HAIRCUT_Palette);



                        int SHIRT_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 3);//Face
                        int SHIRT_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 3);//
                        Config.SetValue<int>("3 SHIRT", "SHIRT_Drawable", SHIRT_Drawable);
                        Config.SetValue<int>("3 SHIRT", "SHIRT_Palette", SHIRT_Palette);



                        int PANTS_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 4);//Face
                        int PANTS_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 4);//
                        Config.SetValue<int>("4 PANTS", "PANTS_Drawable", PANTS_Drawable);
                        Config.SetValue<int>("4 PANTS", "PANTS_Palette", PANTS_Palette);



                        int Gloves_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 5);//Face
                        int Gloves_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 5);//
                        Config.SetValue<int>("5 Hands / Gloves", "Gloves_Drawable", Gloves_Drawable);
                        Config.SetValue<int>("5 Hands / Gloves", "Gloves_Palette", Gloves_Palette);


                        int SHOES_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 6);//Face
                        int SHOES_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 6);//
                        Config.SetValue<int>("6 SHOES", "SHOES_Drawable", SHOES_Drawable);
                        Config.SetValue<int>("6 SHOES", "SHOES_Palette", SHOES_Palette);


                        int Eyes_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 7);//Face
                        int Eyes_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 7);//
                        Config.SetValue<int>("7 Eyes", "Eyes_Drawable", Eyes_Drawable);
                        Config.SetValue<int>("7 Eyes", "Eyes_Palette", Eyes_Palette);


                        int Accessories_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 8);//Face
                        int Accessories_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 8);//
                        Config.SetValue<int>("8 Accessories", "Accessories_Drawable", Accessories_Drawable);
                        Config.SetValue<int>("8 Accessories", "Accessories_Palette", Accessories_Palette);




                        int Mission_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 9);//Face
                        int Mission_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 9);//
                        Config.SetValue<int>("9 Mission Items/ Tasks", "Mission_Drawable", Mission_Drawable);
                        Config.SetValue<int>("9 Mission Items/ Tasks", "Mission_Palette", Mission_Palette);



                        int Decals_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 10);//Face
                        int Decals_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 10);//
                        Config.SetValue<int>("10 Decals", "Decals_Drawable", Decals_Drawable);
                        Config.SetValue<int>("10 Decals", "Decals_Palette", Decals_Palette);

                        int InnerShirts_Drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 11);//Face
                        int InnerShirts_Palette = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, 11);//
                        Config.SetValue<int>("11 Collars and Inner Shirts", "InnerShirts_Drawable", InnerShirts_Drawable);
                        Config.SetValue<int>("11 Collars and Inner Shirts", "InnerShirts_Palette", InnerShirts_Palette);
                        Config.Save();
                        UI.Notify("Outfit saved!");
                    }
                    if (SVL.Index == 1)
                    {
                        Get.Text = "Load";
                        Ped ped = Game.Player.Character;
                        UI.Notify(LP);
                        LoadIniFile(LP + Slots[Sl.Index]);
                        int Head_Drawable = 0; Head_Drawable = Config.GetValue<int>("0 FACE", "Head_Drawable", Head_Drawable);
                        int Head_Palette = 0; Head_Palette = Config.GetValue<int>("0 FACE", "Head_Palette", Head_Palette);




                        int BEARD_Drawable = 0; BEARD_Drawable = Config.GetValue<int>("1 BEARD", "BEARD_Drawable", BEARD_Drawable);
                        int BEARD_Palette = 0; BEARD_Palette = Config.GetValue<int>("1 BEARD", "BEARD_Palette", BEARD_Palette);




                        int HAIRCUT_Drawable = 0; HAIRCUT_Drawable = Config.GetValue<int>("2 HAIRCUT", "HAIRCUT_Drawable", HAIRCUT_Drawable);
                        int HAIRCUT_Palette = 0; HAIRCUT_Palette = Config.GetValue<int>("2 HAIRCUT", "HAIRCUT_Palette", HAIRCUT_Palette);





                        int SHIRT_Drawable = 0; SHIRT_Drawable = Config.GetValue<int>("3 SHIRT", "SHIRT_Drawable", SHIRT_Drawable);
                        int SHIRT_Palette = 0; SHIRT_Palette = Config.GetValue<int>("3 SHIRT", "SHIRT_Palette", SHIRT_Palette);





                        int PANTS_Drawable = 0; PANTS_Drawable = Config.GetValue<int>("4 PANTS", "PANTS_Drawable", PANTS_Drawable);
                        int PANTS_Palette = 0; PANTS_Palette = Config.GetValue<int>("4 PANTS", "PANTS_Palette", PANTS_Palette);





                        int Gloves_Drawable = 0; Gloves_Drawable = Config.GetValue<int>("5 Hands / Gloves", "Gloves_Drawable", Gloves_Drawable);
                        int Gloves_Palette = 0; Gloves_Palette = Config.GetValue<int>("5 Hands / Gloves", "Gloves_Palette", Gloves_Palette);




                        int SHOES_Drawable = 0; SHOES_Drawable = Config.GetValue<int>("6 SHOES", "SHOES_Drawable", SHOES_Drawable);
                        int SHOES_Palette = 0; SHOES_Palette = Config.GetValue<int>("6 SHOES", "SHOES_Palette", SHOES_Palette);




                        int Eyes_Drawable = 0; Eyes_Drawable = Config.GetValue<int>("7 Eyes", "Eyes_Drawable", Eyes_Drawable);
                        int Eyes_Palette = 0; Eyes_Palette = Config.GetValue<int>("7 Eyes", "Eyes_Palette", Eyes_Palette);




                        int Accessories_Drawable = 0; Accessories_Drawable = Config.GetValue<int>("8 Accessories", "Accessories_Drawable", Accessories_Drawable);
                        int Accessories_Palette = 0; Accessories_Palette = Config.GetValue<int>("8 Accessories", "Accessories_Palette", Accessories_Palette);






                        int Mission_Drawable = 0; Mission_Drawable = Config.GetValue<int>("9 Mission Items/ Tasks", "Mission_Drawable", Mission_Drawable);
                        int Mission_Palette = 0; Mission_Palette = Config.GetValue<int>("9 Mission Items/ Tasks", "Mission_Palette", Mission_Palette);





                        int Decals_Drawable = 0; Decals_Drawable = Config.GetValue<int>("10 Decals", "Decals_Drawable", Decals_Drawable);
                        int Decals_Palette = 0; Decals_Palette = Config.GetValue<int>("10 Decals", "Decals_Palette", Decals_Palette);



                        int InnerShirts_Drawable = 0; InnerShirts_Drawable = Config.GetValue<int>("11 Collars and Inner Shirts", "InnerShirts_Drawable", InnerShirts_Drawable);
                        int InnerShirts_Palette = 0; InnerShirts_Palette = Config.GetValue<int>("11 Collars and Inner Shirts", "InnerShirts_Palette", InnerShirts_Palette);


                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, Head_Drawable, Head_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, BEARD_Drawable, BEARD_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, HAIRCUT_Drawable, HAIRCUT_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, SHIRT_Drawable, SHIRT_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, PANTS_Drawable, PANTS_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, Gloves_Drawable, Gloves_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, SHOES_Drawable, SHOES_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, Eyes_Drawable, Eyes_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, Accessories_Drawable, Accessories_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, Mission_Drawable, Mission_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, Decals_Drawable, Decals_Palette, 1);
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, InnerShirts_Drawable, InnerShirts_Palette, 1);

                        int HatComp = 0; HatComp = Config.GetValue<int>("-1 HAT", "Hat_Drawable", HatComp);
                        int HatTex = 0; HatTex = Config.GetValue<int>("-1 Hat", "Hat_Tex", HatTex);
                        int HatPalette = 0; HatPalette = Config.GetValue<int>("-1 Hat", "Hat_Palette", HatPalette);


                        if (HatComp >= 1)
                        { Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, HatComp, HatTex, HatPalette); }
                        else if (HatComp < 1) { Function.Call(Hash.CLEAR_ALL_PED_PROPS, Game.Player.Character); }
                    }
                }

            };
            submenu2.OnListChange += (sender, item, index) =>
            {

                if (SVL.Index == 0)
                { Get.Text = "Save"; }
                if (SVL.Index == 1)
                { Get.Text = "Load"; }
            };

            UIMenuListItem OverideCloth = new UIMenuListItem("Can Overide Model", TF, 0);
            submenu0.AddItem(OverideCloth);
            UIMenuListItem OverideModel = new UIMenuListItem("Overide Model", OVM, 0);
            submenu0.AddItem(OverideModel);
            UIMenuListItem Comp1 = new UIMenuListItem("", Clothes, 0);
            submenu0.AddItem(Comp1);
            UIMenuListItem Drawable = new UIMenuListItem("Item : ", DrawUI, 0);
            submenu0.AddItem(Drawable);
            UIMenuListItem Texture = new UIMenuListItem("Texture : ", TexUI, 0);
            submenu0.AddItem(Texture);
            submenu1.OnItemSelect += (sender, item, index) => //Normal Weapons
            {
                if (item == Set)
                {
                    int Cash = 0;
                    LoadIniFile("scripts//MobileWaredobe//Wardrobe//Weapons.ini");
                    #region Save
                    foreach (WeaponHash W in (WeaponHash[])Enum.GetValues(typeof(WeaponHash)))
                    {//Save
                        if (Game.Player.Character.Weapons.HasWeapon(W))
                        {

                            Game.Player.Character.Weapons.Select(W, false);
                            Config.SetValue<WeaponHash>(W.ToString(), "WeaponName", W);
                            WeaponHash hash = Game.Player.Character.Weapons.Current.Hash;
                            Liv = Game.Player.Character.Weapons.Current.Tint;
                            int Comp = 0;
                            foreach (WeaponComponent WC in (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent)))
                            {

                                try
                                {

                                    if (Function.Call<bool>(Hash.DOES_WEAPON_TAKE_WEAPON_COMPONENT, W.GetHashCode(), WC.GetHashCode()) == true)
                                    {
                                        if (Game.Player.Character.Weapons.Current.IsComponentActive(WC) == true)
                                        {

                                            Config.SetValue<bool>(W.ToString(), "HasComponent" + Comp, true);
                                            Config.SetValue<WeaponComponent>(W.ToString(), "Component" + Comp, WC);
                                            Comp++;
                                            Config.Save();

                                        }

                                        if (Game.Player.Character.Weapons.Current.IsComponentActive(WC) == false)
                                        {

                                            Config.SetValue<bool>(W.ToString(), "HasComponent" + Comp, false);
                                            Config.SetValue<WeaponComponent>(W.ToString(), "Component" + Comp, WC);
                                            Comp++;
                                            Config.Save();

                                        }
                                    }

                                }
                                catch
                                {
                                    Comp++;
                                    UI.Notify("~y~ Warning ~w~: Weapon : " + W + " Failed to save");
                                }

                            }
                            Config.SetValue<WeaponTint>(W.ToString(), "Tint", Liv);
                            Config.Save();
                        }

                    }
                    #endregion
                    ID_O = O.Index;
                    ID_C = Colours[C.Index];

                    Setoutfit(O.Index);


                    UI.Notify("~y~ Warning ~w~ Lag is normal while applying outfits");
                    Script.Wait(2000);
                    UI.Notify("Player is wearing outfit : ~y~" + Outfits[O.Index] + "~w~ with colour : ~y~" + Colours[C.Index] + "~y~");
                    LoadIniFile("scripts//MobileWaredobe//Wardrobe//Weapons.ini");
                    #region Load
                    //    UI.Notify("Giving Weapons, this may take a second, lag spikes are Normal ");

                    foreach (WeaponHash W in (WeaponHash[])Enum.GetValues(typeof(WeaponHash)))
                    {//Load
                        var Weapon = Config.GetValue<WeaponHash>(W.ToString(), "WeaponName", W);

                        if (W == Weapon)
                        {
                            Game.Player.Character.Weapons.Give(W, 9999, true, true);
                            Game.Player.Character.Weapons.Select(W, true);
                            Liv = Config.GetValue<WeaponTint>(W.ToString(), "Tint", Liv);
                            WeaponHash hash = Game.Player.Character.Weapons.Current.Hash;
                            Game.Player.Character.Weapons.Current.Tint = Liv;
                            Comp = 0;
                            foreach (WeaponComponent WC in (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent)))
                            {

                                try
                                {
                                    if (Function.Call<bool>(Hash.DOES_WEAPON_TAKE_WEAPON_COMPONENT, W.GetHashCode(), WC.GetHashCode()) == true)
                                    {
                                        if (Config.GetValue<bool>(W.ToString(), "HasComponent" + Comp, true) == true)
                                        {
                                            Game.Player.Character.Weapons.Current.SetComponent(WC, true);
                                            Comp++;

                                        }
                                        else
                                        if (Config.GetValue<bool>(W.ToString(), "HasComponent" + Comp, true) == false)
                                        {

                                            Comp++;

                                        }
                                    }
                                }
                                catch
                                {
                                    Comp++;
                                }

                            }

                        }

                    }
                    #endregion
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 177293209, 9999);//MK2 Sniper
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 3686625920, 9999);//MK2 LMG
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 4208062921, 9999);//MK2 Carbine
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 2024373456, 9999);//MK2 SMG
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 961495388, 9999);//MK2 Assault Rifle
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 3219281620, 9999);//MK2 Pistol
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 1432025498, 9999);//MK2 Shotgun
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -2009644972, 9999);//MK2 SNS
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -879347409, 9999);//MK2 Revolver
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -1768145561, 9999);//MK2 Special Carbine
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -2066285827, 9999);//MK2 Bullpup
                    Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 1785463520, 9999);//MK2 Marksmen Sniper
                    Game.Player.Character.Weapons.Give(WeaponHash.Unarmed, 9999, true, true);


                }
                if (item == Remove)
                {
                    if (Game.Player.Character.Model == PedHash.FreemodeMale01)
                    {
                        UI.Notify("taking off Outfit, this may take some time, lag spikes are normal");
                        var characterModel = new Model(OldCharacter.Hash);
                        characterModel.Request(500);
                        if (characterModel.IsInCdImage && characterModel.IsValid)
                        {
                            while (!characterModel.IsLoaded) Script.Wait(100);
                            Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                            //      Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, Game.Player.Character.Handle);
                        }
                        characterModel.MarkAsNoLongerNeeded();
                    
                        #region Load
                        //    UI.Notify("Giving Weapons, this may take a second, lag spikes are Normal ");

                        foreach (WeaponHash W in (WeaponHash[])Enum.GetValues(typeof(WeaponHash)))
                        {//Load
                            var Weapon = Config.GetValue<WeaponHash>(W.ToString(), "WeaponName", W);

                            if (W == Weapon)
                            {
                                Game.Player.Character.Weapons.Give(W, 9999, true, true);
                                Game.Player.Character.Weapons.Select(W, true);
                                Liv = Config.GetValue<WeaponTint>(W.ToString(), "Tint", Liv);
                                WeaponHash hash = Game.Player.Character.Weapons.Current.Hash;
                                Game.Player.Character.Weapons.Current.Tint = Liv;
                                Comp = 0;
                                foreach (WeaponComponent WC in (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent)))
                                {

                                    try
                                    {
                                        if (Function.Call<bool>(Hash.DOES_WEAPON_TAKE_WEAPON_COMPONENT, W.GetHashCode(), WC.GetHashCode()) == true)
                                        {
                                            if (Config.GetValue<bool>(W.ToString(), "HasComponent" + Comp, true) == true)
                                            {
                                                Game.Player.Character.Weapons.Current.SetComponent(WC, true);
                                                Comp++;

                                            }
                                            else
                                            if (Config.GetValue<bool>(W.ToString(), "HasComponent" + Comp, true) == false)
                                            {

                                                Comp++;

                                            }
                                        }
                                    }
                                    catch
                                    {
                                        Comp++;
                                    }

                                }

                            }

                        }
                        #endregion
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 177293209, 9999);//MK2 Sniper
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 3686625920, 9999);//MK2 LMG
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 4208062921, 9999);//MK2 Carbine
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 2024373456, 9999);//MK2 SMG
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 961495388, 9999);//MK2 Assault Rifle
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 3219281620, 9999);//MK2 Pistol
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 1432025498, 9999);//MK2 Shotgun
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -2009644972, 9999);//MK2 SNS
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -879347409, 9999);//MK2 Revolver
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -1768145561, 9999);//MK2 Special Carbine
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, -2066285827, 9999);//MK2 Bullpup
                        Function.Call(Hash.ADD_AMMO_TO_PED, Game.Player.Character, 1785463520, 9999);//MK2 Marksmen Sniper
                        Game.Player.Character.Weapons.Give(WeaponHash.Unarmed, 9999, true, true);


                        UI.Notify("Removed Outfit!");
                    }
                }
            };
            submenu1.OnListChange += (sender, item, index) =>
            {
                if (item == O)
                {
                    ID_O = O.Index;
                }
                if (item == C)
                {
                    if (C == O)
                    {
                        ID_C = Colours[C.Index];
                    }
                }
            };

            submenu0.OnListChange += (sender, item, index) =>
            {
                try
                {
                    if (item == Comp1)
                    {
                        if (Game.Player.Character != null)
                        {

                            if (Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, Comp1.Index) > Drawable.Index)
                            {
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }
                            else
                            {
                                Drawable.Index = 0;
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }
                            if (Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, Comp1.Index, Drawable.Index) > Texture.Index)
                            {
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }
                            else
                            {
                                Texture.Index = 0;
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }
                        }

                    }
                    if (item == Drawable)
                    {
                        if (Game.Player.Character != null)
                        {

                            if (Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, Comp1.Index) > Drawable.Index)
                            {
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }
                            else
                            {
                                Drawable.Index = 0;
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);

                            }


                        }

                    }
                    if (item == Texture)
                    {
                        if (Game.Player.Character != null)
                        {

                            if (Function.Call<int>(Hash.GET_NUMBER_OF_PED_TEXTURE_VARIATIONS, Game.Player.Character, Comp1.Index, Drawable.Index) > Texture.Index)
                            {
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);
                            }
                            else
                            {
                                Texture.Index = 0;
                                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, Comp1.Index, Draw[Drawable.Index], Tex[Texture.Index], 1);
                            }

                        }
                    }
                }
                catch
                {

                }

            };
        }
        private void OnTick(object sender, EventArgs e)
        {
            ifruit.Update();
            if (Woredrobepool != null && Woredrobepool.IsAnyMenuOpen() == true)
                Woredrobepool.ProcessMenus();

        }
        void DisplayHelpTextThisFrame(string text)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode==Keys.X)
            {
           
            }
        }
        public void loadMenu(iFruitContact contact)
        {

            //  UI.Notify("Map Area " + MapArea);
            ifruit.Close();

            WoredrobeMainMenu.Visible = !WoredrobeMainMenu.Visible;
        }
    }
}
