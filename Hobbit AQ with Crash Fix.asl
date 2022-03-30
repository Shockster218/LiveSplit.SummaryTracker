//The Hobbit 1.3 Autosplitter by MD_Pi
state("meridian")
{
	bool onCutscene : 0x35CCE4;
	int cutsceneID : 0x35CD00; 
	bool loadScreen: 0x35F8C8;
	int oolState : 0x362B58;
	int levelID : 0x362B5C;
	bool menuClosed : 0x413040;
}
startup
{
	settings.Add("race", false, "Race Mode");
	vars.level = -1;
	refreshRate = 30;
	vars.crashed = false;
}
start
{
	//Timing starts if you are playing a storybook cinema on the title screen
	//Set level variable to Dream World
	if (current.levelID == -1 && current.menuClosed == true && current.oolState == 17)
	{
		//print("Entering Dream World")
		vars.level = 0;
		return true;
	}
}
split
{
	if (settings["race"] == true && vars.level == -1)
    {
        vars.level = 1;
    }

	if(current.oolState == 6 && timer.CurrentPhase == TimerPhase.Running && timer.CurrentSplitIndex > 0)
	{
		vars.crashed = true;
		return false;
	}

	//Split if level is incremented and the vendor  and the storybook cinema are over
	//Set level variable to current level
	if (current.oolState == 19 && (current.levelID - vars.level) > 0)
	{
		if(vars.crashed)
		{
			vars.level = current.levelID + 1;
			vars.crashed = false;
			return false;
		}
		else
		{
			vars.level += 1;
			return true;
		}
	}
	//Split if you are in The Clouds Burst and the Rube Goldberg barrel cinema is playing
	//Set level variable to title screen to prevent automatic reset after credits play
	if (current.levelID == 11 && current.onCutscene == true && current.cutsceneID == 0x3853B400)
	{
		//print("GG")
		vars.level = -1;   
		return true;
	}
}
reset
{
	//Reset if you go from a level other than the title screen to the title screen 
	if (current.levelID == -1 && current.menuClosed == false && vars.level != -1)
	{
		//print("Returned to Title Screen")
		vars.level = -1;
		return true;
	}
}
isLoading
{
	//This parts pretty obvious. If there's a load screen, It's loading
	//print("Loading")
	return current.loadScreen;
}
	