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
  }
}
