# FuelReports

## Overview
FuelReports is a software module that reads petrol stations data from different files, saves it in a database and provides more complex reporting functionality.

## Specification
FuelReports is a command-line interface that deserializes XML files, stores data in a MS SQL database and provides reporting commands. It must provide daily, monthly and yearly  average price reports for a given city, petrol station and/or fuel type. The XML files are shared through SFTP.

## Technology Stack
C#  
MS SQL  
Commander.NET  

## Milestones
1. Parse data from sample XML  
2. SFTP downloader  
3. Store data from XML to database  
4. Command-line interface  
5. Reporting engine  

## Commands definition
### Configuration
``` fuel-reports config --data-dir <path-to-data-dir> ```  
Path: a directory where the data files are stored locally  
Must be an existing directory accessible for reading.
	
### Process
Deserializing all new data:
``` fuel-reports process ```

### Report
Executing a report. Reports should include the average prices for the given flags. The result of each report should be printed to the console.  
``` fuel-reports report --period <yyyy-[MM]-[dd]> [--fuel-type <fuel-type>] [--petrol-station <petrol-station>] [--city <city-name> ] ```  
Day, month, quarter, year:  
```--day <yyyy-MM-dd>, e.g. --day 2021-02-22 ```  
``` --month <yyyy-MM>, e.g. --month 2020-07 ```  
``` --year <yyyy>, e.g. --year 2019 ```  
  
The part in square brackets is optional.  
Sample usage:  
I would like to see the average prices for each fuel for the city ABC for the previous year  
``` fuel-reports report --period 2020 --city ABC ```  
I would like to see the average prices for each fuel for the petrol station chain ABC for December 2020  
``` fuel-reports report --period 2020-12 --petrol-station ABC ```  
I would like to see the average price for Premium Petrol fuel for 31st December 2020  
``` fuel-reports report --period 2020-12-31 --fuel-type Premium Petrol ```  
I would like to see the average prices for each fuel for the petrol station chain ABC in the city DEF for the previous year  
``` fuel-reports report --period 2020 --petrol-station ABC --city DEF ```  
