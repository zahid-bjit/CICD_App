pipeline {  
 agent any 
 	   environment {
                APP_SRV_CREDS = credentials('1919f59d-d637-4108-b80c-0066b932aeed')
            } 
 stages {
    stage('Restore') {
      steps {
        bat "\"${tool 'MSBuild'}\" RESTAPITest.sln -t:restore -p:RestorePackagesConfig=true"
      }
    }
    stage('Clean') {
      steps {
        bat "\"${tool 'MSBuild'}\" RESTAPITest.sln -t:clean -p:RestorePackagesConfig=true"
      }
    }
    stage('Build') {
      steps {
        script {
          bat "\"${tool 'MSBuild'}\" RESTAPITest.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=C:\\inetpub\\wwwroot\\restapi"
        } 
      } 
    }
  	 stage('Create Zip Folder') {
       steps {
		 		 bat '''cd C:\\
		 		 tar.exe acvf restapi.zip inetpub\\wwwroot\\restapi'''
       }
     }
        stage('Stop IIS on Remote Host') {
            steps {
                bat 'powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -Command "Invoke-Command -ComputerName 54.255.72.244 -ScriptBlock { iisreset /stop }"'
            }
        }
      stage('Deploy Artifacts to Web Server') {
        steps {
         withCredentials([usernamePassword(credentialsId: 'myCredentials', passwordVariable: 'PASSWORD', usernameVariable: 'USERNAME')]) {
         bat 'xcopy /s "C:\\\\restapi.zip" "\\54.255.72.244\\c$\\Backup" /U /Y /I /Q'
        }	
      }
  	stage('UnZip Folder') {
      steps {
		 		  bat 'tar.exe -xvf \\\\54.255.72.244\\c$\\Backup\\restapi.zip -C \\\\54.255.72.244\\c$\\'  
      }
    }
	    stage('Start IIS on Remote Host') {
            steps {
                bat 'powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -Command "Invoke-Command -ComputerName 54.255.72.244 -ScriptBlock { iisreset /start }"'
            }
    }
  }
}
}
