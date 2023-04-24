# CodeCaffeineCollapse
 IMGP Group 10


**Branch Control Information**

'Development' is the Default Branch, **which should not be directly committed to **

When creating a new feature, make a new branch from Development named **feat/FeatureName** so it is clear. If possible, create a new unity scene, as Github may have problem converting the changes made to the same Unity Scene. A new unity scene, which can be a copy of the 'main' one, or more likely an empty scene with just the neccesary aspects to test the feature. 

When this feature is completed, you can request a pull request. This will compare your feature branch and the default branch to see if there are any conflicts (i.e one file has a different value than the other). If this is the case then this can be resolved. This pull request can be approved, and your feature branch can be merged into the Development branch. Try to delete each feature branch when you are finished with it. 

For Artists, or anyone adding assets, it is recommended to go into Development, make a new branch named **asset/AssetName(S)** and add any assets to the appropriate places in the unity file. Then a pull request and a merge of these branches can go ahead. This is done so that assets can be assessed by anyone through the development branch. 

If you are in the middle of implementing a feature and want to access newly submitted assets, commit any changes and request a pull request and merge if there are no issues. This will allow you to access development and pull from origin to access the recently added assets. Then, to continue working on the feature, simply make a new branch with the correct name. 

At the end of each week, a Branch will be made off Development called **build/WeekNo**. From this branch the week's prototype build can be built and added to the the files. If anyone wants to access that build, they can access this branch and clone the executable. This branch will not be merged, instead being treated as a save point for development's progress. 

For example, I will try adding an Event system, so I will create a new branch titled feat/EventSystem, and add any files / edit any scripts here. I will also be creating a new unity scene, named "EventSystem".
