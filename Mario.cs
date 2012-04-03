/**  uses
    CPU286,
    Play,
    Players,
    Enemies,
    Buffers,
    VGA256,
    Worlds,
    BackGr,
    KeyBoard,
    Joystick,
    Figures,
    Palettes,
    Txt,
    Crt,
    Dos;
**/
public class Mario
{
	public const int NUM_LEV = 6;
	public const int LAST_LEV = 2 * NUM_LEV -1;
	public const int MAX_SAVE = 3;
	public const int WAIT_BEFORE_DEMO = 500;
	public struct ConfigData
	{
		public bool Sound;
		public bool SLine;
		public GameData[] Games = new GameData[MAX_SAVE-1]
		public bool UseJS;
		public JoyRec JSDat;
	}
	public ConfigData ConfigFile = file;
	public int GameNumber;
	public int CurPlayer;
	public int Passed;
	public bool EndGame;
	public ConfigData Config;
	
#IFDEF DEBUG
	public void MouseHalt()
	{
		//halt
	}
#ENDIF

/**

  {$I Block.$00}

  {$I Intro.$00}
  {$I Intro.$01}
  {$I Intro.$02}

  {$I Start.$00}
  {$I Start.$01}
**/

	public void NewData
	{
		Data[plMario].Lives = 3;
		Data[plLuigi].Lives = 3;
		Data[plMario].Coins = 0;
		Data[plLuigi].Coins = 0;
		Data[plMario].Score = 0;
		Data[plLuigi].Score = 0;
		Data[plMario].Progress = 0;
		Data[plLuigi].Progress = 0;
		Data[plMario].Mode = mdSmall;
		Data[plLuigi].Mode = mdSmall;
	}

	public string GetConfigName
	{
		string S;
		byte len;
		S = ParamStr(0);
		S[Len - 2] = 'C';
		S[Len - 1] = 'F';
		S[Len - 0] = 'G';
		return S;
	}
	
	public void ReadConfig
	{
		int i, j;
		ConfigFile F;
		string Name;
#if MENU
		Assign(F, GetConfigName);
		Reset(F);
		Read(F, Config);
		Close(F);
		if IOResult != 0 
#endif
		{
			NewData();
			for (int i = 0; i < MAX_SAVE - 1; i++)
				Config.Games[i] = Data;
			Config.SLine = true;
			Config.Sound = true;
			Config.UseJS = false;
			GameNumber = -1;
		}
		Config.Play.Stat = SLine;
		Config.Buffers.BeeperSound = Sound;
		Name = ParamStr(0);
		j = 0;
		if (Name.Length() > 9)
			Name = Name.Remove(1, Name.Length() - 9);
		for(int i = 0; i < Name.Length(); i++)
			Inc(j, Ord(Name[i].ToUpper()));
		if (j != 648)
			RunError(201);
	}
	
	public void WriteConfig()
	{
		ConfigFile F;
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
	
	public void CalibrateJoystick
	{
	// TODO if we implement joystick
	}
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
  
  public void ReadCmdLine()
  {
  }
  /**
  procedure ReadCmdLine;
    var
      i, j: Integer;
      S: String;
  begin
    for i := 1 to ParamCount do
    begin
      S := ParamStr (i);
      while S <> '' do
      begin
        if (Length (S) >= 2) and (S[1] in ['/', '-'])
        then
        begin
          case UpCase (S[2]) of
            'S': Play.Stat := TRUE;
            'Q': BeeperOff;
            'J': CalibrateJoystick;

          end;
          Delete (S, 1, 2);
        end
        else
          Delete (S, 1, 1);
      end;
    end;
  end;
**/

public void Demo()
{
}

/**
  procedure Demo;
  begin
    NewData;
    Turbo := FALSE;
    Data.Progress [plMario] := 5;
    PlayMacro;
    PlayWorld (' ', ' ', @Level_6a^, @Options_6a^, @Options_6a^,
      @Level_6b^, @Options_6b^, @Options_6b^, plMario);
    StopMacro;
  end;
**/

public void Intro()
{
	/**
  procedure Intro;
    var
      P, i, j, k, l, wd, ht, xp: Integer;
      NextNumPlayers,
      Selected: Integer;
      IntroDone,
      TestVGAMode,
      Update: Boolean;
      Counter: Integer;
      MacroKey: Char;
      Status,
      OldStatus,
      LastStatus: (ST_NONE,
                   ST_MENU,
                   ST_START,
                   ST_LOAD,
                   ST_ERASE,
                   ST_OPTIONS,
                   ST_NUMPLAYERS);
      Menu: array[1..5] of string[40];
      BG: array[0..MAX_PAGE, 1..5] of Word;
      NumOptions: Integer;
      Page: Byte;

	**/
	//nested procedures
	void Up()
	{
	/**
	procedure Up;
    begin
      if Selected = 1 then
      begin
        if Status = ST_MENU then
          Selected := NumOptions
        else
          MacroKey := kbEsc;
      end
      else
        Dec (Selected);
    end;
	**/
	}
	void Down()
	{
	/**
	procedure Down;
    begin
      if Selected = NumOptions then
      begin
        if Status = ST_MENU then
          Selected := 1
        else
          MacroKey := kbEsc;
      end
      else
        Inc (Selected);
    end;
	**/
	}
/**
	begin
    Page := CurrentPage;
    Status := ST_NONE;
    TestVGAMode := FALSE;
    GameNumber := -1;
    NextNumPlayers := Data.NumPlayers;

    repeat
      if TestVGAMode then
        InitVGA;
      TestVGAMode := FALSE;
      IntroDone := FALSE;
      NewData;

      PlayWorld (#0, #0, @Intro_0^, @Options_0^, @Options_0^,
        @Intro_0^, @Options_0^, @Options_0^, plMario);
      InitBackGr (3, 0);

      OutPalette ($A0, 35, 45, 50);
      OutPalette ($A1, 45, 55, 60);

      OutPalette ($EF, 30, 40, 30);
      OutPalette ($18, 10, 15, 25);

      OutPalette ($8D, 28, 38, 50);
      OutPalette ($8F, 40, 50, 63);

      for i := 1 to 50 do
        BlinkPalette;

      for P := 0 to MAX_PAGE do
      begin
        for i := 1 downto 0 do
          for j := 1 downto 0 do
            for k := 1 downto 0 do
            begin
              DrawImage (38 + i + j, 29 + i + k, 108, 28, @Intro000^);
              DrawImage (159 + i + j, 29 + i + k, 24, 28, @Intro001^);
              DrawImage (198 + i + j, 29 + i + k, 84, 28, @Intro002^);
            end;

        DrawBackGrMap (10 * H + 6, 11 * H - 1, 54, $A0);
        DrawBackGrMap (10 * H + 6, 11 * H - 1, 55, $A1);
        DrawBackGrMap (10 * H + 6, 11 * H - 1, 53, $A1);
        for i := 0 to NH - 1 do
          for j := 0 to NV - 1 do
            if (i in [0, NH - 1]) or (j in [0, NV - 1]) then
              DrawImage (i * W, j * H, W, H, @Block000^);
        DrawPlayer;
        ShowPage;
      end;
      UnlockPal;
      Key := #0;
      FadeUp (64);
      ResetStack;

      FillChar (BG, SizeOf (BG), 0);
      FillChar (Menu, SizeOf (Menu), 0);
      SetFont (0, Bold + Shadow);

      if Status <> ST_OPTIONS then
      begin
        OldStatus := ST_NONE;
        LastStatus := ST_NONE;
        Status := ST_MENU;
        Selected := 1;
      end;
      UpDate := TRUE;

      Counter := 1;
      repeat

        if UpDate or (Status <> OldStatus) then
        begin
          if (Status <> OldStatus) then
            Selected := 1;
          case Status of
            ST_MENU:
              begin
                 Menu[1] := 'START';
                 Menu[2] := 'OPTIONS';
                 Menu[3] := 'END';
                 Menu[4] := '';
                 Menu[5] := '';
                 NumOptions := 3;
                 LastStatus := ST_MENU;
               end;
            ST_OPTIONS:
              begin
                 if BeeperSound then
                   Menu[1] := 'SOUND ON '
                 else
                   Menu[1] := 'SOUND OFF';
                 if Play.Stat then
                   Menu[2] := 'STATUSLINE ON '
                 else
                   Menu[2] := 'STATUSLINE OFF';
                 Menu[3] := '';
                 Menu[4] := '';
                 Menu[5] := '';
                 NumOptions := 2;
                 LastStatus := ST_MENU;
              end;
            ST_START:
              begin
                 Menu[1] := 'NO SAVE';
                 Menu[2] := 'GAME SELECT';
                 Menu[3] := 'ERASE';
                 Menu[4] := '';
                 Menu[5] := '';
                 NumOptions := 3;
                 LastStatus := ST_MENU;
               end;
            ST_NUMPLAYERS:
              begin
                 Menu[1] := 'ONE PLAYER';
                 Menu[2] := 'TWO PLAYERS';
                 Menu[3] := '';
                 Menu[4] := '';
                 Menu[5] := '';
                 if (Status <> OldStatus) then
                   Selected := Data.NumPlayers;
                 NumOptions := 2;
                 LastStatus := ST_START;
               end;
            ST_LOAD,
            ST_ERASE:
              begin
                 Menu[1] := 'GAME #1 '#7' ';
                 Menu[2] := 'GAME #2 '#7' ';
                 Menu[3] := 'GAME #3 '#7' ';
                 Menu[4] := '';
                 Menu[5] := '';
                 for i := 1 to 3 do
                   with Config.Games[i - 1] do
                     if (Progress[plMario] = 0) and (Progress[plLuigi] = 0) then
                       Menu[i] := Menu[i] + 'EMPTY'
                     else
                     begin
                       j := Progress[plMario];
                       k := Byte (Progress [CurPlayer] >= NUM_LEV);
                       if k > 0 then
                         Dec (j, NUM_LEV);
                       if Progress[plLuigi] > j then
                       begin
                         j := Progress[plLuigi];
                         Progress[plMario] := j;
                       end;
                       Menu[i] := Menu[i] +
                         'LEVEL ' + Chr (j + Ord ('0') + 1) + ' ';
                       if k = 0 then
                         Menu[i] := Menu[i] + #7' '
                       else
                         Menu[i] := Menu[i] + '* ';
                       Menu[i] := Menu[i] +
                         Chr (NumPlayers + Ord ('0')) + 'P';
                     end;
                 NumOptions := 3;
                 LastStatus := ST_START;
               end;


          end;
          wd := 0;
          xp := 0;
          for i := 1 to 5 do
          begin
            j := TextWidth (Menu[i]);
            if j > wd then
            begin
              wd := j;
              xp := CenterX (Menu[i]) div 4 * 4;
            end;
            ht := 8;
          end;
          OldStatus := Status;
          Update := FALSE;
        end;

        MacroKey := #0;
        case Key of
          kbEsc:
            if Status = ST_MENU then
            begin
              IntroDone := TRUE;
              QuitGame := TRUE;
            end
            else
              Status := LastStatus;
          kbUpArrow:
            Up;
          kbDownArrow:
            Down;
          kbSP,
          kbEnter:
            case Status of

              ST_MENU:
                case Selected of
                  1: Status := ST_START;
                  2: Status := ST_OPTIONS;
                  3: begin
                       IntroDone := TRUE;
                       QuitGame := TRUE;
                     end;
                end;

              ST_START:
                case Selected of
                  1: Status := ST_NUMPLAYERS;
                  2: Status := ST_LOAD;
                  3: Status := ST_ERASE;
                end;

              ST_OPTIONS:
                case Selected of
                  1: if BeeperSound then
                       BeeperOff
                     else
                       BeeperOn;
                  2: Play.Stat := not Play.Stat;
                end;

              ST_NUMPLAYERS:
                case Selected of
                  1: begin
                       NextNumPlayers := 1;
                       IntroDone := TRUE;
                     end;
                  2: begin
                       NextNumPlayers := 2;
                       IntroDone := TRUE;
                     end;
                end;

              ST_LOAD:
                begin
                  GameNumber := Selected - 1;
                  Config.Games[GameNumber].NumPlayers := 1;
                  with Config.Games[GameNumber] do
                    if (Progress[plMario] = 0) and (Progress[plLuigi] = 0) then
                      Status := ST_NUMPLAYERS
                    else
                    begin
                      IntroDone := TRUE;
                      NextNumPlayers := Config.Games[GameNumber].NumPlayers;
                    end;
                end;

              ST_ERASE:
                begin
                  NewData;
                  Config.Games[Selected - 1] := Data;
                  Config.Games[Selected - 1].NumPlayers := 1;
                  GameNumber := -1;
                end;

            end;
        end;
        if Key <> #0 then
        begin
          Counter := 0;
          Key := MacroKey;
          Update := TRUE;
        end;

        for k := 1 to 5 do
        begin
          if BG[Page, k] <> 0 then
            PopBackGr (BG[Page, k]);
        end;

        for k := 1 to 5 do
        begin
          if Menu[k] <> '' then
          begin
            i := xp;
            j := 56 + 14 * k;
            BG[Page, k] := PushBackGr (50, j, 220, ht);
            if k = Selected then
              WriteText (i - 12, j, #16, 5);
            l := 15;
            if (Length (Menu[k]) > 19) and (Menu[k][19] = '*') then
              l := 14 + (Counter and 1);
            SetPalette (14, 63, 61, 31);
            WriteText (i + 8, j, Menu[k], l);
          end;
        end;

        ShowPage;
        BlinkPalette;
        ResetStack;

        Inc (Counter);
      until IntroDone or (Counter = WAIT_BEFORE_DEMO);
      FadeDown (64);

      if not IntroDone then
        Demo;
    until IntroDone and (not TestVGAMode);

    if GameNumber <> -1 then
      Data := Config.Games[GameNumber];
    Data.NumPlayers := NextNumPlayers;
  end;  { Intro }
**/
	void ShowPlayerName()
	{
	/**
	procedure ShowPlayerName (Player: Byte);
    var
      iW, iH, i: Integer;
  begin
    ClearPalette;
    LockPal;
    ClearVGAMem;
    SetView (0, 0);
    iH := 13;
    for i := 0 to MAX_PAGE do
    begin
      case Player of
        plMario:
          begin
            iW := 116;
            DrawImage (160 - iW div 2, 85 - iH div 2, iW, iH, @Start000^);
          end;
        plLuigi:
          begin
            iW := 108;
            DrawImage (160 - iW div 2, 85 - iH div 2, iW, iH, @Start001^);
          end;
      end;
      ShowPage;
    end;
    NewPalette (P256^);
    UnLockPal;
    Palettes.ReadPalette (Palette);
    for i := 1 to 100 do
      ShowPage;
    ClearPalette;
    ClearVGAMem;
  end;
	**/
	}
/**
	begin  { Mario }
  InitKeyBoard;
  Data.NumPlayers := 1;
  ReadConfig;
  ReadCmdLine;

  jr := Config.JSDat;
  jsEnabled := jsDetected and Config.UseJS;

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

  if not DetectVga then
  begin
    System.WriteLn ('VGA graphics adapter required');
    Halt (1)
  end;

  ResetKeyBoard;

  if not InGraphicsMode then
    InitVGA;

{$IFDEF MENU}
  repeat
{$ENDIF}

    ClearVGAMem;

    InitPlayerFigures;
    InitEnemyFigures;

    EndGame := FALSE;
  {$IFDEF MENU}
    Intro;
  {$ENDIF}

    Randomize;

    with Data do
    begin
      if NumPlayers = 2 then
        if Progress [plMario] > Progress [plLuigi] then
          Progress [plLuigi] := Progress [plMario]
        else
          Progress [plMario] := Progress [plLuigi];

      Lives [plMario] := 3;
      Lives [plLuigi] := 3;
      Coins [plMario] := 0;
      Coins [plLuigi] := 0;
      Score [plMario] := 0;
      Score [plLuigi] := 0;
      Mode [plMario] := mdSmall;
      Mode [plLuigi] := mdSmall;
    end;

    repeat
      if Data.NumPlayers = 1 then
        Data.Lives [plLuigi] := 0;
      for CurPlayer := plMario to Data.NumPlayers - 1 do
      begin
        if not (EndGame or QuitGame) then
          if Data.Lives [CurPlayer] >= 1 then
          begin
            with Data do
            begin
              Turbo := (Progress [CurPlayer] >= NUM_LEV);
              if Progress [CurPlayer] > LAST_LEV then
                Progress [CurPlayer] := NUM_LEV;
            end;
         {$IFDEF MENU}
            ShowPlayerName (CurPlayer);
         {$ENDIF}
            case Data.Progress [CurPlayer] mod NUM_LEV of
              0: Passed := PlayWorld ('x', '1', @Level_1a^, @Options_1a^, @Opt_1a^,
                   @Level_1b^, @Options_1b^, @Options_1b^, CurPlayer);
              1: Passed := PlayWorld ('x', '2', @Level_2a^, @Options_2a^, @Opt_2a^,
                   @Level_2b^, @Options_2b^, @Options_2b^, CurPlayer);
              2: Passed := PlayWorld ('x', '3', @Level_3a^, @Options_3a^, @Opt_3a^,
                   @Level_3b^, @Options_3b^, @Options_3b^, CurPlayer);
              3: Passed := PlayWorld ('x', '4', @Level_5a^, @Options_5a^, @Opt_5a^,
                   @Level_5b^, @Options_5b^, @Options_5b^, CurPlayer);
              4: Passed := PlayWorld ('x', '5', @Level_6a^, @Options_6a^, @Opt_6a^,
                   @Level_6b^, @Options_6b^, @Options_6b^, CurPlayer);
              5: Passed := PlayWorld ('x', '6', @Level_4a^, @Options_4a^, @Opt_4a^,
                   @Level_4b^, @Options_4b^, @Options_4b^, CurPlayer);
              else
                EndGame := TRUE;
            end;

            if Passed then
              Inc (Data.Progress [CurPlayer]);
            if QuitGame then
            begin
              EndGame := TRUE;
            {$IFDEF MENU}
              QuitGame := FALSE;
            {$ENDIF}
            end;
          end;

      end;
    until EndGame or QuitGame or
      (Data.Lives [plMario] + Data.Lives [plLuigi] = 0);

    if GameNumber <> -1 then
      Config.Games[GameNumber] := Data;

{$IFDEF MENU}
  until QuitGame;
{$ENDIF}
  WriteConfig;
end.
**/
}