﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarioPort
{
   public static class Mario
   {
      public const int NUM_LEV = 6;
      public const int LAST_LEV = 2 * NUM_LEV - 1;
      public const int MAX_SAVE = 3;
      public const int WAIT_BEFORE_DEMO = 500;

      public struct ConfigData
      {
         public bool Sound;
         public bool SLine;
         public static Buffers.GameData[] Games =
            {
               Buffers.GameData.Create(),
               Buffers.GameData.Create(),
               Buffers.GameData.Create()
            };
         public bool UseJS;
         //public JoyRec JSDat;
      }
      public static ConfigData ConfigFile;
      public static int GameNumber;
      public static int CurPlayer;
      public static bool Passed;
      public static bool EndGame;
      public static ConfigData Config;

      public enum Statuses
      {
         ST_NONE,
         ST_MENU,
         ST_START,
         ST_LOAD,
         ST_ERASE,
         ST_OPTIONS,
         ST_NUMPLAYERS
      }
      public delegate void Up();

#if (DEBUG)
      public static void MouseHalt()
      {
         //halt
      }
#endif

      public static void NewData()
      {
         Buffers.data.lives = new int[] { 3, 3 };
         Buffers.data.coins = new int[] { 0, 0 };
         Buffers.data.score = new long[] { 0, 0 };
         Buffers.data.progress = new int[] { 0, 0 };
         Buffers.data.mode = new byte[] { Buffers.mdSmall, Buffers.mdSmall };
      }

      public static string GetConfigName(string[] args)
      {
         //byte absolute len = S;
         string S = args[0];
         // Not sure if we absolutely need this function it's in all the preprocessor
         // debug statements
         //S[S.Length - 2] = "C";
         //S[S.Length - 1] = "F";
         //S[S.Length - 0] = "G";
         return S;
      }

      public static void ReadConfig(string[] args)
      {
         ConfigData F;
         int j;
         string Name;
#if MENU
   		Assign(F, GetConfigName());
   		Reset(F);
   		Read(F, Config);
   		Close(F);
   		if IOResult != 0 
#endif
         {
            NewData();
            for(int i = 0; i < MAX_SAVE - 1; i++)
               ConfigData.Games[i] = Buffers.data;  //pretty sure this needs to be Config.Games[i] but it errors out
            Config.SLine = true;
            Config.Sound = true;
            Config.UseJS = false;
            GameNumber = -1;
         }
         Play.Stat = Config.SLine;
         Buffers.BeeperSound = Config.Sound;
         Name = args[0];
         j = 0;
         if(Name.Length > 9)
            Name = Name.Remove(1, Name.Length - 9);
         //for(int i = 0; i < Name.Length; i++)
         //Inc(j, Ord(Name[i].ToUpper));
         //if (j != 648)
         //   RunError(201); //more debugging stuff?
      }

      public static void WriteConfig()
      {
         ConfigData F;
         Config.SLine = Play.Stat;
         Config.Sound = Buffers.BeeperSound;
#if MENU
   		Assign(F, GetConfigName());
   		ReWrite(F);
   		if (IOResult = 0)
   		{
   			Write(F, Config); //May need FileIO specific functions
   			Close(F);
   		}
#endif
      }

      public static void CalibrateJoystick()
      {
         // TODO if we implement joystick
      }
      /**
   
        procedure CalibrateJoystick;
        begin
          Delay (100);
          WriteLn ('Rotate joystick and press button');
          WriteLn ('or press any key to use keyboard...');
          Delay (100);
          Key := #0;
          repeat
            Calibrate;
            Write (#13, 'X = ', Byte (jsRight) - Byte (jsLeft): 2,
                      '  Y = ', Byte (jsDown) - Byte (jsUp): 2);
          until jsButton1 or jsButton2 or (Key <> #0);
          WriteLn;
          if (Key <> #0) then
          begin
            jsEnabled := FALSE;
            ReadJoystick;
          end;
          Config.UseJS := jsEnabled;
          Config.JSDat := jr;
          Key := #0;
        end;
   
        **/

      public static void ReadCmdLine(string[] args)
      {
         string S;
         for(int i = 0; i < args.Length; i++)
         {
            S = args[i];
            while(!string.IsNullOrEmpty(S))
            {
               if(S.Length >= 2 && (S[1] == '/' || S[1] == '-'))
               {
                  switch(S[2])
                  {
                     case 'S':
                        Play.Stat = true;
                        break;
                     case 'Q':
                        Buffers.BeeperOff();
                        break;
                     case 'J':
                        CalibrateJoystick();
                        break;
                     default:
                        break;
                  }
                  S = S.Remove(1, 2);
               }
               else
                  S = S.Remove(1, 1);
            }
         }
      }

      public static void Demo()
      {
         NewData();
         //TurboType Turbo = false;
         Buffers.data.progress[Buffers.plMario] = 5;
         //PlayMacro();
         Play.PlayWorld(' ', ' ', Worlds.Level_6a(), Worlds.Options_6a(), Worlds.Options_6a(),
                 Worlds.Level_6b(), Worlds.Options_6b(), Worlds.Options_6b(), Buffers.plMario);
         //Play.StopMacro();
      }
      public static void Intro()
      {
         int P, l, m, n, wd, ht, xp;
         int NextNumPlayers, Selected;
         bool IntroDone, TestVGAMode, UpDate;
         int Counter;
         char MacroKey;


         Statuses Status, OldStatus, LastStatus;
         string[] Menu = new String[5];
         uint[,] BG = new uint[FormMarioPort.MAX_PAGE, 4];
         int NumOptions;
         //nested procedures
         //Buffers.Page = Buffers.CurrentPage;
         Status = Statuses.ST_NONE;
         TestVGAMode = false;
         GameNumber = -1;
         NextNumPlayers = Buffers.data.numPlayers[CurPlayer];

         do //until IntroDone and (not TestVGAMode);
         {
            //if(TestVGAMode)
            //   InitVGA();
            TestVGAMode = false;
            IntroDone = false;
            NewData();

            Play.PlayWorld('0', '0', Worlds.Intro_0(), Worlds.Options_0(), Worlds.Options_0(),
                    Worlds.Intro_0(), Worlds.Options_0(), Worlds.Options_0(), Buffers.plMario);
            //InitBackGr(3, 0);

            //OutPalette(0xA0, 35, 45, 50);
            //OutPalette(0xA1, 45, 55, 60);

            //OutPalette(0xEF, 30, 40, 30);
            //OutPalette(0x18, 10, 15, 25);

            //OutPalette(0x8D, 28, 38, 50);
            //OutPalette(0x8F, 40, 50, 63);

            //for(int i = 1; i < 50; i++)
            //   BlinkPalette();

            for(int p = 0; p < FormMarioPort.MAX_PAGE; p++)
            {
               for(int i = 1; i == 0; i--)
                  for(int j = 1; j == 0; j--)
                     for(int k = 1; k == 0; k--)
                     {
                        //FormMarioPort.DrawImage(38 + i + j, 29 + i + k, 108, 28, Resources.Intro_000);
                        //FormMarioPort.DrawImage(159 + i + j, 29 + i + k, 24, 28, Resources.Intro_001);
                        //FormMarioPort.DrawImage(198 + i + j, 29 + i + k, 84, 28, Resources.Intro_002);
                     }
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 54, 0xA0);
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 55, 0xA1);
               BackGr.DrawBackGrMap(10 * Buffers.H + 6, 11 * Buffers.H - 1, 53, 0xA1);
               //for(int i = 0; i < Buffers.NH - 1; i++)
               //   for(int j = 0; j < Buffers.NV - 1; j++)
               //      if((i == 0 || i == Buffers.NH - 1) || (j == 0 || j == Buffers.NV - 1))
               //         DrawImage(i * W, j * H, W, H, @Block000);
               //FormMarioPort.DrawPlayer();
               //FormMarioPort.ShowPage();
            }
            //UnlockPal();
            //Keyboard.Key = 0;
            //FadeUp(64);
            //ResetStack();

            //FillChar(BG, BG.Size(), 0);
            //FillChar(Menu, Menu.Size(), 0);
            //SetFont(0, Bold + Shadow);

            if(Status != Statuses.ST_OPTIONS)
            {
               OldStatus = Statuses.ST_NONE;
               LastStatus = Statuses.ST_NONE;
               Status = Statuses.ST_MENU;
               Selected = 1;
            }
            //UpDate = true;
            Counter = 1;

            do //until IntroDone or (Counter = WAIT_BEFORE_DEMO);
            {
               //if(UpDate || Status != OldStatus)
               //{
               //   if(Status != OldStatus)
               //      Selected = 1;
               //   switch(Status)
               //   {
               //      case Statuses.ST_MENU:
               //         Menu[1] = "START";
               //         Menu[2] = "OPTIONS";
               //         Menu[3] = "END";
               //         Menu[4] = "";
               //         Menu[5] = "";
               //         NumOptions = 3;
               //         LastStatus = Statuses.ST_MENU;
               //         break;
               //      case Statuses.ST_OPTIONS:
               //         if(Buffers.BeeperSound)
               //            Menu[1] = "SOUND ON ";
               //         else
               //            Menu[1] = "SOUND OFF";
               //         if(Play.Stat)
               //            Menu[2] = "STATUSLINE OFF";
               //         else
               //            Menu[2] = "STATUSLINE OFF";
               //         Menu[3] = "";
               //         Menu[4] = "";
               //         Menu[5] = "";
               //         NumOptions = 2;
               //         LastStatus = Statuses.ST_MENU;
               //         break;
               //      case Statuses.ST_START:
               //         Menu[1] = "NO SAVE";
               //         Menu[2] = "GAME SELECT";
               //         Menu[3] = "ERASE";
               //         Menu[4] = "";
               //         Menu[5] = "";
               //         NumOptions = 3;
               //         LastStatus = Statuses.ST_MENU;
               //         break;
               //      case Statuses.ST_NUMPLAYERS:
               //         Menu[1] = "ONE PLAYER";
               //         Menu[2] = "TWO PLAYERS";
               //         Menu[3] = "";
               //         Menu[4] = "";
               //         Menu[5] = "";
               //         NumOptions = 3;
               //         LastStatus = Statuses.ST_MENU;
               //         break;
               //      case Statuses.ST_LOAD:
               //      case Statuses.ST_ERASE:
               //         Menu[1] = "GAME #1 " + 7 + " ";
               //         Menu[2] = "Game #2 " + 7 + " ";
               //         Menu[3] = "Game #3 " + 7 + " ";
               //         Menu[4] = "";
               //         Menu[5] = "";
               //         for(int i = 0; i < 3; i++)
               //            if(ConfigData.Games[i].progress[Buffers.plMario] == 0 &&
               //               ConfigData.Games[i].progress[Buffers.plLuigi] == 0)
               //               Menu[i] = Menu[i] + "EMPTY";
               //            else
               //            {
               //               m = ConfigData.Games[i].progress[Buffers.plMario];
               //               n = Convert.ToInt32((ConfigData.Games[i].progress[CurPlayer] >= NUM_LEV));
               //               if(n > 0)
               //                  m -= NUM_LEV;
               //               if(ConfigData.Games[i].progress[Buffers.plLuigi] > m)
               //               {
               //                  m = ConfigData.Games[i].progress[Buffers.plLuigi];
               //                  ConfigData.Games[i].progress[Buffers.plMario] = m;
               //               }
               //               Menu[i] = Menu[i] + "LEVEL " + (char)(m +
               //                       Convert.ToInt32(Encoding.ASCII.GetBytes("0")) + 1) + " ";
               //               if(n == 0)
               //                  Menu[i] = Menu[i] + 7 + " ";
               //               else
               //                  Menu[i] = Menu[i] + "* ";
               //               Menu[i] = Menu[i] + (char)(ConfigData.Games[i].numPlayers[CurPlayer] +
               //                       Convert.ToInt32(Encoding.ASCII.GetBytes("0"))) + 'P';
               //            }
               //         NumOptions = 3;
               //         LastStatus = Statuses.ST_START;
               //         break;
               //      default:
               //         break;
               //   }
               //   wd = 0;
               //   xp = 0;
               //   for(int i = 0; i < 5; i++)
               //   {
               //      //j = TextWidth(Menu[i]);
               //      if(m > wd)
               //      {
               //         wd = m;
               //         //xp = CenterX(Menu[i]) / 4 * 4;
               //      }
               //      ht = 8;
               //   }
               //   OldStatus = Status;
               //   UpDate = false;
               //}
               MacroKey = '0';
               //switch(Keyboard.Key)
               //{
               //   case kbEsc:
               //      if(Status = ST_MENU)
               //      {
               //         IntroDone = true;
               //         QuitGame = true;
               //      }
               //      else
               //         Status = LastStatus;
               //      break;
               //   case kbUpArrow:
               //      Up();
               //      break;
               //   case kbDownArrow:
               //      Down();
               //      break;
               //   case kbSP:
               //   case kbEnter:
               //      switch(Status)
               //      {
               //         case ST_MENU:
               //            switch(Selected)
               //            {
               //               case 1:
               //                  Status = ST_START;
               //                  break;
               //               case 2:
               //                  Status = ST_OPTIONS;
               //                  break;
               //               case 3:
               //                  IntroDone = true;
               //                  QuitGame = true;
               //                  break;
               //            }
               //            break;
               //         case ST_START:
               //            switch(Selected)
               //            {
               //               case 1:
               //                  Status = ST_NUMPLAYERS;
               //                  break;
               //               case 2:
               //                  Status = ST_LOAD;
               //                  break;
               //               case 3:
               //                  Status = ST_ERASE;
               //                  break;
               //            }
               //            break;
               //         case ST_OPTIONS:
               //            switch(Selected)
               //            {
               //               case 1:
               //                  if(Buffers.BeeperSound)
               //                     Buffers.BeeperOff();
               //                  else
               //                     Buffers.BeeperOn();
               //                  break;
               //               case 2:
               //                  Play.Stat = !Play.Stat;
               //                  break;
               //            }
               //            break;
               //         case ST_NUMPLAYERS:
               //            switch(Selected)
               //            {
               //               case 1:
               //                  NextNumPlayers = 1;
               //                  IntroDone = true;
               //                  break;
               //               case 2:
               //                  NextNumPlayers = 2;
               //                  IntroDone = true;
               //                  break;
               //            }
               //            break;
               //         case ST_LOAD:
               //            GameNumber = Selected - 1;
               //            Config.Games[GameNumber].NumPlayers = 1;
               //            if(Config.Games[GameNumber].Progress[plMario] = 0 &&
               //               Config.Games[GameNumber].Progress[plLuigi] = 0)
               //               Status = ST_NUMPLAYERS;
               //            else
               //            {
               //               IntroDone = true;
               //               NextNumPlayers = ConfigGames[GameNumber].NumPlayers;
               //            }
               //            break;
               //         case ST_ERASE:
               //            NewData();
               //            Config.Games[Selected - 1] = Data;
               //            Config.Games[Selected - 1].NumPlayers = 1;
               //            GameNumber = -1;
               //            break;
               //      }
               //}
               //if(Keyboard.Key != 0)
               //{
               //   Counter = 0;
               //   Keyboard.Key = MacroKey;
               //   UpDate = true;
               //}

               //for(int k = 0; k < 5; k++)
               //{
               //   if(BG[Page, k] != 0)
               //      BackGr.PopBackGr(BG[Page, k]);
               //}

               //for(int k = 0; k < 5; k++)
               //{
               //   if(Menu[k] != "")
               //   {
               //      m = xp;
               //      n = 56 + 14 * k;
               //      BG[Page, k] = BackGr.PushBackGr(50, j, 220, ht);
               //      if(k == Selected)
               //         WriteText(i - 12, j, 16, 5);
               //      l = 15;
               //      if(Menu[k].Length > 19 && Menu[k][10] == '*')
               //         //l := 14 + (Counter and 1);
               //         l = 14 + (Counter & l);
               //      SetPalette(14, 63, 61, 31);
               //      WriteText(i + 8, j, Menu[k], l);
               //   }
               //}
               //ShowPage();
               //BlinkPalette();
               //ResetStack();

               Counter++;
            } while(!IntroDone && Counter != WAIT_BEFORE_DEMO);

            //FadeDown(64);

            if(!IntroDone)
               Demo();
         } while(!IntroDone || TestVGAMode);

         if(GameNumber != -1)
            Buffers.data = ConfigData.Games[GameNumber];
         ConfigData.Games[GameNumber].numPlayers[CurPlayer] = NextNumPlayers;
      }

      static void ShowPlayerName(byte Player)
      {
         int iW, iH;
         //ClearPalette();
         //LockPal();
         //ClearVGAMem;
         //SetView(0, 0);
         iH = 13;
         for(int i = 0; i < FormMarioPort.MAX_PAGE; i++)
         {
            switch(Player)
            {
               case Buffers.plMario:
                  iW = 116;
                  //DrawImage (160 - iW / 2, 85 - iH / 2, iW, iH, Start000);
                  break;
               case Buffers.plLuigi:
                  iW = 108;
                  //DrawImage (160 - iW / 2, 85 - iH / 2, iW, iH, @Start001*);
                  break;
            }
            //FormMarioPort.ShowPage;
         }
         //NewPalette (P256*);
         //FormMarioPort.UnLockPal();
         //Palettes.ReadPalettes(Palette);
         //for(int i = 0; i < 100; i++)
         //   FormMarioPort.ShowPage();
         //Palettes.ClearPalette();
         //FormMarioPort.ClearVGAMem();
      }

      public static void main(string[] args)
      {
         //Keyboard.InitKeyBoard();
         Buffers.data.numPlayers[CurPlayer] = 1;
         ReadConfig(args);
         ReadCmdLine(args);

         //jr = Config.JSDat;
         //jsEnabled = jsDetected() && Config.UseJS();

#if DEBUG//asm stuff :(
         /**
{$IFDEF DEBUG}
   MouseHaltAddr := @MouseHalt;
   asm
      xor   ax, ax
      int   33h
      inc   ax
      jnz   @End
      mov   al, 0Ch
      mov   cx, 0Ah
      les   dx, MouseHaltAddr
      int   33h
      @End:
   end;
{$ENDIF}
**/
#endif

         //if(!FormMarioPort.DetectVga)
         //{
         //   Console.Write("VGA graphics adapter required");
         //   //halt (1);
         //}

         //Keyboard.ResetKeyBoard();

         //if(!FormMarioPort.InGraphicsMode)
         //   FormMarioPortInitVGA();

#if DEBUG
         do
         {
#endif
            //FormMarioPort.ClearVGAMem();

            //Figures.InitPlayerFigures();
            //Figures.InitEnemyFigures();

            EndGame = false;
#if MENU
      		Intro();
#endif
            //Randomize();

            if(Buffers.data.numPlayers[CurPlayer] == 2)
               if(Buffers.data.progress[Buffers.plMario] > Buffers.data.progress[Buffers.plLuigi])
                  Buffers.data.progress[Buffers.plLuigi] = Buffers.data.progress[Buffers.plMario];
               else
                  Buffers.data.progress[Buffers.plMario] = Buffers.data.progress[Buffers.plLuigi];

            Buffers.data.lives[Buffers.plMario] = 3;
            Buffers.data.lives[Buffers.plLuigi] = 3;
            Buffers.data.coins[Buffers.plMario] = 0;
            Buffers.data.coins[Buffers.plLuigi] = 0;
            Buffers.data.score[Buffers.plMario] = 0;
            Buffers.data.score[Buffers.plLuigi] = 0;
            Buffers.data.mode[Buffers.plMario] = Buffers.mdSmall;
            Buffers.data.mode[Buffers.plLuigi] = Buffers.mdSmall;

            do //until EndGame or QuitGame 
            {
               if(Buffers.data.numPlayers[CurPlayer] == 1)
                  Buffers.data.lives[Buffers.plLuigi] = 0;
               for(CurPlayer = Buffers.plMario; CurPlayer < Buffers.data.numPlayers[CurPlayer]; CurPlayer++)
               {
                  if(!(EndGame || Buffers.QuitGame))
                     if(Buffers.data.lives[CurPlayer] >= 1)
                     {
                        //Turbo = (ConfigData.Games[GameNumber].progress[CurPlayer] >= NUM_LEV);
                        if(Buffers.data.progress[CurPlayer] > LAST_LEV)
                           Buffers.data.progress[CurPlayer] = NUM_LEV;
#if MENU
      						ShowPlayerName (CurPlayer);
#endif
                        switch(Buffers.data.progress[CurPlayer] % NUM_LEV)
                        {
                           case 0:
                              Passed = Play.PlayWorld('x', '1', Worlds.Level_1a(),
                                     Worlds.Options_1a(), Worlds.Opt_1a(),
                                     Worlds.Level_1b(), Worlds.Options_1b(),
                                     Worlds.Options_1b(), Convert.ToByte(CurPlayer));
                              break;
                           case 1:
                              Passed = Play.PlayWorld('x', '2', Worlds.Level_2a(),
                                     Worlds.Options_2a(), Worlds.Opt_2a(),
                                     Worlds.Level_2b(), Worlds.Options_2b(),
                                     Worlds.Options_2b(), Convert.ToByte(CurPlayer));
                              break;
                           case 2:
                              Passed = Play.PlayWorld('x', '3', Worlds.Level_3a(),
                                     Worlds.Options_3a(), Worlds.Opt_3a(),
                                     Worlds.Level_3b(), Worlds.Options_3b(),
                                     Worlds.Options_3b(), Convert.ToByte(CurPlayer));
                              break;
                           case 3:
                              Passed = Play.PlayWorld('x', '4', Worlds.Level_5a(),
                                     Worlds.Options_5a(), Worlds.Opt_5a(),
                                     Worlds.Level_5b(), Worlds.Options_5b(),
                                     Worlds.Options_5b(), Convert.ToByte(CurPlayer));
                              break;
                           case 4:
                              Passed = Play.PlayWorld('x', '5', Worlds.Level_6a(),
                                     Worlds.Options_6a(), Worlds.Opt_6a(),
                                     Worlds.Level_6b(), Worlds.Options_6b(),
                                     Worlds.Options_6b(), Convert.ToByte(CurPlayer));
                              break;
                           case 5:
                              Passed = Play.PlayWorld('x', '6', Worlds.Level_4a(),
                                      Worlds.Options_4a(), Worlds.Opt_4a(),
                                     Worlds.Level_4b(), Worlds.Options_4b(),
                                     Worlds.Options_4b(), Convert.ToByte(CurPlayer));
                              break;
                           default:
                              EndGame = true;
                              break;
                        }
                        if(Passed)
                           Buffers.data.progress[CurPlayer]++;
                        if(Buffers.QuitGame)
                        {
                           EndGame = true;
#if MENU
      							QuitGame = false;
#endif
                        }
                     }
               }
            } while(!(EndGame || Buffers.QuitGame || (Buffers.data.lives[Buffers.plMario] +
               Buffers.data.lives[Buffers.plLuigi] == 0)));

            if(GameNumber != -1)
               ConfigData.Games[GameNumber] = Buffers.data;
#if MENU
      	} while (!QuitGame)
#endif
            WriteConfig();
         } while(!Buffers.QuitGame);
      }
   }
}
