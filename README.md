# OakBot
Twitch Bot focused on integration with HTML overlays written in C#
Free open solid feature rich Twitch Bot for everyone to use.




Stream overlay Goals:
  - Outputting of events in textfiles
  - Outputting of events as json
      - Think of currentsong, giveaways, timers ect.
      - This would remove the need for Twitch alerts client.

Twitch Dashboard Goals:
  - Set stream game and title.
  - Manual commercials
  - Automated commercials > timespan, length, warning-message and starting message
  - View session followers and subscribers
  - View session donators (from TwitchAlerts API)
  - View session hosts and raids (whenever possible, later stage)
  - Auto host

Twitch Chat Interface Goals:
  - Right-Mouse-Button context menu:
      - Twitch Profile
      - Compose Twitch Message > opens default browser with Twitch compose message
      - Purge (/timeout 1)
      - Timeout 5m
      - Timeout 10m
      - Ban
  - Double-click message > Opens up a new window showing:
      - Same options as context menu
      - Shows Twitch status (that gets loaded, depending on Database entries)
      - Shows users chat messages only

Command System Goals:
  - Simple text replies
  - Advanced command creation [e.g. Deepbot and Ankhbot features]
  - Command chaining [with optional delay]
  - Command cost [when point system is implemented]
  - Output to chat under the bot or streamer's name
  - Streamer template messages (think of a sub hype message)

Song Request Goals:
  - load from YouTube playlist
  - load from local playlist(s)
  - Not capped (whenever possible)
  - One list, insert requests between the playlist sings
  - Streamer requested songs inserted but not pushed down by viewer requests
  - Songlist output to pastebin
  - Song blacklist
  - Build in commands:
      - !songrequest URI > request a song from youtube
      - !blacklist > blacklist current song AND skip (mod+)
      - !nextsong > plays next song (mod+)
      - !prevsong > plays prev song (mod+)
      - !vote !veto > Vote/Veto to skip a song

Further Goals:
  - SFX
  - Giveaway system
  - Timed Messages/Commands
  - Queue System
  - Remote OBS integration
  - Point system
  - Importing data from Deepbot and Ankhbot
  - Backup (automated) database to folder/Box/Drive/Dropbox (outside %appdata%)

Other possibilities at a later stage:
  - Alerts
  - Discord integration
  - Let the bot listen to its own channel
  - API to pull out data
