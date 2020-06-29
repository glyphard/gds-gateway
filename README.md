# gds-gateway
Data Gateway for Google Data Studio Connector


2 items to deploy:
	1. Gateway - (Azure Function or App Service)
		Fill in dbkey_{keyname} in ApplicationHosts file with a sql connection string that has a user that has access to the information schema/tables in sql server.
	2. Google Apps Script Project 
	

	Usage:
		1. Fille in {keyname} in Google Data Studio that matches the connection you want to use
		2. Choose the table or view that you want to query from the list
		3. Develop your Google Data Studio report as normal.
		