
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using MarioPort;
﻿using Resources = MarioPort.Properties.Resources;

/*
 *
 */

namespace MarioPort
{

   public static class Players
   {
      public const int stOnTheGround = 0;
      public const int stJumping     = 1;
      public const int stFalling     = 2;

      public const int SCROLL_AT     = 112;

      public const int JumpVel       = 4;
      public const int JumpDelay     = 6;
      public const int MaxYVel       = JumpVel * 2;
      public const int Slip          = 6;
      public const int BlinkTime     = 125;
      public const int StarTime      = 750;
      public const int GrowTime      = 24;

      public const int MAX_SPEED     = 2;

      public static bool Blinking;
      public static bool Growing;
      public static bool InPipe;
      public static char[] PipeCode = new char[2];
      public static int MapX;
      public static int MapY;
      public static bool EarthQuake;
      public static int EarthQuakeCounter;
      public static int Small;

      //// implementation ////
/*
      private const int Safe = EY1;
      private const int HSafe = H * Safe;

      private static bool keyLeft;
      private static bool keyRight;
      private static bool keyUp;
      private static bool keyDown;
      private static bool keyAlt;
      private static bool keyCtrl;
      private static bool keyLeftShift;
      private static bool keyRightShift;
      private static bool keySpace;

      struct ScreenRec
      {
         public static bool Visible;
         public static int XPos;
         public static int YPos;
         public uint BackGrAddr;
      }
      
      private static ScreenRec[] SaveScreen = new ScreenRec[MAX_PAGE + 1];

      private static int X;
      private static int Y;
      private static int OldX;
      private static int OldY;
      private static int DemoX;
      private static int DemoY;
      private static int DemoCounter1;
      private static int DemoCounter2;
      private static int XVel;
      private static int YVel;
      
      private static byte Direction;
      private static byte Status;
      private static byte Walkingmode;
      private static byte Counter;
      private static byte WalkCount;
      
      private static bool HighJump;
      private static bool HitEnemy;
      private static bool Jumped;
      private static bool Fired;
      
      private static int FireCounter;
      private static int StarCounter;
      private static int GrowCounter;
      private static int BlinkCounter;
      
      private static char AtCh1;
      private static char AtCh2;
      private static char Below1;
      private static char Below2;

      
//      private static void HighMirror (P1, P2: Pointer)
//      {
//        type
//          PlaneBuffer = array[0..2 * H - 1, 0..W / 4 - 1] of Byte;
//          PlaneBufferArray = array[0..3] of PlaneBuffer;
//          PlaneBufferArrayPtr = ^PlaneBufferArray;
//        var
//          Source, Dest: PlaneBufferArrayPtr;
//        private static void Swap (Plane1, Plane2: Byte );
//          var
//            i, j: Byte;
//        {
//          for j = 0 to 2 * H - 1 do
//            for i = 0 to W / 4 - 1 do
//            {
//              Dest^[Plane2, j, i] = Source^[Plane1, j, W / 4 - 1 - i];
//              Dest^[Plane1, j, i] = Source^[Plane2, j, W / 4 - 1 - i];
//            }
//        }
//      {
//        Source = P1;
//        Dest = P2;
//        Swap (0, 3 );
//        Swap (1, 2 );
//      }
//      }

      // NOTE: I'm guessing that "Mirror" means flip over Y axis
      
      private static void HighMirror(Bitmap from, ref Bitmap to)
      {
         
      }

      public static void InitPlayerFigures()
      {
         Pictures[plMario, mdSmall, 0, dirLeft] = Resources.SWMAR_000;
         Pictures[plMario, mdSmall, 1, dirLeft] = Resources.SWMAR_001;
         Pictures[plMario, mdSmall, 2, dirLeft] = Resources.SJMAR_000;
         Pictures[plMario, mdSmall, 3, dirLeft] = Resources.SJMAR_001;

         Pictures[plMario, mdLarge, 0, dirLeft] = Resources.LWMAR_000;
         Pictures[plMario, mdLarge, 1, dirLeft] = Resources.LWMAR_001;
         Pictures[plMario, mdLarge, 2, dirLeft] = Resources.LJMAR_000;
         Pictures[plMario, mdLarge, 3, dirLeft] = Resources.LJMAR_001;
         
         Pictures[plMario, mdFire, 0, dirLeft] = Resources.FWMAR_000;
         Pictures[plMario, mdFire, 1, dirLeft] = Resources.FWMAR_001;
         Pictures[plMario, mdFire, 2, dirLeft] = Resources.FJMAR_000;
         Pictures[plMario, mdFire, 3, dirLeft] = Resources.FJMAR_001;
         
         Pictures[plLuigi, mdSmall, 0, dirLeft] = Resources.SWLUI_000;
         Pictures[plLuigi, mdSmall, 1, dirLeft] = Resources.SWLUI_001;
         Pictures[plLuigi, mdSmall, 2, dirLeft] = Resources.SJLUI_000;
         Pictures[plLuigi, mdSmall, 3, dirLeft] = Resources.SJLUI_001;
         
         Pictures[plLuigi, mdLarge, 0, dirLeft] = Resources.LWLUI_000;
         Pictures[plLuigi, mdLarge, 1, dirLeft] = Resources.LWLUI_001;
         Pictures[plLuigi, mdLarge, 2, dirLeft] = Resources.LJLUI_000;
         Pictures[plLuigi, mdLarge, 3, dirLeft] = Resources.LJLUI_001;
         
         Pictures[plLuigi, mdFire, 0, dirLeft] = Resources.FWLUI_000;
         Pictures[plLuigi, mdFire, 1, dirLeft] = Resources.FWLUI_001;
         Pictures[plLuigi, mdFire, 2, dirLeft] = Resources.FJLUI_000;
         Pictures[plLuigi, mdFire, 3, dirLeft] = Resources.FJLUI_001;
         
         for ( int Pl = plMario; Pl <= plLuigi; Pl++ )
         {
            for ( int Md = mdSmall; Md <= mdFire; Md++ )
               for ( int N = 0; N <= 3; N++ )
                  HighMirror ( Pictures[Pl, Md, N, dirLeft], ref Pictures[Pl, Md, N, dirRight] );
         }
      }

      public static void InitPlayer (int InitX, int InitY, byte Name)
      {
         Player = Name;
         X = InitX;
         Y = InitY;
         OldX = X;
         OldY = Y;
         XVel = 0;
         YVel = 0;
         Direction = dirRight;
         Walkingmode = 0;
         Status = stOnTheGround;
         Jumped = false;
         Fired = false;
         HitEnemy = false;
         
         for( int i = 0; i < MAX_PAGE; i++ )
            SaveScreen[i].Visible = false;
         
         PlayerX1 = X;
         PlayerX2 = X + W - 1;
         PlayerY1 = Y + H;
         PlayerY2 = Y + 2 * H - 1;
         PlayerXVel = XVel;
         PlayerYVel = YVel;
         Blinking = false;
         Star = false;
         Growing = false;
         EarthQuake = false;

      }
      
      private static void DrawDemo()
      {
         int i, j;
         
//         with SaveScreen[CurrentPage] do
//         GetImage (X, Y, W, 2 * H, Buffer );
         SaveScreen[CurrentPage].BackGrAddr = PushBackGr (X, Y, W + 4, 2 * H );
         SaveScreen[CurrentPage].XPos = X;
         SaveScreen[CurrentPage].YPos = Y;
         SaveScreen[CurrentPage].Visible = true;
         
//         case Demo of
         switch (Demo)
         {         
            case dmDownInToPipe:
            case dmUpOutOfPipe:
            {
               DrawPart( X, Y + DemoY, W, 2 * H, 0, 2 * H - DemoY - 1, Pictures[Player, Data.mode[Player], Walkingmode, Direction] );
               break;
            }
            case dmUpInToPipe:
            case dmDownOutOfPipe:
            {
               DrawPart( X, Y + DemoY, W, 2 * H, -DemoY, 2 * H, Pictures[Player, Data.mode[Player], Walkingmode, Direction] );
               Redraw( MapX, MapY - 1 );
               Redraw( MapX + 1, MapY - 1 );
               break;
            }
            case dmDead:
            {
               DrawImage( X, Y, W, 2 * H, Pictures[Player, Data.mode[Player], Walkingmode, Direction] );
               break;
            }
         }
         OldX = X;
         OldY = Y;
      }
      
      public static void DrawPlayer()
      {
         if ( Demo != dmNoDemo )
         {
            DrawDemo;
            return;
         }
         if ( !Blinking || (BlinkCounter % 2 == 0) )
         {
//            with SaveScreen [CurrentPage] do
//            {
//               { GetImage (X, Y, W, 2 * H, Buffer ); }
            SaveScreen[CurrentPage].BackGrAddr = PushBackGr (X, Y, W + 4, 2 * H );
            SaveScreen[CurrentPage].XPos = X;
            SaveScreen[CurrentPage].YPos = Y;
            Visible = true;
//            }
            if ((Data.mode[Player] == mdFire) && keySpace && (FireCounter < 7))
            {
               FireCounter++;
               DrawPart(X, Y + 1, W, 2 * H, 0, 20, Pictures[Player, mdFire, 1, Direction]);
               DrawPart(X, Y, W, 2 * H, 21, 2 * H, Pictures[Player, mdFire, 0, Direction]);
            }
            else
               if (Star || Growing)
                  RecolorImage(X, Y, W, 2 * H, Pictures[Player, Data.mode[Player], Walkingmode, Direction], ((GrowCounter + StarCounter) && 1) >> 4 -
                        (byte)((GrowCounter + StarCounter) && 0xF < 8));
               else
                  DrawImage(X, Y, W, 2 * H, Pictures[Player, Data.mode[Player], Walkingmode, Direction]);
            OldX = X;
            OldY = Y;
         }
      }
      
      public static void ErasePlayer()
      {
         if ( !SaveScreen[CurrentPage].Visible )
            return;
         
         PutImage (XPos, YPos, W, 2 * H, Buffer );
         PopBackGr (XPos, YPos, W + 4, 2 * H, BackGrAddr );
         Visible = false;
      }

      public static void DoDemo()
      {
         Small = 9 * (byte)(Data.mode[Player] == mdSmall );
         switch(Demo)
         {
            case dmDownInToPipe:
            case dmUpOutOfPipe:
            {
               if ( PipeCode[1] == 'ç' )
               {
                  if ( !Passed )
                  {
                     Passed = true;
                     TextCounter = 0;
                  }
               }

               DemoCounter1++;
               if ( DemoCounter1 % 3 == 0 )
               {
                  if ( Demo == dmDownInToPipe )
                  {
                     DemoY++;
                     if ( (DemoY > 2 * H - Small) )
                     {
                        DemoCounter2++;
                        DemoY--;
                        if ( DemoCounter2 > 10 )
                           InPipe = true;
                     }
                  }
                  else
                  {
                     DemoY--;
                     if ( (DemoY < 0) )
                     {
                        DemoY++;
                        Demo = dmNoDemo;
                     }
                  }
               }
               break;
            }
            case dmUpInToPipe:
            case dmDownOutOfPipe:
            {
               DemoCounter1++;
               if ( DemoCounter1 % 3 == 0 )
               {
                  if ( Demo == dmDownOutOfPipe )
                  {
                     DemoY++;
                     if ( DemoY > - Small )
                     {
                        Demo = dmNoDemo;
                        DemoY--;
                     }
                  }
                  else
                  {
                     DemoY--;
                     if ( (DemoY < -2 * H + Small) )
                     {
                        DemoCounter2++;
                        DemoY++;
                        if ( DemoCounter2 > 10 )
                           InPipe = true;
                     }
                  }
               }
               break;
            }

            case dmDead:
            {
               DemoCounter1++;
               if ( DemoCounter1 % 7 == 0 )
                  YVel++;
               Y = Y + YVel;
               if ( Y > NV * H )
                  GameDone = true;
               break;
            }
         }
      }
      
      private static void StartDemo (int dm)
      {
         Demo = dm;
         DemoCounter1 = 0;
         DemoCounter2 = 0;
         DemoX = 0;
         DemoY = 0;
         Below1 = ' ';
         Below2 = ' ';
         AtCh1 = ' ';
         AtCh2 = ' ';
         
         if ( dm == dmDownInToPipe || dm == dmUpInToPipe || dm == dmDownOutOfPipe || dm == dmUpOutOfPipe )
            StartMusic (PipeMusic);
         
         switch (dm)
         {
            case dmUpOutOfPipe:
               DemoY = 2 * H - 9 * (byte)(Data.mode[Player] == mdSmall );
               break;
            case dmDownOutOfPipe:
            {
               DemoY = -2 * H;
               Y += H - 7 * (byte)(Data.mode[Player] == mdSmall) - 2;
               break;
            }
            case dmDead:
            {
              YVel = -3;
              Beep (220 );
              break;
            }
         }
         InPipe = false;
      }
      
      private static void CheckPipeBelow()
      {
         int Mo;
         if ( (XVel != 0) || (YVel != 0) || (Y % H != 0) )
            return;
         Mo = X % W;
//         if ( !(Mo in [4 .. W - 4]) )
         if ( Mo >= 4 && Mo <= W - 4 )
            return;
         if ( (Below1 != '0') || (Below2 != '1') || (!(AtCh1 >= 'à' && AtCh1 <= 'ç')) // $E0..$E7: Enter pipe
               || (!(AtCh2 >= 'à' && AtCh2 <= 'ï')) )
            return;
         PipeCode[1] = AtCh1;
         PipeCode[2] = AtCh2;
         StartDemo (dmDownInToPipe );
      }
      
      private static void CheckPipeAbove (char C1, char C2)
      {
         Mo = X % W;
         if ( !(Mo >= 4 && Mo <= W - 4) )
            return;
         if ( (C1 != '0') || (C2 != '1') )
            return;
         MapX = X / W;
         MapY = Y / H + 1;
         if ( (!(WorldMap[MapX, MapY] >= 'à' && WorldMap[MapX, MapY] <= 'ç')) // $E0..$E7: Enter pipe
                  || (!(WorldMap[MapX + 1, MapY] >= 'à' && WorldMap[MapX + 1, MapY] <= 'ï')) )
            return;
         PipeCode[1] = WorldMap[MapX, MapY];
         PipeCode[2] = WorldMap[MapX + 1, MapY];
         StartDemo (dmUpInToPipe );
      }
      
      
      // !!! CheckFall && CheckJump are inside Check !!! *moved
      private static void Check()
      {
         int Side, NewX1, NewX2, NewY, Y1, Y2, Y3, Mo;
         char NewCh1, NewCh2, NewCh3, ch;
         bool Small, Hold1, Hold2, Hold3, Hit;

         NewCh1 = ' ';
         NewCh2 = ' ';
         NewCh3 = ' ';

         Side = (byte)(XVel > 0) * (W - 1);
         NewX1 = (X + Side) / W;
         NewX2 = (X + Side + XVel) / W;
         Small = Data.mode[Player] == mdSmall;

         if ( NewX1 != NewX2 )
         {
            Y1 = (Y + HSafe + (4)) / H - Safe;
            Y2 = (Y + HSafe + H) / H - Safe;
            Y3 = (Y + HSafe + 2 * H - 1) / H - Safe;
            NewCh1 = WorldMap[NewX2, Y1];
            NewCh2 = WorldMap[NewX2, Y2];
            NewCh3 = WorldMap[NewX2, Y3];

            if ( NewCh3 == '*' )
               HitCoin (NewX2 * W, Y3 * H, false );
            
            if ( NewCh2 == '*' )
               HitCoin (NewX2 * W, Y2 * H, false ); 
            else if ( NewCh2 == 'z' )
               Turbo = true;


         	if (!Small && NewCh1 == '*' )
               HitCoin (NewX2 * W, Y1 * H, false );

            Hold1 = ( CanHoldYou(NewCh1) ) && (!Small );
            Hold2 = ( CanHoldYou(NewCh2) );
            Hold3 = ( CanHoldYou(NewCh3) );

            if ( Hold1 || Hold2 || Hold3 )
            {
               XVel = 0;
               Walkingmode = 0;
            }
         }

         NewX1 = (X + XVel) / W;
         NewX2 = (X + XVel + W - 1) / W;

         if ( cdEnemy != 0 )
            CheckJump;

         if ( (Status == stJumping) )
            NewY = (Y + 1 + (4) + (H - 1 - (4)) * (byte)(Small) + YVel + HSafe) / H - Safe;
         else
            NewY = (Y + 1 + 2 * H + YVel + HSafe) / H - Safe;

         NewCh1 = WorldMap[NewX1, NewY];
         NewCh2 = WorldMap[NewX2, NewY];
         NewCh3 = WorldMap[(X + XVel + W / 2) / W, NewY];
         Hold1 = ( CanHoldYou(NewCh1) || CanStandOn(NewCh1)  );
         Hold2 = ( CanHoldYou(NewCh2) || CanStandOn(NewCh2)  );
         Hold3 = ( CanHoldYou(NewCh3) || CanStandOn(NewCh3)  );

         switch (Status)
         {
            case stFalling:
            {
              	CheckFall();
            }
            case stOnTheGround:
            {
               if ( (cdLift == 0) )
               {
                  if ( !(Hold1 || Hold2) )
                  {
                     Status = stFalling;
                     if ( Math.Abs(XVel) < 2 )
                        Y++;
                  }
                  else
                  {
                     if  ( (NewCh1 == 'K') || (NewCh2 == 'K') )
                        CheckFall();
                     else
                     {
                        if ( XVel == 0 )
                        {
                           Below1 = NewCh1;
                           Below2 = NewCh2;
                        	MapX = NewX1;  //Codes for pipes
                           MapY = NewY - 1;
                        	AtCh1 = WorldMap[MapX, MapY];
                        	AtCh2 = WorldMap[MapX + 1, MapY];

                           //Mo = (X + XVel) % W;
                           Mo = (X) % W;
                           if ( !Hold1 && (Mo >= 1 && Mo <= 5) )
                              XVel--;
                           if ( !Hold2 && (Mo >= W - 5 && Mo <= W - 1) )
                              XVel++;
                        }
                     }
                
                     CheckJump;
                  }
               }
               else
               {
                  YVel = PlayerYVel;
                  CheckJump;
               }
            }

            case stJumping:
            {
               Hold1 = ( CanHoldYou(NewCh1) || CanStandOn(NewCh1)  );
               Hold2 = ( CanHoldYou(NewCh2) || CanStandOn(NewCh2)  );
               Hold3 = ( CanHoldYou(NewCh3) || CanStandOn(NewCh3)  );

               Hit = (Hold1 || Hold2 );
               if ( Hit )
               {
                  Mo = (X + XVel) % W;
                  if ( (Mo >= 1 && Mo <= 4 && Mo >= W - 4 && Mo <= W - 1) && (!Hold3) )
                  {
                     if ( !(( NewCh1 == Buffers.Hidden ) && ( NewCh2 == Buffers.Hidden )) )
                        Hit = false;
                     if ( (Mo < W / 2) && (!( NewCh2 == Buffers.Hidden )) )
                        X -= Mo;
                     else
                        if ( (Mo >= W / 2) && (!( NewCh1 == Buffers.Hidden )) )
                           X += W - Mo;
                  }
               }
               if ( !Hit )
               {
                  if ( newCh1 == '*' )
                     HitCoin (NewX1 * W, NewY * H, false );
                  
                  if ( NewCh1 == '*' )
                     HitCoin (NewX2 * W, NewY * H, false );
                
                  if ( (Counter % (JumpDelay + Byte(HighJump)) = 0) || ((!keyAlt) && (!HitEnemy)) )
                     YVel++;
                  if ( YVel >= 0 )
                  {
                     YVel = 0;
                     Status = stFalling;
                  }
               }
               else
               {
//                  Ch = #0;
                  Ch = (char)0;

                  //switch (Mo)
                  //{
                  if (Mo >= 0 && Mo <= (W / 2 - 1))
                  {
                     if (  CanHoldYou(NewCh1) || CanStandOn(NewCh1)  )
                     {
                        Ch = NewCh1;
                        NewX2 = NewX1;
                     }
                     else
                        Ch = NewCh2;
                  }
                  else if (Mo >= (W / 2) && Mo <= W - 1)
                  {
                     Ch = NewCh2;
                     if ( !( CanHoldYou(Ch) || Ch == Buffers.Hidden ) )
                     {
                        Ch = NewCh1;
                        NewX2 = NewX1;
                     }
                  }
                  //}
             
                  switch (Ch)
                  {
                     case '=':
                        cdHit = 1;
                     case '0':
                     case '1':
                        if ( keyUp )
                           CheckPipeAbove (NewCh1, NewCh2 );

                     case '?':
                     case '$':
                     case 'J':
                     case 'K':
                     {
                        Mo = 0;

                        if ( WorldMap[NewX2, NewY - 1] >= 'à' && WorldMap[NewX2, NewY - 1] <= 'â' )
                        {
                           WorldMap[NewX2, NewY] = '?';
                           Ch = '?';
                        }
                        else if ( WorldMap[NewX2, NewY - 1] == 'ï' )
                        {
                           WorldMap[NewX2, NewY] = 'K';
                           Ch = 'K';
                        }
                        else
                        {
                           if ( !Small && (Ch == 'J') )
                           {
                              BreakBlock (NewX2, NewY );
                              AddScore (10);
                              Mo = 1;
                           }
                        }

                        if ( Mo == 0 )
                        {
                           BumpBlock (NewX2 * W, NewY * H );
                           Beep (110 );
                        }

                        if (WorldMap[NewX2, NewY - 1] >= 'ã' && WorldMap[NewX2, NewY - 1] <= 'ì')
                        {
                           if ( !(Ch == 'J' || Ch == 'K') )
                           {
                              HitCoin (NewX2 * W, NewY * H, true );
                              if ( WorldMap[NewX2, NewY - 1] != ' ' )
                              {
                                 WorldMap[NewX2, NewY - 1] = Succ (WorldMap[NewX2, NewY - 1] );
                                 if ( WorldMap[NewX2, NewY] = '$' )
                                 {
                                    Remove (NewX2 * W, NewY * H, W, H, 2 );
                                    WorldMap[NewX2, NewY] = '?';
                                 }
                              }
                           }
                        }
                        else if (WorldMap[NewX2, NewY - 1] == 'à')
                        {
                           if ( Data.mode[Player] = mdSmall )
                              NewEnemy (tpRisingChamp, 0, NewX2, NewY, 0, -1, 2);
                           else
                              NewEnemy (tpRisingFlower, 0, NewX2, NewY, 0, -1, 2 );
                        }
                        else if (WorldMap[NewX2, NewY - 1] == 'á')
                           NewEnemy (tpRisingLife, 0, NewX2, NewY, 0, -1, 2 );
                        else if (WorldMap[NewX2, NewY - 1] == 'â')
                           NewEnemy (tpRisingStar, 0, NewX2, NewY, 0, -1, 1 );
                        else if (WorldMap[NewX2, NewY - 1] == '*') 
                           HitCoin (NewX2 * W, (NewY - 1) * H, false );
                        else if (WorldMap[NewX2, NewY - 1] == 'í') 
                           NewEnemy (tpRisingChamp, 1, NewX2, NewY, 0, -1, 2 );



                        HitAbove (NewX2, NewY - 1 );
                        if ( Ch == 'K' )
                        {
                           Remove (NewX2 * W, NewY * H, W, H, tpNote );
                           WorldMap[NewX2, NewY] = 'K';
                        }
                        else
                        {
                           if (Ch != 'J')
                              if (!(WorldMap[NewX2, NewY - 1] >= 'ã' && WorldMap[NewX2, NewY - 1] <= 'ì'))
                              {
                                 Remove (NewX2 * W, NewY * H, W, H, 1 );
                                 WorldMap[NewX2, NewY] = '@';
                              }
                        }
                     }
                     default:
                        Beep (30);
                  }
                  
                  if ( (Ch != 'J') || (Data.mode[Player] == mdSmall) )
                  {
                     YVel = 0;
                     Status = stFalling;
                  }
                  if ( Ch == 'K' )
                     YVel = 3;
               }
            }
         }
      }
      
      private static void CheckFall()
      {
         if ( !(Hold1 || Hold2) )
         {
            if ( NewCh1 == '*' )
               HitCoin( NewX1 * W, NewY * H, false );
            
            if ( NewCh1 == '*' )
               HitCoin( NewX2 * W, NewY * H, false );
               
            if ( Counter % JumpDelay == 0 )
               YVel++;
            
            if ( YVel > MaxYVel )
               YVel = MaxYVel;
         }
         else
         {
            if ( (NewCh1 == '=') || (NewCh2 == '=') )
               cdHit = 1;

            Mo = (X + XVel) % W;
            Y = ((Y + YVel + 1 + HSafe) / H - Safe) * H;
            YVel = 0;
            Status = stOnTheGround;
            Jumped = true;

            if ( (NewCh1 == 'K') || (NewCh2 == 'K') )
            {
               StartMusic ( NoteMusic );
               if ( NewCh1 == 'K' )
               {
                  BumpBlock ( NewX1 * W, NewY * H );
                  Remove ( NewX1 * W, NewY * H, W, H, tpNote );
                  WorldMap[NewX1, NewY] = 'K';
               }
               if ( NewCh2 == 'K' )
               {
                  BumpBlock (NewX2 * W, NewY * H );
                  Remove (NewX2 * W, NewY * H, W, H, tpNote );
                  WorldMap[NewX2, NewY] = 'K';
               }
               Counter = 0;
               Status = stJumping;
               Jumped = false;
               HighJump = true;
               YVel = -5;
               HitEnemy = true;
            }
         }
         
         if ( Mo >= 0 && Mo <= W / 2 - 1 )
         {
            if ( Hold1 )
            {
               Ch = NewCh1;
               NewX2 = NewX1;
            }
            else
               Ch = NewCh2;
         }
         else if ( Mo >= W / 2 && Mo <= W )
         {
            if ( Hold2 )
               Ch = NewCh2;
            else
            {
               Ch = NewCh1;
               NewX2 = NewX1;
            }
         }
         
      }
      
      private static void CheckJump()
      {  
         if (cdEnemy != 0)
         {
            HitEnemy = true;
            Jumped = false;
         }
         if ( !Jumped )
         {
            if (keyAlt || HitEnemy)
            {
               Counter = 0;
               Status = stJumping;
               HighJump = (Math.Abs(XVel) == 2) || (HitEnemy && keyAlt );
               YVel = - JumpVel - 2 * (byte)(HitEnemy && keyAlt) - (byte)(Turbo );
            }
         }
         cdEnemy = 0;
      }

      public static void MovePlayer()
      {  
         int MaxSpeed, MinSpeed, OldXVel, OldXView;
         bool CheckX;
         byte OldDir;
         bool LastKeyRight, LastKeyLeft;
       
         if (InPipe)
         {
            if ( WorldMap[MapX, MapY + 1] == '0' )
               StartDemo (dmUpOutOfPipe );
            else
               if ( WorldMap[MapX, MapY - 1] == '0' )
                  StartDemo (dmDownOutOfPipe );
            return;
         }

         if (cdChamp != 0)
         {
            if ( Data.mode[Player] == mdSmall )
            {
               Data.mode[Player] = mdLarge;
               Growing = true;
               GrowCounter = 0;
            }
            StartMusic (GrowMusic );
            cdChamp = 0;
         }
         
         if (cdLife != 0)
         {
            cdLife = 0;
            AddLife( );
         }
         if (cdFlower != 0)
         {
            Data.mode[Player] = mdFire;
            Fired = true;
            FireCounter = 0;
            StartMusic (GrowMusic );
            Growing = true;
            GrowCounter = 0;
            cdFlower = 0;            
         }

         if ( !Blinking && !Star && !Growing )
         {
            if ( cdHit != 0 )

            switch (Data.mode[Player])
            {
               case mdSmall:
               {
                  BlinkCounter = 0;
                  Blinking = true;
                  StartDemo (dmDead );
                  StartMusic (DeadMusic );
                  return;
               }
               case mdLarge:
               case mdFire:
               {
                  Data.mode[Player] = mdSmall;
                  BlinkCounter = 0;
                  Blinking = true;
                  StartMusic (HitMusic );
                  break;
               }
               default:
//                  throw new Exception( );
                  break;
            }
            cdHit = 0;
         }
         else
            cdHit = 0;

         if (Blinking)
         {
            BlinkCounter++;
            if (BlinkConter >= BlinkTime)
               Blinking = false;
         }

         if (cdStar != 0)
         {
            StartMusic(StarMusic );
            StarCounter = 0;
            Star = true;
         }

         if (Star)
         {
            StarCounter++;
            if ( StarCounter >= StarTime )
               Star = false;
            if ( StarCounter % 3 == 0 )
               StartGlitter (X, Y + 11 * (byte)(Data.mode[Player] = mdSmall), W, H + 3 + 11 * (byte)(Data.mode[Player] != mdSmall) );
            cdStar = 0;
         }

         if (Growing)
         {
            GrowCounter++;
            if ( GrowCounter > GrowTime )
               Growing = false;
         }

         Counter++;
         if ( (XVel == 0) && (YVel == 0) )
            Counter = 0;
         CheckX = (Counter % Slip == 0 );
         
         OldDir = Direction;
         OldXVel = XVel;
         
         ReadJoystick( );
         
         LastKeyLeft = KeyLeft;
         LastKeyRight = KeyRight;
         
         keyLeft = kbLeft || jsLeft;
         keyRight = kbRight || jsRight;
         keyUp = kbUp || jsUp;
         keyDown = kbDown || jsDown;
         keyAlt = kbAlt || jsButton1;
         keyCtrl = kbCtrl || jsButton2;
         keySpace = kbSpace || jsButton2;

         if ( keyRight && (!LastKeyRight) && (Direction == dirLeft) )
         {
            OldDir = dirRight;
            OldXVel = -XVel;
         }
         if ( keyLeft && (!LastKeyLeft) && (Direction == dirRight) )
         {
            OldDir = dirLeft;
            OldXVel = -XVel;
         }


         if ( Fired && (!keySpace) )
            Fired = false;

         if ( keySpace && (!Fired) && (Data.mode[Player] == mdFire) )
         {
            FireCounter = 0;
            NewEnemy (tpFireBall, 0, X / W + Direction, (Y + H) / H,
               10 * (-1 + 2 * Direction), 3 + 3 * ((byte)(keyDown) - (byte)(keyUp)), 2 );
            Fired = true;
         }

         if ( cdLift != 0 )
         {
            Y = PlayerY1;
            XVel = PlayerXVel;
            YVel = PlayerYVel;
            Status = stOnTheGround;
         }
         if ( cdStopJump != 0 )
         {
            Jumped = true;
            cdStopJump = 0;
         }

         if ( Jumped && (!keyAlt) )
            Jumped = false;

         MaxSpeed = +MAX_SPEED - 1 + (byte)(keyCtrl) + (byte)(Turbo) + Math.Abs(cdLift * PlayerXVel );
         MinSpeed = -MAX_SPEED + 1 - (byte)(keyCtrl) - (byte)(Turbo) - Math.Abs(cdLift * PlayerXVel );

         if (keyLeft)
         {
            if ( (XVel > MinSpeed) )
            {
               if ( CheckX || (cdLift != 0) )
                  XVel -= 1 + (byte)((cdLift != 0) && keyCtrl );
            }
            else
               XVel = MinSpeed;
            Direction = (byte)(XVel > 0 );
            if ( X + XVel < 0 )
               XVel = - X;
         }
         else
            if ( (XVel < 0) && CheckX && (cdLift == 0) )
               XVel++;

         if ( keyRight )
         {
            if ( (XVel < MaxSpeed) )
            {
               if ( CheckX || cdLift != 0 )
                 XVel += 1 + (byte)( cdLift != 0 && keyCtrl );
            }
            else
               XVel = MaxSpeed;
            Direction = (byte)(XVel >= 0 );
         }
         else
            if ( (XVel > 0) && CheckX && (cdLift == 0) )
               XVel--;

         if ( keyLeft && keyRight )
         {
            Direction = OldDir;
            XVel = OldXVel;
         }

         if ( Y + YVel >= NV * H )
         {
            GameDone = true;
            StartMusic (DeadMusic );
         }

         if ( Status = stOnTheGround )
            HitEnemy = false;

         Check( );

         if ( (Status == stOnTheGround) && YVel == 0 )
         {
            if ( (XVel == 0) || ((cdLift != 0) && (XVel == PlayerXVel)) )
            {
               Walkingmode = 0;
               WalkCount = 0;
            }
            else
            {
               WalkCount++;
               WalkCount = WalkCount & 0xF;
               Walkingmode = (byte)(WalkCount < 0x8);
            }
         }
         else
         {
            if ( YVel < 0 )
               Walkingmode = 2;
            else
               Walkingmode = 3;
         }
        
         if ( keyDown )
            CheckPipeBelow;

         X += XVel;
         Y += YVel;

         OldXView = XView;
         XView = XView - (Word)(kbLeftShift) + (Word)(kbRightShift );
         if ( X + W + SCROLL_AT > XView + 320 )
            XView = X + W + SCROLL_AT - 320;
         if ( X < XView + SCROLL_AT )
            XView = X - SCROLL_AT;
         if ( XView - OldXView > MAX_SPEED + (byte)(Turbo) )
            XView = OldXView + MAX_SPEED + (byte)(Turbo );
         if ( XView - OldXView < -MAX_SPEED - (byte)(Turbo) )
            XView = OldXView - MAX_SPEED - (byte)(Turbo );
         if ( XView < 0 )
         {
            XView = 0;
            if ( X < 0 ) X = 0;
         }

//        with Options do
         if ( XView > (Options.XSize - NH) * W )
            XView = (Options.XSize - NH) * W;
         if ( XView < OldXView )
            if ( (WorldMap[XView / W, NV] == 254) )
               if ( (WorldMap[(XView / W), Math.Round(PlayerY1 / H, 1)] != ' ') )
                  XView = OldXView;
         if ( XView > OldXView )
            if ( (WorldMap[((XView - 1) / W + NH), NV] == 255) )
               if ( (WorldMap[((XView - 1) / W + NH), Math.Round(PlayerY1 / H, 1)] != ' ') )
                  XView = OldXView;
                  
         PlayerX1 = X + XVel;
         PlayerX2 = PlayerX1 + W - 1;
         PlayerY1 = Y;
         if (Data.mode[Player] == mdSmall)
            PlayerY1 = Y + H;
         else
            PlayerY1 = Y;
         PlayerY2 = Y + 2 * H - 1;
         PlayerXVel = XVel;
         PlayerYVel = YVel;

         if ( cdLift != 0 )
         {
            PlayerYVel += 2 - YVel;
            cdLift = 0;
         }
      }
      
*/

   } // end class Players
} // end namespace MarioPort