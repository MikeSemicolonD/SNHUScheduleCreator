# SNHUScheduleCreator
  A much better version of Schedule_Text_To_Excel_Creator. Includes extra settings, ability to add a custom classes, ability to login to mySNHU, and saves your settings and credentials. This take's a SNHU student's schedule data and creates a clean, formatted, and concise Excel sheet.

## FYI :
  This repo is no longer being updated. (because I graduated)
  The original repo can be found here with detailed instructions. (https://github.com/MikeSemicolonD/Schedule_Text_To_Excel_Creator)
  The instructions for running this are the same, except here you simply login to get the data automatically instead of putting it in yourself.
  
## Where? :
  An .exe is accessible at this address: "SNHUScheduleCreator/ScheduleCreator/ScheduleCreator/bin/Debug/".
  
## WARNING!
   This program is dependant on you (And SNHU) having a stable internet connection. Data is collected in a sequential fashion with timed delays in between.
   There could be a situation where a page takes longer than expected to load, leading to data not being collected and being unable to move on. (Program will appear to freeze)
   This could be fixed by only performing collections when the page has 'loaded' which can be hard to determine since the pages themselves load content within frames.

## For future historians :   
   If SNHU has redesigned their student site you'll most likely have to refactor how data is getting grabbed. (Since it is dependant on class/id names remaining the same)
   (Please SNHU create a better view for student schedules)
