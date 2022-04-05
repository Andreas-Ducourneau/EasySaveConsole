# EasySaveConsole

- Remember to put a \ at the end of the path whenever you request to fill in a Target folder but only for the target path

-  In order to simplify, we recommend this folder architecture

- Differential backup path, enter the path for the last full backup. ( You have to use the links of the folder where the complete backups is, the program will identify the most recent by himself).

## Application
When the application is run, an interface will open and offer you:

1. Create a backup job
2. Run Backup :
- Run a differential backup
- Run Full backup

After the run backup it will create a backup of the target folder and create logs and the status file will also be created. To see the logs file, go to the
folder:EasySaveConsole\EasySaveConsole\bin\Debug\netcoreapp3.1

You will find a file named log.

The application code is commented for a better understanding of it.

The full backups include every subdirectory but, its not implemented yet on the differential backup, we advise not use to subdirectory for now.

### Authors
- Andreas DUCOURNEAU
- Thomas DUPIN
- Thibaud RAPIN
- Thomas RAPIN