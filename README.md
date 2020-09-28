Assessment Files.

Piano FileMerge is a middleware program that extracts, transforms and loads CSV files to target software program. 
Piano FileMerge allows a Piano client to generate cosdolidated data from two seperate CSV files. 
The merged file contains details validated using APIs during import to the system.

This program allows Piano to assess my general code management skills for working as a Technical Consultant in Piano.

Readme has two parts: Part A for running C# files and Part B for runnin Python main.py

Part A : Running C# Code.

1 Prerequisites

    1.1 Install Visual Studio 2017 or later and create new project solution, PianoFileMerge2

    1.2 Create Folder BLL

    1.3 Create Colder CSS

    1.4 Add file APIs.cs to folder BLL

    1.5 Add files StatMerge to project root.

    1.6 Set StartMerge as start page.

2 Running application

  2.1 Run solution on local IIS, then On Uer Interface:

  2.2 Button 1 & 2 :

      Select source csv FileA and then FileB.

  2.3 Button 3 :

      Upload both files using single button and display data on two Grids Views on the UI.

  2.4 Button 4 : 

      Cross-check & Validate UIDs using Piano.io APIs and then Merge Files.  Highlight existing UIDs in green on a third Grid View.

  2.5 Button 5 :

      Download Merged CSV file, labelled File C.


Part B : Python Code Piano.io

For Executing Python File main.py

1. Create folder c:\temp\fileuploads

2. Copy three files to folder - two csv source files and code file..
   
      2.1 main.py
    
      2.2 FileA.csv  
    
      2.3 FileB.csv

3. Run cmd.exe

4. in Cmd.EXE change directory to C:\temp\fileuploads

5. Run main.py

6. Follow the three prompts by entering
      prompt 1 enter: C:\temp\fileuploads
      Prpmpt 2 enter: FileA.csv
      Prompt 3 enter: FileC.csv

7. Processing of file A and B. The merged file C is displayed.
