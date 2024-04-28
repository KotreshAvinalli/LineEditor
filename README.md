# LineEditor
Commandline line editor for txt file 
Usage:

1)Build the project using .net core 8
2)Open the command prompt and traverse to LineEditor.exe under bin/net8.0 folder
3)Run the exe
4)Load the txt file to which you have the write permission(with elevated permission)
5)Do the inser and deletion operation as suggested in exe at any line number
6)Save the file and quit

Below are usage example:

C:\LIneE\LineEditor\LineEditor\bin\Debug\net8.0>LineEditor.exe
Enter the full file path to load, for example : C:\temp\myfile.txt
 a>> C:\temp\myfile.txt
File loaded successfully

Please enter following command(s) for corresponding operation(s)
list  : To display all lines
ins n : To insert at line numbur n
del n : To delete line number n
save  : To save the file:
quit  : To quit from the app

 a>> list
1: My name is kotresh
2: My father name is Mallikarjuna
3: line number 3 inserted
4: My Mother name is Manjula
5: My wife name is sunitha
6: line number 6 added
7: line saven added

 a>> del 7
Deleted successfully

 a>> list
1: My name is kotresh
2: My father name is Mallikarjuna
3: line number 3 inserted
4: My Mother name is Manjula
5: My wife name is sunitha
6: line number 6 added

 a>> ins 7
Enter text to insert
line 7 added
Inserted successfully

 a>> list
1: My name is kotresh
2: My father name is Mallikarjuna
3: line number 3 inserted
4: My Mother name is Manjula
5: My wife name is sunitha
6: line number 6 added
7: line 7 added

 a>> save
File saved successfully.

 a>> quit
