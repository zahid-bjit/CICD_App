pipeline {  
 agent any  
 stages {
     stage('Stop IIS on Remote Host') {
       steps {
              script {
                     "invoke-command -computername 54.255.72.244 -scriptblock {iisreset /STOP}"
                     }
                 }
             }
      stage('Deploy Artifacts to Web Server') {
        steps {
         bat 'NET USE \\\\54.255.72.244\\C$ /u:app-srv\\Administrator Acce$$denied4all'
         bat 'xcopy C:\\\\restapi.zip "\\\\54.255.72.244\\c$\\Backup" /F /Y'                
      }
    }
  }
}
