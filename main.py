
# Online Python - IDE, Editor, Compiler, Interpreter

import csv 
import os
import json

# Specify directory source files, e.g 'C:/Temp/FileUploads'
filepath = str(input("\nEnter Directory for File A and File B\nor enter X to Exit: "))

# Specify import files, this is hard coded, but could be input also. (FileA, FileB)
file_a = str(input("\nEnter name of FIRST source file including .csv extension\nor enter X to Exit: "))

file_b = str(input("\nnEnter name of SECOND source file including .csv extension\nor enter X to Exit: "))

#file path & location
filelocation_a = os.path.join(filepath,file_a)
filelocation_b = os.path.join(filepath,file_b)
filelocation_c = os.path.join(filepath,'filemerge.csv')

# Terminate if neuther file exists.
if not os.path.isfile(file_a) and not os.path.isfile(file_b)  :
    print ("\n\nFileA or FileB does not found.")
else:
    #Display 
    print("\n\nprocessing, please wait.....\n\n")

    #2 D Array needed for storing target file data during file a, b extraction.
    rows, cols = (11, 5) 
    arr = [[0]*cols]*rows 


    #Array indexer for tracking user ids in file A and file B.
    i = 0 

    # Use 2D arrayfor target file data
    # PS Array length, outer dimensions can be modified to take parameterized value for number of users.
    # fore example:  row = int(input("Input the number of rows in input file: ")).
    # open file in read mode and extract row values.
    with open(filelocation_a, mode ='r')as fileOne:
          
      # reading the CSV file 
      csvFileOne = csv.reader(fileOne) 
      
      # Save to a 5 col array, begining with 2 cols in file A.
      for lines in csvFileOne:
        arr[i][0] = lines[0]
        arr[i][1] = lines[1]

        #Indexer for second file, used to update other 2 cols of array from file B.
        j = 0
        with open(filelocation_b, mode ='r')as fileTwo:
          csvFileTwo = csv.reader(fileTwo) 
          for nestedlines in csvFileTwo:
            if arr[i][0] == nestedlines[0]:
              arr[i][2] = nestedlines[1]
              arr[i][3] = nestedlines[2]
              j += 1

        
        # Displaying the contents of the combine array data ready for CSV file 
        print(i,arr[j][0],arr[j][1],arr[j][2],arr[j][3])


        #write records to csv file. if file exists, exception thrown.
        with open(filelocation_c, 'w') as csvfile:
          filewriter = csv.writer(csvfile, delimiter=',',
                            quotechar='|', quoting=csv.QUOTE_MINIMAL)
          # filewriter.writerow(arr[j][0], arr[j][1],arr[j][2],arr[j][3])
          #userdetails = [arr[j][0], arr[j][1],arr[j][2],arr[j][3]]
          #','.join(userdetails)
          #filewriter.writerow(userdetails)

        #move to next record.
        i += 1

    file_b = str(input("\nnEnter name of Third target file to download including .csv extension\nor enter X to Exit: "))


