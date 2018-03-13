There is no base version for this code.
All files were created during codeathon 2014.


How to Use this tool:

Copy the below files to any of your directory from \\RAD\RAD\LEO
1-Load_Effort_Optimisation.exe
2-ICSharpCode.SharpZipLib.dll

ex-(E:\Radisys\MS_DV\Project\LEO)

Copy all the syslogs, MS statistics, AT output file and top command output under the above folder.



Double click on Load_Effort_Optimisation.exe. A Windows form will appear on your screen with Overall report summarry. Overall report(comment.txt) will be created in your current directory. You can edit comment.txt file if some addition information/load model specific data needs to added before upload.

you can navigate to below tabs and see the required results in details for the load test.

Memory analysis:
	
	Overall memory graph
	
	CP/SCRM Usage graph
	
	SE usage graph
	
	OAMP Usage graph
	
	VXML usage graph


Syslog analysis:
	
	Load modelling analysis
	
	Error/Warning analysis
	
	Slip cycles


Statistics analysis:
	
	RTP packet loss
	
	DSP Utilisation analysis


Report:
	
	Overall report
	
	Submit load results
	

	(when you click Submit_to_LOAD_DB button, This tool will create an archived in gunzip tar format(archive.tar.gz) of current direcorty and upload to LOAD DV using HTTP POST method.)



Note: please delete unnecessary logs and have only the logs which you need to analyse in the folder


