pipeline {  
 agent any  
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
              script {
                     "invoke-command -computername 54.255.72.244 -scriptblock {iisreset /STOP}"
                     }
                 }
             }
      stage('Deploy Artifacts to Web Server') {
        steps {
         bat 'NET USE \\\\54.255.72.244\\C$ /u:app-srv\\${USER_NAME}:${PASS}'
         bat 'xcopy C:\\\\restapi.zip "\\\\54.255.72.244\\c$\\Backup" /F /Y'                
      }
    }
  	stage('UnZip Folder') {
      steps {
		 		  bat 'tar.exe -xvf \\\\54.255.72.244\\c$\\Backup\\restapi.zip -C \\\\54.255.72.244\\c$\\'  
      }
    }
       stage('Start IIS on Remote Host') {
             steps {
                 script {
                     "invoke-command -computername 54.255.72.244 -scriptblock {iisreset /START}"
                     }
                 }
             }
}
}
