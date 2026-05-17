# NullRAT
A sophisticated Remote Access Trojan coded in C# with 31 RAT commands, and professional UI control commands.

## C2/CONFIG
The configuration can be changed in NullRAT-C#/Config.

Change both:
    NullRAT-C#/Config/Config.cs - Change "ServerIP"
    NullRAT-C#/Config/Settings.json - Change "Host"

You are also free to change the AES keys to whatever you want, just make sure they keep the **correct** structure.

## COMMANDS
*UI commands*
    agents / list - Show all connected agents
    use <id> - Switch to agent by ID
    kill <id> - Terminate specific agent
    help - Show this help
    clear - Clear console
    exit - Close server

*RAT commands*
    shell <command> - Runs a shell command
    cd <path> - Changes directory to the provided one
    messagebox <text> - Displays a messagebox
    taskkill <target> - Kills a task/process
    download <url/path> - Downloads a file from a URL
    whoami - Runs the "whoami" command/provides user
    sysinfo - Returns system info
    ls - Lists files in directory
    dir - Lists directory
    pwd - Current directory path
    screenshot - Takes a screenshot
    webcam - Takes a webcam picture
    clipboard get - Grabs clipboard
    clipboard clear - Clears/wipes clipboard
    keylog_start - Starts a keylogger
    keylog_stop - Stops a keylogger
    keylog_dump - Dumps keylog data
    processes - Lists processes
    tasklist - Lists tasks
    drives - Lists system drives
    wifi - Lists wifi credentials
    chrome - Steals saved Chrome credentials
    reboot - Reboots computer
    shutdown - Shuts computer down
    persistence - Attempts persistence/autolaunch
    selfdestruct - Removes itself
    disable_defender - Disables Windows Defender
    uac_off - Disables UAC systemwide
    lock - Locks the workstation
    bsod - Black Screen of Death
    info - Detailed system info

## WARNINGS
Remote Access Trojans are not legal to distribute or launch on systems you do not control or have permission to access. Please only run the output compiled file in a sandboxed environment that you own.

I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.

## CONTACT
If you find an issue, you can contact me on either Telegram or Discord.
    TELEGRAM - silly_puppy (https://t.me/silly_puppy)
    DISCORD - silly.pup

## MODIFYING
You are free to modify:
    NullRAT-C#/Config/Config.cs
    NullRAT-C#/Config/Settings.json

You are free to modify if you put credit:
    NullRAT-C#/Server/UI
    NullRAT-C#/Server/AsciiArt.cs

You are free to modify if permitted in DMS and put credit:
    NullRAT-C#/

**CREDIT meaning: A link to this GitHub repository**

## DONATIONS
If you would like to donate to this project at some point and help out, here are some crypto addresses of mine.
    BTC - bc1q7vq8g50nyn2sw7mzx0d55jj3yqw7pe2zu6sa9p
    LTC - LQvtkJxeTNiZLo5jgd94aptpRWztGF5L2X
    ETH - 0xbc16846a31c012006ec33FBeCC58039553C45dd1
    SOL - CKjZAXLo1iS3viGExoJAH1b1gzQ4hyqoBRptrgxw58kY
