PhotoBuddy by Gold Rush (team 4)
***************************************

Date: November 7th 2011

Date Modified: November 8th 2011

Contents
********

0 Deployment information
1 System requirements
2 Known issues
3 Change log


0 Deployment information
***********************

PhotoBuddy.exe is distributed as a standalone executable file with a configuration file. 
1. Copy the file PhotoBuddy.exe and PhotoBuddy.exe.config from the CD to the location of your choice on your computer's hard drive. 
2. Run the exe file to launch PhotoBuddy.

1 System requirements
*********************

PhotoBuddy has the following requirements:

- Operating system
	- Windows XP, SP2 (32 or 64 Bit) or
	- Windows Vista (32 or 64 Bit, SP 1) or
	- Windows 7 (32 or 64 Bit) 

- .NET Framework 4 Client Profile


2 Known issues
*****************************************

Windows Vista -
- Some buttons may require 2 clicks to activate the first time the button is used.

Windows (Vista,XP, and 7)
- Manually editing Photo Buddy xml data or removing Photo Buddy's cached image files can lead to unexpected behavior including program crashes.  

Album View -
- Users may experince delays when loading an album with many photos, or if the photos are very high resolution.
- Application may run out of resources if too many photos are in one album, the exact number of photos is not known.

3 Change Log
*****************

* Nov.7, 2011: 2.0.4328.35835 release
******************************

o Implemented search by display name.

o Added delete photo to the thumb

o Fixed bug that did not allow two photos in the search result to have the same display name.

o Moved rename photo to the right click menu.

o Automatically truncates and numbers photo names that exceed the maximum name length. 

o Fixed locking bug when generating images from files.

o Delete photo now deletes the cached photo if no album references the image.

o Fixed bug in rename album view label display (ampersand not showing)

o User Interface design refresh.

o Improved resource and memory management

o Added application and taskbar icons.

o Implemented crop photo.

o Sped up album view load.

o Implemented album count label in album view.

o Implemented selectable album cover photo.

o Implemented automatic slideshow.

o Fixed album starts at 0 instead of 1.

o Fixed deleted cover photo crash.

o Fixed case-sensitvie search.

o Fixed cropping bug with portrait oriented photos.

o Fixed problem with incorrectly rotated thumbnails.

o Pressing enter key in search box invokes search.

o Fixed label on rename album view.

* Oct.29, 2011: 2.0.4319.22464 release
******************************

o Implemented Rename Photo

o Fixed photo name display bug on the view photo screen.

o Fixed crash when renaming photos in a brand new album.

o Fixed crash when photos missing from storage location.

o Added Album thumbnail to album view.

o Implemented Delete Album.

o Implemented Delete Photo.

o Implemented multi-photo import.

o Implemented Multi-photo import confirmation form.

o Added version number to the about box.

o Added wrap around behavior to the slideshow buttons, when reaching end of the album, the slideshow returns
  to the first item, and visa versa.

o Cached the last visited directory for importing photos.

* Oct.22.11: 2.0.4312.30697 release
***************************

o Added thumbnail user control.

o Added settings and app.config.

o Fixed bug reading folder reference from settings.

o Fixed Image Hash Alogrithm Implementation.  

o Corrected bug in the way photo Paths were combined.

o Set the maximum name length for photos and albums at 32 characters.

o Implemented features to allow end user to enter special characters "&" and "<" in photo and album name.

o Implemented features to allow only single instance of PhotoBuddy.exe program running.

o Implemented features to restrict unique photos based on file bits in the same album.

o Implemented single instance per user.

o Fixed blank photo name bug.

o Fixed crash on display photo.

o Fixed all-whitespace album name bug.


* Oct.9, 2011: 1.0.0.5 release
******************************

o fixed capitalization errors in dialog messages.

o fixed error messages when attempting to add valid image files.


* Oct.9, 2011: 1.0.0.4 release
******************************

o fixed incorrect windows titles.

o fixed textboxes not receiving initial focus.

o fixed return key not functioning in textboxes.

o fixed PhotoBuddy label click does nothing.

o fixed return key not functioning in textboxes.


* Oct.7, 2011: 1.0.0.3 release
******************************

o fixed identifiably display the photo to add.

o fixed name selected photo when adding.

o fixed cancel and back button.

o Added view photo feature.

o Added rename album feature.

o Added ability to cycle through all photos in an album.

o fixed cancel and back button.


* Oct.6, 2011: 1.0.0.2 release
******************************

o fixed photo album error message

o Changed error message text

o fixed crash on opening view.


* Oct.2.11: 1.0.0.1 release
***************************

o First linkage between UI and Business Rule

o fixed incorrect post view.

o fixed XML file no updated.

o fixed  Require Unique Album name  - no error displayed

o Added rule to disallow empty album names.

o Added rule to navigate to photo


* Oct.2.11: 1.0.0.0 release
***************************

o First album linkage with UI

o No functionality was implemented



